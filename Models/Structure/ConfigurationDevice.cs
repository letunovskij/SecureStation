using System;

namespace DivisionWebGlobal.Models.Structure
{
    // TODO: make as Enumerable

    public struct ConfigurationDevice : IEquatable<ConfigurationDevice>, IComparable<ConfigurationDevice>, IDevice
    {
        // тип устройства: DV-RB или DV-IR
        public string Type { get; set; }
        // адрес устройства на DV-HEAD: 1, 2, 3, ...
        public int Address { get; set; }
        // порт устройства на DV-HEAD: 1, 2
        public int Port { get; set; }
        // состояние устройства во время сканирования
        public string StateAfterScan { get; set; }
        // наличие ошибки
        public string FatalErrorFlag { get; set; }
        // счетчик ошибок. Сбрасывается при отключении питания
        public int ErrorCount { get; set; }
        // дата последней ошибки. Строка - месяц день время
        public string LastErrorDate;

        public ConfigurationDevice(string type, int address, int port, 
            string stateAfterScan, string fatalErrorFlag, int errorCount, string lastErrorDate)
        {
            this.Type = type;
            this.Address = address;
            this.Port = port;
            this.StateAfterScan = stateAfterScan;
            this.FatalErrorFlag = fatalErrorFlag;
            this.ErrorCount = errorCount;
            this.LastErrorDate = lastErrorDate;
        }

        public override String ToString()
        {
            String _port = Port == 0 ? "***" : Convert.ToString(Port);

            return "Устройство типа: " + Type + "; по адресу: " + Address + "; на порту: " + _port + "; состояние при сканировании:" +
                StateAfterScan + "; фатальная ошибка: " + FatalErrorFlag + "; счетчик ошибок: " + ErrorCount + ";  дата последней ошибки: " + LastErrorDate + ".";
        }

        public bool Equals(ConfigurationDevice other)
        {
            return this.Address.Equals(other.Address) &&
                this.Port.Equals(other.Port) &&
                this.ErrorCount.Equals(other.ErrorCount) &&
                (
                    object.ReferenceEquals(this.Type, other.Type) ||
                    this.Type != null &&
                    this.Type.Equals(other.Type)
                ) &&
                (
                    object.ReferenceEquals(this.StateAfterScan, other.StateAfterScan) ||
                    this.StateAfterScan != null &&
                    this.StateAfterScan.Equals(other.StateAfterScan)
                ) &&
                (
                    object.ReferenceEquals(this.FatalErrorFlag, other.FatalErrorFlag) ||
                    this.FatalErrorFlag != null &&
                    this.FatalErrorFlag.Equals(other.FatalErrorFlag)
                ) &&
                (
                    object.ReferenceEquals(this.LastErrorDate, other.LastErrorDate) ||
                    this.LastErrorDate != null &&
                    this.LastErrorDate.Equals(other.LastErrorDate)
                );
        }

        public bool Equals(ExternalDevice other)
        {
            return this.Address.Equals(other.Address) &&
                this.Port.Equals(other.Port) &&
                (
                    object.ReferenceEquals(this.Type, other.Type) ||
                    this.Type != null &&
                    this.Type.Equals(other.Type)
                );
        }

        public bool ReplaceErrorCodes()
        {
            //if (this.StateAfterScan.ToUpper() != "0F" || 
            //    this.FatalErrorFlag.ToUpper() != "0F" ||
            //    this.StateAfterScan.ToUpper() != "00" ||
            //    this.FatalErrorFlag.ToUpper() != "00") return false;

            StateAfterScan = StateAfterScan == "0F" ? "***" : "OK";
            FatalErrorFlag = FatalErrorFlag == "0F" ? "***" : "OK";

            return true;
        }

        /// <summary>
        /// Сравнивает два устройства по номеру порта
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ConfigurationDevice other)
        {
            return this.Address.CompareTo(other.Address);
        }

        /// <summary>
        /// Конвертирует ConfigurationDevice в ExternalDevice
        /// </summary>
        /// <returns></returns>
        public ExternalDevice ConvertToExternalDevice()
        {
            return new ExternalDevice(this.Type, this.Address, this.Port);
        }
    }
}