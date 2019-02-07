using IOTConnect.Domain.IO;
using IOTConnect.Domain.System.Enumerations;
using IOTConnect.Persistence.IO.Imports;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Persistence.IO
{
    public class IOController : IPersistenceControllable
    {
        // -- fields

        private Dictionary<ExportTypes, IExportable> _exports;
        private Dictionary<ImportTypes, IImportable> _imports;

        // -- constructors

        public IOController()
        {
            InitExports();
            InitImports();
        }

        // -- methods

        public void InitExports()
        {
            _exports = new Dictionary<ExportTypes, IExportable>();

            // add new export types here
        }

        public void InitImports()
        {
            _imports = new Dictionary<ImportTypes, IImportable>();
            IImportable import;

            import = new JsonImporter();
            _imports.Add(import.Type, import);

            // add new import types here
        }

        /// <summary>
        /// Gibt die Export-Funktionalität entsprechend des Typs aus
        /// </summary>
        /// <param name="type">Die Art des Exports</param>
        /// <returns>die Export-Funktionalität</returns>
        public IExportable Exporter(ExportTypes type)
        {
            return _exports[type];
        }

        /// <summary>
        /// Gibt die Import-Funktionalität entsprechend des Typs aus
        /// </summary>
        /// <param name="type">Die Art des Imports</param>
        /// <returns>die Import-Funktionalität</returns>
        public IImportable Importer(ImportTypes type)
        {
            return _imports[type];
        }
    }
}
