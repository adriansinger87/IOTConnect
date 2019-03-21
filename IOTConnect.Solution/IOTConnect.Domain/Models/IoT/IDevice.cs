namespace IOTConnect.Domain.Models.IoT
{
    public interface IDevice
    {
        void ClearData();

        // -- properties
        
        string Id { get; set; }
        string Name { get; set; }
    }
}