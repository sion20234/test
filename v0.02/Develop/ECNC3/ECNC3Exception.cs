using ECNC3.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ECNC3.Models;
using ECNC3.Enumeration;

namespace ECNC3.Views
{
    /// <summary>
    /// ECNC3ソリューション内で例外発生時のCatch処理クラス
    /// </summary>
    public class ECNC3Exception : AidException
    {
        public ECNC3Exception()
        {

        }
        /// <summary>
        /// XML処理時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string XmlFilter(Exception ex,
                                            Form owner = null,
                                            ExceptionHandling handle = ExceptionHandling.WindowsMessageBox,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "")
        {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            AidLog logs = new AidLog(functionName);
            bool unexpected = true;
            if ((ex is ArgumentNullException) ||
                (ex is XmlException) ||
                (ex is ArgumentException) ||
                (ex is PathTooLongException) ||
                (ex is DirectoryNotFoundException) ||
                (ex is FileNotFoundException) ||
                (ex is IOException) ||
                (ex is UnauthorizedAccessException) ||
                (ex is NotSupportedException) ||
                (ex is System.Security.SecurityException))
            { unexpected = false; }   //	想定内の例外。
            switch (handle)
            {
                case ExceptionHandling.WindowsMessageBox:
                    MessageBox.Show(msg);
                    break;

                case ExceptionHandling.MessageDialog:
                    MessageDialog dia = new MessageDialog();
                    dia.Subject = MessageBoxIcon.Error;
                    dia.UpdateData();
                    dia.Item = new Models.StructureOperatorMessageItem();
                    dia.Item.Title = ex.Message;
                    dia.Item.Text = ex.StackTrace;
                    if (owner == null)
                    {
                        dia.ShowDialog();
                    }
                    else
                    {
                        dia.ShowDialog(owner);
                    }
                    break;

                case ExceptionHandling.LogOnly:

                    break;
            }
            logs.Exception(ex, unexpected);
            return msg;
        }
        /// <summary>
        /// System.IOのファイル操作時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string FileIOFilter(Exception ex,
                                            Form owner = null,
                                            ExceptionHandling handle = ExceptionHandling.WindowsMessageBox, 
                                            [CallerFilePath]string path = "", 
                                            [CallerMemberName]string functionName = "" )
        {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            AidLog logs = new AidLog(functionName);
            bool unexpected = true;
            if ((ex is DirectoryNotFoundException) ||
                (ex is FileNotFoundException) ||
                (ex is IOException) ||
                (ex is UnauthorizedAccessException) ||
                (ex is NotSupportedException) ||
                (ex is System.Security.SecurityException))
            { unexpected = false; }   //	想定内の例外。
            switch(handle)
            {
                case ExceptionHandling.WindowsMessageBox:
                    MessageBox.Show(msg);
                    break;

                case ExceptionHandling.MessageDialog:
                    MessageDialog dia = new MessageDialog();
                    dia.Subject = MessageBoxIcon.Error;
                    dia.UpdateData();
                    dia.Item = new Models.StructureOperatorMessageItem();
                    dia.Item.Title = ex.Message;
                    dia.Item.Text = ex.StackTrace;
                    if(owner == null)
                    {
                        dia.ShowDialog();
                    }
                    else
                    {
                        dia.ShowDialog(owner);
                    }
                    break;

                case ExceptionHandling.LogOnly:

                    break;
            }
            logs.Exception(ex, unexpected);
            return msg;
        }
        /// <summary>
        /// リストや配列の処理時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string CollectionsFilter(Exception ex,
                                            Form owner = null,
                                            ExceptionHandling handle = ExceptionHandling.WindowsMessageBox,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "")
        {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            AidLog logs = new AidLog(functionName);
            bool unexpected = true;
            if ((ex is IndexOutOfRangeException) ||
                (ex is KeyNotFoundException))
            { unexpected = false; }   //	想定内の例外。
            switch (handle)
            {
                case ExceptionHandling.WindowsMessageBox:
                    MessageBox.Show(msg);
                    break;

                case ExceptionHandling.MessageDialog:
                    MessageDialog dia = new MessageDialog();
                    dia.Subject = MessageBoxIcon.Error;
                    dia.UpdateData();
                    dia.Item = new Models.StructureOperatorMessageItem();
                    dia.Item.Title = ex.Message;
                    dia.Item.Text = ex.StackTrace;
                    if (owner == null)
                    {
                        dia.ShowDialog();
                    }
                    else
                    {
                        dia.ShowDialog(owner);
                    }
                    break;

                case ExceptionHandling.LogOnly:

                    break;
            }
            logs.Exception(ex, unexpected);
            return msg;
        }
        /// <summary>
        /// スレッド処理時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string ThreadExceptionFilter(Exception ex,
                                            Form owner = null,
                                            ExceptionHandling handle = ExceptionHandling.WindowsMessageBox,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "")
        {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            AidLog logs = new AidLog(functionName);
            bool unexpected = true;
            if ((ex is AggregateException))
            {   
                //	想定内の例外。
                unexpected = false;
                logs.Exception(ex, unexpected);
                throw (ex as AggregateException).Flatten();
            }
            switch (handle)
            {
                case ExceptionHandling.WindowsMessageBox:
                    MessageBox.Show(msg);
                    break;

                case ExceptionHandling.MessageDialog:
                    MessageDialog dia = new MessageDialog();
                    dia.Subject = MessageBoxIcon.Error;
                    dia.UpdateData();
                    dia.Item = new Models.StructureOperatorMessageItem();
                    dia.Item.Title = ex.Message;
                    dia.Item.Text = ex.StackTrace;
                    if (owner == null)
                    {
                        dia.ShowDialog();
                    }
                    else
                    {
                        dia.ShowDialog(owner);
                    }
                    break;

                case ExceptionHandling.LogOnly:

                    break;
            }
            logs.Exception(ex, unexpected);
            return msg;
        }

        /// <summary>
        /// System.Diagnostics.Processクラス使用時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string ProcessFilter(Exception ex,
                                            Form owner = null,
                                            ExceptionHandling handle = ExceptionHandling.WindowsMessageBox,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "")
        {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            AidLog logs = new AidLog(functionName);
            bool unexpected = true;
            if ((ex is Win32Exception) ||
                (ex is ObjectDisposedException) ||
                (ex is FileNotFoundException))
            { unexpected = false; }   //	想定内の例外。
            switch (handle)
            {
                //ウィンドウズ標準メッセージボックス
                case ExceptionHandling.WindowsMessageBox:
                    MessageBox.Show(msg);
                    break;

                    //独自メッセージボックス
                case ExceptionHandling.MessageDialog:
                    MessageDialog dia = new MessageDialog();
                    dia.Subject = MessageBoxIcon.Error;
                    //メッセージダイアログの初期化
                    dia.UpdateData();
                    dia.Item = new Models.StructureOperatorMessageItem();
                    dia.Item.Title = ex.Message;
                    dia.Item.Text = ex.StackTrace;
                    //親フォームとの紐づけ
                    if (owner == null)
                    {
                        dia.ShowDialog();
                    }
                    else
                    {
                        dia.ShowDialog(owner);
                    }
                    break;
                    
                    //ログ出力のみ
                case ExceptionHandling.LogOnly: break;
            }
            logs.Exception(ex, unexpected);
            return msg;
        }

    }
}
