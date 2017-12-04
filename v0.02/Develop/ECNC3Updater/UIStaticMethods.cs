using System;
using System.Collections.Generic;
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

    }
}
