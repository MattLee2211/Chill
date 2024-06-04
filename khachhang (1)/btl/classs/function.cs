using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.classs
{
    internal class function
    {
        public static SqlConnection conn;
        public static string connstring;
        public static void connect()
        {
            connstring = "Data Source=ZOOKEEPER;Initial Catalog=BTLC;Integrated Security=True;";
            conn = new SqlConnection();
            conn.ConnectionString = connstring;
            conn.Open();
            //MessageBox.Show("Ket noi thanh cong");
        }
        public static void disconnect() 
        {
            if(conn.State ==ConnectionState.Open) 
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        public static DataTable  GetDataTable(string sql)
        { SqlDataAdapter mydata = new SqlDataAdapter();
            mydata.SelectCommand = new SqlCommand();
            mydata.SelectCommand.Connection = classs.function.conn;
            mydata.SelectCommand.CommandText = sql;
            DataTable table = new DataTable();
            mydata.Fill(table);
            return table;
        }
        public static bool checkey(string sql)
        {
            SqlDataAdapter mydata = new SqlDataAdapter(sql,classs.function.conn);
            DataTable table = new DataTable();
            mydata.Fill(table);
            if(table.Rows.Count > 0 ) 
                return true;
            else
                return false;
        }
        public static void runsql(string sql)
        { 
            SqlCommand cmd;
            cmd=new SqlCommand();
            cmd.Connection= classs.function.conn;
            cmd.CommandText= sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(System.Exception loi)
            {
                MessageBox.Show(loi.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
    }
}
