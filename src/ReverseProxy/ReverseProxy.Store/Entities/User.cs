using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Store.Entities
{
    public class User
    {
        public long Id { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        public List<string> Role { get; set; }
        public List<string> Permission { get; set; }
    }
}
