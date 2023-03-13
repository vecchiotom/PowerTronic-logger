// Decompiled with JetBrains decompiler
// Type: Dx.SDK.Command
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

namespace Dx.SDK
{
    internal enum Command : byte
    {
        charLowerCaseA = 97, // 0x61
        charLowerCaseB = 98, // 0x62
        charLowerCaseD = 100, // 0x64
        charLowerCaseE = 101, // 0x65
        charLowerCaseF = 102, // 0x66
        ProcessorTwo = 232, // 0xE8
        CooberConnect = 245, // 0xF5
        WriteDeviceID = 245, // 0xF5
        Connect = 247, // 0xF7
        Burn = 249, // 0xF9
        ReadECUData = 250, // 0xFA
        Write = 251, // 0xFB
        RealTime = 253, // 0xFD
        BootLoaderBLM = 254, // 0xFE
    }
}
