// Decompiled with JetBrains decompiler
// Type: Dx.SDK.SerialPortFixer
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;


namespace Dx.SDK
{
    public class SerialPortFixer : IDisposable
    {
        private const int DcbFlagAbortOnError = 14;
        private const int CommStateRetries = 10;
        private SafeFileHandle m_Handle;

        public static void Execute(string portName)
        {
            using (new SerialPortFixer(portName))
                ;
        }

        public static List<string> GetPortNames(string alt = null)
        {
            if (System.Environment.OSVersion.Platform == PlatformID.Unix || System.Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                var allPorts = new List<string>();

                if (Directory.Exists("/dev/"))
                {
                    // cleanup now
                    GC.Collect();
                    // mono is failing in here on linux "too many open files"
                    try
                    {
                        if (Directory.Exists("/dev/serial/by-id/"))
                            allPorts.AddRange(Directory.GetFiles("/dev/serial/by-id/", "*"));
                    }
                    catch
                    {
                    }

                    try
                    {
                        allPorts.AddRange(Directory.GetFiles("/dev/", "ttyACM*"));
                    }
                    catch
                    {
                    }

                    try
                    {
                        allPorts.AddRange(Directory.GetFiles("/dev/", "ttyUSB*"));
                    }
                    catch
                    {
                    }

                    try
                    {
                        allPorts.AddRange(Directory.GetFiles("/dev/", "rfcomm*"));
                    }
                    catch
                    {
                    }

                    try
                    {
                        allPorts.AddRange(Directory.GetFiles("/dev/", "*usb*"));
                    }
                    catch
                    {
                    }

                    try
                    {
                        allPorts.AddRange(Directory.GetFiles("/dev/", "tty.*"));
                    }
                    catch
                    {
                    }

                    try
                    {
                        allPorts.AddRange(Directory.GetFiles("/dev/", "cu.*"));
                    }
                    catch
                    {
                    }
                }

                string[] ports = null;

                try
                {
                    ports = System.IO.Ports.SerialPort.GetPortNames();
                    // any exceptions will still result in a list
                    ports = ports.Select(p => p?.TrimEnd()).ToArray();
                }
                catch
                {
                }

                if (ports != null)
                    allPorts.AddRange(ports);

                return allPorts.Distinct().ToList().FindAll((p) => { return p.Contains(alt != null ? alt: "CP2104"); });
            }
            else if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var stringList = new List<string>();

                ManagementObjectCollection objectCollection = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'").Get();
                foreach (ManagementObject managementObject in objectCollection)
                {
                    if (managementObject["Name"].ToString().Contains(!String.IsNullOrEmpty(alt) ? alt : "Silicon Labs CP210x"))
                    {
                        managementObject["Name"].ToString();
                        int startIndex = managementObject["Name"].ToString().IndexOf("(") + 1;
                        int length = managementObject["Name"].ToString().IndexOf(")") - startIndex;
                        string str = managementObject["Name"].ToString().Substring(startIndex, length);
                        if (!stringList.Contains(str))
                            stringList.Add(str);
                    }
                }
                return stringList.Distinct().ToList();
            }
            return new List<string>();
        }

        public static string GetNiceName(string port)
        {
            //TODO: get nice name
            return "";
        }

        public void Dispose()
        {
            if (this.m_Handle == null)
                return;
            this.m_Handle.Close();
            this.m_Handle = (SafeFileHandle)null;
        }

