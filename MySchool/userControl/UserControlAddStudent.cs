﻿using System;
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
    public partial class UserControlAddStudent : UserControl
    {
        public UserControlAddStudent()
        {
            InitializeComponent();
            guna2TabControl1.RightToLeft = RightToLeft.Yes;
            guna2TabControl1.RightToLeftLayout = true;
            guna2TabControl1.Alignment = TabAlignment.Top;
            

        }

        private void UserControlAddStudent_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}