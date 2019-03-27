namespace IOTConnect.Domain.Models.IoT
{
    public interface IDevice
    {
        void ClearData();
        object[] GetData();

        // -- properties

        string Id { get; set; }
        string Name { get; set; }
    }
}