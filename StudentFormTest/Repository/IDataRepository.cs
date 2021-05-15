using System;
using System.Collections.Generic;
using System.Text;
using NTT_Assessment.Model;

namespace NTT_Assessment.Repository
{
    public interface IDataRepository
    {
        StudentInfo GetStudentInfo();
    }
}
