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
            
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var addEditEmployee = new AddEditEmployee();
            addEditEmployee.ShowDialog();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var addEditEmployee = new AddEditEmployee();
            addEditEmployee.ShowDialog();
        }

        private void BtnFire_Click(object sender, EventArgs e)
        {

        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {

        }
        private void RefreshEmployeeData(string filter)
        {
            var employee = fileHelper.DeserializeFromFile();
            employee = employee.OrderBy(x => x.Id).ToList();

            if (comboBoxFilter.SelectedItem.ToString() != Program.NoFilterString)
                employee = employee;  //.FindAll(x => x.IsFired).ToList();


            dgvEmploee.DataSource = employee;




        }
    }
}
