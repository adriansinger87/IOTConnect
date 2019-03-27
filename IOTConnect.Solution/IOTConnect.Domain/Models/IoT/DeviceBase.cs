namespace IOTConnect.Domain.Models.IoT
{
    public abstract class DeviceBase : IDevice
    {

        // -- constructors

        public DeviceBase(string id)
        {
            Id = id;
        }

        // -- methods

        public abstract void ClearData();

        public abstract object[] GetData();

        public override string ToString() => $"{Id}: {Name}";
        
        // -- properties

        public string Id { get; set; }

        public string Name { get; set; }

    }
}
