Imports DevExpress.DataAccess.UI.Sql
Imports DevExpress.DataAccess.UI.Wizard
Imports DevExpress.DataAccess.Wizard.Model
Imports DevExpress.DataAccess.Wizard.Presenters
Imports DevExpress.DataAccess.Wizard.Views
Imports DevExpress.XtraReports.Wizards3
Imports System.ComponentModel.Design
' ...

Namespace ReplaceQueryBuilderSample
    Friend Class MyWizardCustomizationService
        Implements IWizardCustomizationService, ISqlEditorsCustomizationService

        #Region "Implementation of IWizardCustomizationService"
        Public Sub CustomizeReportWizard(ByVal tool As IWizardCustomization(Of XtraReportModel)) _
        Implements IWizardCustomizationService.CustomizeReportWizard
            ' Register a custom query customization page in the Report Wizard.
            tool.RegisterPage(Of ConfigureQueryPage(Of XtraReportModel), MyConfigureQueryPageEx(Of XtraReportModel))()
            tool.RegisterPageView(Of IConfigureQueryPageView, MyConfigureQueryPageView)()
        End Sub

        ' Register a custom query customization page in the Data SOurce Wizard. 
        Public Sub CustomizeDataSourceWizard(ByVal tool As IWizardCustomization(Of XtraReportModel)) _
        Implements IWizardCustomizationService.CustomizeDataSourceWizard
            RegisterPages(tool)
        End Sub

        Public Function TryCreateDataSource(ByVal model As IDataSourceModel, <System.Runtime.InteropServices.Out()> _
        ByRef dataSource As Object, <System.Runtime.InteropServices.Out()> ByRef dataMember As String) _
        As Boolean Implements IWizardCustomizationService.TryCreateDataSource
            dataSource = Nothing
            dataMember = Nothing
            Return False
        End Function
        Public Function TryCreateReport(ByVal designerHost As IDesignerHost, ByVal model As XtraReportModel, _
        ByVal dataSource As Object, ByVal dataMember As String) As Boolean Implements IWizardCustomizationService.TryCreateReport
            Return False
        End Function
        #End Region


        #Region "Implementation of ISqlEditorsCustomizationService"
        ' Replace the Query Editor dialog with a cusom one.
        Public Sub CustomizeEditor(ByVal editor As SqlEditorId, ByVal tool As IWizardCustomization(Of SqlDataSourceModel)) _
        Implements ISqlEditorsCustomizationService.CustomizeEditor
            If editor = SqlEditorId.Query Then
                RegisterPages(tool)
            End If
        End Sub

#End Region

        Private Shared Sub RegisterPages(Of TModel As ISqlDataSourceModel)(ByVal tool As IWizardCustomization(Of TModel))
            tool.RegisterPage(Of ConfigureQueryPage(Of TModel), MyConfigureQueryPage(Of TModel))()
            tool.RegisterPageView(Of IConfigureQueryPageView, MyConfigureQueryPageView)()
        End Sub
    End Class
End Namespace
