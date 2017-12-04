///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : MaintenanceTimerView.cs
// (3) 概要         : タイマー設定クラス
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using ECNC3.Enumeration;
using System;
using System.Collections.Generic;
using System.Timers;
using ECNC3.Models;

namespace ECNC3.Views
{
    /// <summary>
    /// タイマー処理クラス
    /// </summary>
    /// <remarks>インスタンス化時に引数なしでタイマースタート</remarks>
    public class MaintenanceTimerView
    {
        #region Constractor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>タイマーの初期化と開始処理</remarks>
        public MaintenanceTimerView()
        {
            _maintTimerList.Read();
            setTimer();
            onStart();
        }
        #endregion
        #region VariableMember
        /// <summary>
        /// タイマークラスの宣言
        /// </summary>
        private System.Timers.Timer timer = null;
        private FileMaintenanceTimer _maintTimerList = new FileMaintenanceTimer();
        public bool HasWarning = false;
        #endregion
        #region PrivateMethod
        /// <summary>
        /// タイマーを設定するためのメソッド（初期処理などで呼ぶ）
        /// </summary>
        /// <remarks>引数指定で処理間隔指定処理。引数なしで1ミリ秒固定。</remarks>
        private void setTimer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;                                                                  // 上のIntervalで指定した処理を行う間隔を指定します。(値はﾐﾘ秒)
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(onTimer);
        }
        /// <summary>
        /// インターバルごとの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onTimer(object sender, ElapsedEventArgs e)
        {
            if(_maintTimerList != null 
                && _maintTimerList.Items.Count > 0)
            {
                bool IsWarning = false;
                foreach(StructureMaintenanceTimerItem item in _maintTimerList.Items)
                {
                    switch(item.Category)
                    {
                        case MaintenanceTimerCategory.NULL: continue;
                        case MaintenanceTimerCategory.DateTime:
                            if (((int)(item.Value)) >= ((int)(item.Limit)))
                            {
                                IsWarning = true;
                            }
                            break;

                        case MaintenanceTimerCategory.PowerON:
                        case MaintenanceTimerCategory.DischargeON:
                            if (item.CountFlag == true) item.Value++;
                            if (((int)(item.Value)) >= ((int)(item.Limit * 3600)))
                            {
                                IsWarning = true;
                            }
                            break;
                    }
                   
                }
                if (IsWarning != HasWarning) HasWarning = IsWarning;
            }
        }
        private long TimeToSec(long Hour, long Min, long Sec)
        {
            return (Hour * 3600) + (Min * 60) + Sec;
        }
        private void SecToTime(long timecount, out long Hour, out long Min, out long Sec)
        {
            if (timecount >= 3600){
                Hour = timecount / 3600;
            }
            else
            {
                Hour = 0;
            }

            if (timecount % 3600 >= 60)
            {
                Min = (timecount % 3600) / 60;
            }
            else
            {
                Min = 0;
            }

            if ((timecount % 3600) % 60 > 0)
            {
                Sec = (timecount % 3600) % 60;
            }
            else
            {
                Sec = 0;
            }
        }
        #endregion
        #region PublicMethods
        public ResultCodes SaveTimer()
        {
            return _maintTimerList.Write();
        }
        /// <summary>
        /// タイマーの開始処理。
        /// </summary>
        internal void onStart()
        {
            timer.Start();
        }
        /// <summary>
        /// タイマーの停止処理。
        /// </summary>
        internal void onStop()
        {
            timer.Stop();
        }
        #region MainPowerTimer
        /// <summary>
        /// 主電源ON時間タイマー開始
        /// </summary>
        public void MainPowerTimerStart()
        {
            foreach(StructureMaintenanceTimerItem item in _maintTimerList.Items)
            {
                if (item.Category != MaintenanceTimerCategory.PowerON) continue;
                if (!item.CountFlag) item.CountFlag = true;
            }
        }
        /// <summary>
        /// 主電源ON時間タイマー終了
        /// </summary>
        public void MainPowerTimerStop()
        {
            foreach (StructureMaintenanceTimerItem item in _maintTimerList.Items)
            {
                if (item.Category != MaintenanceTimerCategory.PowerON) continue;
                if (item.CountFlag) item.CountFlag = false;
            }
        }
        #endregion
        #region DischargeTimer
        /// <summary>
        /// 加工時間タイマー開始
        /// </summary>
        public void DischargeTimerStart()
        {
            foreach (StructureMaintenanceTimerItem item in _maintTimerList.Items)
            {
                if (item.Category != MaintenanceTimerCategory.DischargeON) continue;
                if (!item.CountFlag) item.CountFlag = true;
            }
        }
        /// <summary>
        /// 加工時間タイマー終了
        /// </summary>
        public void DischargeTimerStop()
        {
            foreach (StructureMaintenanceTimerItem item in _maintTimerList.Items)
            {
                if (item.Category != MaintenanceTimerCategory.DischargeON) continue;
                if (item.CountFlag) item.CountFlag = false;
            }
        }
        #endregion
        public ResultCodes ReadTimer()
        {
            return _maintTimerList.Read();
        }
        /// <summary>
        /// 行追加
        /// </summary>
        /// <param name="index"></param>
        /// <param name="addItem"></param>
        /// <returns></returns>
        public ResultCodes TimerAdd(int index, StructureMaintenanceTimerItem addItem)
        {
            if (_maintTimerList.Items.Count <= index) return ResultCodes.InvalidArgument;
            if(addItem != null)
            {
                addItem.Number = index + 1;
                _maintTimerList.Items.Insert(index, (StructureMaintenanceTimerItem)addItem.Clone());
            }
            else
            {
                _maintTimerList.Items.Insert(index, new StructureMaintenanceTimerItem(index + 1));
            }
            
            return ResultCodes.Success;
        }
        /// <summary>
        /// 行削除
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ResultCodes TimerRemove(int index)
        {
            if (_maintTimerList.Items.Count <= index) return ResultCodes.InvalidArgument;
            _maintTimerList.Items.RemoveAt(index);
            return ResultCodes.Success;

        }
        /// <summary>
        /// リスト取得
        /// </summary>
        /// <returns></returns>
        public StructureMaintenanceTimerList GetTimerList()
        {
            return (StructureMaintenanceTimerList)_maintTimerList.Items.Clone();
        }
        /// <summary>
        /// リスト設定
        /// </summary>
        /// <param name="index"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public ResultCodes SetTimerList(int index, StructureMaintenanceTimerItem source)
        {
            if (source == null) return ResultCodes.InvalidArgument;
            source.Number = index + 1;
            if (_maintTimerList.Items.Count <= index || source == null) return ResultCodes.InvalidArgument;
            _maintTimerList.Items[index] = (StructureMaintenanceTimerItem)source.Clone();
            return ResultCodes.Success;
        }
        #endregion
    }
}
