using IOTConnect.Domain.Models.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.Models.IoT
{
    public class DeviceBase
    {
        private const int _defaultBuffer = 1000;

        // -- constructors

        public DeviceBase()
        {
            Data = new CircularBuffer<ValueState>(_defaultBuffer);
        }

        public DeviceBase(int dataBuffer)
        {
            ClearData(dataBuffer);
        }

        // -- methods

        public void ClearData(int size = _defaultBuffer)
        {
            Data = new CircularBuffer<ValueState>(size);
        }

        public override string ToString()
        {
            return $"{Id}: last value: {Data.Peek.Value}";
        }

        // -- properties

        public string Id { get; set; }

        public CircularBuffer<ValueState> Data { get; set; }
    }
}
