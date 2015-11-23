using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GoogleSearch
{
    public partial class LinksTable : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dt = new DataTable();
        public LinksTable(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        private void LinksTable_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = this.dt;
        }
    }
}