using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QL_SanBongMini
{
    class Dbconnect
    {
        public SqlConnection conn;
        public Dbconnect()
        {
            conn = new SqlConnection(@"Data Source=KIETSO2AICUNGSO\SQLEXPRESS;Initial Catalog=QLSB;Integrated Security=True");
        }
        public void Open()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        public void Close()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public int getNonquery(string s)
        {
            Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            int kq = cmd.ExecuteNonQuery();
            Close();
            return kq;
        }
        public object getScalar(string s)
        {
            Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            object kq = cmd.ExecuteScalar();
            Close();
            return kq;
        }
        public SqlDataReader getdataReader(string s)
        {
            Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public DataTable getDataTable(string s)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(s, conn);
            adapter.Fill(dt);
            return dt;
        }

        public int updateDatable(DataTable dt,string sql)
        {
            //SqlDataAdapter adapter = new SqlDataAdapter("select *from Lop", conn);   
            //var bd = new SqlCommandBuilder(adapter);
            //adapter.Fill(t);
            //adapter.Update(t);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder bd = new SqlCommandBuilder(adapter);

            int kq = adapter.Update(dt);
            return kq;
        }
    }
}
