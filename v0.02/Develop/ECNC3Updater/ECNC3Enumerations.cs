using System.Drawing;

namespace ECNC3.Enumeration
{
	/// <summary>軸番号定義</summary>
	public enum AxisNumbers
	{
		/// <summary>X軸(軸番号1／左右軸)</summary>
		X,
		/// <summary>Y軸(軸番号2／前後軸)</summary>
		Y,
		/// <summary>W軸(軸番号3／上下軸)</summary>
		W,
		/// <summary>Z軸(軸番号4／上下軸(電極))</summary>
		Z,
		/// <summary>A軸(軸番号5／付加軸(ヘッド／ワーク傾き等))</summary>
		A,
		/// <summary>B軸(軸番号6／付加軸(ヘッド／ワーク傾き等))</summary>
		B,
		/// <summary>C軸(軸番号7／付加軸(ヘッド／ワーク傾き等))</summary>
		C,
		/// <summary>I軸(軸番号8／AECインデックス)</summary>
		I,
		/// <summary>不定</summary>
		Unknown,
	}
	/// <summary>軸定義</summary>
	public enum AxisBits
	{
		/// <summary>未設定(初期状態)</summary>
		Free = 0,
		/// <summary>X軸(軸番号1／左右軸)</summary>
		X = 0x01,
		/// <summary>Y軸(軸番号2／前後軸)</summary>
		Y = 0x02,
		/// <summary>W軸(軸番号3／上下軸)</summary>
		W = 0x04,
		/// <summary>Z軸(軸番号4／上下軸(電極))</summary>
		Z = 0x08,
		/// <summary>A軸(軸番号5／付加軸(ヘッド／ワーク傾き等))</summary>
		A = 0x10,
		/// <summary>B軸(軸番号6／付加軸(ヘッド／ワーク傾き等))</summary>
		B = 0x20,
		/// <summary>C軸(軸番号7／付加軸(ヘッド／ワーク傾き等))</summary>
		C = 0x40,
		/// <summary>I軸(軸番号8／AECインデックス)</summary>
		I = 0x80,
	}
	/// <summary>電極位置</summary>
	public enum ElectrodePositions
	{
		/// <summary>交換位置</summary>
		Exchange = 0,
		/// <summary>前位置</summary>
		Foward = 1,
		/// <summary>待機位置</summary>
		StandBy = 2,
		/// <summary>不明</summary>
		Unknown = -1,
	}
	/// <summary>電極位置</summary>
	public enum GuidePositions
	{
		/// <summary>交換位置</summary>
		Exchange = 0,
		/// <summary>待機位置</summary>
		StandBy = 2,
		/// <summary>不明</summary>
		Unknown = -1,
	}
	/// <summary>ESFアーム移動位置</summary>
	public enum EsfArmPositions
	{
		/// <summary>前端</summary>
		Foward = 0,
		/// <summary>中間</summary>
		Middle = 1,
		/// <summary>後端</summary>
		Back = 2,
		/// <summary>不明</summary>
		Unknown = -1,
	}
	/// <summary>GSFアーム移動位置</summary>
	public enum GsfArmPositions
	{
		/// <summary>前端</summary>
		Foward = 0,
		/// <summary>後端</summary>
		Back = 1,
		/// <summary>不明</summary>
		Unknown = -1,
	}

