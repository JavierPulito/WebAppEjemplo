using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEjemplo
{
    public partial class pagina3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string myval = Request["texto"];
            ((Label)Label1.FindControl("Label1")).Text = myval;
        }
    }
}