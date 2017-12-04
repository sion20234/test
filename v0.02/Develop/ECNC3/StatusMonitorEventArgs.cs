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
    public delegate void StatusMonitoringEventHandler(StatusMonitorEventArgs e);
    public class StatusMonitorEventArgs : EventArgs
    {
        private MonitoringItems items = new MonitoringItems();
        public StatusMonitorEventArgs(MonitoringItems param)
        {
            items = param;
        }
        public MonitoringItems Items
        {
            get
            {
                return items;
            }
        }
    }
}
