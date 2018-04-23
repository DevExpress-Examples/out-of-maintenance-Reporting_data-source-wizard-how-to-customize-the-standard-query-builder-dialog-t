Imports DevExpress.DataAccess.UI.Sql
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.Wizards3
' ...

Namespace ReplaceQueryBuilderSample
    Partial Public Class FrmDesigner
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
            Dim customizationService As New MyWizardCustomizationService()
            reportDesignerMDIController.AddService(GetType(IWizardCustomizationService), customizationService)
            reportDesignerMDIController.AddService(GetType(ISqlEditorsCustomizationService), customizationService)
            reportDesignerMDIController.CreateNewReportWizard()
        End Sub
    End Class
End Namespace
