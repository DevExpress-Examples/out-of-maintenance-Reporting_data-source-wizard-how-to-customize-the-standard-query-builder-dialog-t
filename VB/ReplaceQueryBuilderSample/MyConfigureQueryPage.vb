Imports DevExpress.DataAccess.Sql
Imports DevExpress.DataAccess.Wizard
Imports DevExpress.DataAccess.Wizard.Model
Imports DevExpress.DataAccess.Wizard.Presenters
Imports DevExpress.DataAccess.Wizard.Services
Imports DevExpress.DataAccess.Wizard.Views
Imports DevExpress.Xpo.DB
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
' ...

Namespace ReplaceQueryBuilderSample
    Friend Class MyConfigureQueryPage(Of TModel As ISqlDataSourceModel)
        Inherits MultiQueryConfigurePage(Of TModel)


        Private dbTables_Renamed As Dictionary(Of String, DBTable)
        Private ReadOnly dbSchemaProvider As IDBSchemaProviderEx

        Public Sub New(ByVal view As IMultiQueryConfigurePageView, ByVal context As IWizardRunnerContext, ByVal dbSchemaProvider As IDBSchemaProviderEx, ByVal parameterService As IParameterService)
            MyBase.New(context, view, dbSchemaProvider, parameterService)

            Me.dbSchemaProvider = dbSchemaProvider
        End Sub

        Private ReadOnly Property DbTables() As Dictionary(Of String, DBTable)
            Get
                If dbTables_Renamed Is Nothing Then
                    LoadTablesAndViews(False)
                    dbTables_Renamed = DBSchema.Tables.Union(DBSchema.Views).ToDictionary(Function(t) t.Name)
                End If
                Return dbTables_Renamed
            End Get
        End Property

        #Region "Overrides of ConfigureQueryPage<TModel>"

        Protected Overrides Function RunQueryBuilder(ByVal query As SqlQuery) As SqlQuery
            Dim tableName As String = Nothing
            Dim topRecords As Integer = 100

            ' Analyze the query that is being edited (if any).
            Dim selectQuery As SelectQuery = TryCast(query, SelectQuery)
            If selectQuery IsNot Nothing AndAlso selectQuery.Tables.Count = 1 AndAlso selectQuery.Columns.Count = 1 _
                AndAlso String.IsNullOrEmpty(selectQuery.FilterString) AndAlso selectQuery.Skip = 0 Then
                Dim columns = TryCast(selectQuery.Columns(0), AllColumns)
                If columns IsNot Nothing AndAlso columns.Table Is Nothing Then
                    tableName = selectQuery.Tables(0).Name
                    topRecords = selectQuery.Top
                End If
            End If
            If tableName Is Nothing AndAlso query IsNot Nothing Then
                ShowMessage("This query is too complex and cannot be displayed.")
                Return Nothing
            End If

            ' Display a custom query builder form.
            Using form = New FrmQueryBuilder()
                form.Initialize(DbTables.Keys.OrderBy(Function(s) s))
                If tableName IsNot Nothing Then
                    form.Selected = tableName
                    form.TopRecords = topRecords
                End If
                If form.ShowDialog(CType(View, Control).FindForm()) <> DialogResult.OK Then
                    Return Nothing
                End If
                tableName = form.Selected
                topRecords = form.TopRecords
            End Using

            ' Generate a query based on user input.
            If String.IsNullOrEmpty(tableName) Then
                Return Nothing
            End If
            Dim result As SqlQuery = SelectQueryFluentBuilder.
                AddTable(tableName).
                SelectAllColumns().
                Top(topRecords).
                Build(tableName)

            ' Make sure that the table columns have been loaded.
            dbSchemaProvider.LoadColumns(Model.DataConnection, DBSchema.Tables.First(Function(t) t.Name = tableName))

            Return result
        End Function

        #End Region
    End Class
End Namespace

