// Decompiled with JetBrains decompiler
// Type: Dx.SDK.SDKConstants
// Assembly: Dx.SDK, Version=3.0.1.0, Culture=neutral, PublicKeyToken=059ecd15ff0f36d8
// MVID: 0DBCEE9A-D322-49EE-8A82-2549055149A1
// Assembly location: C:\Program Files (x86)\PowerTRONIC R-Tune 3.0\Dx.SDK.dll

namespace Dx.SDK
{
    public sealed class SDKConstants
    {
        public const string ECU_WRITE_DEVICE_DATA_TOPIC = "ECU_WRITE_DEVICE_DATA";
        public const string ECU_CONTEXTFORM_RESPONSE_TOPIC = "ECU_CONTEXTFORM_RESPONSE";
        public const string ECU_TPS_READ_DATA_TOPIC = "ECU_TPS_REAL_TIME_DATA";
        public const string ECU_TPS_WRITE_RESPONSE_TOPIC = "ECU_TPS_WRITE_DATA";
        public const string ECU_READ_MAP_DATA_TOPIC = "ECU_MAP_READ_DATA";
        public const string ECU_LOAD_EXT_DATA_TOPIC = "ECU_LOAD_EXT_DATA";
        public const string ECU_READ_DATA_TOPIC = "ECU_READ_DATA";
        public const string ECU_REAL_TIME_DATA_TOPIC = "ECU_REAL_TIME_DATA";
        public const string ECU_CONNECT_RESPONSE_TOPIC = "ECU_CONNECT_RESPONSE";
        public const string ECU_NORMAL_CONNECT_TO_DEVICE_RESPONSE_TOPIC = "ECU_NORMAL_CONNECT_TO_DEVICE_RESPONSE";
        public const string ECU_CONNECT_TO_BOOTLOADER_RECOVERY_RESPONSE_TOPIC = "ECU_CONNECT_BOOTLOADER_RECOVERY_RESPONSE";
        public const string ECU_WRITE_RESPONSE_TOPIC = "ECU_WRITE_RESPONSE";
        public const string ECU_BURN_RESPONSE_TOPIC = "ECU_BURN_RESPONSE";
        public const string ECU_LOCK_BURN_RESPONSE_TOPIC = "ECU_LOCK_BURN_RESPONSE_TOPIC";
        public const string ECU_BOOTLOADER_RESPONSE_TOPIC = "ECU_ROOTLOADER_RESPONSE";
        public const string ECU_FIRMWAREUPGRADE_RESPONSE_TOPIC = "ECU_FIRMWAREUPGRADE_RESPONSE";
        public const string ECU_UPGRADE_FIRMWARE_RESPONSE_TOPIC_STEP_1 = "ECU_UPGRADE_FIRMWARE_STEP_1_RESPONSE";
        public const string ECU_UPGRADE_FIRMWARE_RESPONSE_TOPIC_STEP_2 = "ECU_UPGRADE_FIRMWARE_STEP_2_RESPONSE";
        public const string ECU_UPGRADE_FIRMWARE_RESPONSE_TOPIC_STEP_3 = "ECU_UPGRADE_FIRMWARE_STEP_3_RESPONSE";
        public const string ECU_UPGRADE_FIRMWARE_RESPONSE_TOPIC_STEP_4 = "ECU_UPGRADE_FIRMWARE_STEP_4_RESPONSE";
        public const string ECU_UPGRADE_FIRMWARE_RESPONSE_PROC_2_TOPIC_STEP_1 = "ECU_UPGRADE_FIRMWARE_PROC_2_STEP_1_RESPONSE";
        public const string ECU_FIRMWARE_UPGRADE_REC_BLM_ENTER_TOPIC = "ECU_FIRMWARE_UPGRADE_REC_BLM_ENTER_TOPIC_RESPONSE";
        public const string ECU_FIRMWARE_UPGRADE_SEND_CHAR_A_TOPIC = "ECU_FIRMWARE_UPGRADE_SEND_CHAR_A_TOPIC_RESPONSE";
        public const string ECU_FIRMWARE_UPGRADE_SEND_CHAR_B_TOPIC = "ECU_FIRMWARE_UPGRADE_SEND_CHAR_B_TOPIC_RESPONSE";
        public const string ECU_FIRMWARE_UPGRADE_SEND_CHAR_D_TOPIC = "ECU_FIRMWARE_UPGRADE_SEND_CHAR_D_TOPIC_RESPONSE";
        public const string ECU_FIRMWARE_UPGRADE_REC_PROC_2_TOPIC = "ECU_FIRMWARE_UPGRADE_REC_PROC_2_TOPIC_RESPONSE";
        public const string ECU_READ = "ECU_READ_RESPONCE";
        public const string SDK_ERROR_TOPIC = "SDK_ERROR";
        public const string SDK_READ_TOPIC = "SDK_READ";
        public const string CONNECTION_ERROR_TOPIC = "CONNECTION_ERROR";
        public const int REAL_TIME_COMMAND_DELAY = 90;
        public const int REALTIME_TIMEOUT = 1500;
        public const int CONNECT_TIMEOUT = 1000;
        public const int READ_TIMEOUT = 4500;
        public const int WRITE_TIMEOUT = 10000;
        public const int BURN_TIMEOUT = 1500;
    }
}
