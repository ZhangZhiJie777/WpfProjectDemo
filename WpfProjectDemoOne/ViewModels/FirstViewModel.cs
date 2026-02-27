using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfProjectDemoOne.Core;

namespace WpfProjectDemoOne.ViewModels
{
    public class FirstViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged 接口实现

        /// <summary>
        /// 属性变化事件，通知界面刷新绑定的属性
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 触发属性变化通知事件(不属于接口实现，但是属于实际开发中用来“手动通知界面属性变更”的标准写法。)
        /// </summary>
        /// <param name="propertyName">发生变化的属性名</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // 命令
        public RelayCommand relayCommand { get; }
        public RelayCommand relay1Command { get; }

        public FirstViewModel()
        {
            relayCommand = new RelayCommand(SayHello, CanSayHello);
            relay1Command = new RelayCommand(SayHello, CanSayHello);
        }

        #region 属性        
        private string _buttonText = "初始按钮";
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged(nameof(ButtonText)); // 通知界面刷新绑定的属性
                }
            }
        }

        private string _textBoxText = "承太郎";

        public string TextBoxText
        {
            get { return _textBoxText; }
            set 
            { 
                _textBoxText = value;
                OnPropertyChanged(nameof(TextBoxText));
            }
        }

        #endregion



        public void SayHello(object? parameter)
        {
            // 假设我们传入一个名字字符串
            string name = parameter?.ToString() ?? "未知用户";

            // 生成返回值（也可以是调用服务返回的）
            string result = $"你好，{name}，现在是 {DateTime.Now:HH:mm:ss}";

            // 修改属性，让按钮显示该返回值
            ButtonText = result;

            MessageBox.Show("Hello World");

            DateTime dt = DateTime.Now;
            dt = CrossDayProcess(dt);
        }


        private bool CanSayHello(object? parameter)
        {
           return true; // 你也可以根据状态控制按钮是否可用
        }


        public static DateTime CrossDayProcess(DateTime time_data)
        {
            try
            {
                Double plctime_delta = -86400000;
                time_data = time_data.AddMilliseconds(plctime_delta); // 误差超12小时，数据时间往前推一天


                // 解决跨天问题
                plctime_delta = (DateTime.Now - time_data).TotalMilliseconds;
                if (plctime_delta < -43200000)
                {
                    plctime_delta = -86400000;
                    time_data = time_data.AddMilliseconds(plctime_delta); // 误差超12小时，数据时间往前推一天
                }
                else if (plctime_delta > 43200000)
                {
                    plctime_delta = 86400000;
                    time_data = time_data.AddMilliseconds(plctime_delta); // 误差超12小时，数据时间往后推一天
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return time_data;
        }

    }
}
