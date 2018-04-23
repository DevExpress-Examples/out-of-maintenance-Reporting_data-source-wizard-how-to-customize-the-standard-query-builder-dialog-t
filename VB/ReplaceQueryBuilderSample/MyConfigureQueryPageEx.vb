Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.Wizard
Imports DevExpress.DataAccess.Wizard.Services
Imports DevExpress.DataAccess.Wizard.Views
Imports DevExpress.XtraReports.Wizards
Imports DevExpress.XtraReports.Wizards.Presenters
Imports System
' ...

Namespace ReplaceQueryBuilderSample
    Friend Class MyConfigureQueryPageEx(Of TModel As XtraReportModel)
        Inherits MyConfigureQueryPage(Of TModel)

        Public Sub New(ByVal view As IMultiQueryConfigurePageView, ByVal context As IWizardRunnerContext, ByVal dbSchemaProvider As IDBSchemaProviderEx, ByVal parameterService As IParameterService)
            MyBase.New(view, context, dbSchemaProvider, parameterService)
        End Sub

        #Region "Overrides of ConfigureQueryPage<TModel>"

        #Region "Overrides of ConfigureQueryPage<TModel>"

        Public Overrides ReadOnly Property MoveNextEnabled() As Boolean
            Get
                Return MyBase.MoveNextEnabled OrElse MyBase.FinishEnabled
            End Get
        End Property

        Public Overrides ReadOnly Property FinishEnabled() As Boolean
            Get
                Return False
            End Get
        End Property

        #End Region

        Public Overrides Function GetNextPageType() As Type
            Return If(MyBase.MoveNextEnabled, MyBase.GetNextPageType(), GetType(MultiQuerySelectFieldsPage(Of TModel)))
        End Function

        #End Region
    End Class
End Namespace
