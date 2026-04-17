using AvalonDock.Layout;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfProjectDemoOne.Common;
using WpfProjectDemoOne.Models;

namespace WpfProjectDemoOne.Views
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestWindow : Window
    {
        private readonly DeviceLoader _deviceLoader;
        private List<DeviceConfig> _devices;

        private readonly Dictionary<string, LayoutDocument> _openDocs = new Dictionary<string, LayoutDocument>();


        public TestWindow()
        {
            InitializeComponent();

            _deviceLoader = new DeviceLoader("devices.json");
            _devices = _deviceLoader.LoadDevices();


            DeviceComboBox.Items.Clear();

            foreach (var item in _devices)
            {
                DeviceComboBox.Items.Add(item);
            }

            this.DataContext = new ViewModels.TestViewModel();
            
        }                    

        // 只存“打开状态”，不存 LayoutDocument
        private readonly HashSet<string> _openKeys = new HashSet<string>();


        private void DeviceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDevice = DeviceComboBox.SelectedItem as DeviceConfig;

            if (selectedDevice == null)
            {
                return;
            }

            string key = selectedDevice.DeviceName ?? "设备x";

            // 1. 如果已经打开，直接激活
            if (_openKeys.Contains(key))
            {
                // 不要用 DocumentPane.Children（拖动后会失效）
                var existDoc = DockManager.Layout.Descendents()
                    .OfType<LayoutDocument>()
                    .FirstOrDefault(x => x.Title == key);

                if (existDoc != null)
                {
                    existDoc.IsActive = true;
                    return;
                }

                // 防止状态残留
                _openKeys.Remove(key);
            }

            // 2. 创建页面
            var devicePage = CreateDevicePage(selectedDevice);

            if (devicePage == null)
            {
                return;
            }

            var doc = new LayoutDocument
            {
                Title = key,
                Content = devicePage
            };

            // 3. 找到“当前真正的 DocumentPane”
            var documentPane = DockManager.Layout.Descendents()
                .OfType<LayoutDocumentPane>()
                .FirstOrDefault();

            if (documentPane == null)
            {
                return;
            }

            documentPane.Children.Add(doc);

            doc.IsActive = true;

            _openKeys.Add(key);

            // 4. 关闭事件（只清状态，不手动 Remove）
            doc.Closed += (s, args) =>
            {
                _openKeys.Remove(key);
            };

            DockManager.UpdateLayout(); // 刷新


        }


        private UserControl CreateDevicePage(DeviceConfig device)
        {
            var assembly = typeof(TestWindow).Assembly;

            var type = assembly.GetType($"WpfProjectDemoOne.Views.{device.PageType}");

            if (type != null)
            {
                var page = (UserControl)Activator.CreateInstance(type);
                return page;
            }

            return null;
        }


              
    }
}
