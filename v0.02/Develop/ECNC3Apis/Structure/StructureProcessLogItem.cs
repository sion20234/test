using ECNC3.Enumeration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECNC3.Models
{
    /// <summary>ユーザ設定</summary>
    public class StructureProcessLogItem : IDisposable, ICloneable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>コンストラクタ</summary>
        public StructureProcessLogItem()
        {
            
        }
        public string OutPutPath { get; set; }
        /// <summary>ログ取得開始日時</summary>
        public string StartTimeStamp { get; set; }
        /// <summary>ログ取得開始日時</summary>
        public string EndTimeStamp { get; set; }
        /// <summary>
        /// 2.ログタイトル
        /// </summary>
        public string OutLogTitle { get; set; }

        //機械座標
        public decimal dmAxisX { get; set; }
        public decimal dmAxisY { get; set; }
        public decimal dmAxisW { get; set; }
        public decimal dmAxisZ { get; set; }
        public decimal dmAxisA { get; set; }
        public decimal dmAxisB { get; set; }
        public decimal dmAxisC { get; set; }
        //ワーク座標          
        public decimal dwAxisX { get; set; }
        public decimal dwAxisY { get; set; }
        public decimal dwAxisW { get; set; }
        public decimal dwAxisZ { get; set; }
        public decimal dwAxisA { get; set; }
        public decimal dwAxisB { get; set; }
        public decimal dwAxisC { get; set; }
        /// <summary>
        /// サンプリング周期
        /// </summary>
        public int SamplingInterval { get; set; }
        /// <summary>インスタンスの破棄</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);    //  ファイナライザによるDispose()呼び出しの抑制。
        }
        /// <summary>インスタンスの破棄</summary>
        /// <param name="disposing">呼び出し元の判別
        ///     <list type="bullet" >
        ///         <item>true=Dispose()関数からの呼び出し。</item>
        ///         <item>false=ファイナライザによる呼び出し。</item>
        ///     </list>
        /// </param>
        private void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    {
                        //  マネージリソースの解放
                    }
                    //  アンマネージリソースの解放
                }
                _disposed = true;
            }
            finally
            {
                ;   //  基底クラスのDispose()を確実に呼び出す。
                    //base.Dispose( disposing );
            }
        }
      
        /// <summary>クローン</summary>
        /// <returns>クローンされたインスタンス</returns>
        public object Clone()
        {
            StructureProcessLogItem temp = new StructureProcessLogItem();
            temp.OutPutPath = OutPutPath;
            temp.StartTimeStamp = StartTimeStamp;
            temp.EndTimeStamp = EndTimeStamp;
            temp.OutLogTitle = OutLogTitle;
            temp.dmAxisX = dmAxisX;
            temp.dmAxisY = dmAxisY;
            temp.dmAxisW = dmAxisW;
            temp.dmAxisZ = dmAxisZ;
            temp.dmAxisA = dmAxisA;
            temp.dmAxisB = dmAxisB;
            temp.dmAxisC = dmAxisC;
            temp.dwAxisX = dwAxisX;
            temp.dwAxisY = dwAxisY;
            temp.dwAxisW = dwAxisW;
            temp.dwAxisZ = dwAxisZ;
            temp.dwAxisA = dwAxisA;
            temp.dwAxisB = dwAxisB;
            temp.dwAxisC = dwAxisC;
            return temp;   
        }
    }
}
