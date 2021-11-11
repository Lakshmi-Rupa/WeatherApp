using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DBProject.Services
{
    public class ApiResponse<T>
    {
        public T ResponseBody { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
