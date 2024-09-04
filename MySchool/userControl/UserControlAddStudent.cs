using Guna.UI2.WinForms;
using MySchool.TOOLS_HELPER;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MySchool.userControl
{
    public partial class UserControlAddStudent : UserControl
    {
        private readonly SchoolDBEntities db = new SchoolDBEntities();
        private tools tool = new tools();

        public UserControlAddStudent()
        {
            InitializeComponent();
            LoadData();
        }

        private void UserControlAddStudent_Load(object sender, EventArgs e)
        {
        }

        private void LoadData()
        {
            // Load data for guna2ComboBox1 (Classes)
            var classData = db.Classes
                .Select(C => C.ClassName)
                .ToArray();
            tool.FillComboBox(guna2ComboBox1, classData);

            // Load data for guna2ComboBox3 (Student Gender)
            var GenderName = db.Students
                .Select(S => S.Gender)
                .Distinct() // Ensure unique gender values
                .ToArray();
            tool.FillComboBox(guna2ComboBox3, GenderName);
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected class name from guna2ComboBox1
            string selectedClassName = guna2ComboBox1.SelectedItem.ToString();

            // Find the ClassID based on the selected class name
            var selectedClass = db.Classes.FirstOrDefault(c => c.ClassName == selectedClassName);
            if (selectedClass != null)
            {
                int classId = selectedClass.ClassID;

                // Filter divisions based on the selected ClassID
                var divisionNames = db.Divisions
                    .Where(d => d.ClassID == classId)
                    .Select(d => d.DivisionName)
                    .ToArray();

                // Fill the guna2ComboBox2 with the filtered divisions
                tool.FillComboBox(guna2ComboBox2, divisionNames);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
