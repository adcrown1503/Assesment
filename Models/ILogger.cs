using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assesment.Models
{
    interface ILogger
    {
        void Debug(string msg, string arg = null);
        void Info(string msg, string arg = null);
        void Error(string msg, string arg = null);
        void Warning(string msg, string arg = null);

    }
}
