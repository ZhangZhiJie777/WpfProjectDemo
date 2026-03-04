using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfProjectDemoOne.Core
{
    /// <summary>
    /// 通用命令类，实现 ICommand 接口，类似作为一个中转器，将界面按钮等控件与 ViewModel 中的方法绑定
    /// 用于将界面按钮等控件与 ViewModel 中的方法绑定
    /// </summary>
    public class RelayCommand : ICommand
    {

        // 要执行的逻辑方法（例如按钮点击后执行的内容）
        private readonly Action<object?> _execute;       

        // 判断命令是否可以执行的逻辑（可为空，表示总是可执行）
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        /// 当命令的可执行状态发生变化时触发（界面会重新评估按钮是否可用）
        /// 初始化时，所有实现了ICommandSource 的事件，都会自动注册 CommandManager.RequerySuggested 事件
        /// 当界面元素状态发生变化时，会自动触发此事件，这个事件会通知所有控件的 0nCanExecuteChanged 事件,
        /// 0nCanExecuteChanged事件会调用 ICommand.CanExecute 方法，根据返回的 bool 值，决定控件的 isEnabled 属性
        /// 这里的value，就是控件的 0nCanExecuteChanged 事件
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 构造函数，传入要执行的方法
        /// </summary>
        /// <param name="execute">执行逻辑的方法</param>
        /// <param name="canExecute">判断是否可执行的方法（可选）</param>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// 判断命令是否可以执行（如果未提供 canExecute，默认返回 true）        
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// 执行命令绑定的方法
        /// </summary>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
        
    }
}
