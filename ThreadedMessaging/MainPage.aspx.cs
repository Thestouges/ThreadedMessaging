using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ThreadedMessaging
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath("~/SQLConnectionString.txt")))
            {
                Globals.connectionStr = File.ReadAllText(Server.MapPath("~/SQLConnectionString.txt"));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                  "err_msg",
                  "alert('Database not loaded');",
                  true);
            }
            DisplayMessages();
        }

        protected void NewThread(object sender, EventArgs e)
        {
            
        }

        private void DisplayMessages()
        {

        }

        private TreeView CreateMessageTree()
        {
            SQLModule sQLModule = new SQLModule();
            DataTable dt = sQLModule.GetAllMessages();
            TreeView result = new TreeView();

            var parent = from item in dt.AsEnumerable()
                         where item.Field<int>("ParentMessageID") == null
                         select new {
                             ID = item.Field<int>("ID"),
                             Message = item.Field<int>("Message")
                         };

            foreach(var item in parent)
            {
                Console.WriteLine(item.ID + " "+item.Message);
            }

            return result;
        }
    }
}