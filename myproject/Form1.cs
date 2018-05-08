using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myproject
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        

        public Form1()
        {
            InitializeComponent();
            this.Text = "Login";
            this.CenterToScreen();
            //DBConnection dbConn = new DBConnection();
            conn = DBConnection.Instance();
            password_txt.PasswordChar = '*';

            
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (username_txt.Text == "" || password_txt.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Username và Password");
                return;
            }

            try
            {
                //Create SqlConnection
               

                
                SqlCommand cmd = new SqlCommand("SELECT * FROM users where username=@username and password=@password", conn);
                cmd.Parameters.AddWithValue("@username", username_txt.Text);
                cmd.Parameters.AddWithValue("@password", password_txt.Text);
                conn.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                conn.Close();
                int count = ds.Tables[0].Rows.Count;
                //If count is equal to 1, than show frmMain form
                if (count == 1)
                {
                    //MessageBox.Show("Login Successful!");

                    

                    this.Hide();
                    Dashboard dashboard = new Dashboard();

                    //Console.WriteLine(username_txt.Text);
                    //Program.username = username_txt.Text;
                    //Console.WriteLine(Program.username);

                    Program.setUsername(username_txt.Text);


                    dashboard.Show();

                    /*this.Hide();
                    Maps maps = new Maps();
                    maps.Show();*/

                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

           
        }

        private void username_txt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
