using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Yutai.Security
{
	public class CErrorLog
	{
		private static string m_ErrorLogFullPath;

		private static string m_ErrorLogFileName;

		public static string ErrorLogFileName
		{
			get
			{
				return CErrorLog.m_ErrorLogFileName;
			}
			set
			{
				CErrorLog.m_ErrorLogFileName = value;
			}
		}

		public static string ErrorLogFullPath
		{
			get
			{
				return CErrorLog.m_ErrorLogFullPath;
			}
			set
			{
				string str = value;
				if (str.Substring(str.Length - 1).Equals("\\"))
				{
					CErrorLog.m_ErrorLogFullPath = str;
				}
				else
				{
					CErrorLog.m_ErrorLogFullPath = str.Insert(str.Length, "\\");
				}
			}
		}

		static CErrorLog()
		{
			CErrorLog.old_acctor_mc();
		}

		public CErrorLog()
		{
		}

		public static void backupErrorLog()
		{
			string str = string.Concat(CErrorLog.m_ErrorLogFullPath, CErrorLog.m_ErrorLogFileName, ".log");
			try
			{
				if (File.Exists(str))
				{
					string[] mErrorLogFullPath = new string[] { CErrorLog.m_ErrorLogFullPath, CErrorLog.m_ErrorLogFileName, "_", null, null, null, null };
					DateTime now = DateTime.Now;
					mErrorLogFullPath[3] = now.ToShortDateString();
					mErrorLogFullPath[4] = "_";
					now = DateTime.Now;
					mErrorLogFullPath[5] = now.ToLongTimeString().Replace(":", "-");
					mErrorLogFullPath[6] = ".bak";
					File.Copy(str, string.Concat(mErrorLogFullPath), true);
				}
			}
			catch (Exception exception)
			{
				Trace.WriteLine(exception);
			}
		}

		public static void clearErrorLog()
		{
			string str = string.Concat(CErrorLog.m_ErrorLogFullPath, CErrorLog.m_ErrorLogFileName, ".log");
			StreamWriter streamWriter = null;
			try
			{
				if (!File.Exists(str))
				{
					return;
				}
				else
				{
					streamWriter = File.CreateText(str);
				}
			}
			catch (Exception exception)
			{
				Trace.WriteLine(exception);
				return;
			}
			try
			{
				try
				{
					streamWriter.Write("");
				}
				catch (Exception exception1)
				{
					Trace.WriteLine(exception1);
				}
			}
			finally
			{
				streamWriter.Flush();
				streamWriter.Close();
			}
		}

		public static void deleteErrorLog(bool bool_0)
		{
			string str = string.Concat(CErrorLog.m_ErrorLogFullPath, CErrorLog.m_ErrorLogFileName, ".log");
			try
			{
				if (File.Exists(str))
				{
					if (bool_0)
					{
						CErrorLog.backupErrorLog();
					}
					File.Delete(str);
				}
			}
			catch (Exception exception)
			{
				Trace.WriteLine(exception);
			}
		}

		private static void old_acctor_mc()
		{
			CErrorLog.m_ErrorLogFileName = "ErrorLog";
		}

		public static void writeErrorLog(object object_0, Exception exception_0, string string_0)
		{
			string str = string.Concat(CErrorLog.m_ErrorLogFullPath, CErrorLog.m_ErrorLogFileName, ".log");
			StreamWriter streamWriter = null;
			try
			{
				streamWriter = File.AppendText(str);
			}
			catch (Exception exception)
			{
				Trace.WriteLine(exception);
				return;
			}
			try
			{
				try
				{
					streamWriter.WriteLine();
					streamWriter.WriteLine("----------------------------------------------");
					streamWriter.WriteLine(string.Concat("错误信息：", string_0));
					string str1 = "";
					if (object_0 != null)
					{
						str1 = object_0.ToString();
					}
					streamWriter.WriteLine(string.Concat("对象信息：", str1));
					string str2 = "";
					if (exception_0 != null)
					{
						str2 = exception_0.ToString();
					}
					streamWriter.WriteLine(string.Concat("异常信息：", str2));
					string longDateString = DateTime.Now.ToLongDateString();
					DateTime now = DateTime.Now;
					streamWriter.WriteLine(string.Concat("时间：", longDateString, " ", now.ToLongTimeString()));
					streamWriter.WriteLine(string.Concat("主机名：", Dns.GetHostName()));
				}
				catch (Exception exception1)
				{
					Trace.WriteLine(exception1);
				}
			}
			finally
			{
				streamWriter.Flush();
				streamWriter.Close();
			}
		}
	}
}