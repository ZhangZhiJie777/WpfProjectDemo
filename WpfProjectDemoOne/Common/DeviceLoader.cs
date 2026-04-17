using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfProjectDemoOne.Models;

namespace WpfProjectDemoOne.Common
{
    public class DeviceLoader
    {
        private readonly string _configPath;

        public DeviceLoader(string configPath)
        {
            _configPath = configPath;
        }

        public List<DeviceConfig> LoadDevices()
        {
            var json = File.ReadAllText(_configPath, Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<DeviceConfig>>(json);
        }

        public Type GetDeviceType(string typeName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName == typeName);
        }
    }
}
