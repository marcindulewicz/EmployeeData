using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeData
{
    public class Employee
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public decimal FolderNumber { get; set; }
        public DateTime DateOfHire { get; set; }
        public DateTime DateOfFire { get; set; }
        public bool IsFired { get; set; }
        public string Comments { get; set; }

    }
}
