using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        }
    }
}