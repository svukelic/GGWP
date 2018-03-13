using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GGWP.Models.Entities
{
    public class UserModel
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string rpass { get; set; }
        public string session { get; set; }
        public string ime { get; set; }
        //public string spol { get; set; }
        public string dob { get; set; }
        public string email { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}