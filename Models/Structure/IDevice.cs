namespace DivisionWebGlobal.Models.Structure
{
    public interface IDevice
    {
        string Type { get; set; }
        // адрес устройства на DV-HEAD: 1, 2, 3, ...
        int Address { get; set; }
        // порт устройства на DV-HEAD: 1, 2
        int Port { get; set; }
    }
}
