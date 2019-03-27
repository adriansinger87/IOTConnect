using IOTConnect.Application.Values;
using IOTConnect.Domain.Models.IoT;
using System;

namespace IOTConnect.Application.Devices
{
    public class SensorDevice : DeviceBase
    {
        // -- fields

        private const int _defaultBuffer = 1000;

        private int _dataBuffer;

        // -- constructors

        /// <summary>
        /// Default constructor instanciates the Data property and sets a Guid as Id property
        /// </summary>
        public SensorDevice() : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataBuffer"></param>
        public SensorDevice(string id, int dataBuffer = _defaultBuffer) : base(id)
        {
            _dataBuffer = dataBuffer;
            ClearData();
        }

        // -- methods

        public override void ClearData()
        {
            Data = new CircularBuffer<ValueState>(_dataBuffer);
        }

        public override object[] GetData()
        {
            return Data.ToArray();
        }

        public override string ToString() => $"{base.ToString()} last value: {Data?.Peek?.Value ?? "N/A"}";

        // -- properties

        /// <summary>
        /// Gets or sets the data of type ValueState of the sensor and stores it in a circular buffer
        /// </summary>
        public CircularBuffer<ValueState> Data { get; set; }
    }
}
