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
using System.IO;


namespace WindowsApplication1
{
    public partial class Form1 : XtraForm
    {
        DataTable tableNumQuery = new DataTable();
        List<string> queries = new List<string>();

        public Form1()
        {
            InitializeComponent();
            tableNumQuery.Columns.Add("Number", typeof(int));
            tableNumQuery.Columns.Add("Link", typeof(string));
        }

        #region Загрузка вопросов

        /// <summary>
        /// Загружает список вопросов
        /// </summary>
        public void LoadQueries()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] qtext;
                    using (StreamReader sr = new StreamReader(dialog.FileName, Encoding.Default))
                        qtext = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    DialogResult res = MessageBox.Show("Вопросы пронумерованы?", "Нумерация", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                        qtext = TrimNumbers(qtext);
                    this.queries = new List<string>(qtext);
                    recQueries.ReadOnly = true;
                    recQueries.Text = "";
                    for (int i = 0; i < qtext.Length; i++)
                        recQueries.Text += qtext[i] + Environment.NewLine;
                }
            }
        }

        public string[] TrimNumbers(string[] qtext)
        {
            int index = 0;
            for (int i = 0; i < qtext.Length; i++)
            {
                index = qtext[i].IndexOf('.');
                qtext[i] = qtext[i].Substring(index + 1, qtext[i].Length - index-1).Trim();
            }
            return qtext;
        }

        #endregion

        /// <summary>
        /// Ищет ссылки, по которым будет осуществляться поиск текста
        /// </summary>
        /// <param name="queries">Лист с вопросами</param>
        public void RunSearchLinks(List<string> queries)
        {
            for (int i = 0; i < queries.Count; i++)
            {
                if (queries[i] == "") continue;
                WebQuery query = new WebQuery(queries[i].Replace(".", "").Replace(",",""));
                query.StartIndex.Value = 1;
                query.HostLangauge.Value = "Russian";
                List<string> urls = new List<string>();

                IGoogleResultSet<GoogleWebResult> resultSet = GoogleService.Instance.Search<GoogleWebResult>(query);
                for (int j = 0; j < resultSet.Results.Count; j++)
                {
                    tableNumQuery.Rows.Add(new object[] { i + 1, resultSet.Results[j].Url });
                }
                System.Threading.Thread.Sleep(500);
            }
            
        }

        private void bbRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           RunSearchLinks(queries);
            int ii = 0;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadQueries();
        }

        private void bbLinks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (LinksTable lt = new LinksTable(this.tableNumQuery))
                lt.ShowDialog();
        }
    }
}