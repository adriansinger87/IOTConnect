﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.IO
{
    /// <summary>
    /// This interface specifies how to deserialze a generic data object, mostly a string, into a domain or application specific type.
    /// </summary>
    public interface IDeserializable
    {
        /// <summary>
        /// Deserializes the data object into the target type T
        /// </summary>
        /// <typeparam name="T">the output target type T</typeparam>
        /// <param name="data">the input data</param>
        /// <returns>the output data of type T</returns>
        T Deserialize<T>(object data) where T : new();
    }
}