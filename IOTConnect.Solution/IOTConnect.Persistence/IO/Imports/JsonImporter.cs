using IOTConnect.Domain.IO;
using IOTConnect.Domain.System.Enumerations;
using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.IO.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Persistence.IO.Imports
{
    public class JsonImporter : IImportable
    {
        // -- fields

        private FileSettings _setting;

        string _importJson;

        // -- methods

        public IImportable Setup(IOSettingsBase setting)
        {
            if (setting is FileSettings)
            {
                _setting = setting as FileSettings;
            }
            else
            {
                Log.Error("The json import setting has the wrong type and was not accepted.");
            }
            return this;
        }

        public T ConvertWith<T>(IAdaptable adapter) where T : new()
        {
            return adapter.Adapt<T, string>(_importJson);
        }

        public IImportable Import()
        {
            _importJson = JsonIO.ReadJson(_setting.FullPath);
            return this;
        }

        // -- properties

        public ImportTypes Type { get { return ImportTypes.Json; } }

    }
}
