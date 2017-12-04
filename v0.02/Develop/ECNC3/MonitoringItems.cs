using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECNC3.Models.McIf;

namespace ECNC3.Views
{
    public class MonitoringItems
    {
        /// <summary>
        /// シャットダウン信号
        /// </summary>
        internal bool ShutdownSygnal = false;
        /// <summary>
        /// 放電加工目標加工量
        /// </summary>
        internal int PrcsTargetDist { get; set; }
        /// <summary>
        /// 放電加工現在加工量
        /// </summary>
        internal int PrcsNowDist { get; set; }
        /// <summary>
        /// ガイド貫通動作許可コマンド
        /// </summary>
        internal bool GuideThroughEnable { get; set; }
        /// <summary>
        /// メンテナンス要フラグ
        /// </summary>
        internal bool HasMaint { get; set; }
        /// <summary>
        /// 実行中G/Mコード番号
        /// </summary>
        internal int GMNumber { get; set; }
        /// <summary>
        /// 実行中G/Mコードフラグ
        /// </summary>
        internal int GMCodeFlg { get; set; }
        /// <summary>
        /// 運転時間タイマー
        /// </summary>
        internal int MachineTimer { get; set; }
        /// <summary>
        /// 加工時間タイマー
        /// </summary>
        internal int DisChgTimer { get; set; }
        /// <summary>
        /// 1穴加工時間タイマー
        /// </summary>
        internal int ProcTimer { get; set; }
        /// <summary>
        /// 加工時間タイマーのミリ秒
        /// </summary>
        internal string ProcTimermSec { get; set; }
        /// <summary>
        /// スタートボタン（ハードスイッチ）のオンエッジONフラグ
        /// </summary>
        public bool StartSwBtnOnEdge { get; set; }
        /// <summary>
        /// スタートボタン（ハードスイッチ）のオフエッジOFFフラグ
        /// </summary>
        public bool StartSwBtnOffEdge { get; set; }
        /// <summary>
        /// 現在の機械座標
        /// </summary>
        public Models.StructureAxisCoordinate MacAxisPos = new Models.StructureAxisCoordinate();
        /// <summary>
        /// 現在のワーク座標
        /// </summary>
        public Models.StructureAxisCoordinate WorkAxisPos = new Models.StructureAxisCoordinate();
        /// <summary>
        /// 接触感知有効/無効設定
        /// </summary>
        public bool ContactSensing { get; set; }
        /// <summary>
        /// イニシャルセット有効無効設定
        /// </summary>
        public bool InitialSet { get; set; }
        /// <summary>
        /// ブザー音有効無効設定
        /// </summary>
        public bool Buzzer { get; set; }
        /// <summary>
        /// スピンドル状態
        /// </summary>
        public bool SpindleOn { get; set; }
        /// <summary>
        /// ポンプ状態
        /// </summary>
        public bool PumpOn { get; set; }
        /// <summary>
        /// 放電中
        /// </summary>
        public bool DischargeOn { get; set; }
        /// <summary>
        /// ガイドホルダークランプ状態
        /// </summary>
        public bool GuideHolderClampOn { get; set; }
        /// <summary>
        /// コレットクランプ状態
        /// </summary>
        public bool ColletClampOn { get; set; }
        /// <summary>
        /// フィンガーアーム状態
        /// </summary>
        public bool FingerArmClampOn { get; set; }
        /// <summary>
        /// フィンガーアーム位置
        /// </summary>
        public Enumeration.EsfArmPositions FingerArmPos { get; set; }
        /// <summary>
        /// GSFアーム後退端
        /// </summary>
        public bool GsfArmBack { get; set; }
        /// <summary>
        /// GSFアーム前進端
        /// </summary>
        public bool GsfArmFoward { get; set; }
        /// <summary>
        /// ガイド番号設定状態
        /// </summary>
        public int GuideNumber { get; set; }
        /// <summary>
        /// 電極数
        /// </summary>
        public short ElectrodeCount { get; set; }
        /// <summary>
        /// ガイド数
        /// </summary>
        public short GuideCount { get; set; }
        /// <summary>
        /// 電極番号設定状態
        /// </summary>
        public int ElectrodeNumber { get; set; }
        /// <summary>
        /// 動作モード
        /// </summary>
        public Enumeration.McTaskModes ProcessMode { get; set; }
        /// <summary>
        /// オプショナルストップ
        /// </summary>
        public bool OptionalStop { get; set; }
        /// <summary>
        /// 相対測定点設定時軸移動
        /// </summary>
        public bool IncrimentalReferenceAxisMove { get; set; }
        /// <summary>
        /// W軸上限値
        /// </summary>
        public int WAxisUpperLimit { get; set; }
        /// <summary>
        /// ドライラン
        /// </summary>
        public bool DryRun { get; set; }
        /// <summary>
        /// AEC1週停止
        /// </summary>
        public bool PartitionRoundStop { get; set; }
        /// <summary>
        /// IOデータ一覧
        /// </summary>
        public ushort IoDataList { get; set; }
        /// <summary>DAT_STATUS 取得情報</summary>
        public StructureMcDatStatus McStatus { get; set; }
        /// <summary>
        /// シーケンス完了
        /// </summary>
        public bool SequenceEnd { get; set; }
        /// <summary>
        /// FG完了
        /// </summary>
        public bool FGEnd { get; set; }
        /// <summary>
        /// FG途中停止中
        /// </summary>
        public bool FGStopped { get; set; }
        /// <summary>
        /// FG中
        /// </summary>
        public bool FGRunning { get; set; }
        /// <summary>
        /// 返送コマンド
        /// </summary>
        public bool ReturnCmd { get; set; }
        /// <summary>
        /// 加工条件番号
        /// </summary>
        public int ProcCondNum { get; set; }
        /// <summary>
        /// AEC有効
        /// </summary>
        public bool AecByLife { get; set; }
        /// <summary>
        /// ブロックスキップ有効
        /// </summary>
        public bool BlockSkipEn { get; set; }
        /// <summary>
        /// M02によるプログラム終了有効
        /// </summary>
        public bool M02Dis { get; set; }
        /// <summary>
        /// 角度補正有効
        /// </summary>
        public bool CorrectAngleEn { get; set; }
        /// <summary>
        /// マシンロック有効
        /// </summary>
        public bool MachineLockEn { get; set; }
        /// <summary>
        /// オーバーライド値
        /// </summary>
        public int OverRide { get; set; }
        /// <summary>
        /// プログラム実行行
        /// </summary>
        public int ProgRowNum { get; set; }
        /// <summary>
        /// シングルステップ
        /// </summary>
        public bool SingleStep { get; set; }
        /// <summary>
        /// 角度補正値
        /// </summary>
        public int CorrectAngleValue { get; set; }
        /// <summary>
        /// A軸有効
        /// </summary>
        public bool AxisAEn { get; set; }
        /// <summary>
        /// B軸有効
        /// </summary>
        public bool AxisBEn { get; set; }
        /// <summary>
        /// C軸有効
        /// </summary>
        public bool AxisCEn { get; set; }
        /// <summary>
        /// ESF有効
        /// </summary>
        public bool EsfEn { get; set; }
        /// <summary>
        /// GSF有効
        /// </summary>
        public bool GsfEn { get; set; }
        /// <summary>
        /// SF02有効
        /// </summary>
        public bool Sf02En { get; set; }
        /// <summary>
        /// 細線設定有効
        /// </summary>
        public bool ThinEn { get; set; }
        /// <summary>
        /// 原点復帰済
        /// </summary>
        public bool CompletedReturnOriginEn { get; set; }
    }
}
