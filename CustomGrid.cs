using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace ss
{
    class CustomGrid : System.Windows.Forms.DataGrid
    {
        private WhiteServerCollection _serverList1;
        private BlueServerCollection _serverList2;

        public  new CustomDataTable DataSource
        {
            get { return (CustomDataTable)base.DataSource; }
            set { base.DataSource = value; }
        }

        public CustomGrid()
        {
            _serverList1 = new WhiteServerCollection();
            _serverList2 = new BlueServerCollection();
        }

        public void UpdateRows(string sender)
        {
            //
            ServerCollection sList;
            if (sender == "白LAN")
                sList = this._serverList1;
            else
                sList = this._serverList2;

            DataTable dt = (DataTable)this.DataSource;
            foreach (Server server in sList)
            {

                DataRow h = dt.Select(String.Format("[Server] = '{0}'", server.name))[0];

                h["Server"] = server.name;
                h["StartUp"] = server.StartUpCheck();
                string[] errorlist = server.StartUpErrorCheck();
                h["errorDetail"] = errorlist;
                h["Error"] = errorlist.Length;

                h["WF"] = server.WfServiceLogCheck();

            }
        }

        private void ChangeNullText()
        {
            //
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = "hoge";
            this.TableStyles.Add(ts);
            this.TableStyles["hoge"].GridColumnStyles["StartUp"].Width = 100;
            ts.RowHeadersVisible = false;
            this.TableStyles["hoge"].GridColumnStyles["errorDetail"].Width = 0;
            DataGridTextBoxColumn cs;
            foreach (DataColumn dc in this.DataSource.Columns)
            {
                //
                if (dc.DataType == typeof(bool))
                {
                    System.Windows.Forms.DataGridBoolColumn bc = (DataGridBoolColumn)ts.GridColumnStyles[dc.ColumnName];
                    bc.NullValue = true;
                    continue;
                }
                cs = (DataGridTextBoxColumn)ts.GridColumnStyles[dc.ColumnName];
                //(Null)を変更する
                cs.NullText = "";
            }
        }

        public void Init()
        {
            //
            _serverList1.RestoreState();
            _serverList2.RestoreState();

            this.DataSource = new CustomDataTable();

            this.ChangeNullText();
            
            this.DataSource.AddRows(_serverList1);
            this.DataSource.AddRows(_serverList2);
          
        }

    }
}
