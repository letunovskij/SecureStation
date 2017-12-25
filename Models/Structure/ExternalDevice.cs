using System;

namespace DivisionWebGlobal.Models.Structure
{
    public class ExternalDevice : IEquatable<ExternalDevice>, IComparable<ExternalDevice>, IDevice
    {
        // тип устройства: DV-RB или DV-IR
        public string Type { get; set; }
        // адрес устройства на DV-HEAD: 1, 2, 3, ...
        public int Address { get; set; }
        // порт устройства на DV-HEAD: 1, 2
        public int Port { get; set; }
        // является ли запись фейковой (бизнес-требования)
        public bool NullType { get; set; }
        //public static NullExternalDevice = new ExternalDevice ( ) 

        public ExternalDevice(string type, int address, int port)
        {
            this.Type = type;
            this.Address = address;
            this.Port = port;
            this.NullType = false;
        }

        public override string ToString()
        {
            String _port = Port == 0 ? "***" : Convert.ToString(Port);

            return "Устройство типа: " + Type + "; по адресу: " + Address + "; на порту: " + _port;
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

        public bool Equals(ConfigurationDevice other)
        {
            return this.Address.Equals(other.Address) &&
                this.Port.Equals(other.Port) &&
                (
                    object.ReferenceEquals(this.Type, other.Type) ||
                    this.Type != null &&
                    this.Type.Equals(other.Type)
                );
        }

        /// <summary>
        /// Сравнивает два устройства по номеру порта
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ExternalDevice other)
        {
            return this.Address.CompareTo(other.Address);
        }
    }
}