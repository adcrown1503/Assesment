using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assesment.Models
{
    public class DbManagerFactory
    {
        string dbType;
        public DbManagerFactory(string str)
        {
            dbType = str;
        }
        public IProject GetDbManager()
        {
            IProject returnValue = null;
            if (dbType == "SQL")
            {

                returnValue = new SQLManager();
            }
            if (dbType == "Oracle")
            {
                returnValue = new OracleManager();
            }
            return returnValue;
        }
    }
}