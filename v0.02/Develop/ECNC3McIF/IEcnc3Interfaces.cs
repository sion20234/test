using ECNC3.Enumeration;

namespace ECNC3.Models.McIf
{
	/// <summary>データ取得クラスインターフェース</summary>
	public interface IEcnc3McDatReadOnly
	{
		/// <summary>MCデータ読み込み</summary>
		/// <returns>実行結果</returns>
		ResultCodes Read();
	}
	/// <summary>データ取得クラスインターフェース</summary>
	public interface IEcnc3McDatWriteOnly
	{
		/// <summary>MCデータ書き込み</summary>
		/// <returns>実行結果</returns>
		ResultCodes Write();
	}
	/// <summary>データ取得／設定クラスインターフェース</summary>
	public interface IEcnc3McDatReadWrite
	{
		/// <summary>MCデータ読み込み</summary>
		/// <returns>実行結果</returns>
		ResultCodes Read();
		/// <summary>MCデータ書き込み</summary>
		/// <returns>実行結果</returns>
		ResultCodes Write();
	}
	/// <summary>コマンド発行クラスインターフェース</summary>
	public interface IEcnc3McCommand
	{
		/// <summary>MCボードへコマンド発行</summary>
		/// <returns>実行結果</returns>
		ResultCodes Execute();
	}
}
