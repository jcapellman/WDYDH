using WDYDH.lib.Objects;

namespace WDYDH.lib.Windows.Abstractions
{
    public abstract class BaseHardwareInformation
    {
        public abstract CPUInformation GetCPUInformation();

        public abstract List<NetworkAdapter> GetNetworkAdapters();

        public abstract List<StorageDevice> GetStorageDevices();
    }
}