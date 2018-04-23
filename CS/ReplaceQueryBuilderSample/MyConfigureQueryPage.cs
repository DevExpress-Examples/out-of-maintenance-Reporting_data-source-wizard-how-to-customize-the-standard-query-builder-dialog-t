using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Wizard;
using DevExpress.DataAccess.Wizard.Model;
using DevExpress.DataAccess.Wizard.Presenters;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.DataAccess.Wizard.Views;
using DevExpress.Xpo.DB;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
// ...

namespace ReplaceQueryBuilderSample {
    class MyConfigureQueryPage<TModel> : MultiQueryConfigurePage<TModel> where TModel : class, ISqlDataSourceModel {
        Dictionary<string, DBTable> dbTables;
        readonly IDBSchemaProviderEx dbSchemaProvider;

        public MyConfigureQueryPage(IMultiQueryConfigurePageView view, IWizardRunnerContext context, IDBSchemaProviderEx dbSchemaProvider, IParameterService parameterService)
            : base(context, view, dbSchemaProvider, parameterService) {

            this.dbSchemaProvider = dbSchemaProvider;
        }

        Dictionary<string, DBTable> DbTables {
            get {
                if (dbTables == null) {
                    LoadTablesAndViews(false);
                    dbTables = DBSchema.Tables.Union(DBSchema.Views).ToDictionary(t => t.Name);
                }
                return dbTables;
            }
        }

        #region Overrides of ConfigureQueryPage<TModel>

        protected override SqlQuery RunQueryBuilder(SqlQuery query) {
            string tableName = null;
            int topRecords = 100;

            // Analyze the query that is being edited (if any).
            SelectQuery selectQuery = query as SelectQuery;
            if (selectQuery != null && selectQuery.Tables.Count == 1 && selectQuery.Columns.Count == 1 && string.IsNullOrEmpty(selectQuery.FilterString) && selectQuery.Skip == 0) {
                var columns = selectQuery.Columns[0] as AllColumns;
                if (columns != null && columns.Table == null) {
                    tableName = selectQuery.Tables[0].Name;
                    topRecords = selectQuery.Top;
                }
            }
            if (tableName == null && query != null) {
                ShowMessage("This query is too complex and cannot be displayed.");
                return null;
            }

            // Display a custom query builder form.
            using (var form = new FrmQueryBuilder()) {
                form.Initialize(DbTables.Keys.OrderBy(s => s));
                if (tableName != null) {
                    form.Selected = tableName;
                    form.TopRecords = topRecords;
                }
                if (form.ShowDialog(((Control)View).FindForm()) != DialogResult.OK)
                    return null;
                tableName = form.Selected;
                topRecords = form.TopRecords;
            }

            // Generate a query based on user input.
            if (string.IsNullOrEmpty(tableName))
                return null;
            SqlQuery result = SelectQueryFluentBuilder
                .AddTable(tableName)
                .SelectAllColumns()
                .Top(topRecords)
                .Build(tableName);

            // Make sure that the table columns have been loaded.
            dbSchemaProvider.LoadColumns(Model.DataConnection, DBSchema.Tables.First(t => t.Name == tableName));

            return result;
        }

        #endregion
    }
}

