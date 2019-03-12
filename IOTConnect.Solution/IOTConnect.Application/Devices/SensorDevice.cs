using IOTConnect.Application.Values;
using IOTConnect.Domain.Models.IoT;
using System;

namespace IOTConnect.Application.Devices
{
    public class SensorDevice : DeviceBase
    {
        // -- fields

        private const int _defaultBuffer = 1000;

        // -- constructors

        /// <summary>
        /// Default constructor instanciates the Data property and sets a Guid as Id property
        /// </summary>
        public SensorDevice() : base(Guid.NewGuid().ToString())
        {
            Data = new CircularBuffer<ValueState>(_defaultBuffer);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataBuffer"></param>
        public SensorDevice(string id, int dataBuffer = _defaultBuffer) : this()
        {
            base.Id = id;
            ClearData(dataBuffer);
        }

        // -- methods

        public void ClearData(int size = _defaultBuffer)
        {
            Data = new CircularBuffer<ValueState>(size);
        }

        public override string ToString()
        {
            return $"{base.ToString()} last value: {Data?.Peek?.Value ?? "N/A"}";
        }

        // -- properties

        /// <summary>
        /// Gets or sets the data of type ValueState of the sensor and stores it in a circular buffer
        /// </summary>
        public CircularBuffer<ValueState> Data { get; set; }
    }
}
