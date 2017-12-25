using System;

namespace DivisionWebGlobal.Models.Structure
{
    public static class DeviceExtensionMethods
    {
        /// <summary>
        /// Получить название устройства по его коду
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ExtractDeviceNameByCode(this IDevice device)
        {
            int _type = Convert.ToInt32(device.Type, 16);

            switch (_type)
            {
                case 1:
                    return "DV-RB 30";
                case 3:
                    return "DV-RB 30";
                case 6:
                    return "DV-RB 8";
                case 7:
                    return "DV-Sens 7";
                case 10:
                    return "DV-RB 1";
                case 14:
                    return "DV-RB 4";
                case 48:
                    return "30-входы";
                case 32:
                    return "DV-DM 1";
                case 36:
                    return "DV-APR 1";
                case 42:
                    return "DV-REG";
                case 50:
                    return "DV-RB 8 ОПС";
                case 102:
                    return "DV-Meteo";
                case 103:
                    return "DV-TMPRO";
                case 104:
                    return "DV-TMPR";
                case 205:
                    return "DV-Proxy 1";
                case 208:
                    return "DV-Proxy 2 AC";
                case 210:
                    return "DV-Proxy 2 Guest";
                case 212:
                    return "DV-IR 4";
                case 216:
                    return "DV-IR 8";
                case 223:
                    return "DV-IR 16";
                default:
                    return device.Type;
            }
        }
    }
}