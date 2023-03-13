// Decompiled with JetBrains decompiler
// Type: Dx.SDK.ResponseType
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

namespace Dx.SDK
{
    internal enum ResponseType : byte
    {
        CommandAckRecovery = 200, // 0xC8
        CommandTemp = 246, // 0xF6
        CommandComplete = 248, // 0xF8
        CommandAck = 252, // 0xFC
    }
}
