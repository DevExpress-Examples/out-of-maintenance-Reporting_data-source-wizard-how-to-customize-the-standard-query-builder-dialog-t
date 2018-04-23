using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Wizard;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.DataAccess.Wizard.Views;
using DevExpress.XtraReports.Wizards;
using DevExpress.XtraReports.Wizards.Presenters;
using System;
// ...

namespace ReplaceQueryBuilderSample {
    class MyConfigureQueryPageEx<TModel> : MyConfigureQueryPage<TModel> where TModel : XtraReportModel {
        public MyConfigureQueryPageEx(IMultiQueryConfigurePageView view, IWizardRunnerContext context, IDBSchemaProviderEx dbSchemaProvider, IParameterService parameterService) : base(view, context, dbSchemaProvider, parameterService) { }

        #region Overrides of ConfigureQueryPage<TModel>

        #region Overrides of ConfigureQueryPage<TModel>

        public override bool MoveNextEnabled { get { return base.MoveNextEnabled || base.FinishEnabled; } }

        public override bool FinishEnabled { get { return false; } }

        #endregion

        public override Type GetNextPageType() { return base.MoveNextEnabled ? base.GetNextPageType() : typeof(MultiQuerySelectFieldsPage<TModel>); }

        #endregion
    }
}
