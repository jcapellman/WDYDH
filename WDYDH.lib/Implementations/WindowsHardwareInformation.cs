using System.Linq;
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
        public override CPUInformation? GetCPUInformation()
        {
            var cpuInformation = new CPUInformation();

            try
            {
                var searchCollection = new ManagementObjectSearcher("select * from Win32_Processor").Get();

                ManagementObject? wmiObject = searchCollection.OfType<ManagementObject>().FirstOrDefault();

                if (wmiObject == null)
                {
                    return null;
                }

                cpuInformation.CPUManufacturer = wmiObject["Manufacturer"].ToString().Trim();
                cpuInformation.CPUName = wmiObject["Name"].ToString().Trim();
                cpuInformation.CoreCount = Convert.ToInt32(wmiObject["NumberOfCores"]);
                cpuInformation.LogicalCoreCount = Convert.ToInt32(wmiObject["NumberOfLogicalProcessors"]);
                cpuInformation.CPUCoreSpeed = wmiObject["MaxClockSpeed"].ToString();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

            return cpuInformation;
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
    }
}
