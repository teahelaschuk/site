using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Threading;

namespace haiku
{
    public partial class haiku : System.Web.UI.Page
    {
        Random r;
        List<string> lines;
        int lineNum;
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();

        /// <summary>
        /// Sets up the page
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            r = new Random();
            string url = Server.MapPath("~/App_Data/mhyph.txt");
            lines = new List<string>(File.ReadAllLines(url));

            if (!IsPostBack)
            {
                label_prompt.Text = "Hit 'Generate Haiku' to get started. <br/> Save your best haiku in the guestbook below, and vote for your favourites.";
            }
        }

        /// <summary>
        /// Triggered when the user saves their haiku to the guestbook. 
        /// Adds the haiku to the database and sets up the next screen layout.
        /// </summary>
        protected void btn_saveq_Click(object sender, EventArgs e)
        {
            // format user's initials
            if (text_initials.Text.Equals("Enter your initials") 
                || String.IsNullOrEmpty(text_initials.Text))
            { 
                text_initials.Text = "Anonymous";
            }
            else
            {
                text_initials.Text = text_initials.Text.ToUpper();
            }

            // add haiku to db
            if(SaveHaiku(label_haiku1.Text, label_haiku2.Text, label_haiku3.Text, text_initials.Text))
            {
                label_prompt.Text = "Saved!";
            }
            else
            {
                label_prompt.Text = "Something went wrong. (I'm working on it)";
            }

            // update guestbook
            text_initials.Visible = false;
            btn_saveq.Visible = false;
            DataList_gl.DataBind();
            UpdatePanel1.Update();

        }

        /// <summary>
        /// Takes haiku data and adds to database. Returns true is successful.
        /// </summary>
        protected bool SaveHaiku(string line1, string line2, string line3, string name)
        {
            // set up db connection
            con = new OleDbConnection()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()
            };
            con.Open();

            cmd = new OleDbCommand(@"INSERT INTO[haikus](line1, line2, line3, author, entrydate, votes)" +
                "VALUES (@l1, @l2, @l3, @nm, @date, 0)", con);
            cmd.Parameters.AddWithValue("@l1", line1);
            cmd.Parameters.AddWithValue("@l2", line2);
            cmd.Parameters.AddWithValue("@l3", line3);
            cmd.Parameters.AddWithValue("@nm", name);
            cmd.Parameters.AddWithValue("@date", DateTime.Today);

            // execute + check for errors -- incomplete
            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// not implemented yet
        /// </summary>
        /// <returns></returns>
        protected bool checkAllowance()
        {
            int n = (Convert.ToInt32(Session["haikuCount"]));
            if (n < 5)
            {
                Session["haikuCount"] = n + 1;
            }

            // for testing
            string s = (n + 1).ToString();
            string script = "alert(\"" + s + "!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            return true;
        }
                

        /// <summary>
        /// Calls on MakeLine() to form a haiku and displays it.
        /// </summary>
        protected void btn_generate_Click(object sender, EventArgs e)
        {
            label_haiku1.Text = MakeLine(5);
            label_haiku2.Text = MakeLine(7);
            label_haiku3.Text = MakeLine(5);

            label_prompt.Text = "Is it brilliant? Add it to the guestbook.";       
            text_initials.Visible = true;
            btn_saveq.Visible = true;
        }

        /// <summary>
        /// Makes a single line of a haiku given the number of syllables required.
        /// </summary>
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
                while (!AnalyzeWord(lines[lineNum], ref s, ref syls, max));

                max -= syls;
                line += s + " ";
            }
            return line;
        }

        /// <summary>
        /// Takes a random work from the dictionary and determines whether it is viable, taking
        /// the requirements of the line in consideration.
        /// </summary>
        protected bool AnalyzeWord(string raw, ref string s, ref int syls, int max)
        {
            int count = 1;
            string temp = "";
            foreach (char c in raw)
            {
                if (c.Equals('='))
                {
                    if (++count > max)
                    { 
                        return false;
                    }
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

        /// <summary>
        /// Gets information about vote and passes information to UpdateVotes to process, then 
        /// updates the guestbook with the new data.
        /// </summary>
        protected void votebtn_Click(object sender, EventArgs e)
        {
            using (Button b = (Button)sender) {
                string[] commandArgs = b.CommandArgument.ToString().Split(new char[] { ',' });
                int id = Convert.ToInt32(commandArgs[0]);
                int votes = Convert.ToInt32(commandArgs[1]);

                if (b.CommandName == "upClick")
                {
                    votes += 1;
                }
                else if (b.CommandName == "downClick")
                {
                    votes -= 1;
                }

                // update db
                UpdateVotes(id, votes);

                // update guestbook
                DataList_gl.DataBind();
                UpdatePanel1.Update();
            }            
        }

        /// <summary>
        /// Connects to the database and updates the number of votes on the selected haiku
        /// </summary>
        protected void UpdateVotes(int id, int newVotes)
        {
            // set up db connection
            con = new OleDbConnection()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()
            };
            con.Open();

            cmd = new OleDbCommand(@"UPDATE [haikus] SET votes = @votes WHERE id = @ID", con);
            cmd.Parameters.AddWithValue("@votes", newVotes);
            cmd.Parameters.AddWithValue("@ID", id);

            // execute + check for errors
            int a = cmd.ExecuteNonQuery();
            if (a <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Something went wrong! Cannot add to database')", true);
            }
            con.Close();
        }
    }
}