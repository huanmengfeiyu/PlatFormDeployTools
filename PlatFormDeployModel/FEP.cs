using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    /// <summary>
    /// 前置机
    /// </summary>
    public class FEP : IPlatFormDeployInfo
    {
        /// <summary>
        /// 主调度IP
        /// </summary>
        public string mip { get; set; }
        /// <summary>
        /// 主调度端口
        /// </summary>
        public string mport { get; set; }
        public List<FEPNode> NodeList { get; set; }
        public FEP()
        {
            NodeList = new List<FEPNode>();
        }
    }
    public class FEPNode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string nodeid { get; set; }
        /// <summary>
        /// 管理WEB IP
        /// </summary>
        public string managerip { get; set; }
        /// <summary>
        /// 管理WEB端口
        /// </summary>
        public string managerport { get; set; }
    }
}
