using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GGWP.Models.Entities;
using Newtonsoft.Json;

namespace GGWP.Models
{
    public class ResultModel
    {
        public string data;
        public object obj;
        public bool success;

        public ResultModel()
        {
            this.data = null;
            this.obj = null;
            this.success = false;
        }

        public void SetResults(object data, bool success)
        {
            this.success = success;
            this.obj = data;

            this.data = JsonConvert.SerializeObject(data);
        }
    }
}