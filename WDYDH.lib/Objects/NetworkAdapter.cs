namespace WDYDH.lib.Objects
{
    public class NetworkAdapter
    {
        public string ConnectionName { get; set; }

        public string Name { get; set; }

        public string NICType { get; set; }

        public string Status { get; set; }

        public long Speed { get; internal set; }
    }
}