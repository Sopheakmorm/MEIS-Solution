using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEIS.Models;

namespace MEIS.Patterns
{
    public class SingletonObject
    {
        private static Models.dbMEIS context;

        public static dbMEIS Context()
        {
            return context ?? new dbMEIS();
        }
    }
}