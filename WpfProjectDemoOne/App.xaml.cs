using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using WpfProjectDemoOne.Views;

namespace WpfProjectDemoOne
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// 重写OnStartup方法，在程序启动时执行自定义逻辑
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //var firstWindow = new FirstWindow();

            //firstWindow.Show();

            //var twoWindow = new TwoWindow();
            //twoWindow.Show();

            //var threeWindow = new ThreeWindow();
            //threeWindow.Show();

            //var testWindow = new TestWindow();
            //testWindow.Show();

            string path = @"E:\Project\GitHub\MySelf\WpfProjectDemo\WpfProjectDemoOne";

            foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                byte[] bytes = File.ReadAllBytes(file);

                string header = System.Text.Encoding.UTF8.GetString(bytes);

                if (header.StartsWith("%TSD-Header"))
                {
                    Console.WriteLine($"被加密文件: {file}");
                }
            }

            var demoWindow = new DemoWindow();
            demoWindow.Show();  

        }


    }

}
