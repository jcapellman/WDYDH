using WDYDH.lib.Objects;

namespace WDYDH.lib.Windows.Abstractions
{
    public abstract class BaseHardwareInformation
    {
        public abstract BIOSInformation BIOSInformation { get; }

        public abstract List<CPUInformation> CPUInformation { get; }

        public abstract List<NetworkAdapter> NetworkAdapters { get; }

        public abstract List<StorageDevice> StorageDevices { get; }

        public abstract List<SystemMemory> SystemMemory { get; }

        public abstract List<Device> SystemDeviceDrivers { get; }
    }
}