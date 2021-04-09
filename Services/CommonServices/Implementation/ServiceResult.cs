using Services.CommonServices.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CommonServices.Implementation
{
    public class ServiceResult<TData> : IServiceResult<TData>
    {
        public TData Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }

        public IServiceResult<TData> Fail(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccess = false;
            return this;
        }

        public IServiceResult<TData> Ok(TData data)
        {
            Data = data;
            IsSuccess = true;
            return this;
        }
    }
}
