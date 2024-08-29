using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Data.Entity; // For Entity Framework 6
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MySchool.userControl
{
    public partial class userControlStages : UserControl
    {
        private readonly SchoolDBEntities db = new SchoolDBEntities();
        private int? currentEditingStageID = null;
        private int? currentEditingClassID = null;
        private int? currentEditingDivisionID = null;

        public userControlStages()
        {
            InitializeComponent();
            InitializeDataGridViewColumns();
            LoadData();

            StyleDataGridView(guna2DataGridView1);
            StyleDataGridView(guna2DataGridView2);
            StyleDataGridView(guna2DataGridView3);

            guna2DataGridView2.Columns["StudentCount"].Width = 150;

            // Attach event handlers for each DataGridView
            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;
            guna2DataGridView2.CellContentClick += guna2DataGridView2_CellContentClick;
            guna2DataGridView3.CellContentClick += guna2DataGridView3_CellContentClick;
            
            // fill comboBox in the Class
            var stageNames = db.Stages
                .Select(stage => stage.StageName)
                .ToArray();

            guna2ComboBox2.Items.Clear(); 
            guna2ComboBox2.Items.AddRange(stageNames);

            // Retrieve all ClassNames from the Classes table and store them in a string array
            var ClassNames = db.Classes
                .Select(c => c.ClassName)
                .ToArray();

            guna2ComboBox1.Items.Clear(); // Clear previous items
            guna2ComboBox1.Items.AddRange(ClassNames);
        }

        private void InitializeDataGridViewColumns()
        {
            // this for Stages
            guna2DataGridView2.Columns.Clear();
            guna2DataGridView2.Columns.Add("StageID", "#");
            guna2DataGridView2.Columns.Add("StageName", "اسم المرحلة");
            guna2DataGridView2.Columns.Add("Classes", "الصفوف");
            guna2DataGridView2.Columns.Add("StudentCount", "إجمالي الطلاب");
            guna2DataGridView2.Columns.Add("Note", "الملاحظة");
            AddButtonColumns();
            // this for Classes
            guna2DataGridView3.Columns.Clear();
            guna2DataGridView3.Columns.Add("ClassID", "#");
            guna2DataGridView3.Columns.Add("ClassName", "الصف");
            guna2DataGridView3.Columns.Add("StageName", "المرحلة");
            guna2DataGridView3.Columns.Add("DivisionName", "الشعب");
            guna2DataGridView3.Columns.Add("Active", "الحالة");

            var buttonColumn3 = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "حذف",
                Text = "حذف",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            guna2DataGridView3.Columns.Add(buttonColumn3);
            var buttonColumnedit = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "تعديل",
                Text = "تعديل",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            guna2DataGridView3.Columns.Add(buttonColumnedit);
            //this for division
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.Columns.Add("DivisionID", "#");
            guna2DataGridView1.Columns.Add("DivisionName", "الشعبة");
            guna2DataGridView1.Columns.Add("className", "الصف");
            guna2DataGridView1.Columns.Add("SUMStudent", "إجمالي الطلاب");
            var buttonColumn1 = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "حذف",
                Text = "حذف",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            guna2DataGridView1.Columns.Add(buttonColumn1);
            var buttonColumnedit1 = new DataGridViewButtonColumn
            {
                Name = "Edit",
                HeaderText = "تعديل",
                Text = "تعديل",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            guna2DataGridView1.Columns.Add(buttonColumnedit1);
        }

        private void StyleDataGridView(Guna2DataGridView x)
        {
            guna2TabControl1.RightToLeft = RightToLeft.Yes;
            guna2TabControl1.RightToLeftLayout = true;

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
            x.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
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

        private void LoadData()
        {
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView3.Rows.Clear();
            guna2DataGridView1.Rows.Clear();

            var stagesData = db.Stages
                .Select(s => new
                {
                    s.StageID,
                    s.StageName,
                    Classes = s.Classes.Count(),
                    StudentCount = s.Classes.SelectMany(c => c.Students).Count(),
                    s.Note
                })
                .ToList();

            var ClassData = db.Classes
                .Select(s => new
                {
                    s.ClassID,
                    s.ClassName,
                    StageName = s.Stages.StageName,
                    DivisionCount = s.Divisions.Count(),
                    StudentCount = s.Divisions.SelectMany(c => c.Students).Count(),
                    classActive=s.IsActive
                })
                .ToList();
            var DivisionData = db.Divisions
                .Select(D => new
                {
                    D.DivisionID,
                    D.DivisionName,
                    ClassName = D.Classes.ClassName,
                    StudentCount=D.Students.Count(), 
                });
            foreach ( var division in DivisionData )
            {
                var index = guna2DataGridView1.Rows.Add(division.DivisionID, division.DivisionName, division.ClassName, string.Join(", ", division.StudentCount));
                guna2DataGridView1.Rows[index].Height = 35;
            }
            foreach (var _class in ClassData)
            {
                var index = guna2DataGridView3.Rows.Add(_class.ClassID,_class.ClassName, _class.StageName, string.Join(", ", _class.DivisionCount), _class.classActive);
                guna2DataGridView3.Rows[index].Height = 35;
            }

            foreach (var stage in stagesData)
            {
                var index = guna2DataGridView2.Rows.Add(stage.StageID, stage.StageName, string.Join(", ", stage.Classes), stage.StudentCount, stage.Note);
                guna2DataGridView2.Rows[index].Height = 35;
            }
        }

        private void AddButtonColumns()
        {
           
                AddButtonColumn("Edit", "تعديل", "تعديل");
                AddButtonColumn("Delete", "حـذف", "حـذف");

        }

        private void AddButtonColumn(string name, string headerText, string buttonText)
        {
            var buttonColumn = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = headerText,
                Text = buttonText,
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            guna2DataGridView2.Columns.Add(buttonColumn);
        }
        
        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var selectedStageID = guna2DataGridView2.Rows[e.RowIndex].Cells["StageID"].Value?.ToString();

                if (!string.IsNullOrEmpty(selectedStageID))
                {
                    int stageID = int.Parse(selectedStageID);

                    if (guna2DataGridView2.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        EditStage(stageID);
                    }
                    else if (guna2DataGridView2.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        DeleteStage(stageID);
                    }
                }
            }
        }

        private void EditStage(int stageID)
        {
            var stage = db.Stages.Find(stageID);
            if (stage != null)
            {
                guna2TextBox4.Text = stage.StageName;
                guna2TextBox3.Text = stage.Note;

                currentEditingStageID = stageID;
                guna2TileButton2.Text = "تعديل"; // Change the button text to "تعديل" (Edit)
            }
            else
            {
                MessageBox.Show("المرحلة غير موجودة");
            }
        }

        private void AddStage()
        {
            if (ValidateInputs())
            {
                try
                {
                    var stage = new Stages
                    {
                        StageName = guna2TextBox4.Text,
                        Note = guna2TextBox3.Text,
                        Active = true,
                        HireDate = DateTime.Now,
                        YearID = 1
                    };

                    db.Stages.Add(stage);
                    db.SaveChanges();
                    MessageBox.Show("!تم إضافة المرحلة بنجاح");

                    LoadData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ: " + ex.Message);
                }
            }
        }

        private void UpdateStage()
        {
            if (currentEditingStageID.HasValue && ValidateInputs())
            {
                try
                {
                    var stage = db.Stages.Find(currentEditingStageID.Value);

                    if (stage != null)
                    {
                        stage.StageName = guna2TextBox4.Text;
                        stage.Note = guna2TextBox3.Text;

                        db.SaveChanges();

                        MessageBox.Show("!تم تعديل المرحلة بنجاح");

                        LoadData();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show(".المرحلة غير موجودة");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ: " + ex.Message);
                }
            }
        }

        private void DeleteStage(int stageID)
        {
            var result = MessageBox.Show("هل أنت متأكد من أنك تريد حذف المرحلة ؟", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var stage = db.Stages.Find(stageID);
                if (stage != null)
                {
                    db.Stages.Remove(stage);
                    db.SaveChanges();
                    MessageBox.Show("ّ!تم حذف المرحلة بنجاح");

                    LoadData();
                }
                else
                {
                    MessageBox.Show(".المرحلة غير موجودة");
                }
            }
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(guna2TextBox4.Text))
            {
                MessageBox.Show("الرجاء إدخال اسم المرحلة.");
                return false;
            }

            if (string.IsNullOrEmpty(guna2TextBox3.Text))
            {
                MessageBox.Show("الرجاء إدخال ملاحظة.");
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            guna2TextBox1.Text = string.Empty;
            guna2TextBox4.Text = string.Empty;
            guna2TextBox2.Text = string.Empty;
            guna2TextBox3.Text = string.Empty;
            guna2ComboBox2.Text= string.Empty;
            guna2ComboBox1.Text = string.Empty;
            guna2TileButton2.Text = "إضافة+"; // Reset to Add mode
            guna2TileButton3.Text = "إضافة+"; // Reset to Add mode
            guna2TileButton1.Text = "إضافة+"; // Reset to Add mode
            currentEditingStageID = null;
            currentEditingClassID = null;
            
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            if (guna2TileButton2.Text == "تعديل")
            {
                UpdateStage();
            }
            else
            {
                AddStage();
            }
        }
        
        // Event handler for guna2DataGridView3 (Classes)*******************************************************************************
        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var selectedClassID = guna2DataGridView3.Rows[e.RowIndex].Cells["ClassID"].Value?.ToString();

                if (!string.IsNullOrEmpty(selectedClassID))
                {
                    int classID = int.Parse(selectedClassID);

                    if (guna2DataGridView3.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        EditClass(classID);  // Class edit logic
                    }
                    else if (guna2DataGridView3.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        DeleteClass(classID);  // Class delete logic
                    }
                }
            }
        }

        // Example of Edit/Delete methods for Classes (similar to Stages)
        private void EditClass(int classID)
        {
            // Fetch the Class record based on classID
            var class1 = db.Classes.Include(c => c.Stages).FirstOrDefault(c => c.ClassID == classID);


            if (class1 != null)
            {
                // Retrieve all StageNames from the Stages table and store them in a string array
                var stageNames = db.Stages
                    .Select(stage => stage.StageName)
                    .ToArray();

                // Optionally, you can assign the array to a ComboBox or process it as needed
                guna2ComboBox2.Items.Clear(); // Clear previous items
                guna2ComboBox2.Items.AddRange(stageNames); // Add the stage names to the ComboBox

                // Set the class information to the appropriate UI elements
                guna2ComboBox2.Text = class1.Stages?.StageName;  // Access the StageName through the navigation property
                guna2TextBox2.Text = class1.ClassName;

                // Set the current editing ID to track the class being edited
                currentEditingClassID = classID;
                guna2TileButton3.Text = "تعديل"; // Change the button text to "تعديل" (edit)
            }
            else
            {
                MessageBox.Show("المرحلة غير موجودة");
            }
        }
        private void AddClass()
        {
            if (ValidateInputsForClass())
            {
                try
                {
                    // Find the selected stage based on the stage name in guna2ComboBox2
                    var selectedStage = db.Stages.FirstOrDefault(s => s.StageName == guna2ComboBox2.Text);

                    if (selectedStage != null)
                    {
                        // Create new class object
                        var class1 = new Classes
                        {
                            ClassName = guna2TextBox2.Text,
                            StageID = selectedStage.StageID, // Assign the StageID
                            IsActive = 1,
                            ClassYear = DateTime.Now.Year.ToString(),
                        };

                        db.Classes.Add(class1); // Add class to the database
                        db.SaveChanges(); // Save changes
                        MessageBox.Show("!تم إضافة الصـف بنجاح");

                        LoadData(); // Reload data to refresh the DataGridView
                        ResetForm(); // Reset the form fields after successful addition
                    }
                    else
                    {
                        MessageBox.Show("!الرجاء اختيار المرحلة المناسبة.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ: " + ex.Message);
                }
            }
        }

        private bool ValidateInputsForClass()
        {
            if (string.IsNullOrEmpty(guna2ComboBox2.Text))
            {
                MessageBox.Show("الرجاء إختيار اسم المرحلة.");
                return false;
            }

            if (string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                MessageBox.Show("الرجاء إدخال الصـف.","error");
                return false;
            }

            return true;
        }
        private bool ValidateInputsForDivision()
        {
            if (string.IsNullOrEmpty(guna2ComboBox1.Text))
            {
                MessageBox.Show("الرجاء إختيار اسم الصـف.");
                return false;
            }

            if (string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                MessageBox.Show("الرجاء إدخال الشعبة.", "error");
                return false;
            }

            return true;
        }
        private void UpdateClass()
        {
            if (currentEditingClassID.HasValue && ValidateInputsForClass())
            {
                try
                {
                    // Fetch the Class record to be updated
                    var class1 = db.Classes.Include(s => s.Stages).FirstOrDefault(c => c.ClassID == currentEditingClassID);

                    if (class1 != null)
                    {
                        // Update the class name from the TextBox
                        class1.ClassName = guna2TextBox2.Text;

                        // Find and update the related Stage using the selected stage name from the ComboBox
                        var selectedStage = db.Stages.FirstOrDefault(s => s.StageName == guna2ComboBox2.Text);
                        if (selectedStage != null)
                        {
                            class1.StageID = selectedStage.StageID; // Update the StageID to the selected Stage's ID
                        }

                        // Save changes to the database
                        db.SaveChanges();

                        MessageBox.Show("!تم تعديل المرحلة بنجاح"); // Show success message

                        // Reload data and reset form fields
                        LoadData();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show(".المرحلة غير موجودة"); // Show error message if class not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ: " + ex.Message); // Show error message in case of an exception
                }
            }
        }

        private void DeleteClass(int classID)
        {
            var result = MessageBox.Show("هل أنت متأكد من أنك تريد حذف الصـف ؟", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var Class1 = db.Classes.Find(classID);
                if (Class1 != null)
                {
                    db.Classes.Remove(Class1);
                    db.SaveChanges();
                    MessageBox.Show("ّ!تم حذف الصـف بنجاح");

                    LoadData();
                }
                else
                {
                    MessageBox.Show(".الصـف غير موجودة");
                }
            }
        }
        
        private void guna2TileButton3_Click_1(object sender, EventArgs e)
        {
            if (guna2TileButton3.Text == "تعديل")
            {
                UpdateClass();
            }
            else
            {
                AddClass();
            }
        }

        // Event handler for guna2DataGridView1***************************************************************************************
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var selectedDivisionID = guna2DataGridView1.Rows[e.RowIndex].Cells["DivisionID"].Value?.ToString();

                if (!string.IsNullOrEmpty(selectedDivisionID))
                {
                    int DivisionID = int.Parse(selectedDivisionID);

                    if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        EditDivision(DivisionID);  // Implement class editing logic
                    }
                    else if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        DeleteDivision(DivisionID);  // Implement class deletion logic
                    }
                }
            }
        }

        private void EditDivision(int DivisionID)
        {
            // Fetch the Class record based on classID
            var Division = db.Divisions.Include(c => c.Classes).FirstOrDefault(c => c.DivisionID == DivisionID);


            if (Division != null)
            {
                // Retrieve all ClassNames from the Classes table and store them in a string array
                var ClassNames = db.Classes
                    .Select(c => c.ClassName)
                    .ToArray();

                guna2ComboBox1.Items.Clear(); // Clear previous items
                guna2ComboBox1.Items.AddRange(ClassNames); // Add the stage names to the ComboBox

                guna2ComboBox1.Text = Division.Classes?.ClassName; 
                guna2TextBox1.Text = Division.DivisionName;

                currentEditingDivisionID = DivisionID;
                guna2TileButton1.Text = "تعديل"; // Change the button text to "تعديل" (edit)
            }
            else
            {
                MessageBox.Show("الشعبة غير موجودة");
            }
        }

        private void AddDivision()
        {
            if (ValidateInputsForDivision())
            {
                try
                {
                    // Find the selected stage based on the stage name in guna2ComboBox2
                    var selectedClass = db.Classes.FirstOrDefault(c => c.ClassName== guna2ComboBox1.Text);

                    if (selectedClass != null)
                    {
                        // Create new class object
                        var division = new Divisions
                        {
                            DivisionName = guna2TextBox1.Text,
                            ClassID = selectedClass.ClassID,
                        };

                        db.Divisions.Add(division);
                        db.SaveChanges();
                        MessageBox.Show("!تم إضافة الشعبة بنجاح");

                        LoadData(); // Reload data to refresh the DataGridView
                        ResetForm(); // Reset the form fields after successful addition
                    }
                    else
                    {
                        MessageBox.Show("!الرجاء اختيار الصـف المناسبة.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ: " + ex.Message);
                }
            }
        }


        private void DeleteDivision(int classID)
        {
            
        }

        private void UpdateDivision()
        {
            if (currentEditingDivisionID.HasValue && ValidateInputsForDivision())
            {
                try
                {
                    // Fetch the Class record to be updated
                    var Division = db.Divisions.Include(C => C.Classes).FirstOrDefault(c => c.DivisionID == currentEditingDivisionID);

                    if (Division != null)
                    {
                        Division.DivisionName = guna2TextBox1.Text;

                        var selectedClass = db.Classes.FirstOrDefault(c => c.ClassName == guna2ComboBox1.Text);
                        if (selectedClass != null)
                        {
                            Division.ClassID = selectedClass.ClassID;
                        }

                        db.SaveChanges();

                        MessageBox.Show("!تم تعديل الشبعة بنجاح"); // Show success message

                        LoadData();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show(".الشعبة غير موجودة"); // Show error message if class not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ: " + ex.Message); // Show error message in case of an exception
                }
            }
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (guna2TileButton1.Text == "تعديل")
            {
                UpdateDivision();
            }
            else
            {
                AddDivision();
            }
        }

    }
}
