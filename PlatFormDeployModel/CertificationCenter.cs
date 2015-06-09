using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    /// <summary>
    /// 认证中心
    /// </summary>
    public class CertificationCenter
    {
        /// <summary>
        /// 主调度IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 主调度端口
        /// </summary>
        public string SchedulingPort { get; set; }
        /// <summary>
        /// dc数据库IP
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// dc 数据库端口
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// dc 数据库SID
        /// </summary>
        public string Table { get; set; }
        /// <summary>
        /// dc 数据库用户名
        /// </summary>
        public string usr { get; set; }
        /// <summary>
        /// dc数据库密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public List<string> NodeIDList = new List<string>();
    }
}
