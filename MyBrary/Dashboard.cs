using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using MyBrary.UserControls;
using MySql.Data.MySqlClient;

namespace MyBrary
{
    public partial class Dashboard : Form
    {

        private bool isLoggedIn = false;
        private string userRole = "";
        private int userId = 0;

        private DBConnection dbConnection;


        public Dashboard(bool isLoggedIn, string userRole, int userId)
        {
            InitializeComponent();

            this.isLoggedIn = isLoggedIn;
            this.userRole = userRole;
            this.userId = userId;

            dbConnection = new DBConnection();

            panel3.AutoScroll = true;


        }

        private void AddButtonsToPanel()
        {
            panel3.Controls.Clear(); // Bersihkan panel3 sebelum menambahkan kontrol baru

            // Menambahkan beberapa tombol ke panel3
            for (int i = 0; i < 20; i++)
            {
                Button btn = new Button
                {
                    Text = "Button " + (i + 1),
                    Location = new Point(10, 30 * i + 10), // Mengatur lokasi setiap tombol secara vertikal
                    Size = new Size(100, 25)
                };

                panel3.Controls.Add(btn);
            }
        }


        private void AddUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(userControl);
            userControl.BringToFront();
        }


        private void Dashboard_Load(object sender, EventArgs e)
        {
            updateLoginStatus();
            AddButtonsToPanel();
            Home home = new Home();
            AddUserControl(home);
        }

        private void updateLoginStatus()
        {
            if(isLoggedIn)
            {

                dbConnection.OpenConnection();
                MySqlConnection conn = dbConnection.GetConnection();

                loginBtn.Visible = false;
                logoutBtn.Visible = true;
                profileBtn.Visible = true;
                nameLbl.Visible = true;

                string query = "SELECT * FROM user_tb WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                nameLbl.Text = rdr.GetString("name");

                dbConnection.CloseConnection();
            } else
            {
                loginBtn.Visible=true;
                logoutBtn.Visible = false;
                profileBtn.Visible =false;
                nameLbl.Visible = false;
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            HelpAndSupport help = new HelpAndSupport();
            AddUserControl(help);
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            AddUserControl(home);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Books books = new Books();
            AddUserControl(books);
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            logout();
        }

        public void logout()
        {
            updateLoginStatus();
            isLoggedIn = false;
            userId = 0;
            userRole = "";
        }

        private void profileBtn_Click(object sender, EventArgs e)
        {
            Profile ba = new Profile();
            AddUserControl(ba);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
