using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace IOTConnect.Application.Values
{
    [DataContract(Name = "ValueState", Namespace = "http://linkedfactory.iwu.fraunhofer.de/M40")]
    public class ValueState
    {
        // -- fields

        private string _time;

        // -- constructor

        public ValueState()
        {
            UtcTime = new DateTime();
        }

        // -- methods

        private void SetTime(string input)
        {
            // z.B. 2019-02-01T19:25:38.030Z
            _time = input;
            try
            {
                DateTime dateValue;
                if (DateTime.TryParse(_time, out dateValue))
                {
                    UtcTime = dateValue;
                }
                else
                {
                    double milliseconds = DateTime.Now.Ticks;
                    bool success = double.TryParse(_time, out milliseconds);
                    if (success)
                    {
                        UtcTime = (new DateTime(1970, 1, 1)).AddMilliseconds(milliseconds);
                    }
                    else
                    {
                        UtcTime = DateTime.Now;
                    }
                }
            }
            catch
            {
                UtcTime = DateTime.Now;
            }
        }

        public override string ToString() => $"t: {UtcTime}, v: {Value}";

        // -- properties

        [DataMember(Name = "value")]
        public object Value { get; set; }

        public DateTime UtcTime { get; private set; }

        public DateTime LocalTime => UtcTime.ToLocalTime();

        [DataMember(Name = "timestamp")]
        public string Time
        {
            get { return _time; }
            set { SetTime(value); }
        }
    }
}
