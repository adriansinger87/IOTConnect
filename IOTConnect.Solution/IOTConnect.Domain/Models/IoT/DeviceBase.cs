namespace IOTConnect.Domain.Models.IoT
{
    public abstract class DeviceBase
    {

        // -- constructors

        public DeviceBase(string id)
        {
            Id = id;
        }

        // -- methods

        public override string ToString() => $"{Id}: {Name}";

        // -- properties

        public string Id { get; set; }

        public string Name { get; set; }

    }
}
