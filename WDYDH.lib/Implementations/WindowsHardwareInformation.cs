using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.Versioning;

using WDYDH.lib.Objects;
using WDYDH.lib.Windows.Abstractions;

namespace WDYDH.lib.Implementations
{
    [SupportedOSPlatform("windows")]
    public class WindowsHardwareInformation : BaseHardwareInformation
    {
        public override BIOSInformation GetBIOSInformation()
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
                } else
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

        public override List<CPUInformation> GetCPUInformation()
        {
            var cpuList = new List<CPUInformation>();

            try
            {
                var searchCollection = new ManagementObjectSearcher("select * from Win32_Processor").Get();

                foreach (var wmiObject in searchCollection)
                {
                    var cpuInformation = new CPUInformation();

                    cpuInformation.CPUManufacturer = wmiObject["Manufacturer"].ToString().Trim();
                    cpuInformation.CPUName = wmiObject["Name"].ToString().Trim();
                    cpuInformation.CoreCount = Convert.ToInt32(wmiObject["NumberOfCores"]);
                    cpuInformation.LogicalCoreCount = Convert.ToInt32(wmiObject["NumberOfLogicalProcessors"]);
                    cpuInformation.CPUCoreSpeed = wmiObject["MaxClockSpeed"].ToString();

                    cpuList.Add(cpuInformation);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

            return cpuList;
        }

        public override List<NetworkAdapter>? GetNetworkAdapters()
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
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

            return networkAdapters;
        }

        public override List<StorageDevice>? GetStorageDevices()
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
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

            return storageDevices;
        }

        public override List<SystemMemory> GetSystemMemory()
        {
            var systemMemory = new List<SystemMemory>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

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
}