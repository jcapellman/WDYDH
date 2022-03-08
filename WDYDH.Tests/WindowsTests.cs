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

        [TestMethod]
        public void BIOSInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var bios = winInfo.GetBIOSInformation();

            Assert.IsNotNull(bios);
        }

        [TestMethod]
        public void NetworkInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var net = winInfo.GetNetworkAdapters();

            Assert.IsNotNull(net);
        }

        [TestMethod]
        public void DriveInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var drives = winInfo.GetStorageDevices();

            Assert.IsNotNull(drives);
        }

        [TestMethod]
        public void SystemMemory()
        {
            var winInfo = new WindowsHardwareInformation();

            var memory = winInfo.GetSystemMemory();

            Assert.IsNotNull(memory);
        }
    }
}