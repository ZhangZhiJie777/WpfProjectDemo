using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDemoOne.ViewModels
{
    public class DemoViewModel : INotifyPropertyChanged
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

        public DemoViewModel()
        {

        }
    }
}
