using Analyzer;
using Grpc.Net.Client;
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
    public class ThreeViewModel : INotifyPropertyChanged
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

        public RelayCommand RelayCommand { get; set; }

        public ThreeViewModel()
        {
            RelayCommand = new RelayCommand(Test, CanTest);
        }

        private async void Test(object? param)
        {
            await CallGrpc();
        }

        private bool CanTest(object? param)
        {
            return true;
        }

        private async Task CallGrpc()
        {
            // 创建 gRPC 通道，连接到服务端
            var channel = GrpcChannel.ForAddress("http://192.168.8.59:5000");  // 服务端地址

            // 创建客户端
            var client = new AnalyzerAPI.AnalyzerAPIClient(channel);

            try
            {
                #region SetParameters
                // 设置参数请求
                // 创建请求消息
                var request = new SetParametersRequest
                {
                    Parameters = new Parameters
                    {
                        Pairs = { new NameValuePair { Name = "XPSSample", Value = 1 }, new NameValuePair { Name = "UCA", Value = 1100 } }
                    }
                };

                // 调用 SetParameters RPC 方法
                var response = await client.SetParametersAsync(request); // 异步调用 
                #endregion

                #region UpdateElectrodes
                //// 更新电极请求
                //var request = new UpdateElectrodesRequest();

                //var response = await client.UpdateElectrodesAsync(request);

                #endregion

                #region UpdateBoards
                //// 更新板卡请求
                //var request = new UpdateBoardsRequest();

                //var response = await client.UpdateBoardsAsync(request);
                #endregion

                #region UpdateAll
                //// 全部更新请求
                //var request = new UpdateAllRequest();

                //var response = await client.UpdateAllAsync(request);
                #endregion

                #region GetAllData
                // 获取所有数据请求
                var getAllDataRequest = new GetAllDataRequest();

                // 调用异步方法 GetAllDataAsync，获取数据
                var getAllDataResponse = await client.GetAllDataAsync(getAllDataRequest); 
                #endregion

                // 显示获取的数据
                MessageBox.Show($"Success: {response.Success}\nMessage: {response.Message}");


                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
