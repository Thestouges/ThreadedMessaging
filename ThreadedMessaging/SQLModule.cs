using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace ThreadedMessaging
{
    public class SQLModule
    {
        public void AddMessage(string msg, int parent = -1)
        {
            SqlConnection conn = new SqlConnection(Globals.connectionStr);
            SqlCommand cmd = new SqlCommand("AddMessage", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@message", SqlDbType.NVarChar);
            cmd.Parameters["@message"].Value = msg;

            if(parent != -1)
            {
                cmd.Parameters.Add("@parentID", SqlDbType.Int);
                cmd.Parameters["@parentID"].Value = parent;
            }

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAllMessages()
        {
            SqlConnection conn = new SqlConnection(Globals.connectionStr);
            SqlCommand cmd = new SqlCommand("GetAllMessages", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqladapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);

            return dt;
        }
    }
}