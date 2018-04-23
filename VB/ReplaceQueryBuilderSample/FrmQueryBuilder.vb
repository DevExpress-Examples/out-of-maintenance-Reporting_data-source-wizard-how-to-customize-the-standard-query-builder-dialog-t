Imports DevExpress.XtraEditors
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
' ...

Namespace ReplaceQueryBuilderSample
    Partial Public Class FrmQueryBuilder
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Public ReadOnly Property Selected() As String
            Get
                Return CStr(lstTables.SelectedItem)
            End Get
        End Property

        Public Sub Initialize(ByVal tables As IEnumerable(Of String))
            lstTables.Items.Clear()
            lstTables.Items.AddRange(tables.ToArray())
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            DialogResult = DialogResult.Cancel
        End Sub

        Private Sub btnOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
            DialogResult = DialogResult.OK
        End Sub
    End Class
End Namespace
