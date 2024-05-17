using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ResponseModels
{
    public class BaseFailedResponseModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }
    }
}
