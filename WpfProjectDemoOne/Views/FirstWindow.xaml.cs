using System;
using System.Collections.Generic;
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

namespace WpfProjectDemoOne.Views
{
    /// <summary>
    /// FirstWindow.xaml 的交互逻辑
    /// 这里负责界面初始化和绑定 ViewModel 到 DataContext
    /// </summary>
    public partial class FirstWindow : Window
    {
        public FirstWindow()
        {
            InitializeComponent();

            // 设置运行时的 DataContext，绑定 ViewModel 实例
            // 创建 ViewModel 实例并绑定到窗口的 DataContext
            // 这样 XAML 中的绑定表达式会在这个 ViewModel 上寻找属性和命令            
            this.DataContext = new ViewModels.FirstViewModel();
        }


    }
}
