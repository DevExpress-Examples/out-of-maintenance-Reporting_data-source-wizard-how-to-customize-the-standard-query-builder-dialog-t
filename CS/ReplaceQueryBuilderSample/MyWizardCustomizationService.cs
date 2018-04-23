using DevExpress.DataAccess.UI.Sql;
using DevExpress.DataAccess.UI.Wizard;
using DevExpress.DataAccess.Wizard.Model;
using DevExpress.DataAccess.Wizard.Presenters;
using DevExpress.DataAccess.Wizard.Views;
using DevExpress.XtraReports.Wizards3;
using System.ComponentModel.Design;
// ...

namespace ReplaceQueryBuilderSample {
    class MyWizardCustomizationService : IWizardCustomizationService, ISqlEditorsCustomizationService {
        #region Implementation of IWizardCustomizationService
        public void CustomizeReportWizard(IWizardCustomization<XtraReportModel> tool) {
            // Register a custom query customization page in the Report Wizard.
            tool.RegisterPage<ConfigureQueryPage<XtraReportModel>, MyConfigureQueryPageEx<XtraReportModel>>();
            tool.RegisterPageView<IConfigureQueryPageView, MyConfigureQueryPageView>();
        }

        // Register a custom query customization page in the Data SOurce Wizard. 
        public void CustomizeDataSourceWizard(IWizardCustomization<XtraReportModel> tool) { RegisterPages(tool); }
        
        public bool TryCreateDataSource(IDataSourceModel model, out object dataSource, out string dataMember) {
            dataSource = null;
            dataMember = null;
            return false;
        }
        public bool TryCreateReport(IDesignerHost designerHost, XtraReportModel model, object dataSource, string dataMember) { return false; }
        #endregion

        #region Implementation of ISqlEditorsCustomizationService
        // Replace the Query Editor dialog with a cusom one.
        public void CustomizeEditor(SqlEditorId editor, IWizardCustomization<SqlDataSourceModel> tool) {
            if(editor == SqlEditorId.Query)
                RegisterPages(tool);
        }
        #endregion

        static void RegisterPages<TModel>(IWizardCustomization<TModel> tool) where TModel : ISqlDataSourceModel {
            tool.RegisterPage<ConfigureQueryPage<TModel>, MyConfigureQueryPage<TModel>>();
            tool.RegisterPageView<IConfigureQueryPageView, MyConfigureQueryPageView>();
        }
    }
}
