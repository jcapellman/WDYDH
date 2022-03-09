using WDYDH.lib.Objects;

namespace WDYDH.lib.Windows.Abstractions
{
    public abstract class BaseHardwareInformation
    {
        public abstract BIOSInformation GetBIOSInformation();

        public abstract List<CPUInformation> GetCPUInformation();

        public abstract List<NetworkAdapter> GetNetworkAdapters();

        public abstract List<StorageDevice> GetStorageDevices();

        public abstract List<SystemMemory> GetSystemMemory();
    }
}