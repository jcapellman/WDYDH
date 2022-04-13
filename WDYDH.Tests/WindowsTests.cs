using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Versioning;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using WDYDH.lib.Implementations;
using WDYDH.lib.Objects;

namespace WDYDH.Tests
{
    [TestClass]
    [SupportedOSPlatform("windows")]
    public class WindowsTests
    {
        public class HardwareInfo
        {
            public List<CPUInformation> CPUs { get; set; }

            public BIOSInformation Bios { get; set; }

            public List<NetworkAdapter> NetworkAdapters { get; set; }

            public List<GPU> GPUs { get; set; }

            public List<Mouse> Mice { get; set; }

            public List<StorageDevice> StorageDevices { get; set; }

            public List<SystemMemory> SystemMemories { get; set; }

            public List<Device> Devices { get; set; }
        }


        [TestMethod]
        public void AllTheThings()
        {
            var winInfo = new WindowsHardwareInformation();

            var hardwareInfo = new HardwareInfo
            {
                CPUs = winInfo.CPUInformation,
                Bios = winInfo.BIOSInformation,
                Devices = winInfo.SystemDeviceDrivers,
                GPUs = winInfo.GPUs,
                Mice = winInfo.Mice,
                NetworkAdapters = winInfo.NetworkAdapters ?? new List<NetworkAdapter>(),
                StorageDevices = winInfo.StorageDevices ?? new List<StorageDevice>(),
                SystemMemories = winInfo.SystemMemory,
            };

            var json = JsonSerializer.Serialize(hardwareInfo);

            System.IO.File.WriteAllText(System.IO.Path.Combine(AppContext.BaseDirectory, "hardware.json"), json);
        }

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
        public void MouseInformation()
        {
            var winInfo = new WindowsHardwareInformation();

            var mice = winInfo.Mice;

            Assert.IsNotNull(mice);
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