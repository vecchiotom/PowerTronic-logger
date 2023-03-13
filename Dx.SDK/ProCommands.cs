// Decompiled with JetBrains decompiler
// Type: Dx.SDK.ProCommands
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using System;

namespace Dx.SDK
{
    internal class ProCommands : ICommand
    {
        public Serial Serial { get; set; }

        public byte[] sendData { get; set; }

        public void Execute()
        {
            try
            {
                if (this.sendData != null)
                {
                    ECUManager.Instance.PauseRealTime();
                    ECUManager.Instance.publishResponseToQueue((object)this.Serial.CallWriteDeviceIDCommand(this.sendData), "ECU_WRITE_DEVICE_DATA");
                }
                else
                    ECUManager.Instance.publishResponseToQueue((object)"ECU Data not well formed", "SDK_ERROR");
            }
            catch (Exception ex)
            {
                ECUManager.Instance.publishResponseToQueue((object)"Error Reading ECU data", "SDK_ERROR");
            }
            finally
            {
                ECUManager.Instance.ResumeRealTime(false);
            }
        }
    }
}
