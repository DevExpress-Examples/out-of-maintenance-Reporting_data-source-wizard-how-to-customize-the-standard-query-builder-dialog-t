Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.DataAccess.UI.Wizard.Views
Imports DevExpress.DataAccess.Wizard
Imports DevExpress.DataAccess.Wizard.Services
' ...

Namespace ReplaceQueryBuilderSample
    Friend Class MyConfigureQueryPageView
        Inherits ConfigureQueryPageView

        Public Sub New(ByVal displayNameProvider As IDisplayNameProvider, ByVal propertyGridServices As IServiceProvider, ByVal customQueryValidator As ICustomQueryValidator, ByVal options As SqlWizardOptions)
            MyBase.New(displayNameProvider, propertyGridServices, customQueryValidator, options)
        End Sub

        Public ReadOnly Property Window() As IWin32Window
            Get
                Return FindForm()
            End Get
        End Property
    End Class
End Namespace
