// Decompiled with JetBrains decompiler
// Type: Dx.SDK.RealTimeCommand
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using log4net;
using System;
using System.IO;
using System.Reflection;

namespace Dx.SDK
{
    public class RealTimeCommand : ICommand
    {
        private static readonly ILog logger = LogManager.GetLogger(Assembly.GetExecutingAssembly(), typeof(RealTimeCommand));

        public Serial Serial { get; set; }

        public byte[] sendData { get; set; }

        public void Execute()
        {
            try
            {
                RealTimeData data = this.Serial.CallRealTimeCommand();
                ECUManager.Instance.publishResponseToQueue((object)data, "ECU_REAL_TIME_DATA");
                ECUManager.Instance.publishResponseToQueue((object)data, "ECU_TPS_REAL_TIME_DATA");
            }
            catch (IOException ex)
            {
                if (RealTimeCommand.logger.IsErrorEnabled)
                    RealTimeCommand.logger.Error((object)"Error in realtime command", (Exception)ex);
                ECUManager.Instance.Disconnect();
                ECUManager.Instance.publishResponseToQueue((object)"Connection error!!", "CONNECTION_ERROR");
            }
            catch (InvalidOperationException ex)
            {
                if (RealTimeCommand.logger.IsErrorEnabled)
                    RealTimeCommand.logger.Error((object)"Error in realtime command", (Exception)ex);
                ECUManager.Instance.publishResponseToQueue((object)"Error Reading realtime data2", "SDK_ERROR");
            }
            catch (Exception ex)
            {
                if (RealTimeCommand.logger.IsErrorEnabled)
                    RealTimeCommand.logger.Error((object)"Error in realtime command", ex);
                ECUManager.Instance.publishResponseToQueue((object)"Error Reading realtime data3", "SDK_ERROR");
            }
        }
    }
}
