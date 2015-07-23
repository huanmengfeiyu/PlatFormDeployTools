using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    public class HTTPAPI : IPlatFormDeployInfo
    {
        /// <summary>
        /// 服务端口
        /// </summary>
        public List<string> PortList { get; set; }
        /// <summary>
        /// Dc数据库
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// DC数据库用户名
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// DC数据库密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// Dc数据库2
        /// </summary>
        public string Database2 { get; set; }
        /// <summary>
        /// DC数据库用户名2
        /// </summary>
        public string User2 { get; set; }
        /// <summary>
        /// DC数据库密码2
        /// </summary>
        public string Pwd2 { get; set; }
        public HTTPAPI()
        {
            PortList = new List<string>();
        }
    }
}
