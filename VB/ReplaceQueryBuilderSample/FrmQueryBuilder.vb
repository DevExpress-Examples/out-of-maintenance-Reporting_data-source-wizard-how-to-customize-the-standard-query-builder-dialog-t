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

        Public Property Selected() As String
            Get
                Return CStr(lstTables.SelectedItem)
            End Get
            Set(ByVal value As String)
                lstTables.SelectedItem = value
            End Set
        End Property

        Public Property TopRecords() As Integer
            Get
                Dim value As Integer = Nothing
                Return If(Integer.TryParse(txtTop.Text, value), value, 0)
            End Get
            Set(ByVal value As Integer)
                txtTop.Text = value.ToString()
            End Set
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
