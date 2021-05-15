using System;
using System.Collections.Generic;
using System.Text;
using NTT_Assessment.Model;

namespace NTT_Assessment.Repository
{
    public abstract class AbstractDataRepository : IDataRepository
    {
        public abstract StudentInfo GetStudentInfo();
    }
}
