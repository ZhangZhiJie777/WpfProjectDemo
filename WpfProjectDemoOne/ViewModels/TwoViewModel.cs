using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfProjectDemoOne.Core;
using WpfProjectDemoOne.Services;

namespace WpfProjectDemoOne.ViewModels
{
    public class TwoViewModel : INotifyPropertyChanged
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

        public RelayCommand TestRelayCommand { get; set; }

        public RelayCommand ClientRelayCommand { get; set; }

        public TwoViewModel()
        {
            TestRelayCommand = new RelayCommand(DoTest, CanDoTest);
            ClientRelayCommand = new RelayCommand(ClientDoTest, ClientanDoTest);
        }


        #region 服务端
        
        public void DoTest(object? parameter)
        {
            Task.Run(() => StartGrpcServer());
        }

        public bool CanDoTest(object? parameter)
        {
            return true;
        }

        // 服务端实现方法
        private void StartGrpcServer()
        {
            // 创建 gRPC 服务端实例
            var server = new Server
            {
                Services = { Calculator.BindService(new CalculatorService()) },  // 绑定服务
                Ports = { new ServerPort("localhost", 5001, ServerCredentials.Insecure) } // 设置端口
            };

            // 启动服务器
            server.Start();
            //Dispatcher.Invoke(() =>
            //{
            //    MessageBox.Show("gRPC Server Started on localhost:5001");
            //});

            // 阻止服务端线程退出
            server.ShutdownTask.Wait();
        }

        #endregion



        #region 客户端

        public async void ClientDoTest(object? parameter)
        {
            await StartGrpcClient();
        }

        public bool ClientanDoTest(object? parameter)
        {
            return true;
        }


        private async Task StartGrpcClient()
        {
            // 创建 gRPC 通道
            var channel = GrpcChannel.ForAddress("http://localhost:5001");

            // 创建客户端
            var client = new Calculator.CalculatorClient(channel);

            // 创建请求
            var request = new AddRequest { Num1 = 10, Num2 = 20 };

            try
            {
                // 调用服务端的 Add 方法
                //var response = client.Add(request); // 同步调用
                var response = await client.AddAsync(request);

                // 显示响应结果
                MessageBox.Show($"gRPC Client Response: {response.Result}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calling server: {ex.Message}");
            }
        }
        #endregion

    }
}
