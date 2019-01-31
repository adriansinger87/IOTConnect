using IOTConnect.Domain.System.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTests.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests
{
    public class BaseTests
    {
        [TestInitialize]
        public virtual void Arrange()
        {
            if (Log.IsNotNull == false)
            {
                Log.Inject(new TestLogger());
            }
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Log.Stop();
        }
    }
}
