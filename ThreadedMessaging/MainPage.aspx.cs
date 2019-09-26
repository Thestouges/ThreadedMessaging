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
        public class Item
        {
            public int ID { get; set; }
            public string Message { get; set; }
            public int? ParentID { get; set; }
        }
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
            if (!IsPostBack)
            {
                MessageID.Value = "";
            }
            DisplayMessages();
        }

        protected void NewThread(object sender, EventArgs e)
        {
            
        }

        private void DisplayMessages()
        {
            CreateMessageTree();
            tvMessages.ExpandAll();
        }

        private TreeView CreateMessageTree()
        {
            tvMessages.Nodes.Clear();
            SQLModule sQLModule = new SQLModule();
            DataTable dt = sQLModule.GetAllMessages();
            TreeView result = new TreeView();

            IEnumerable<Item> items = from item in dt.AsEnumerable()
                         select new Item
                         {
                             ID = item.Field<int>("ID"),
                             Message = item.Field<string>("Message"),
                             ParentID = item.Field<int?>("ParentMessageID")
                         };

            IEnumerable<Item> parent = from item in dt.AsEnumerable()
                         where item.IsNull("ParentMessageID")
                         select new Item{
                             ID = item.Field<int>("ID"),
                             Message = item.Field<string>("Message"),
                             ParentID = item.Field<int?>("ParentMessageID")
                         };

            foreach (Item item in parent.ToList())
            {
                TreeNode tvi = new TreeNode(item.Message);
                tvi.ToolTip = item.ID.ToString();
                BuildChildNodes(tvi, items.ToList(), item.ID);
                tvMessages.Nodes.Add(tvi);
            }

            return result;
        }

        
        private void BuildChildNodes(TreeNode parentNode, List<Item> items, int parentID)
        {
            List<Item> children = items.FindAll(p => p.ParentID == parentID);
            foreach (Item item in children)
            {
                TreeNode tvi = new TreeNode(item.Message);
                tvi.ToolTip = item.ID.ToString();
                parentNode.ChildNodes.Add(tvi);
                BuildChildNodes(tvi, items, item.ID);
            }
        }
        

        protected void btnDiscard_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMessage.InnerText.Trim() == "")
            {
                return;
            }
            SQLModule sQLModule = new SQLModule();
            if (MessageID.Value == "")
            {
                sQLModule.AddMessage(txtMessage.Value.ToString());
            }
            else
            {
                sQLModule.AddMessage(txtMessage.Value.ToString(), Int32.Parse(MessageID.Value));
            }
            MessageID.Value = "";
            DisplayMessages();
            txtMessage.InnerText = "";
        }

        protected void tvMessages_SelectedNodeChanged(object sender, EventArgs e)
        {
            MessageID.Value = tvMessages.SelectedNode.ToolTip.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openThreadModal();", true);
        }
    }
}