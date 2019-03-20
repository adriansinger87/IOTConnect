namespace IOTConnect.Domain.Models.IoT
{
    public interface IDevice
    {
        void ClearData();
        void ClearData(int buffer);

        string Id { get; set; }
        string Name { get; set; }
    }
}