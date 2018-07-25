using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Configuration;

namespace haiku
{
    public partial class haiku : System.Web.UI.Page
    {
        string haikoo;
        Random r;
        List<string> lines;
        int lineNum;

        protected void Page_Load(object sender, EventArgs e)
        {
            haikoo = "testing\ntesting\ntesting";
            r = new Random();

            string url = Server.MapPath("~/App_Data/mhyph.txt");
            lines = new List<string>(File.ReadAllLines(url));

            if(IsPostBack)
            {
                DataList1.DataBind();
            }
        }

        protected void btn_saveq_Click(object sender, EventArgs e)
        {
            // check name
            if (text_initials.Text.Equals("Enter your initials") 
                || String.IsNullOrEmpty(text_initials.Text))
                text_initials.Text = "Anonymous";
            else
                text_initials.Text = text_initials.Text.ToUpper();

            // set up connection
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();

            // insert data
            cmd.CommandText = 
                "INSERT INTO [haikus] (line1, line2, line3, author, entrydate, votes)" +
                "VALUES (@l1, @l2, @l3, @nm, @date, 0)";
            cmd.Parameters.AddWithValue("@l1", label_haiku1.Text);
            cmd.Parameters.AddWithValue("@l2", label_haiku2.Text);
            cmd.Parameters.AddWithValue("@l3", label_haiku3.Text);
            cmd.Parameters.AddWithValue("@nm", text_initials.Text);
            cmd.Parameters.AddWithValue("@date", DateTime.Today);
            cmd.Connection = con;

            // execute + check for errors
            int a = cmd.ExecuteNonQuery();
            if (a <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong! Cannot add to database')", true);
            }

            // refresh
            Response.Redirect(Request.RawUrl);
        }

        protected void btn_generate_Click(object sender, EventArgs e)
        {
            haikoo = "";

            label_haiku1.Text = MakeLine(5);
            label_haiku2.Text = MakeLine(7);
            label_haiku3.Text = MakeLine(5);

            label_saveq.Visible = true;       
            text_initials.Visible = true;
            btn_saveq.Visible = true;
            div_accent.Attributes["style"] = "visibility: visible";

        }

        protected string MakeLine(int max)
        {
            string s = "";
            string line = "";
            int syls = -1;
            while (max > 0)
            {
                do
                {
                    lineNum = r.Next(0, lines.Count - 1);
                }
                while (!analyzeWord(lines[lineNum], ref s, ref syls, max));

                max -= syls;
                line += s + " ";
            }

            return line;
        }

        protected bool analyzeWord(string raw, ref string s, ref int syls, int max)
        {
            int count = 1;
            string temp = "";
            foreach (char c in raw)
            {
                if (c.Equals('='))
                {
                    if (++count > max)
                        return false;
                }
                else
                {
                    temp += c;
                }
            }
            s = temp;
            syls = count;
            return true;
        }

       
    }

}