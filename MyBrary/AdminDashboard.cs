using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyBrary.UserControls;

namespace MyBrary
{
    public partial class AdminDashboard : Form
    {

        private string userRole = "";
        private int userId = 0;

        public AdminDashboard(string userRole, int userId)
        {
            InitializeComponent();
            this.userRole = userRole;
            this.userId = userId;
        }

        private void AddUserControl(UserControl userControl) 
        { 
            userControl.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            Reads r = new Reads();
            AddUserControl(r);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            Reads r = new Reads();
            AddUserControl(r);

            if(userRole.ToString() == "petugas")
            {
                guna2Button2.Visible = false;
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            BooksAdmin ba = new BooksAdmin();
            AddUserControl(ba);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Officers ba = new Officers();
            AddUserControl(ba);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard(false, "", 0);
            dashboard.Show();
            this.Hide();
        }

        private void profileBtn_Click(object sender, EventArgs e)
        {
            Profile ba = new Profile();
            AddUserControl(ba);
        }
    }
}
