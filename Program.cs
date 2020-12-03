using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeData
{
    



    static class Program
    {
        public static string _filePath = Path.Combine(Environment.CurrentDirectory, "employee.txt");
        public static string NoFilterString = "Wszyscy pracownicy";
        public static List<string> filterList = new List<string>() { NoFilterString, "Pracujący", "Zwolnieni" };
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
