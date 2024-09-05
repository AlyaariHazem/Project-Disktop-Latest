using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySchool.TOOLS_HELPER
{
    internal class tools
    {

        public void StyleDataGridView(Guna2DataGridView x, Guna2TabControl tabControl =null)
        {
            if (tabControl != null)
            {
                tabControl.RightToLeftLayout = true;
                tabControl.RightToLeft = RightToLeft.Yes;
            }
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



        public void InitializeDataGridView(Guna2DataGridView gridView, Dictionary<string, string> columns, Dictionary<string, (string HeaderText, string ButtonText)> buttonColumns = null, Dictionary<string, string> imageColumns = null)
        {
            gridView.Columns.Clear();

            // Add standard columns
            foreach (var column in columns)
            {
                gridView.Columns.Add(column.Key, column.Value);
            }


            // Add image columns if provided
            if (imageColumns != null)
            {
                foreach (var imageColumn in imageColumns)
                {
                    var imageCol = new DataGridViewImageColumn
                    {
                        Name = imageColumn.Key,
                        HeaderText = imageColumn.Value,
                        ImageLayout = DataGridViewImageCellLayout.Stretch,
                        Width = 100
                    };
                    gridView.Columns.Add(imageCol);
                }

            }
                // Add button columns if provided
                if (buttonColumns != null)
            {
                foreach (var buttonColumn in buttonColumns)
                {
                    var buttonCol = new DataGridViewButtonColumn
                    {
                        Name = buttonColumn.Key,
                        HeaderText = buttonColumn.Value.HeaderText,
                        Text = buttonColumn.Value.ButtonText,
                        UseColumnTextForButtonValue = true,
                        Width = 80
                    };
                    gridView.Columns.Add(buttonCol);
                }
            }

         
            
       
        
        }




        public void FillComboBox(ComboBox comboBox, object []items) { 
            comboBox.Items.Clear();
            comboBox.Items.AddRange(items);
        
        }

        public void LoadDataIntoDataGridView<T>( Guna2DataGridView gridView, IEnumerable<T> dataSource, Func<T, object[]> rowSelector, int rowHeight = 35)
        {
            gridView.Rows.Clear();

            foreach (var item in dataSource)
            {
                var rowData = rowSelector(item);
                var index = gridView.Rows.Add(rowData);
                gridView.Rows[index].Height = rowHeight;
            }
        }



        public void AddButtonColumn(Guna2DataGridView gridView, string name, string headerText, string buttonText)
        {
            var buttonColumn = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = headerText,
                Text = buttonText,
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            gridView.Columns.Add(buttonColumn);
        }






        public void ResetForms(Control container, string buttonResetText = "إضافة+")
        {
           
            foreach (Control control in container.Controls)
            {
              
                if (control is Guna2TextBox textBox)
                {
                    textBox.Text = "";
                }
                else if (control is Guna.UI2.WinForms.Guna2ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1; // Reset to no selection
                    comboBox.Text = string.Empty; // Clear text
                }
                //else if (control is Guna.UI2.WinForms.Guna2TileButton button)
                //{
                //    button.Text = buttonResetText; // Reset button text
                //}
                else if (control.HasChildren)
                {
                    // If the control contains other controls, reset them recursively
                    ResetForms(control, buttonResetText);
                }
            }

        }


        

        public bool ValidateInputs(Control form, List<Control> optionalControls = null, int maxInvalidControls = 1)
        {
            int invalidControlCount = 0;

            foreach (Control control in form.Controls)
            {
                // Skip optional controls
                if (optionalControls != null && optionalControls.Contains(control))
                {
                    continue;
                }

                if (control is Guna.UI2.WinForms.Guna2TextBox textBox)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        textBox.Focus();
                        textBox.BorderColor = Color.Red; // Highlight with red border
                        invalidControlCount++;
                    }
                    else
                    {
                        textBox.BorderColor = SystemColors.Window; // Reset to default border color
                    }
                }
                else if (control is Guna.UI2.WinForms.Guna2ComboBox comboBox)
                {
                    if (comboBox.SelectedIndex == -1)
                    {
                        comboBox.Focus();
                        comboBox.BorderColor = Color.Red; // Highlight with red border
                        invalidControlCount++;
                    }
                    else
                    {
                        comboBox.BorderColor = SystemColors.Window; // Reset to default border color
                    }
                }
                else if (control is Guna.UI2.WinForms.Guna2DateTimePicker dateTimePicker)
                {
                    if (dateTimePicker.Value == DateTime.MinValue)
                    {
                        dateTimePicker.Focus();
                        dateTimePicker.BorderColor = Color.Red; // Highlight with red border
                        invalidControlCount++;
                    }
                    else
                    {
                        dateTimePicker.BorderColor = SystemColors.Window; // Reset to default border color
                    }
                }
                else if (control.HasChildren)
                {
                    // If the control contains other controls, validate them recursively
                    if (!ValidateInputs(control, optionalControls, maxInvalidControls - invalidControlCount))
                    {
                        return false;
                    }
                }

                // If the number of invalid controls exceeds the maximum allowed, return false
                if (invalidControlCount >= maxInvalidControls)
                {
                    return false;
                }
            }

            // Return true if the number of invalid controls is less than the maximum allowed
            return invalidControlCount < maxInvalidControls;
        }



        public Image LoadImage(string relativeFilePath)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(basePath, relativeFilePath);

            if (File.Exists(fullPath))
            {
                return Image.FromFile(fullPath);
            }
            else
            {
                Console.WriteLine("Image not found: " + fullPath); // Debugging output
                return Properties.Resources.DefaultProfileImage;
            }
        }



     




    }
}
