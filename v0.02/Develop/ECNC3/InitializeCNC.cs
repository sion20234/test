using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECNC3.Models.McIf;
using System.Windows.Forms;
using ECNC3.Enumeration;
using ECNC3.Models.Common;

namespace ECNC3.Views
{
    class InitializeCNC
    {
        internal InitializeCNC()
        {
            Initialize();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        public McCommProc _mcIf = null;

        private void Initialize()
        {
            
            if (null == _mcIf)
            {
                _mcIf = new McCommProc();
            }
            _mcIf.Initialize();
            //	初期化が完了したので設定可能なステータスを反映する。

            using (McDatStatus mc = new McDatStatus())
            {
                mc.Read();
            }
            //	有効軸設定
            using (McDatRomSwitch mc = new McDatRomSwitch())
            {
                mc.Read();

            }
            
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                if (null != _mcIf)
                {
                    _mcIf.Dispose();
                    _mcIf = null;
                }
            }
            finally
            {
                ;
            }
        }
    }
}
