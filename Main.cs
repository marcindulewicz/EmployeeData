﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeData
{
    public partial class Main : Form

    {
        private FileHelper<List<Employee>> fileHelper = new FileHelper<List<Employee>>(Program._filePath);
        public Main()
        {
            InitializeComponent();
            RefreshEmployeeData();
            comboBoxFilter.DataSource = Program.filterList;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var addEditEmployee = new AddEditEmployee();
            addEditEmployee.FormClosing += AddEditEmployee_FormClosing;
            addEditEmployee.ShowDialog();
        }
        private void AddEditEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshEmployeeData();
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {

            var addEditEmployee = new AddEditEmployee(Convert.ToInt32(dgvEmploee.SelectedRows[0].Cells[0].Value));
            addEditEmployee.FormClosing += AddEditEmployee_FormClosing;
            addEditEmployee.ShowDialog();
        }
        private void BtnFire_Click(object sender, EventArgs e)
        {
            RefreshEmployeeData();
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshEmployeeData();
        }
        private void RefreshEmployeeData()
        {
            var employee = fileHelper.DeserializeFromFile().OrderBy(x => x.Id).ToList();

            if (comboBoxFilter.SelectedIndex == 1)
                employee = employee.FindAll(x => x.IsFired == false);
            else if (comboBoxFilter.SelectedIndex == 2)
                employee = employee.FindAll(x => x.IsFired == true);

            dgvEmploee.DataSource = employee;
        }

    }
}
