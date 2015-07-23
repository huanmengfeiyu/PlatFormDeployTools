using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    /// <summary>
    /// 权限中心
    /// </summary>
    public class CompetenceCenter : IPlatFormDeployInfo
    {
        /// <summary>
        /// 主调度IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 主调度端口
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// dc数据库链接
        /// </summary>
        public string sid { get; set; }
        /// <summary>
        /// dc 数据库用户名
        /// </summary>
        public string usr { get; set; }
        /// <summary>
        /// dc数据库密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public List<string> nodeidList { get; set; }
        public CompetenceCenter()
        {
            nodeidList = new List<string>();
        }
    }
}
