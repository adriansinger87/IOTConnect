﻿using IOTConnect.Domain.System.Logging;
using IOTConnect.Persistence.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Persistence
{
    [TestClass]
    public class LogTests
    {
        [TestInitialize]
        public void Arrange()
        {
            Log.Inject(new NLogger());
        }

        [TestCleanup]
        public void Cleanup()
        {
            // cleanup
        }

        [TestMethod]
        public void TestNLogger()
        {
            // pre-assert
            Assert.IsTrue(Log.IsNotNull, "logger should be injected");

            // act
            Log.Trace("eine Trace Nachricht");
            Log.Debug("eine Debug Nachricht");
            Log.Info("eine Info Nachricht");
            Log.Warn("eine Warn Nachricht");
            Log.Error("eine Error Nachricht");

            try
            {
                Throw();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }
        }

        private void Throw()
        {
            try
            {
                ThrowInner();
            }
            catch (Exception ex)
            {
                throw new Exception("an exception was thrown", ex);
            }
        }

        private void ThrowInner()
        {
            throw new Exception("throw inner ex");
        }

    }
}
