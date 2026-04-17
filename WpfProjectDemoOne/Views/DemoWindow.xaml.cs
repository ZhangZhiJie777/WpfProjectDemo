using System;
using System.Windows;
using AvalonDock.Layout;

namespace WpfProjectDemoOne.Views
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑。
    /// 这个窗口主要用于演示一个类似 Visual Studio 的 AvalonDock 工作区。
    /// </summary>
    public partial class DemoWindow : Window
    {
        /// <summary>
        /// 构造函数：
        /// 1. 初始化界面组件
        /// 2. 写入初始输出日志
        /// 3. 绑定运行时 ViewModel
        /// </summary>
        public DemoWindow()
        {
            InitializeComponent();
            AppendOutput("Workspace ready.");
            
            this.DataContext = new ViewModels.DemoViewModel();
        }

        /// <summary>
        /// File -> New Workspace。
        /// 将焦点切回启动页，模拟新建工作区。
        /// </summary>
        private void NewWorkspaceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StartPageDocument.IsActive = true;
            SetStatus("已切换到新的工作区视图");
            AppendOutput("New workspace opened.");
        }

        /// <summary>
        /// File -> Open Layout。
        /// 激活中央编辑文档，模拟打开布局或设计页面。
        /// </summary>
        private void OpenLayoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditorDocument.IsActive = true;
            SetStatus("已打开布局预览");
            AppendOutput("Layout preview opened.");
        }

        /// <summary>
        /// File -> Save Layout。
        /// 当前示例只更新状态和日志，不执行真实持久化。
        /// </summary>
        private void SaveLayoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("布局已保存");
            AppendOutput("Layout saved.");
        }

        /// <summary>
        /// File -> Exit。
        /// 关闭当前窗口。
        /// </summary>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Edit -> Undo。
        /// 示例行为：更新状态并写日志。
        /// </summary>
        private void UndoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("撤销操作已触发");
            AppendOutput("Undo invoked.");
        }

        /// <summary>
        /// Edit -> Cut。
        /// 示例行为：更新状态并写日志。
        /// </summary>
        private void CutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("剪切操作已触发");
            AppendOutput("Cut invoked.");
        }

        /// <summary>
        /// Edit -> Copy。
        /// 示例行为：更新状态并写日志。
        /// </summary>
        private void CopyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("复制操作已触发");
            AppendOutput("Copy invoked.");
        }

        /// <summary>
        /// Edit -> Paste。
        /// 示例行为：更新状态并写日志。
        /// </summary>
        private void PasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("粘贴操作已触发");
            AppendOutput("Paste invoked.");
        }

        /// <summary>
        /// View -> Toolbox。
        /// 激活左侧工具箱。
        /// </summary>
        private void ToolboxMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(ToolboxAnchorable, "工具箱");
        }

        /// <summary>
        /// View -> Solution Explorer。
        /// 激活右侧解决方案资源管理器。
        /// </summary>
        private void SolutionExplorerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(SolutionExplorerAnchorable, "解决方案资源管理器");
        }

        /// <summary>
        /// View -> Properties。
        /// 激活右侧属性窗口。
        /// </summary>
        private void PropertiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(PropertiesAnchorable, "属性窗口");
        }

        /// <summary>
        /// View -> Output。
        /// 激活底部输出窗口。
        /// </summary>
        private void OutputMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(OutputAnchorable, "输出窗口");
        }

        /// <summary>
        /// Tools -> Run Demo。
        /// 激活中央编辑文档，并写入运行日志。
        /// </summary>
        private void RunDemoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditorDocument.IsActive = true;
            SetStatus("演示运行中");
            AppendOutput("Demo run started.");
        }

        /// <summary>
        /// Tools -> Reset Layout。
        /// 重新显示主要停靠窗口，并把焦点切回启动页。
        /// </summary>
        private void ResetLayoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowAnchorable(ToolboxAnchorable);
            ShowAnchorable(SolutionExplorerAnchorable);
            ShowAnchorable(PropertiesAnchorable);
            ShowAnchorable(OutputAnchorable);
            ShowAnchorable(ErrorsAnchorable);

            StartPageDocument.IsActive = true;
            DockManager.UpdateLayout();

            SetStatus("布局已重置");
            AppendOutput("Layout reset to default.");
        }

        /// <summary>
        /// Tools -> Options。
        /// 当前示例中复用属性窗口来模拟选项入口。
        /// </summary>
        private void OptionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(PropertiesAnchorable, "选项面板");
        }

        /// <summary>
        /// Help -> Welcome。
        /// 激活欢迎页。
        /// </summary>
        private void WelcomeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StartPageDocument.IsActive = true;
            SetStatus("欢迎页已激活");
            AppendOutput("Welcome page focused.");
        }

        /// <summary>
        /// Help -> Support。
        /// 当前示例中将支持信息切到输出窗口展示。
        /// </summary>
        private void SupportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(OutputAnchorable, "支持信息");
            AppendOutput("Support entry selected.");
        }

        /// <summary>
        /// Help -> About。
        /// 弹出关于对话框并记录日志。
        /// </summary>
        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                ".NET 8 + Dirkster.AvalonDock 4.70.0\nDemo Studio 是一个仿 Visual Studio 2022 风格的 AvalonDock 演示窗口。",
                "About Demo Studio",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            SetStatus("已显示关于窗口");
            AppendOutput("About dialog displayed.");
        }

        /// <summary>
        /// 激活一个可停靠面板。
        /// 如果面板此前被隐藏，会先把它显示出来。
        /// </summary>
        private void ActivateAnchorable(LayoutAnchorable anchorable, string panelName)
        {
            ShowAnchorable(anchorable);
            anchorable.IsActive = true;
            SetStatus($"{panelName}已激活");
            AppendOutput($"{panelName} activated.");
        }

        /// <summary>
        /// 若面板处于隐藏状态，则将其恢复显示。
        /// </summary>
        private static void ShowAnchorable(LayoutAnchorable anchorable)
        {
            if (anchorable.IsHidden)
            {
                anchorable.Show();
            }
        }

        /// <summary>
        /// 更新底部状态栏文本。
        /// </summary>
        private void SetStatus(string message)
        {
            StatusTextBlock.Text = message;
        }

        /// <summary>
        /// 向输出窗口追加日志。
        /// 会自动补换行、追加时间戳并滚动到底部。
        /// </summary>
        private void AppendOutput(string message)
        {
            var prefix = string.IsNullOrWhiteSpace(OutputTextBox.Text) ? string.Empty : Environment.NewLine;
            OutputTextBox.Text += $"{prefix}> {DateTime.Now:HH:mm:ss}  {message}";
            OutputTextBox.ScrollToEnd();
        }
    }
}