	/// <summary>出力レベル</summary>
	public enum LogLevels
	{
		/// <summary>エラー</summary>
		Error = 0,
		/// <summary>インフォメーション</summary>
		Info = 1,
		/// <summary>デバッグ情報</summary>
		Debug = 2,
	}
	/// <summary>起動オプション</summary>
	public enum BootModes
	{
		/// <summary>実機</summary>
		Machine = 1,
		/// <summary>机上PC</summary>
		Desktop = 2,
		/// <summary>シミュレータ</summary>
		Simulator = 3,
	}
	/// <summary>回転状態</summary>
	public enum SpinStates
	{
		/// <summary>停止</summary>
		Stop = 0,
		/// <summary>時計(右)回り</summary>
		Clockwise = 1,
		/// <summary>反時計(左)回り</summary>
		Counterclockwise = 2,
		/// <summary>不明</summary>
		Unknown = -1,
	}
	/// <summary>クランプ状態</summary>
	public enum ClampStates
	{
		/// <summary>アンクランプ</summary>
		Unclamp = 0,
		/// <summary>クランプ</summary>
		Clamp = 1,
		/// <summary>不明</summary>
		Unknown = -1,
	}
	/// <summary>座標系指定</summary>
	public enum CoordTypes
	{
		/// <summary>未設定</summary>
		NotSet,
		/// <summary>機械座標系</summary>
		Machine,
		/// <summary>論理座標系</summary>
		Work,
	}
	/// <summary>RTMC64動作モード</summary>
	public enum McTaskModes
	{
		/// <summary>セッティングモード</summary>
		Setting = 0,
		/// <summary>手動</summary>
		Manual = 1,
		/// <summary>自動</summary>
		Auto = 2,
		/// <summary>OT無視</summary>
		IngnoreOT = 3,
		/// <summary>DNC</summary>
		DNC = 4,
		/// <summary>サポート外の設定</summary>
		NotSupported = -1,
	}
	/// <summary>オーバライド種別</summary>
	public enum OverrideModes
	{
		/// <summary>全体</summary>
		Overall,
		/// <summary>補間</summary>
		Interpolation,
		/// <summary>主軸</summary>
		Spindle,
	}

	/// <summary>実行中のプログラム種別</summary>
	public enum ProgramCodeTypes
	{
		/// <summary>ユーザプログラム</summary>
		Main = 0,
		/// <summary>ユーザ定義 Gコードプログラム</summary>
		GCode = 1,
		/// <summary>ユーザ定義 Mコードプログラム</summary>
		MCode = 2,
		/// <summary>不明</summary>
		Unknown = -1,
	}
	/// <summary>出力ログ種別</summary>
	public enum AlarmLogKinds
	{
		/// <summary>アプリケーション起動</summary>
		AppStart = 100,
		/// <summary>アプリケーション終了</summary>
		AppEnd = 101,
		/// <summary>全体アラーム発生</summary>
		AlarmMain = 0,
		/// <summary>軸アラーム</summary>
		AlarmAxis = 1,
		/// <summary>タスクアラーム</summary>
		AlarmTask = 2,
		/// <summary>ECNCアラーム</summary>
		AlarmEcnc = 3,
		/// <summary>不定</summary>
		Unknown = -1,
	}

	/// <summary>共通エラーコード</summary>
	public enum ResultCodes
	{
		/// <summary>成功</summary>
		Success,
		/// <summary>引数異常</summary>
		InvalidArgument,
		/// <summary>該当なし</summary>
		NotFound,
		/// <summary>サポートされない機能の呼び出し</summary>
		NotSupported,
		/// <summary>不要につき実行しなかった</summary>
		NotExecute,
		/// <summary>入力範囲外</summary>
		OutOfRange,
		/// <summary>登録済み</summary>
		AlreadyRegistered,
		/// <summary>ファイル読み込み失敗</summary>
		FailToReadFile,
		/// <summary>ファイル書き込み失敗</summary>
		FailToWriteFile,
		/// <summary>暗号化失敗</summary>
		FailToEncryption,
		/// <summary>復号化失敗</summary>
		FailToDecryption,
		/// <summary>事前条件の不備</summary>
		/// <remarks>
		/// プロパティの設定等、事前に行われているべき手順が実施されていないことをあらわします。
		/// </remarks>
		LackOfPreparation,
		/// <summary>MCインターフェース未初期化</summary>
		/// <remarks>
		/// MCボードとの通信インターフェースの初期化が行われていません。
		/// </remarks>
		McNotInitialize,

