using System;
using System.Collections.Generic;
using System.Text;

namespace IOTConnect.Domain.Context
{
    public interface IContextable
    {
        List<string> GetAllRessources();

        object[] GetData(string id);

    }
}
