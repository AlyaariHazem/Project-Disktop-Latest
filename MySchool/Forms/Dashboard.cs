using MySchool.userControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySchool.Forms
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();

            userControlDashboard();

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Get the selected node
            TreeNode selectedNode = e.Node;

            // Build a message to display information about the selected node
            string message = selectedNode.Text;

            if (message.ToLower() == "المراحل والصفوف")
            {
                LoadUserControlStage();


            }
            else if(message.ToLower() == "جميع الطلاب")
            {
                LoadUserControlStudents();
            }
            else if (message.ToLower() == "عرض المواد")
            {
                LoadUserControlSubject();
            }
            else if (message.ToLower() == "إضافة درجات")
            {
                LoadUserControlStudents();
            }
        }
        private void LoadUserControlStage()
        {
            // Clear the panel before adding a new control
            panel1.Controls.Clear();

            userControlStages userControlStages = new userControlStages();

            userControlStages.Dock = DockStyle.Fill;

            panel1.Controls.Add(userControlStages);
        }
        private void LoadUserControlStudents()
        {
            panel1.Controls.Clear();

            UserControlStudents userControlStudents = new UserControlStudents();

            userControlStudents.Dock = DockStyle.Fill;

            panel1.Controls.Add(userControlStudents);
        }

        private void LoadUserControlSubject()
        {
            panel1.Controls.Clear();

            UserControlSubjects userControlSubjects = new UserControlSubjects();

            userControlSubjects.Dock = DockStyle.Fill;

            panel1.Controls.Add(userControlSubjects);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

<<<<<<< HEAD
        private void MainClick(object sender, EventArgs e)
        {
            userControlDashboard();
        }
        private void userControlDashboard()
        {
            panel1.Controls.Clear();

            UserControlDashboard userControlDashboard = new UserControlDashboard();

            userControlDashboard.Dock = DockStyle.Fill;

            panel1.Controls.Add(userControlDashboard);
        }

        private void TextClick(object sender, EventArgs e)
        {
            userControlDashboard();
=======
        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
>>>>>>> salah5
        }
    }
}
