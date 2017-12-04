///////////////////////////////////////////////////////////////////////////////////////////
//
// (1) システム名称 : ELENIX ECNCⅢ
// (2) ファイル名   : TimerView.cs
// (3) 概要         : タイマー設定クラス
// (4) 作成日       : 2016.07.19
// (5) 作成者       : 八野
// (6) 特記事項     : VisualStudio2015 C#
// (7) 更新履歴     :
//
///////////////////////////////////////////////////////////////////////////////////////////

using ECNC3.Enumeration;
using ECNC3.Models;
using System;
using System.Collections.Generic;
using System.Timers;

namespace ECNC3.Views
{
    /// <summary>
    /// タイマー処理クラス
    /// </summary>
    /// <remarks>インスタンス化時に引数なしでタイマースタート</remarks>
    public class TimerView
    {
        #region Constractor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>タイマーの初期化と開始処理</remarks>
        public TimerView()
        {
            using (FileMaintenanceTimer fileTimer = new FileMaintenanceTimer())
            {
                fileTimer.Read();
                _mainPowerSupplyOnTime = fileTimer.Items.MainPowerCount;
                _processingPowerOnTime = fileTimer.Items.ProcessPowerCount;
            }
            setTimer();
            onStart();
        }
        /// <summary>
        /// タイマー間隔指定のコンストラクタ
        /// </summary>
        /// <param name="Interval">処理間隔(単位：ﾐﾘ秒)</param>
        /// <remarks>タイマーの初期化(間隔指定)と開始処理</remarks>
        public TimerView(int Interval)
        {
            setTimer(Interval);
            onStart();
        }
        #endregion
        #region VariableMember
        /// <summary>
        /// タイマークラスの宣言
        /// </summary>
        private System.Timers.Timer timer = null;
        private static ulong _mainPowerSupplyOnTime = 0;
        private static bool _mainPowerSupplyOnFlag = false;
        private static ulong _processingPowerOnTime = 0;
        private static bool _processingPowerOnFlag = false;
        private static ulong _manualMode_programProcessingTime = 0;
        private static bool _manualMode_programProcessingFlag = false;
        private static ulong _manualMode_dischargeOnTime = 0;
        private static bool _manualMode_dischargeOnFlag = false;
        private static double _manualMode_oneProcessingTime = 0;
        private static bool _manualMode_oneProcessingFlag = false;
        private static ulong _autoMode_programProcessingTime = 0;
        private static bool _autoMode_programProcessingFlag = false;
        private static ulong _autoMode_dischargeOnTime = 0;
        private static bool _autoMode_dischargeOnFlag = false;
        private static double _autoMode_oneProcessingTime = 0;
        private static bool _autoMode_oneProcessingFlag = false;
        private short _movingUpCount = 0;
        #endregion
        #region PrivateMethod
        /// <summary>
        /// タイマーを設定するためのメソッド（初期処理などで呼ぶ）
        /// </summary>
        /// <remarks>引数指定で処理間隔指定処理。引数なしで1ミリ秒固定。</remarks>
        private void setTimer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 100;                                                                  // 上のIntervalで指定した処理を行う間隔を指定します。(値はﾐﾘ秒)
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(onTimer);
        }
        /// <summary>
        /// 処理間隔指定でタイマーを設定するためのメソッド（初期処理などで呼ぶ）
        /// </summary>
        /// <param name="Interval">処理間隔(単位：ﾐﾘ秒)</param>
        /// <remarks>引数指定で処理間隔指定処理。引数なしで1ミリ秒固定。</remarks>
        private void setTimer(int Interval)
        {
            timer = new Timer();
            timer.Interval = Interval;                                                              // 上のIntervalで指定した処理を行う間隔を指定します。(値はﾐﾘ秒)
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(onTimer);
        }
        /// <summary>
        /// インターバルごとの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onTimer(object sender, ElapsedEventArgs e)
        {
            //最小単位は1秒
            if(_movingUpCount >= 10)
            {   //主電源ON時間
                if (_mainPowerSupplyOnFlag == true) _mainPowerSupplyOnTime++;
                //加工電源ON時間
                if (_processingPowerOnFlag == true) _processingPowerOnTime++;
                //プログラム実行時間
                if (_manualMode_programProcessingFlag == true) _manualMode_programProcessingTime++;
                if (_autoMode_programProcessingFlag == true) _autoMode_programProcessingTime++;
                //加工時間
                if (_manualMode_dischargeOnFlag == true) _manualMode_dischargeOnTime++;
                if (_autoMode_dischargeOnFlag == true) _autoMode_dischargeOnTime++;
                _movingUpCount = 0;
            }
            //最小単位が0.1秒
            //1穴加工時間
            if (_manualMode_oneProcessingFlag == true) _manualMode_oneProcessingTime += 0.1;
            if (_autoMode_oneProcessingFlag == true) _autoMode_oneProcessingTime += 0.1;
            _movingUpCount++;

        }
        private static ulong TimeToSec(ulong Hour, ulong Min, ulong Sec)
        {
            return (Hour * 3600) + (Min * 60) + Sec;
        }
        private static void SecToTime(ulong timecount, out ulong Hour, out ulong Min, out ulong Sec)
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
        public static void MainPowerTimerStart()
        {
            if (!_mainPowerSupplyOnFlag) _mainPowerSupplyOnFlag = true;
        }
        /// <summary>
        /// 主電源ON時間タイマー終了
        /// </summary>
        public static void MainPowerTimerStop()
        {
            if (_mainPowerSupplyOnFlag) _mainPowerSupplyOnFlag = false;
        }
        /// <summary>
        /// 時分秒で主電源ON時間タイマーをよみとる。
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public static void GetMainPowerTime(out ulong hour, out ulong minute, out ulong second )
        {
            SecToTime(_mainPowerSupplyOnTime, out hour, out minute, out second);
        }
        public static ulong GetMainPowerTime()
        {
            return _mainPowerSupplyOnTime;
        }
        /// <summary>
        /// 主電源ON時間タイマーをリセットする。
        /// </summary>
        public static void MainPowerTimeReset()
        {
            _mainPowerSupplyOnTime = 0;
        }
        #endregion
        #region ProcessingPowerTimer
        /// <summary>
        /// 加工電源ON時間タイマー開始
        /// </summary>
        public static void ProcessingPowerTimerStart()
        {
            if (!_processingPowerOnFlag) _processingPowerOnFlag = true;
        }
        /// <summary>
        /// 加工電源ON時間タイマー終了
        /// </summary>
        public static void ProcessingPowerTimerStop()
        {
            if (_processingPowerOnFlag) _processingPowerOnFlag = false;
        }
        /// <summary>
        /// 時分秒で加工電源ON時間タイマーをよみとる。
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public static void GetProcessingPowerTime(out ulong hour, out ulong minute, out ulong second)
        {
            SecToTime(_processingPowerOnTime, out hour, out minute, out second);
        }
        public static ulong GetProcessingPowerTime()
        {
            return _processingPowerOnTime;
        }
        /// <summary>
        /// 加工電源ON時間タイマーをリセットする。
        /// </summary>
        public static void ProcessingPowerTimeReset()
        {
            _processingPowerOnTime = 0;
        }
        #endregion
        #region ProgramProcessingTimer
        /// <summary>
        /// プログラム運転時間タイマー開始
        /// </summary>
        public static void ProgramProcessingTimerStart(McTaskModes procMode)
        {
            switch(procMode)
            {
                case McTaskModes.Manual:
                    if (!_manualMode_programProcessingFlag) _manualMode_programProcessingFlag = true;
                    break;

                case McTaskModes.Auto:
                    if (!_autoMode_programProcessingFlag) _autoMode_programProcessingFlag = true;
                    break;

            }
        }
        /// <summary>
        /// プログラム運転時間タイマー終了
        /// </summary>
        public static void ProgramProcessingTimerStop(McTaskModes procMode)
        {
            switch (procMode)
            {
                case McTaskModes.Manual:
                    if (_manualMode_programProcessingFlag) _manualMode_programProcessingFlag = false;
                    break;

                case McTaskModes.Auto:
                    if (!_autoMode_programProcessingFlag) _autoMode_programProcessingFlag = false;
                    break;

            }
        }
        /// <summary>
        /// 時分秒でプログラム運転時間タイマーをよみとる。
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public static void GetProgramProcessingTime(out ulong hour, out ulong minute, out ulong second, McTaskModes procMode)
        {
            hour = minute = second = 0;
            switch(procMode)
            {
                case McTaskModes.Manual:
                    SecToTime(_manualMode_programProcessingTime, out hour, out minute, out second);
                    break;

                case McTaskModes.Auto:
                    SecToTime(_autoMode_programProcessingTime, out hour, out minute, out second);
                    break;

            }
        }
        public static ulong GetProgramProcessingTime(McTaskModes procMode)
        {
            switch (procMode)
            {
                case McTaskModes.Manual:
                    return _manualMode_programProcessingTime;

                case McTaskModes.Auto:
                    return _autoMode_programProcessingTime;

            }
            return 0;
        }
        /// <summary>
        /// プログラム運転時間タイマーをリセットする。
        /// </summary>
        public static void ProgramProcessingTimeReset(McTaskModes procMode)
        {
            switch(procMode)
            {
                case McTaskModes.Manual:
                    _manualMode_programProcessingTime = 0;
                    break;

                case McTaskModes.Auto:
                    _manualMode_programProcessingTime = 0;
                    break;

            }
        }
        #endregion
        #region DischargeTimer
        /// <summary>
        /// 加工時間タイマー開始
        /// </summary>
        public static void DischargeTimerStart(McTaskModes procMode)
        {
            switch(procMode)
            {
                case McTaskModes.Manual:
                    if (!_manualMode_dischargeOnFlag) _manualMode_dischargeOnFlag = true;
                    break;

                case McTaskModes.Auto:
                    if (!_autoMode_dischargeOnFlag) _autoMode_dischargeOnFlag = true;
                    break;

            }
        }
        /// <summary>
        /// 加工時間タイマー終了
        /// </summary>
        public static void DischargeTimerStop(McTaskModes procMode)
        {
            switch(procMode)
            {
                case McTaskModes.Manual:
                    if (_manualMode_dischargeOnFlag) _manualMode_dischargeOnFlag = false;
                    break;

                case McTaskModes.Auto:
                    if (_autoMode_dischargeOnFlag) _autoMode_dischargeOnFlag = false;
                    break;

            }
        }
        /// <summary>
        /// 時分秒で加工時間タイマーをよみとる。
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public static void GetDischargeTime(out ulong hour, out ulong minute, out ulong second, McTaskModes procMode)
        {
            hour = minute = second = 0;
            switch (procMode)
            {
                case McTaskModes.Manual:
                    SecToTime(_manualMode_dischargeOnTime, out hour, out minute, out second);
                    break;

                case McTaskModes.Auto:
                    SecToTime(_autoMode_dischargeOnTime, out hour, out minute, out second);
                    break;

            }
        }
        public static ulong GetDischargeTime(McTaskModes procMode)
        {
            switch(procMode)
            {
                case McTaskModes.Manual:
                    return _manualMode_dischargeOnTime;

                case McTaskModes.Auto:
                    return _autoMode_dischargeOnTime;

                default: return 0;
            }
        }
        /// <summary>
        /// 加工時間タイマーをリセットする。
        /// </summary>
        public static void DischargeTimeReset(McTaskModes procMode)
        {
            switch(procMode)
            {
                case McTaskModes.Manual:
                    _manualMode_dischargeOnTime = 0;
                    break;

                case McTaskModes.Auto:
                    _autoMode_dischargeOnTime = 0;
                    break;

            }
        }
        #endregion
        #region OneProcessingTimer
        /// <summary>
        /// 1穴加工時間タイマー開始
        /// </summary>
        public static void OneProcessingTimerStart(McTaskModes procMode)
        {
            switch(procMode)
            {
                case McTaskModes.Manual:
                    if (!_manualMode_oneProcessingFlag) _manualMode_oneProcessingFlag = true;
                    break;

                case McTaskModes.Auto:
                    if (!_autoMode_oneProcessingFlag) _autoMode_oneProcessingFlag = true;
                    break;

            }
        }
        /// <summary>
        /// 1穴加工時間タイマー終了
        /// </summary>
        public static void OneProcessingTimerStop(McTaskModes procMode)
        {
            switch(procMode)
            {
                case McTaskModes.Manual:
                    if (_manualMode_oneProcessingFlag) _manualMode_oneProcessingFlag = false;
                    break;

                case McTaskModes.Auto:
                    if (_autoMode_oneProcessingFlag) _autoMode_oneProcessingFlag = false;
                    break;
            }
        }
        /// <summary>
        /// 秒で1穴加工時間タイマーをよみとる。
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public static double GetOneProcessingTime(McTaskModes procMode)
        {
            switch (procMode)
            {
                case McTaskModes.Manual:
                    return _manualMode_oneProcessingTime;

                case McTaskModes.Auto:
                    return _autoMode_oneProcessingTime;

                default: return 0;
            }
        }
        /// <summary>
        /// 1穴加工時間タイマーをリセットする。
        /// </summary>
        public static void OneProcessingTimeReset(McTaskModes procMode)
        {
            switch (procMode)
            {
                case McTaskModes.Manual:
                    _manualMode_oneProcessingTime = 0;
                    break;

                case McTaskModes.Auto:
                    _autoMode_oneProcessingTime = 0;
                    break;

            } 
        }
        #endregion
        public static void SaveTimer()
        {
            using (FileMaintenanceTimer fileTimer = new FileMaintenanceTimer())
            {
                fileTimer.Read();
                fileTimer.Items.MainPowerCount = _mainPowerSupplyOnTime;
                fileTimer.Items.ProcessPowerCount = _processingPowerOnTime;
                fileTimer.Write();
            }
        }
        #endregion
    }
}