		#region テクノ通信ライブラリ
		/// <summary>デバイス未初期化</summary>
		McCommErrorNotInitialize,
		/// <summary>通信パラメータ設定異常</summary>
		McCommErrorInvalidParameter,
		/// <summary>タイムアウト発生</summary>
		McCommErrorTimeout,
		/// <summary>リトライオーバー発生</summary>
		McCommErrorRetryOver,
		/// <summary>多重リトライ発生</summary>
		McCommErrorMultipleRetry,
		/// <summary>通信ハードウェアエラー</summary>
		McCommErrorHardware,
		/// <summary>要求データが存在しない</summary>
		McCommErrorNotRequest,
		/// <summary>送信データ書込不可</summary>
		McCommErrorUnwritable,
		/// <summary>通信データフォーマットエラー</summary>
		McCommErrorFormat,
		/// <summary>運転プログラム書込中断</summary>
		McCommErrorProgramWriteAbort,
		/// <summary>運転プログラムバッファオーバーフロー</summary>
		McCommErrorBufferoverflow,
		/// <summary>コマンド実行不可</summary>
		McCommErrorNotBeExecuted,
		/// <summary>空き通信ハンドル無し</summary>
		McCommErrorEmptyHandle,
		/// <summary>無効ハンドル</summary>
		McCommErrorInvalidHandle,
		/// <summary>通信ビジー</summary>
		McCommErrorBusy,
		/// <summary>パラメータ書き込みエラー</summary>
		McCommErrorWriteParameter,
		/// <summary>プログラム停止位置でない</summary>
		McCommErrorProgramStopPosition,
		/// <summary>不明エラー</summary>
		McCommErrorUnknown,
		#endregion

		#region テクノコード変換ライブラリ
		/// <summary>テキスト運転プログラムファイルエラー</summary>
		McGcdErrorFile,
		/// <summary>運転プログラムバッファオーバーフロー</summary>
		McGcdErrorBufferOverflow,
		/// <summary>テキスト運転プログラムフォーマットエラー</summary>
		McGcdErrorFileFormat,
		/// <summary>プログラム変換演算エラー</summary>
		McGcdErrorConvertion,
		/// <summary>作業メモリオーバーフロー</summary>
		McGcdErrorWorkMemoryOverflow,
		/// <summary>変換ライブラリ未初期化エラー</summary>
		McGcdErrorNotInitialize,
		/// <summary>ユーザー定義Ｇ／Ｍコード循環呼出エラー</summary>
		McGcdErrorUserCodeCirculateCall,
		/// <summary>未定義コード指定エラー</summary>
		McGcdErrorUndefinedCodeSpecified,
		/// <summary>無効Ｇ／Ｍコード指定エラー</summary>
		McGcdErrorUserCodeInvalidSpecified,
		/// <summary>テクノコード変換未定義のコード</summary>
		McGcdErrorUnknown,
        #endregion
        /// <summary>OSによりキャッチされた例外</summary>
        /// <remarks>
        /// 詳細はログファイルを参照してください。
        /// 以下の例外をキャッチした可能性があります。
        /// <list type="bullet" >
        ///		<item>ApplicationException</item>
        ///		<item>ArgumentException</item>
        ///		<item>ArgumentNullException</item>
        ///		<item>ArgumentOutOfRangeException</item>
        ///		<item>DirectoryNotFoundException</item>
        ///		<item>DllNotFoundException</item>
        ///		<item>EntryPointNotFoundException</item>
        ///		<item>Exception</item>
        ///		<item>FileNotFoundException</item>
        ///		<item>FormatException</item>
        ///		<item>InvalidOperationException</item>
        ///		<item>IO.IOException</item>
        ///		<item>IOException</item>
        ///		<item>NotSupportedException</item>
        ///		<item>NullReferenceException</item>
        ///		<item>ObjectDisposedException</item>
        ///		<item>PathTooLongException</item>
        ///		<item>Security.SecurityException</item>
        ///		<item>Threading.AbandonedMutexException</item>
        ///		<item>UnauthorizedAccessException</item>
        ///		<item>Xml.XmlException</item>
        ///		<item>Xml.XPath.XPathException</item>
        ///		<item>XmlException</item>
        ///		<item>XPathException</item>
        /// </list>
        /// </remarks>
        ExceptionFromWindows,

        #region UI
        //未選択状態
        NotSelect,
        //処理済み
        AlreadySetting,
        //書き込み制限
        WriteProtected
        #endregion
    }
    public class ECNC3Color
	{
		public static readonly Color StandardBack = Color.FromArgb( 35, 35, 0 );
		public static readonly Color StandardFore = Color.White;
	}
}
