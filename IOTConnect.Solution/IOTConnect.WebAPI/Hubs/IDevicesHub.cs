using System.Threading.Tasks;

namespace IOTConnect.WebAPI.Hubs
{
    public interface IDevicesHub
    {
        Task MqttReceived(string topic, string message);
    }
}
