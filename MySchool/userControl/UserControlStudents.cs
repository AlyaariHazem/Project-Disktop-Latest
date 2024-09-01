using Guna.UI2.WinForms;
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
    public partial class UserControlStudents : UserControl
    {
        public UserControlStudents()
        {
            InitializeComponent();
            StyleDataGridView(guna2DataGridView1);
            InitializeDataGridViewColumns();
        }

        private void InitializeDataGridViewColumns()
        {
            // this for Stages
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.Columns.Add("StudentID", "#");
            guna2DataGridView1.Columns.Add("StudentName", "اسم الطالب");
            guna2DataGridView1.Columns.Add("Stage", "المرحلة");
            guna2DataGridView1.Columns.Add("Class1", "الصـف");
            guna2DataGridView1.Columns.Add("Division", "الشعبة");
            guna2DataGridView1.Columns.Add("Age", "العمر");
            guna2DataGridView1.Columns.Add("Type", "النوع");
            guna2DataGridView1.Columns.Add("HireDate", "تاريخ التسجيل");
            

            var buttonColumn3 = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "حذف",
                Text = "حذف",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            guna2DataGridView1.Columns.Add(buttonColumn3);
            var buttonColumnedit = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "تعديل",
                Text = "تعديل",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            guna2DataGridView1.Columns.Add(buttonColumnedit);
            
        }

        private void StyleDataGridView(Guna2DataGridView x)
        {
            // DataGridView properties
            x.AllowUserToAddRows = false;
            x.AllowUserToDeleteRows = false;
            x.AllowUserToResizeColumns = false;
            x.AllowUserToResizeRows = false;
            x.ReadOnly = false;
            x.MultiSelect = false;
            x.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            x.GridColor = Color.LightGray;
            x.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            x.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);

            x.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            x.ColumnHeadersHeight = 40;
            x.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            x.EnableHeadersVisualStyles = true;

            x.DefaultCellStyle.BackColor = Color.White;
            x.DefaultCellStyle.ForeColor = Color.Black;
            x.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            x.DefaultCellStyle.SelectionForeColor = Color.Black;
            x.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            x.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            x.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);

            x.RowTemplate.Height = 60;
            x.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            x.BorderStyle = BorderStyle.None;
            x.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            x.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            x.BackgroundColor = Color.WhiteSmoke;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();

            UserControlAddStudent userControlAddStudent = new UserControlAddStudent();

            userControlAddStudent.Dock = DockStyle.Fill;

            this.Controls.Add(userControlAddStudent);
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