        private SerialPortFixer(string portName)
        {
            SafeFileHandle hFile = portName != null && portName.StartsWith("COM", StringComparison.OrdinalIgnoreCase) ? SerialPortFixer.CreateFile("\\\\.\\" + portName, -1073741824, 0, IntPtr.Zero, 3, 1073741824, IntPtr.Zero) : throw new ArgumentException("Invalid Serial Port", nameof(portName));
            if (hFile.IsInvalid)
                SerialPortFixer.WinIoError();
            try
            {
                int fileType = SerialPortFixer.GetFileType(hFile);
                if (fileType != 2 && fileType != 0)
                    throw new ArgumentException("Invalid Serial Port", nameof(portName));
                this.m_Handle = hFile;
                this.InitializeDcb();
            }
            catch
            {
                hFile.Close();
                this.m_Handle = (SafeFileHandle)null;
                throw;
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int FormatMessage(
          int dwFlags,
          HandleRef lpSource,
          int dwMessageId,
          int dwLanguageId,
          StringBuilder lpBuffer,
          int nSize,
          IntPtr arguments);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetCommState(SafeFileHandle hFile, ref SerialPortFixer.Dcb lpDcb);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetCommState(SafeFileHandle hFile, ref SerialPortFixer.Dcb lpDcb);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ClearCommError(
          SafeFileHandle hFile,
          ref int lpErrors,
          ref SerialPortFixer.Comstat lpStat);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern SafeFileHandle CreateFile(
          string lpFileName,
          int dwDesiredAccess,
          int dwShareMode,
          IntPtr securityAttrs,
          int dwCreationDisposition,
          int dwFlagsAndAttributes,
          IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int GetFileType(SafeFileHandle hFile);

        private void InitializeDcb()
        {
            SerialPortFixer.Dcb lpDcb = new SerialPortFixer.Dcb();
            this.GetCommStateNative(ref lpDcb);
            lpDcb.Flags &= 4294950911U;
            this.SetCommStateNative(ref lpDcb);
        }

        private static string GetMessage(int errorCode)
        {
            StringBuilder lpBuffer = new StringBuilder(512);
            return SerialPortFixer.FormatMessage(12800, new HandleRef((object)null, IntPtr.Zero), errorCode, 0, lpBuffer, lpBuffer.Capacity, IntPtr.Zero) != 0 ? lpBuffer.ToString() : "Unknown Error";
        }

        private static int MakeHrFromErrorCode(int errorCode) => -2147024896 | errorCode;

        private static void WinIoError()
        {
            int lastWin32Error = Marshal.GetLastWin32Error();
            throw new IOException(SerialPortFixer.GetMessage(lastWin32Error), SerialPortFixer.MakeHrFromErrorCode(lastWin32Error));
        }

        private void GetCommStateNative(ref SerialPortFixer.Dcb lpDcb)
        {
            int lpErrors = 0;
            SerialPortFixer.Comstat lpStat = new SerialPortFixer.Comstat();
            for (int index = 0; index < 10; ++index)
            {
                if (!SerialPortFixer.ClearCommError(this.m_Handle, ref lpErrors, ref lpStat))
                    SerialPortFixer.WinIoError();
                if (SerialPortFixer.GetCommState(this.m_Handle, ref lpDcb))
                    break;
                if (index == 9)
                    SerialPortFixer.WinIoError();
            }
        }

        private void SetCommStateNative(ref SerialPortFixer.Dcb lpDcb)
        {
            int lpErrors = 0;
            SerialPortFixer.Comstat lpStat = new SerialPortFixer.Comstat();
            for (int index = 0; index < 10; ++index)
            {
                if (!SerialPortFixer.ClearCommError(this.m_Handle, ref lpErrors, ref lpStat))
                    SerialPortFixer.WinIoError();
                if (SerialPortFixer.SetCommState(this.m_Handle, ref lpDcb))
                    break;
                if (index == 9)
                    SerialPortFixer.WinIoError();
            }
        }

        private struct Comstat
        {
            public readonly uint Flags;
            public readonly uint cbInQue;
            public readonly uint cbOutQue;
        }

        private struct Dcb
        {
            public readonly uint DCBlength;
            public readonly uint BaudRate;
            public uint Flags;
            public readonly ushort wReserved;
            public readonly ushort XonLim;
            public readonly ushort XoffLim;
            public readonly byte ByteSize;
            public readonly byte Parity;
            public readonly byte StopBits;
            public readonly byte XonChar;
            public readonly byte XoffChar;
            public readonly byte ErrorChar;
            public readonly byte EofChar;
            public readonly byte EvtChar;
            public readonly ushort wReserved1;
        }
    }
}
