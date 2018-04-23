using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
// ...

namespace ReplaceQueryBuilderSample {
    public partial class FrmQueryBuilder : XtraForm {
        public FrmQueryBuilder() {
            InitializeComponent();
        }

        public string Selected {
            get { return (string)lstTables.SelectedItem; }
            set { lstTables.SelectedItem = value; }
        }

        public int TopRecords {
            get {
                int value;
                return int.TryParse(txtTop.Text, out value) ? value : default(int);
            }
            set { txtTop.Text = value.ToString(); }
        }

        public void Initialize(IEnumerable<string> tables) {
            lstTables.Items.Clear();
            lstTables.Items.AddRange(tables.ToArray());
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }
    }
}
