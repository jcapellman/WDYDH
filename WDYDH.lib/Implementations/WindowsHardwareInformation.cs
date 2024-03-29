﻿using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.Versioning;

using WDYDH.lib.Objects;
using WDYDH.lib.Windows.Abstractions;

namespace WDYDH.lib.Implementations
{
    [SupportedOSPlatform("windows")]
    public class WindowsHardwareInformation : BaseHardwareInformation
    {
        public override BIOSInformation BIOSInformation
        {
            get
            {
                var biosInformation = new BIOSInformation();

                try
                {
                    var searchCollection = new ManagementObjectSearcher("select * from Win32_BIOS").Get();

                    ManagementObject? wmiObject = searchCollection.OfType<ManagementObject>().FirstOrDefault();

                    if (wmiObject == null)
                    {
                        return null;
                    }

                    if (wmiObject["BIOSVersion"] is string)
                    {
                        biosInformation.Name = wmiObject["BIOSVersion"].ToString().Trim();
                    }
                    else
                    {
                        biosInformation.Name = string.Join(" ", (string[])wmiObject["BIOSVersion"]);
                    }

                    biosInformation.Manufacturer = wmiObject["Manufacturer"].ToString();
                    biosInformation.SerialNumber = wmiObject["SerialNumber"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

                return biosInformation;
            }
        }

        public override List<CPUInformation> CPUInformation
        {
            get
            {
                var cpuList = new List<CPUInformation>();

                try
                {
                    var searchCollection = new ManagementObjectSearcher("select * from Win32_Processor").Get();

                    foreach (var wmiObject in searchCollection)
                    {
                        var cpuInformation = new CPUInformation
                        {
                            CPUManufacturer = wmiObject["Manufacturer"].ToString().Trim(),
                            CPUName = wmiObject["Name"].ToString().Trim(),
                            CoreCount = Convert.ToInt32(wmiObject["NumberOfCores"]),
                            LogicalCoreCount = Convert.ToInt32(wmiObject["NumberOfLogicalProcessors"]),
                            CPUCoreSpeed = wmiObject["MaxClockSpeed"].ToString()
                        };

                        cpuList.Add(cpuInformation);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

                return cpuList;
            }
        }

        public override List<NetworkAdapter>? NetworkAdapters
        {
            get
            {
                var networkAdapters = new List<NetworkAdapter>();

                try
                {
                    foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                    {
                        var networkAdapter = new NetworkAdapter
                        {
                            ConnectionName = nic.Name,
                            Name = nic.Description,
                            NICType = nic.NetworkInterfaceType.ToString(),
                            Status = nic.OperationalStatus.ToString(),
                            Speed = nic.Speed
                        };

                        networkAdapters.Add(networkAdapter);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

                return networkAdapters;
            }
        }

        public override List<StorageDevice>? StorageDevices
        {
            get
            {
                var storageDevices = new List<StorageDevice>();

                try
                {
                    var searchCollection = new ManagementObjectSearcher("select * from Win32_DiskDrive").Get();

                    foreach (var obj in searchCollection)
                    {
                        var drive = new StorageDevice
                        {
                            Name = obj["Model"].ToString(),
                            StorageType = obj["MediaType"].ToString(),
                            Size = Convert.ToUInt64(obj["Size"]),
                            Status = obj["Status"].ToString(),
                            Firmware = obj["FirmwareRevision"].ToString()
                        };

                        storageDevices.Add(drive);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

                return storageDevices;
            }
        }

        public override List<SystemMemory> SystemMemory
        {
            get
            {
                var systemMemory = new List<SystemMemory>();

                try
                {
                    ManagementObjectSearcher searcher = new ("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

                    foreach (var obj in searcher.Get())
                    {
                        var memory = new SystemMemory
                        {
                            Manufacturer = obj["Manufacturer"].ToString(),
                            ClockSpeed = Convert.ToInt32(obj["Speed"]),
                            SerialNumber = obj["SerialNumber"].ToString(),
                            Size = Convert.ToUInt64(obj["Capacity"])
                        };

                        systemMemory.Add(memory);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

                return systemMemory;
            }
        }

        public override List<Device> SystemDeviceDrivers
        {
            get
            {
                var systemDevices = new List<Device>();

                try
                {
                    ManagementObjectSearcher searcher = new("root\\CIMV2", "SELECT * FROM Win32_SystemDriver");

                    foreach (var obj in searcher.Get())
                    {
                        var memory = new Device
                        {
                            Name = obj["DisplayName"]?.ToString(),
                            Path = obj["PathName"]?.ToString(),
                            Status = obj["State"].ToString(),
                            Type = obj["ServiceType"]?.ToString()
                        };

                        systemDevices.Add(memory);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

                return systemDevices.OrderBy(a => a.Name).ToList();
            }
        }

        public override List<GPU> GPUs {
            get
            {
                var gpuList = new List<GPU>();

                try
                {
                    var searchCollection = new ManagementObjectSearcher("select * from Win32_VideoController").Get();

                    foreach (var wmiObject in searchCollection)
                    {
                        var gpuInformation = new GPU
                        {
                            DriverVersion = wmiObject["DriverVersion"].ToString().Trim(),
                            Name = wmiObject["Description"].ToString().Trim(),
                            Memory = (Convert.ToUInt64(wmiObject["AdapterRAM"]) / 1024) / 1024
                        };

                        gpuList.Add(gpuInformation);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

                return gpuList;
            }
        }

        public override List<Mouse> Mice
        {
            get
            {
                var mouseList = new List<Mouse>();

                try
                {
                    var searchCollection = new ManagementObjectSearcher("select * from Win32_PointingDevice").Get();

                    foreach (var wmiObject in searchCollection)
                    {
                        var mouse = new Mouse
                        {
                            Manufacturer = wmiObject["Manufacturer"].ToString().Trim(),
                            Name = wmiObject["Name"].ToString().Trim(),
                            DeviceID = wmiObject["DeviceID"].ToString().Trim()
                        };

                        mouseList.Add(mouse);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }

                return mouseList;
            }
        }
    }
}