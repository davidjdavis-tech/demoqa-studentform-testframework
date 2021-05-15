using System;
using System.Collections.Generic;
using System.Text;

namespace NTT_Assessment.Model
{
    public class StudentInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Subjects { get; set; }
        public string Hobbies { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PictureUri { get; set; }
        public string Address { get; set; }

        public StudentInfo(string csvLine)
        {
            var values = csvLine.Split(",");
            FirstName = values[0].Trim();
            LastName = values[1].Trim();
            Email = values[2].Trim();
            Gender = values[3].Trim();
            MobileNumber = values[4].Trim();
            DateOfBirth = values[5].Trim();
            Subjects = values[6].Trim();
            Hobbies = values[7].Trim();
            State = values[8].Trim();
            City = values[9].Trim();
            PictureUri = values[10].Trim();
            Address = values[11].Trim();
        }

        public StudentInfo() {}
    }

    
}
