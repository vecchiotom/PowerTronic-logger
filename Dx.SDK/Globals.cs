// Decompiled with JetBrains decompiler
// Type: Dx.SDK.Globals
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.Reflection;
using System.Text;

namespace Dx.SDK
{
    public class Globals
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string GetStringFromBytes(byte[] data)
        {
            try
            {
                if (data == null)
                    return "";
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte num in data)
                    stringBuilder.Append(num).Append(',');
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                Globals.logger.Error((object)"Error while converting byte array to string");
                return "";
            }
        }
    }
}
