using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NTT_Assessment.Model;

namespace NTT_Assessment.Repository
{
    public class CsvDataRepository : AbstractDataRepository
    {
        public override StudentInfo GetStudentInfo()
        {
            var path = "Data\\CSV\\Student.csv";
            return GetStudentInfo(path);
        }

        public StudentInfo GetStudentInfo(string csvPath)
        {
            var fullyQualifiedPath = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), csvPath);
            var lines = File.ReadAllLines(fullyQualifiedPath);
            var values = lines[1].Split(",");

            var si = new StudentInfo
            {

                FirstName = values[0].Trim(),
                LastName = values[1].Trim(),
                Email = values[2].Trim(),
                Gender = values[3].Trim(),
                MobileNumber = values[4].Trim(),
                DateOfBirth = values[5].Trim(),
                Subjects = values[6].Trim(),
                Hobbies = values[7].Trim(),
                State = values[8].Trim(),
                City = values[9].Trim(),
                PictureUri = values[10].Trim(),
                Address = values[11].Trim()
            };

            return si;
        }


    }
}
