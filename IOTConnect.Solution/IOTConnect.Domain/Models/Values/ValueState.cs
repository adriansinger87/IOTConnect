using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IOTConnect.Domain.Models.Values
{
    [DataContract(Name = "ValueState", Namespace = "http://linkedfactory.iwu.fraunhofer.de/M40")]
    public class ValueState
    {
        // -- fields

        private string _time;
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

        [DataMember(Name = "value")]
        public object Value { get; set; }

        public DateTime UtcTime { get { return _date; } }

        public DateTime LocalTime { get { return UtcTime.ToLocalTime(); } }

        [DataMember(Name = "timestamp")]
        public string Time
        {
            get { return _time; }
            set
            {
                // z.B. 2019-02-01T19:25:38.030Z
                this._time = value;

                DateTime result;
                var success = DateTime.TryParse(value,null, global::System.Globalization.DateTimeStyles.RoundtripKind, out result);

                if (success)
                {
                    _date = result;
                }
                else
                {
                    _date = DateTime.Now; 
                }

            }
        }
    }
}
