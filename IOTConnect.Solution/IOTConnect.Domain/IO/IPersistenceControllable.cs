using IOTConnect.Domain.System.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.IO
{
    /// <summary>
    /// Interface for controlling IO functionality based on strategy-pattern to access import and export conretions
    /// </summary>
    public interface IPersistenceControllable
    {
        /// <summary>
        /// Initalizes all export implementations
        /// </summary>
        void InitExports();

        /// <summary>
        /// Initializes all import implementations 
        /// </summary>
        void InitImports();

        /// <summary>
        /// Gets the concrete export functionality based on its enum identifier
        /// </summary>
        /// <param name="type">The enum to access the export instance</param>
        /// <returns>The export instance</returns>
        IExportable Exporter(ExportTypes type);

        /// <summary>
        /// Gets the concrete import functionality based on its enum identifier
        /// </summary>
        /// <param name="type">The enum to access the import instance</param>
        /// <returns>The import instance</returns>
        IImportable Importer(ImportTypes type);
    }
}
