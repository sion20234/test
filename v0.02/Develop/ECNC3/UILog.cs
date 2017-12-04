using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Views
{
    public class UILog : ECNC3.Models.Common.AidLog
    {
        public UILog(string functionName)
        {
            FunctionName = "UI," + functionName;
        }
    }
}
