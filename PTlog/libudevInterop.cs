using System;
using System.IO;
using System.Runtime.InteropServices;

public static class LibudevInterop
{
    private const string LibudevLibrary = "libudev.so.1.6.4";

    [DllImport(LibudevLibrary)]
    private static extern IntPtr udev_new();

    [DllImport(LibudevLibrary)]
    private static extern void udev_unref(IntPtr udev);

    [DllImport(LibudevLibrary)]
    private static extern IntPtr udev_device_new_from_syspath(IntPtr udev, string syspath);

    [DllImport(LibudevLibrary)]
    private static extern IntPtr udev_device_get_property_value(IntPtr udev_device, string key);

    [DllImport(LibudevLibrary)]
    private static extern void udev_device_unref(IntPtr udev_device);

    public static string GetUsbDeviceName(string devicePath)
    {
        string deviceName = null;

        // Use libudev to get the device's friendly name
        IntPtr udev = udev_new();
        IntPtr udevDevice = udev_device_new_from_syspath(udev, devicePath);
        if (udevDevice != IntPtr.Zero)
        {
            deviceName = Marshal.PtrToStringAnsi(udev_device_get_property_value(udevDevice, "ID_MODEL"));
        }
        udev_device_unref(udevDevice);
        udev_unref(udev);

        return deviceName;
    }
}
