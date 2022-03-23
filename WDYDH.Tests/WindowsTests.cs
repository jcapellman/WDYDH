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

            var cpu = winInfo.CPUInformation;

            Assert.IsNotNull(cpu);
        }

        [TestMethod]
        public void BIOSInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var bios = winInfo.BIOSInformation;

            Assert.IsNotNull(bios);
        }

        [TestMethod]
        public void NetworkInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var net = winInfo.NetworkAdapters;

            Assert.IsNotNull(net);
        }

        [TestMethod]
        public void GPUInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var gpu = winInfo.GPUs;

            Assert.IsNotNull(gpu);
        }

        [TestMethod]
        public void SystemDeviceDrivers()
        {
            var winInfo = new WindowsHardwareInformation();

            var devices = winInfo.SystemDeviceDrivers;

            Assert.IsNotNull(devices);
        }

        [TestMethod]
        public void DriveInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var drives = winInfo.StorageDevices;

            Assert.IsNotNull(drives);
        }

        [TestMethod]
        public void SystemMemory()
        {
            var winInfo = new WindowsHardwareInformation();

            var memory = winInfo.SystemMemory;

            Assert.IsNotNull(memory);
        }
    }
}