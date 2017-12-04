using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECNC3.Views
{
    //方向
    internal enum Direction
    {
        //上
        Up, 
        //下
        Down,
        //右
        Right,
        //左
        Left,
        //前
        Front,
        //後
        Back
    }
    /// <summary>
    /// 値の送り方向
    /// </summary>
    internal enum ValueVector
    {
        /// <summary>
        /// 取得
        /// </summary>
        Get,
        /// <summary>
        /// 設定
        /// </summary>
        Set
    }
    /// <summary>
    /// 手動画面で実行できる主軸動作
    /// </summary>
    internal enum ManualModeProcCommands
    {
        Null = 0,
        /// <summary>
        /// 回転
        /// </summary>
        Spin = 1,
        /// <summary>
        /// ポンプ
        /// </summary>
        Pomp = 2,
        /// <summary>
        /// 放電
        /// </summary>
        Discharge = 3
    }
    /// <summary>
    /// ワーク座標の種類
    /// </summary>
    internal enum WorkPositions
    {
        Work
    }
    /// <summary>
    /// 単位
    /// </summary>
    internal enum UnitCategory
    {
        /// <summary>
        /// ﾐﾘ
        /// </summary>
        mm,
        /// <summary>
        /// インチ
        /// </summary>
        inch
    }
    /// <summary>
    /// 言語
    /// </summary>
    internal enum Language
    {
        /// <summary>
        /// 日本語
        /// </summary>
        JP,
        /// <summary>
        /// 英語
        /// </summary>
        EN
    }
    /// <summary>
    /// タイマー項目
    /// </summary>
    internal enum TimerCategory
    {
        /// <summary>
        ///主電源ON時間
        /// </summary>
        MainPowerOnTime,
        /// <summary>
        /// 加工電源ON時間
        /// </summary>
        ProcessingPowerOnTime,
        /// <summary>
        /// プログラム運転時間
        /// </summary>
        ProgramTime,
        /// <summary>
        /// 加工時間
        /// </summary>
        Discharge,
        /// <summary>
        /// 1穴加工時間
        /// </summary>
        OneProcessingTime
    }

    internal enum ControlSize
    {
        /// <summary>
        /// 最大サイズ
        /// </summary>
        Maximum,
        /// <summary>
        /// ハーフサイズ(2分の1)
        /// </summary>
        Half,
        /// <summary>
        /// クォーターサイズ(4分の1)
        /// </summary>
        HalfAndHalf,
        /// <summary>
        /// 最小サイズ
        /// </summary>
        Minimum
    }

    internal enum ElectWord
    {
        /// <summary>
        /// 電圧
        /// </summary>
        Voltage,
        /// <summary>
        /// 電流
        /// </summary>
        Anpair
    }

    internal enum MAINFormCategory
    {
        /// <summary>
        /// 手動画面
        /// </summary>
        Manual,
        /// <summary>
        /// MDI画面
        /// </summary>
        MDI,
        /// <summary>
        /// 編集画面
        /// </summary>
        Edit,
        /// <summary>
        /// 自動画面
        /// </summary>
        Auto
    }

    internal enum ButtonCheck
    {
        /// <summary>
        /// 未チェック状態
        /// </summary>
        UnCheck,
        /// <summary>
        /// チェック状態
        /// </summary>
        Check
    }

    internal enum LogModes
    {
        /// <summary>
        /// コマンド処理ログ用
        /// </summary>
        MachineControlMode = (int)0,
        /// <summary>
        /// UI操作ログ用
        /// </summary>
        UserInterfaceMode = (int)1
    }
    internal enum MonitoringMode
    {
        Main,
        IOChk
    }

    internal enum LabelStatus
    {
        Null,
        Safety,
        Caution,
        Warning
    }

    /// <summary>
    /// 加工条件項目
    /// </summary>
    internal enum ProcessConditions
    {
        Null,
        PNumber,
        TurnOn,
        TurnOff,
        IP,
        CPS,
        SC,
        SFRDown,
        SFRUp,
        CRS,
        JumpDown,
        JumpUp,
        Pol,
        Hydraulic,
        ElctPhi,
        OverRide

    }

    /// <summary>
    /// 軸動作種類
    /// </summary>
    public enum AxisPosMode
    {
        Machine,
        Work
    }
    /// <summary>
    /// 軸の移動モード
    /// </summary>
    public enum AxisMoveMode
    {
        /// <summary>
        /// ｲﾝｸﾘﾒﾝﾀﾙ
        /// </summary>
        Inc,
        /// <summary>
        /// ｱﾌﾞｿﾘｭｰﾄ
        /// </summary>
        Abs
    }
    /// <summary>
    /// 座標軸名
    /// </summary>
    internal enum AxisName
    {
        Null,
        AxisX,
        AxisY,
        AxisW,
        AxisZ,
        AxisA,
        AxisB,
        AxisC,
        AxisI
    }

    /// <summary>
    /// 各動作の列挙
    /// </summary>
    internal enum Sequences
    {
        Null,
        /// <summary>
        /// 全軸原点復帰
        /// </summary>
        OriginAll,
        /// <summary>
        /// Z軸原点復帰
        /// </summary>
        ZOrigin,
        /// <summary>
        /// アブソPTP位置決め（機械座標）
        /// </summary>
        AbsoluteMacMovePointToPoint,
        /// <summary>
        /// W軸上限待避有効アブソPTP位置決め（機械座標）
        /// </summary>
        WaxisUpperSavedAbsMacMovePntToPnt,
        /// <summary>
        /// W軸上限待避有効アブソPTP位置決め（論理座標）
        /// </summary>
        WaxisUpperSavedAbsWorkMovePntToPnt,
        /// <summary>
        /// ガイドクランプ
        /// </summary>
        GuideClamp,
        /// <summary>
        /// ガイドアンクランプ
        /// </summary>
        GuideUnClamp,
        /// <summary>
        /// スピンドルクランプ
        /// </summary>
        SpindleClamp,
        /// <summary>
        /// スピンドルアンクランプ
        /// </summary>
        SpindleUnClamp,
        /// <summary>
        /// フィンガーアームクランプ
        /// </summary>
        FingerArmClamp,
        /// <summary>
        /// フィンガーアームアンクランプ
        /// </summary>
        FingerArmUnClamp,
        /// <summary>
        /// フィンガーアーム後退
        /// </summary>
        FingerArmBack,
        /// <summary>
        /// フィンガーアーム前進1
        /// </summary>
        FingerArmFront1,
        /// <summary>
        /// フィンガーアーム前進2
        /// </summary>
        FingerArmFront2,
        /// <summary>
        /// ESFマガジン回転
        /// </summary>
        MagazineIncFront,
        /// <summary>
        /// ESF自動電極装着
        /// </summary>
        EsfInstall,
        /// <summary>
        /// ESF自動電極脱着
        /// </summary>
        EsfUnInstall,
        /// <summary>
        /// GSFアーム後退：追加：柏原
        /// </summary>
        GsfArmBack,
        /// <summary>
        /// GSFアーム前進：追加：柏原
        /// </summary>
        GsfArmFront,
        /// <summary>
        /// GSF自動ガイド装着
        /// </summary>
        GsfInstall,
        /// <summary>
        /// GSF自動ガイド脱着
        /// </summary>
        GsfUnInstall,
        /// <summary>
        /// 放電ON
        /// </summary>
        DischargeOn,
        /// <summary>
        /// ポンプON
        /// </summary>
        PompOn,
        /// <summary>
        /// 主軸回転ON
        /// </summary>
        SpindleOn,
        /// <summary>
        /// ワーク座標PTP位置決め
        /// </summary>
        AbsoluteWorkMovePointToPoint,
        /// <summary>
        /// インクリPTP位置決め
        /// </summary>
        IncMovePointToPoint,
        /// <summary>
        /// プログラム実行
        /// </summary>
        ProgramStart,
        ProgramSelect
    }

    /// <summary>
    /// 各種監視対象の列挙
    /// </summary>
    internal enum MonitorTargets
    {
        /// <summary>
        /// 動作モード
        /// </summary>
        ProccessMode,
        /// <summary>
        /// ワーク原点値
        /// </summary>
        WorkOrgPos,
        /// <summary>
        /// 現在の機械座標
        /// </summary>
        MachineAxis,
        /// <summary>
        /// スタートボタン（ハードスイッチ）
        /// </summary>
        StartSwBtn,
        /// <summary>
        /// 各種シーケンス完了
        /// </summary>
        SequenceEnd,
        /// <summary>
        /// スピンドル状態
        /// </summary>
        SpindleOn,
        /// <summary>
        /// ポンプ状態
        /// </summary>
        PumpOn,
        /// <summary>
        /// 放電中
        /// </summary>
        DischargeOn,
        /// <summary>
        /// FG完了（軌跡完了）
        /// </summary>
        FgEnd,
        /// <summary>
        /// ガイドホルダークランプ状態
        /// </summary>
        GuideHolderClampOn,
        /// <summary>
        /// コレットクランプ状態
        /// </summary>
        ColletClampOn,
        /// <summary>
        /// ガイド番号設定状態
        /// </summary>
        GuideNumber,
        /// <summary>
        /// 自動ガイド装着
        /// </summary>
        GsfInstall,
        /// <summary>
        /// 自動ガイド脱着
        /// </summary>
        GsfUnInstall,
        /// <summary>
        /// GSFアーム後退：追加：柏原
        /// </summary>
        GsfArmBack,
        /// <summary>
        /// GSFアーム前進：追加：柏原
        /// </summary>
        GsfArmFront,
        /// <summary>
        /// 電極番号設定状態
        /// </summary>
        ElectrodeNumber,
        /// <summary>
        /// 電極数
        /// </summary>
        ElectrodeCount,
        /// <summary>
        /// ガイド数
        /// </summary>
        GuideCount,
        /// <summary>
        /// GSF有無
        /// </summary>
        GsfEnable,
        /// <summary>
        /// ESF有無
        /// </summary>
        EsfEnable,
        /// <summary>
        /// 原点復帰完了済み
        /// </summary>
        CompletedOrigin,
        /// <summary>
        /// A軸有無
        /// </summary>
        AaxisEnable,
        /// <summary>
        /// B軸有無
        /// </summary>
        BaxisEnable,
        /// <summary>
        /// C軸有無
        /// </summary>
        CaxisEnable,
        /// <summary>
        /// オーバーライド
        /// </summary>
        Override,
        /// <summary>
        /// 接触感知有効/無効設定
        /// </summary>
        ContactSensing,
        /// <summary>
        /// イニシャルセット有効無効設定
        /// </summary>
        InitialSet,
        /// <summary>
        /// ブザー音有効無効設定
        /// </summary>
        Buzzer,
        WorkAxis,
        ReturnCmd,
        ProcCondNum
    }

    /// <summary>
    /// 各設定処理名の列挙
    /// </summary>
    internal enum Settings
    {
        Null,
        /// <summary>
        /// 返送コマンド
        /// </summary>
        ReturnCmd,
        /// <summary>
        /// ワーク原点値設定
        /// </summary>
        WorkOrgPos,
        /// <summary>
        /// 手動パルサー動作許可設定
        /// </summary>
        PulseHandle,
        /// <summary>
        /// X/Yインターロック有効/無効設定
        /// </summary>
        XandYaxisInterLock,
        /// <summary>
        /// W軸上限値設定
        /// </summary>
        WaxisUpper,
        /// <summary>
        /// 電極番号設定
        /// </summary>
        ElectroadNumber,
        /// <summary>
        /// ガイド番号設定
        /// </summary>
        GuideNumber,
        /// <summary>
        /// 接触感知有効/無効設定
        /// </summary>
        ContactSensing,
        /// <summary>
        /// イニシャルセット有効無効設定
        /// </summary>
        InitialSet,
        /// <summary>
        /// ブザー音有効無効設定
        /// </summary>
        Buzzer,
        /// <summary>
        /// 手動モードのオーバーライドINDEX設定
        /// </summary>
        ManualOverRide,
        /// <summary>
        /// 自動モードのオーバーライドINDEX設定
        /// </summary>
        AutoOverRide,
        /// <summary>
        /// オプショナルストップ設定
        /// </summary>
        OptionalStop,
        /// <summary>
        /// 相対測定点設定時軸移動ON設定
        /// </summary>
        IncrimentalReferenceAxisMove,
        /// <summary>
        /// ドライラン設定
        /// </summary>
        DryRun,
        /// <summary>
        /// AEC1週停止設定
        /// </summary>
        PartitionRoundStop,
        /// <summary>
        /// 電極交換設定
        /// </summary>
        AECByLife,
        /// <summary>
        /// 角度補正設定
        /// </summary>
        CorrectAngleEn,
        /// <summary>
        /// ブロックスキップ設定
        /// </summary>
        BlockSkipEn,
        /// <summary>
        /// マシンロック設定
        /// </summary>
        MachineLockEn,
        /// <summary>
        /// M02によるプログラム終了設定
        /// </summary>
        M02En,
        /// <summary>
        /// シングルブロック(シングルステップ)設定
        /// </summary>
        SingleBlock,
        /// <summary>
        /// 実行プログラム設定
        /// </summary>
        Program,
        /// <summary>
        /// プログラム番号設定
        /// </summary>
        ProgramNo,
        /// <summary>
        /// 電極装着（コレットクランプ）までの主軸回転速度
        /// </summary>
        EsfClampCrsSpdChg

    }
	internal enum AECSequences
	{
		/// <summary>
		/// 指定なし
		/// </summary>
		Null,
		/// <summary>
		/// ESFアームクランプ開
		/// </summary>
		FingerArmUnClamp,
		/// <summary>
		/// ESFアームクランプ閉
		/// </summary>
		FingerArmClamp,
		/// <summary>
		/// ESFアーム後退
		/// </summary>
		FingerArmBack,
		/// <summary>
		/// ESFアーム前進1
		/// </summary>
		FingerArmFront1,
		/// <summary>
		/// ESFアーム前進2
		/// </summary>
		FingerArmFront2,
		/// <summary>
		/// ガイドクランプ開
		/// </summary>
		GuideUnClamp,
		/// <summary>
		/// ガイドクランプ閉
		/// </summary>
		GuideClamp,
		/// <summary>
		/// スピンドルクランプ開
		/// </summary>
		SpindleUnClamp,
		/// <summary>
		/// スピンドルクランプ閉
		/// </summary>
		SpindleClamp,
		/// <summary>
		/// 電極番号設定
		/// </summary>
		SetElectroadNumber,
		/// <summary>
		/// ガイド番号設定
		/// </summary>
		SetGuideNumber,
		/// <summary>
		/// 自動電極装着
		/// </summary>
		EsfInstall,
		/// <summary>
		/// 自動電極脱着
		/// </summary>
		EsfUnInstall,
		/// <summary>
		/// 自動ガイド装着
		/// </summary>
		GsfInstall,
		/// <summary>
		/// 自動ガイド脱着
		/// </summary>
		GsfUnInstall,
		/// <summary>
		/// GSFアーム後退：追加：柏原
		/// </summary>
		GsfArmBack,
		/// <summary>
		/// GSFアーム前進：追加：柏原
		/// </summary>
		GsfArmFront,
		/// <summary>
		/// マガジン送り
		/// </summary>
		MagazineIncFront,
		/// <summary>
		/// 主軸回転
		/// </summary>
		CrsSpin,
		/// <summary>
		/// Z軸原点復帰
		/// </summary>
		ZOriginMove,
		/// <summary>
		/// 電極交換位置X
		/// </summary>
		ElctdExchPosX,
		/// <summary>
		/// 電極交換位置Y
		/// </summary>
		ElctdExchPosY,
		/// <summary>
		/// 電極交換位置W
		/// </summary>
		ElctdExchPosW,
		/// <summary>
		/// 電極交換位置ABC
		/// </summary>
		ElctdExchPosABC,
		/// <summary>
		/// W軸電極交換前位置オフセット
		/// </summary>
		ElctdExchOfsW1,
		/// <summary>
		/// W軸電極交換待機位置オフセット
		/// </summary>
		ElctdExchOfsW2,
        /// <summary>
        /// ガイド交換位置始点X
        /// </summary>
        GuideExchStartPosX,
        /// <summary>
        /// ガイド交換位置始点Y
        /// </summary>
        GuideExchStartPosY,
        /// <summary>
        /// ガイド交換位置始点W
        /// </summary>
        GuideExchStartPosW,
        /// <summary>
        /// ガイド交換位置始点ABC
        /// </summary>
        GuideExchStartPosABC,
        /// <summary>
        /// ガイド交換位置始点X
        /// </summary>
        GuideExchEndPosX,
        /// <summary>
        /// ガイド交換位置始点Y
        /// </summary>
        GuideExchEndPosY,
        /// <summary>
        /// W軸電極交換前位置オフセット
        /// </summary>
        GuideExchOfsW1,
        /// <summary>
        /// W軸電極交換待機位置オフセット
        /// </summary>
        GuideExchOfsW2,
        /// <summary>
        /// ガイド貫通動作
        /// </summary>
        GuideThrough
    }
}
