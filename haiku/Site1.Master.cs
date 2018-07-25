using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace haiku
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void navClick(object sender, EventArgs e)
        {
            if (sender == link_home)
                Response.Redirect("default.aspx");
            else if (sender == link_gallery)
                Response.Redirect("gallery.aspx");
            else if (sender == link_haiku)
                Response.Redirect("haiku.aspx");
            else
                Response.Redirect("wip.aspx");
        }
    }
}