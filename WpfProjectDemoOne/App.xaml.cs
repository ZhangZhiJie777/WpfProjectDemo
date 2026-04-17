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



            var demoWindow = new DemoWindow();
            demoWindow.Show();

        }


    }

}
