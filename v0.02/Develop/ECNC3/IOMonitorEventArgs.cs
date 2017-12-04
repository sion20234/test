using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Views
{
    /// <summary>
    /// 状態監視処理のイベントハンドラ
    /// </summary>
    /// <param name="e"></param>
    public delegate void IOMonitoringEventHandler(IOMonitorEventArgs e);
    public class IOMonitorEventArgs : EventArgs
    {
        private Models.StructureIODataList items = new Models.StructureIODataList();
        public IOMonitorEventArgs(Models.StructureIODataList IOData)
        {
            items = IOData;
        }
        public Models.StructureIODataList Items
        {
            get
            {
                return items;
            }
        }
    }
}
