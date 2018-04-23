using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.DataAccess.UI.Wizard.Views;
using DevExpress.DataAccess.Wizard;
using DevExpress.DataAccess.Wizard.Services;
// ...

namespace ReplaceQueryBuilderSample {
    class MyConfigureQueryPageView : ConfigureQueryPageView {
        public MyConfigureQueryPageView(IDisplayNameProvider displayNameProvider, IServiceProvider propertyGridServices, ICustomQueryValidator customQueryValidator, SqlWizardOptions options) : base(displayNameProvider, propertyGridServices, customQueryValidator, options) { }

        public IWin32Window Window { get { return FindForm(); } }
    }
}
