using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace EmployeeData
{
    public class FileHelper<T> where T: new()
    {
        private static string _filePath = Program._filePath;

        public static void SerializeToFile(T employee)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, employee);
                streamWriter.Close();
            }
        }

        public static T DeserializeFromFile()
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var streamReader = new StreamReader(_filePath))
            {
                var employee = (T) serializer.Deserialize(streamReader);
                streamReader.Close();
                return employee;
            }
        }
    }
}
