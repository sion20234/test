using ECNC3.Enumeration;

namespace ECNC3.Models
{
	/// <summary>バックアップメソッドインターフェース</summary>
	public interface IEcnc3Backup
	{
		/// <summary>名称</summary>
		string Name { get; }
		/// <summary>バックアップ</summary>
		/// <param name="backupDirectory">バックアップ先ディレクトリ</param>
		/// <returns>実行結果</returns>
		ResultCodes Backup( string backupDirectory );
		/// <summary>リストア</summary>
		/// <param name="restoreDirectory">リストア元ディレクトリ</param>
		/// <returns>実行結果</returns>
		ResultCodes Restore( string restoreDirectory );
	}
    /// <summary>データ取得クラスインターフェース</summary>
    public interface IEcnc3FileReadOnly
    {
        string FilePath { set; }
        /// <summary>ファイル読み込み</summary>
        /// <returns>実行結果</returns>
        ResultCodes Read();
    }
}
