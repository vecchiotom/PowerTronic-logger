// Decompiled with JetBrains decompiler
// Type: Dx.SDK.ReadCommand
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.IO;
using System.Reflection;

namespace Dx.SDK
{
    public class ReadCommand : ICommand
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(ReadCommand));

        public Serial Serial { get; set; }

        public byte[] sendData { get; set; }

        public void Execute()
        {
            try
            {
                if (ReadCommand.logger.IsDebugEnabled)
                    ReadCommand.logger.Debug((object)"Calling ECU Read");
                ECUManager.Instance.PauseRealTime();
                ECUManager.Instance.publishResponseToQueue((object)this.Serial.CallReadCommand(), "ECU_READ_DATA");
            }
            catch (IOException ex)
            {
                if (ReadCommand.logger.IsErrorEnabled)
                    ReadCommand.logger.Error((object)"Error in realtime command", (Exception)ex);
                ECUManager.Instance.publishResponseToQueue((object)"Connection error!!", "CONNECTION_ERROR");
            }
            catch (InvalidOperationException ex)
            {
                if (ReadCommand.logger.IsErrorEnabled)
                    ReadCommand.logger.Error((object)"Error in read command", (Exception)ex);
                ECUManager.Instance.publishResponseToQueue((object)"Error Reading ECU data", "SDK_ERROR");
            }
            catch (Exception ex)
            {
                if (ReadCommand.logger.IsErrorEnabled)
                    ReadCommand.logger.Error((object)"Error in read command", ex);
                ECUManager.Instance.publishResponseToQueue((object)"Error Reading ECU data", "SDK_ERROR");
            }
            finally
            {
                ECUManager.Instance.ResumeRealTime(false);
            }
        }
    }
}
