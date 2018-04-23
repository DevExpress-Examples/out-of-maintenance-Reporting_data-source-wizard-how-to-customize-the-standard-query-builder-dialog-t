using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Wizard;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.DataAccess.Wizard.Views;
using DevExpress.XtraReports.Wizards3;
using DevExpress.XtraReports.Wizards3.Presenters;
using System;
// ...

namespace ReplaceQueryBuilderSample {
    class MyConfigureQueryPageEx<TModel> : MyConfigureQueryPage<TModel> where TModel : XtraReportModel {
        public MyConfigureQueryPageEx(IConfigureQueryPageView view, IWizardRunnerContext context, SqlWizardOptions options, IDBSchemaProvider dbSchemaProvider, IParameterService parameterService, ICustomQueryValidator customQueryValidator) : base(view, context, options, dbSchemaProvider, parameterService, customQueryValidator) { }

        #region Overrides of ConfigureQueryPage<TModel>

        #region Overrides of ConfigureQueryPage<TModel>

        public override bool MoveNextEnabled { get { return base.MoveNextEnabled || base.FinishEnabled; } }

        public override bool FinishEnabled { get { return false; } }

        #endregion

        public override Type GetNextPageType() { return base.MoveNextEnabled ? base.GetNextPageType() : typeof(SelectColumnsPage<TModel>); }

        #endregion
    }
}
