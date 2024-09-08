using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySchool.userControl
{
    public partial class UserControlDashboard : UserControl
    {
        private readonly SchoolDBEntities db = new SchoolDBEntities();
        public UserControlDashboard()
        {
            InitializeComponent();
            var totalStudents = db.Students.Count();
            label5.Text = totalStudents.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
