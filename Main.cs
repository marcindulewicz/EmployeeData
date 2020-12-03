using System;
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
            SetHeadersToDataGridEmployee();
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
            WhenNoSelectionRow();
            var addEditEmployee = new AddEditEmployee(Convert.ToInt32(dgvEmploee.SelectedRows[0].Cells[0].Value));
            addEditEmployee.FormClosing += AddEditEmployee_FormClosing;
            addEditEmployee.ShowDialog();
        }
        private void BtnFire_Click(object sender, EventArgs e)
        {
            WhenNoSelectionRow();


            var fireEmployeeConfirmationDialog   =MessageBox.Show("Czy na pewno chcesz zwolnić pracownika?","Zwolnienie pracownika",MessageBoxButtons.YesNo);

            if (fireEmployeeConfirmationDialog == DialogResult.Yes)
            {
                FireEmployee();
                RefreshEmployeeData();
            }
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
        private void SetHeadersToDataGridEmployee()
        {
            dgvEmploee.Columns[0].HeaderText = "ID";
            dgvEmploee.Columns[1].HeaderText = "Imie";
            dgvEmploee.Columns[2].HeaderText = "Nazwisko";
            dgvEmploee.Columns[3].HeaderText = "Stanowisko";
            dgvEmploee.Columns[4].HeaderText = "E-mail";
            dgvEmploee.Columns[5].HeaderText = "Data zatrudnienia";
            dgvEmploee.Columns[6].HeaderText = "Data zwolnienia";
            dgvEmploee.Columns[7].HeaderText = "Zwolniony";

        }
        private void WhenNoSelectionRow()
        {
            if (dgvEmploee.SelectedRows.Count == 0)
                throw new Exception("Proszę wskaż dane w tabeli");
        }
        private void FireEmployee()
        {
            var employee = (Employee)dgvEmploee.SelectedRows[0].DataBoundItem;
            var employees = fileHelper.DeserializeFromFile();
            employees.Remove(employees.Find(x=>x.Id ==employee.Id));

            employee.DateOfFire = DateTime.Now;
            employee.IsFired = true;

            employees.Add(employee);
            fileHelper.SerializeToFile(employees);


        }
    }
}
