using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using GoogleSearch;
using GoogleSearchAPI;
using GoogleSearchAPI.Query;


namespace WindowsApplication1
{
    public partial class Form1 : XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bbRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WebQuery query = new WebQuery("Магазин цветов Пятигорск");
            query.StartIndex.Value = 1;
            query.HostLangauge.Value = "Russain";

            IGoogleResultSet<GoogleWebResult> resultSet = GoogleService.Instance.Search<GoogleWebResult>(query);
            int i = 0;
        }
    }
}