using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    public class API
    {
        /// <summary>
        /// 主调度IP
        /// </summary>
        public string DCIP { get; set; }
        /// <summary>
        /// 主调度端口
        /// </summary>
        public string DCPort { get; set; }
        /// <summary>
        /// 节点ID  
        /// </summary>
        public List<string> NodeIdList = new List<string>();
    }
}
 