// Decompiled with JetBrains decompiler
// Type: Dx.SDK.WriteCommand
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.IO;
using System.Reflection;

namespace Dx.SDK
{
    public class WriteCommand : ICommand
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(WriteCommand));

        public Serial Serial { get; set; }

        public byte[] sendData { get; set; }

        public void Execute()
        {
            try
            {
                if (this.sendData != null)
                {
                    ECUManager.Instance.PauseRealTime();
                    ECUManager.Instance.publishResponseToQueue((object)this.Serial.CallWriteCommand(this.sendData), "ECU_WRITE_RESPONSE");
                }
                else
                    ECUManager.Instance.publishResponseToQueue((object)"ECU Data not well formed", "SDK_ERROR");
            }
            catch (IOException ex)
            {
                if (WriteCommand.logger.IsErrorEnabled)
                    WriteCommand.logger.Error((object)"Error in realtime command", (Exception)ex);
                ECUManager.Instance.publishResponseToQueue((object)"Connection error!!", "CONNECTION_ERROR");
            }
            catch (InvalidOperationException ex)
            {
                if (WriteCommand.logger.IsErrorEnabled)
                    WriteCommand.logger.Error((object)"Error in realtime command", (Exception)ex);
                ECUManager.Instance.publishResponseToQueue((object)"Error realtime data to ECU", "SDK_ERROR");
            }
            catch (Exception ex)
            {
                if (WriteCommand.logger.IsErrorEnabled)
                    WriteCommand.logger.Error((object)"Error in write command", ex);
                ECUManager.Instance.publishResponseToQueue((object)"Error sending data to ECU", "SDK_ERROR");
            }
            finally
            {
                ECUManager.Instance.ResumeRealTime(true);
            }
        }
    }
}
