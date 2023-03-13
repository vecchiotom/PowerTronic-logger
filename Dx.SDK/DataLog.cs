// Decompiled with JetBrains decompiler
// Type: Dx.SDK.DataLog
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Dx.SDK
{
    public class DataLog
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void LogInfoToFile(
          Type type,
          DataLog.LogType logType,
          string data,
          Exception ex = null,
          [CallerLineNumber] int lineNumber = 0,
          [CallerMemberName] string caller = null)
        {
            try
            {
                string path1 = "C:\\RD\\EventLog.log";
                string path2 = "C:\\RD\\";
                if (!Directory.Exists(path2))
                    Directory.CreateDirectory(path2);
                StreamWriter streamWriter = new StreamWriter(path1, true);
                string str1 = "  " + (object)logType;
                string str2 = "---" + (object)DateTime.Now.Date;
                string str3 = "---" + (object)type;
                string str4 = "---" + data;
                string str5 = "";
                string str6 = "";
                string str7 = " at line " + (object)lineNumber + " (" + caller + ")";
                if (ex != null)
                {
                    str5 = "  Message :" + ex.Message;
                    str6 = "  StackTrace :" + ex.StackTrace;
                }
                string str8 = str1 + str2 + str3 + str7 + str4 + Environment.NewLine + str5 + str6;
                DataLog.logger.Error((object)(Environment.NewLine + Environment.NewLine + str8 + Environment.NewLine + Environment.NewLine));
                streamWriter.WriteLine(str8);
                streamWriter.WriteLine();
                streamWriter.Close();
            }
            catch (Exception ex1)
            {
                int num = 0 + 6;
            }
        }

        public enum LogType
        {
            Info,
            Warn,
            Error,
            Exception,
        }
    }
}
