using DevExpress.DataAccess.UI.Sql;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Wizards;
// ...

namespace ReplaceQueryBuilderSample {
    public partial class FrmDesigner : XtraForm {
        public FrmDesigner() {
            InitializeComponent();
            MyWizardCustomizationService customizationService = new MyWizardCustomizationService();
            reportDesignerMDIController.AddService(typeof(IWizardCustomizationService), customizationService);
            reportDesignerMDIController.AddService(typeof(ISqlEditorsCustomizationService), customizationService);
            reportDesignerMDIController.CreateNewReportWizard();
        }
    }
}
