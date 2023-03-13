// Decompiled with JetBrains decompiler
// Type: Dx.SDK.SimpleCommandResponsetData
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

namespace Dx.SDK
{
    public class SimpleCommandResponsetData
    {
        private bool success = false;

        public bool Success
        {
            get => this.success;
            set => this.success = value;
        }

        public bool isInBootloaderMode { get; set; }

        public byte[] logData { get; set; }

        public byte[] logData2 { get; set; }

        public byte[] logData3 { get; set; }

        public bool success2 { get; set; }
    }
}
