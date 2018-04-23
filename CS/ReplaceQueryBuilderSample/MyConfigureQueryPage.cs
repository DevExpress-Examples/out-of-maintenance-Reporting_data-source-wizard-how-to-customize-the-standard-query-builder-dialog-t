using DevExpress.DataAccess.Native.Sql;
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
    class MyConfigureQueryPage<TModel> : ConfigureQueryPage<TModel> where TModel : ISqlDataSourceModel {
        Dictionary<string, DBTable> dbTables;
        readonly IDBSchemaProvider dbSchemaProvider;
        object dataSchema;

        public MyConfigureQueryPage(IConfigureQueryPageView view, IWizardRunnerContext context, SqlWizardOptions options, IDBSchemaProvider dbSchemaProvider, IParameterService parameterService, ICustomQueryValidator customQueryValidator) : base(view, context, options, dbSchemaProvider, parameterService, customQueryValidator) {
            this.dbSchemaProvider = dbSchemaProvider;
        }

        Dictionary<string, DBTable> DbTables {
            get {
                if(dbTables == null) {
                    LoadDbSchema();
                    dbTables = DBSchema.Tables.Union(DBSchema.Views).ToDictionary(t => t.Name);
                }
                return dbTables;
            }
        }

        #region Overrides of ConfigureQueryPage<TModel>

        protected override void RunQueryBuilder() {
            // Display a custom query builder form and generate a TableQuery based on user input.
            using (var form = new FrmQueryBuilder()) {
                form.Initialize(DbTables.Keys.OrderBy(s => s));
                if(form.ShowDialog(((MyConfigureQueryPageView)View).Window) != DialogResult.OK)
                    return;
                string tableName = form.Selected;
                if(string.IsNullOrEmpty(tableName))
                    return;
                DBTable dbTable = DbTables[tableName];
                if(dbTable.Columns.Count == 0)
                    dbSchemaProvider.LoadColumns(Model.DataConnection, dbTable);
                TableQuery query = new TableQuery(tableName);
                query.AddTable(tableName).SelectColumns(dbTable.Columns.Select(c => c.Name).ToArray());
                SetTableOrCustomSqlQuery(query);
                dataSchema = query.GetDataSchema(DBSchema);
                RaiseChanged();
            }
        }

        #region Overrides of ConfigureQueryPage<TModel>

        public override void Begin() {
            base.Begin();
            dataSchema = Model.DataSchema;
        }

        #endregion

        public override void Commit() {
            base.Commit();
            Model.DataSchema = dataSchema;
        }

        #endregion
    }
}
