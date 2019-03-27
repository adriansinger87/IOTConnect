using IOTConnect.Domain.Models.IoT;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.Context
{
    public interface IContextable
    {
        List<string> GetAllResources();

        IDevice GetResource(string id, out bool found);

        object[] GetData(string id);

    }
}
