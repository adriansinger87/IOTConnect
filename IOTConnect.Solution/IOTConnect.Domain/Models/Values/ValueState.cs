using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IOTConnect.Domain.Models.Values
{
    public class ValueState
    {
        // -- fields

        private long _time;
        private DateTime _date;

        // -- constructor

        public ValueState()
        {
            _date = new DateTime();
        }

        // -- methods

        public override string ToString()
        {
            return $"t: {Time}, v: {Value}";
        }

        // -- properties

        public object Value { get; set; }

        public DateTime UtcTime { get { return _date; } }

        public DateTime LocalTime { get { return UtcTime.ToLocalTime(); } }

        public long Time
        {
            get { return _time; }
            set
            {
                this._time = value;
                _date = DateTimeOffset.FromUnixTimeMilliseconds(value).UtcDateTime;
            }
        }
    }
}
