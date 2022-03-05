using System.Runtime.Versioning;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using WDYDH.lib.Implementations;

namespace WDYDH.Tests
{
    [TestClass]
    [SupportedOSPlatform("windows")]
    public class WindowsTests
    {
        [TestMethod]
        public void CPUInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var cpu = winInfo.GetCPUInformation();

            Assert.IsNotNull(cpu);
        }
    }
}