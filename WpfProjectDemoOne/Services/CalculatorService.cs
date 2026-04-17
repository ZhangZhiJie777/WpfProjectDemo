using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDemoOne.Services
{
    public class CalculatorService : Calculator.CalculatorBase
    {
        public override Task<AddResponse> Add(AddRequest request, ServerCallContext context)
        {
            // 执行加法操作并返回结果
            var result = request.Num1 + request.Num2;
            return Task.FromResult(new AddResponse { Result = result });
        }
    }
}
