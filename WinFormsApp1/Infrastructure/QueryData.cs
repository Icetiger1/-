using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Infrastructure
{
    public class QueryData
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public void Execute(string sql)
        {

            //context.Open();
            //context.Query(sql);

        }

        public DataTable Execute(string sql, DataTable dataTable)
        {

            //context.Open();
            //context.Query(sql);

            //dataTable = context.GetDataTable(sql);

            return dataTable;
        }


    }
}
