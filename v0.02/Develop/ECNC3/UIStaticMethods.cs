using ECNC3.Enumeration;
using ECNC3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECNC3.Views
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    ///     検証・エラーチェックをサポートした静的クラスです。
    /// </summary>
    /// -----------------------------------------------------------------------------

    public sealed class UIStaticMethods
    {
        public static bool IsAlphabet(string target)
        {
            return new Regex("^[a-zA-Z]+$").IsMatch(target);
        }

        public static bool IsNum(string target)
        {
            return new Regex("^[0-9]+$").IsMatch(target);
        }

        //-----------------------------------------------------------------------------------------
        //
        //		Title			: 指定した精度の数値に切り捨てる処理
        //		Function Name	: ToRoundDown
        //		Input		    : dValue===丸め対象の倍精度浮動小数点数。
        //                        iDigits===戻り値の有効桁数の精度。
        //		Output		    : 
        //		Return		    :  iDigits に等しい精度の数値に切り捨てられた数値。
        //		Description	    : エラー/確認/ステータスバーのいずれかにメッセージを表示させる
        //
        //-----------------------------------------------------------------------------------------
        public static double ToRoundDown(double dValue, int iDigits)
        {
            double dCoef = System.Math.Pow(10, iDigits);

            return dValue > 0 ? System.Math.Floor(dValue * dCoef) / dCoef :
                                System.Math.Ceiling(dValue * dCoef) / dCoef;
        }

        #region　IsNumeric メソッド (+1)

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     文字列が数値であるかどうかを返します。</summary>
        /// <param name="stTarget">
        ///     検査対象となる文字列。<param>
        /// <returns>
        ///     指定した文字列が数値であれば true。それ以外は false。</returns>
        /// -----------------------------------------------------------------------------
        public static bool IsNumeric(string stTarget)
        {
            double dNullable;

            return double.TryParse(
                stTarget,
                System.Globalization.NumberStyles.Any,
                null,
                out dNullable
            );
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     オブジェクトが数値であるかどうかを返します。</summary>
        /// <param name="oTarget">
        ///     検査対象となるオブジェクト。<param>
        /// <returns>
        ///     指定したオブジェクトが数値であれば true。それ以外は false。</returns>
        /// -----------------------------------------------------------------------------
        public static bool IsNumeric(object oTarget)
        {
            return IsNumeric(oTarget.ToString());
        }

        #endregion
        #region Import/Export
        /// <summary>
        /// マスターフォルダ内のXMLファイルインポート処理
        /// </summary>
        /// <param name="fileName">
        /// ファイル名
        /// </param>
        /// <returns>
        /// Success：成功
        /// その他：失敗
        /// </returns>
        public static ResultCodes ImportMasterFile(string fileName)
        {
            string sourceFilePath = FilePathInfo.Usb + fileName;
            string destFilePath = FilePathInfo.MasterData + fileName;
            try
            {
                //コピー元パス、コピー先パスの確認
                if (File.Exists(sourceFilePath) == false) return ResultCodes.FailToReadFile;
                if (Directory.Exists(FilePathInfo.MasterData) == false) return ResultCodes.FailToWriteFile;
                //コピー実行
                File.Copy(sourceFilePath, destFilePath, true);
            }
            //例外処理
            catch (Exception ex) {
                ECNC3Exception.FileIOFilter(ex, ExceptionHandling.MessageDialog);
                return ResultCodes.FailToWriteFile;
            }
            return ResultCodes.Success;
        }
        /// <summary>
        /// マスターフォルダ内のXMLファイルエクスポート処理
        /// </summary>
        /// <param name="fileName">
        /// ファイル名
        /// </param>
        /// <returns>
        /// Success：成功
        /// その他：失敗
        /// </returns>
        public static ResultCodes ExportMasterFile(string fileName)
        {
            string sourceFilePath = FilePathInfo.MasterData + fileName;
            string destFilePath = FilePathInfo.Usb + fileName;
            try
            {
                //コピー元パス、コピー先パスの確認
                if (File.Exists(sourceFilePath) == false) return ResultCodes.FailToReadFile;
                if (Directory.Exists(FilePathInfo.Usb) == false) return ResultCodes.FailToWriteFile;
                //コピー実行
                File.Copy(sourceFilePath, destFilePath, true);
            }
            //例外処理
            catch (Exception ex)
            {
                ECNC3Exception.FileIOFilter(ex, ExceptionHandling.MessageDialog);
                return ResultCodes.FailToWriteFile;
            }
            return ResultCodes.Success;
        }
        #endregion
    }
}
