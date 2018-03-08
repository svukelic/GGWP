using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GGWP.Models.Api
{
    public class ApiResponse
    {
        public string code { get; set; }
        public object payload { get; set; }
    }
}