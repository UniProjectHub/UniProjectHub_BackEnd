using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ResponseModels
{
    public class BaseResponseModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
