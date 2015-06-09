using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    public class ChildSchedule
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
        public List<string> NODEIDList = new List<string>();
        /// <summary>
        /// Pro数据库链接
        /// </summary>
        public string DB { get; set; }
        /// <summary>
        /// Pro数据库用户名
        /// </summary>
        public string USER { get; set; }
        /// <summary>
        /// Pro数据库密码
        /// </summary>
        public string PWD { get; set; }
    }
}
