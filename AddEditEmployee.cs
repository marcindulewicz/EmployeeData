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
    public partial class AddEditEmployee : Form
    {
        private FileHelper<List<Employee>> fileHelper = new FileHelper<List<Employee>>(Program._filePath);
        private int _employeeID;
        private List<Employee> employees;

        public AddEditEmployee(int employeeID = 0)
        {
            InitializeComponent();
            _employeeID = employeeID;
            dtpEmployeeFire.Value = DateTimePicker.MinimumDateTime;
            employees = fileHelper.DeserializeFromFile();
            if (_employeeID != 0)
                RemoveEmployeeFromList();
            else
                SetHighestID();
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {          
            var employee = CreateNewEmployee();
            employees.Add(employee);
            fileHelper.SerializeToFile(employees);
            Close();
        }
        private void SetHighestID()
        {
            var employeeWithHighestId = employees.OrderByDescending(x => x.Id).FirstOrDefault();
            _employeeID = employeeWithHighestId == null ? 1 : employeeWithHighestId.Id + 1;
        }
        private void RemoveEmployeeFromList()
        {
            var employeeToRemove = (Employee)employees.Find(x => x.Id == _employeeID);
            Text = "Edytowanie danych pracownika";
            FillEmployeeData(employeeToRemove);
            employees.Remove(employeeToRemove);
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FillEmployeeData(Employee employee)
        {
            tbId.Text = _employeeID.ToString();
            tbName.Text = employee.FirstName;
            tbLastName.Text = employee.LastName;
            tbPosition.Text = employee.Position;
            tbEmail.Text = employee.Email;
            dtpEmployeeHire.Value = employee.DateOfHire;
            dtpEmployeeFire.Value = employee.DateOfFire;

        }
        private Employee CreateNewEmployee()
        {
            var employee = new Employee()
            {
                Id = _employeeID,
                FirstName = tbName.Text,
                LastName = tbLastName.Text,
                Position = tbPosition.Text,
                Email = tbEmail.Text,
                DateOfHire = dtpEmployeeHire.Value,
                DateOfFire = dtpEmployeeFire.Value < dtpEmployeeHire.Value ? DateTimePicker.MinimumDateTime : dtpEmployeeFire.Value,
                IsFired = dtpEmployeeFire.Value == DateTimePicker.MinimumDateTime ? false : true
            };

            return employee;
        }

    }
}
