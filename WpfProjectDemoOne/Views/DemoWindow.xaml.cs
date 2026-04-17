using System;
using System.Windows;
using AvalonDock.Layout;

namespace WpfProjectDemoOne.Views
{
    /// <summary>
    /// DemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DemoWindow : Window
    {
        public DemoWindow()
        {
            InitializeComponent();
            AppendOutput("Workspace ready.");
        }

        private void NewWorkspaceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StartPageDocument.IsActive = true;
            SetStatus("已切换到新的工作区视图");
            AppendOutput("New workspace opened.");
        }

        private void OpenLayoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditorDocument.IsActive = true;
            SetStatus("已打开布局预览");
            AppendOutput("Layout preview opened.");
        }

        private void SaveLayoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("布局已保存");
            AppendOutput("Layout saved.");
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UndoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("撤销操作已触发");
            AppendOutput("Undo invoked.");
        }

        private void CutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("剪切操作已触发");
            AppendOutput("Cut invoked.");
        }

        private void CopyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("复制操作已触发");
            AppendOutput("Copy invoked.");
        }

        private void PasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetStatus("粘贴操作已触发");
            AppendOutput("Paste invoked.");
        }

        private void ToolboxMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(ToolboxAnchorable, "工具箱");
        }

        private void SolutionExplorerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(SolutionExplorerAnchorable, "解决方案资源管理器");
        }

        private void PropertiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(PropertiesAnchorable, "属性窗口");
        }

        private void OutputMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(OutputAnchorable, "输出窗口");
        }

        private void RunDemoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditorDocument.IsActive = true;
            SetStatus("演示运行中");
            AppendOutput("Demo run started.");
        }

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

        private void OptionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(PropertiesAnchorable, "选项面板");
        }

        private void WelcomeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StartPageDocument.IsActive = true;
            SetStatus("欢迎页已激活");
            AppendOutput("Welcome page focused.");
        }

        private void SupportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActivateAnchorable(OutputAnchorable, "支持信息");
            AppendOutput("Support entry selected.");
        }

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

        private void ActivateAnchorable(LayoutAnchorable anchorable, string panelName)
        {
            ShowAnchorable(anchorable);
            anchorable.IsActive = true;
            SetStatus($"{panelName}已激活");
            AppendOutput($"{panelName} activated.");
        }

        private static void ShowAnchorable(LayoutAnchorable anchorable)
        {
            if (anchorable.IsHidden)
            {
                anchorable.Show();
            }
        }

        private void SetStatus(string message)
        {
            StatusTextBlock.Text = message;
        }

        private void AppendOutput(string message)
        {
            var prefix = string.IsNullOrWhiteSpace(OutputTextBox.Text) ? string.Empty : Environment.NewLine;
            OutputTextBox.Text += $"{prefix}> {DateTime.Now:HH:mm:ss}  {message}";
            OutputTextBox.ScrollToEnd();
        }
    }
}
