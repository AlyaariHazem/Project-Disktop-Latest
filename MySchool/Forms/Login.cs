using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySchool.Forms
{
    public partial class Login : Form
    {
       private readonly SchoolDBEntities db = new SchoolDBEntities();

        public Login()
        {
            InitializeComponent();
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            string username=guna2TextBox1.Text;
            string password=guna2TextBox2.Text;

            var user=db.Users.FirstOrDefaultAsync(u=>u.UserName == username && u.Password==password);

            if (user != null)
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Visible = false;
            }
            else
            {
                guna2TextBox1.Text = "";
                guna2TextBox2.Text = "";
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            guna2TextBox2.PasswordChar = guna2CheckBox1.Checked ? '\0' : '*';
        }
    }
}
