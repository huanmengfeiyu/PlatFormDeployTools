using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    /// <summary>
    /// 应用调度
    /// </summary>
    public class Dispatch : IPlatFormDeployInfo
    {
        /// <summary>
        /// 主调度IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 主调度端口
        /// </summary>
        public string PORT { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public List<string> nodeIdList { get; set; }
        public Dispatch()
        {
            nodeIdList = new List<string>();
        }
    }
}
