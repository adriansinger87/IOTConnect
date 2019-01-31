using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.System.Logging
{
    public static class Log
    {

        // -- fields

        private static ILoggable _logger;
        private static readonly string NO_LOGGER_EXCEPTION = "no logger instance is present";
        private static readonly string SEP = "-";

        // -- methods

        private static string ToMessage(string message, string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return message;
            }
            else
            {
                return $"{source} {SEP} {message}";
            }
        }

        public static void Trace(string msg, string src = null)
        {
            L.Trace(ToMessage(msg, src));
        }

        public static void Debug(string msg, string src = null)
        {
            L.Debug(ToMessage(msg, src));
        }

        public static void Info(string msg, string src = null)
        {
            L.Info(ToMessage(msg, src));
        }

        public static void Warn(string msg, string src = null)
        {
            L.Warn(ToMessage(msg, src));
        }

        public static void Error(string msg, string src = null)
        {
            L.Error(ToMessage(msg, src));
        }

        public static void Fatal(Exception ex)
        {
            L.Fatal(ex);
        }

        public static void Stop()
        {
            L.Stop();
        }

        /// <summary>
        /// Übernimmt eine fertig initialisierte Instanz vom Typ ILoggable und
        /// nutzt diese für den statiscchen Zugriff auf die Log-Funktionen.
        /// </summary>
        /// <param name="logger">Eine Objekt vom Typ ILoggalbe</param>
        public static void Inject(ILoggable logger)
        {
            L = logger;
        }

        // -- properties

        /// <summary>
        /// Gets the information, if the logger instance is present or not.
        /// </summary>
        public static bool IsNotNull { get { return _logger != null; } }

        private static ILoggable L
        {
            get
            {
                if (_logger != null)
                {
                    return _logger;
                }
                else
                {
                    throw new NullReferenceException(NO_LOGGER_EXCEPTION);
                }
            }
            set
            {
                if (value != null)
                {
                    _logger = value;
                }
                else
                {
                    throw new NullReferenceException(NO_LOGGER_EXCEPTION);
                }
            }
        }
    }
}
