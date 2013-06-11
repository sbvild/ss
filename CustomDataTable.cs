using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ss
{
    class CustomDataTable : DataTable
    {

        public void AddRows(ServerCollection serverList)
        {
            //
            foreach (Server sss in serverList)
            {
                //
                DataRow n = this.NewRow();
                n["Server"] = sss.name;
                n["etc."] = false;
                this.Rows.Add(n);
            }
        }

        public CustomDataTable() : base("hoge")
        {
            //
            
            this.Columns.Add("Server");
            this.Columns.Add("StartUp");
            this.Columns.Add("Error");
            this.Columns.Add("WF");
            this.Columns.Add("etc.", typeof(bool));
            this.Columns.Add("errorDetail", typeof(string[]));
        }

    }
}
