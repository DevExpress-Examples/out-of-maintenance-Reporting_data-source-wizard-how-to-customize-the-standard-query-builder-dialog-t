Imports DevExpress.DataAccess.Native.Sql
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
        Inherits ConfigureQueryPage(Of TModel)


        Private dbTables_Renamed As Dictionary(Of String, DBTable)
        Private ReadOnly dbSchemaProvider As IDBSchemaProvider
        Private ReadOnly parameterService As IParameterService
        Private schema As Object

        Public Sub New(ByVal view As IConfigureQueryPageView, ByVal context As IWizardRunnerContext, _
        ByVal options As SqlWizardOptions, ByVal dbSchemaProvider As IDBSchemaProvider, _
        ByVal parameterService As IParameterService, ByVal customQueryValidator As ICustomQueryValidator)
            MyBase.New(view, context, options, dbSchemaProvider, parameterService, customQueryValidator)
            Me.dbSchemaProvider = dbSchemaProvider
            Me.parameterService = parameterService
        End Sub

        Private ReadOnly Property DbTables() As Dictionary(Of String, DBTable)
            Get
                If dbTables_Renamed Is Nothing Then
                    LoadDbSchema()
                    dbTables_Renamed = DBSchema.Tables.Union(DBSchema.Views).ToDictionary(Function(t) t.Name)
                End If
                Return dbTables_Renamed
            End Get
        End Property

#Region "Overrides of ConfigureQueryPage<TModel>"

        Protected Overrides Sub RunQueryBuilder()
            ' Display a custom query builder form and generate a TableQuery based on user input.
            Using form = New FrmQueryBuilder()
                form.Initialize(DbTables.Keys.OrderBy(Function(s) s))
                If form.ShowDialog(CType(View, MyConfigureQueryPageView).Window) <> DialogResult.OK Then
                    Return
                End If
                Dim tableName As String = form.Selected
                If String.IsNullOrEmpty(tableName) Then
                    Return
                End If
                Dim dbTable As DBTable = DbTables(tableName)
                If dbTable.Columns.Count = 0 Then
                    dbSchemaProvider.LoadColumns(Model.DataConnection, dbTable)
                End If

                Dim query As SelectQuery = _
                    SelectQueryFluentBuilder.AddTable(tableName).SelectColumns(dbTable.Columns.[Select](Function(c) c.Name).ToArray()).Build(tableName)
                SetTableOrCustomSqlQuery(query)

                schema = SqlQueryHelper.GetDataSchema(query, DBSchema, Model.DataConnection, New ConnectionOptions(), _
                If(parameterService Is Nothing, Nothing, parameterService.Parameters))

                RaiseChanged()
            End Using
        End Sub

#Region "Overrides of ConfigureQueryPage<TModel>"

        Public Overrides Sub Begin()
            MyBase.Begin()
            schema = Model.DataSchema
        End Sub

#End Region

        Public Overrides Sub Commit()
            MyBase.Commit()
            Model.DataSchema = schema
        End Sub

#End Region
    End Class
End Namespace
