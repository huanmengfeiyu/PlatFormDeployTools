﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    public class MasterScheduler : IPlatFormDeployInfo
    {
        /// <summary>
        /// 主调度IP
        /// </summary>
        public string serverIP { get; set; }
        /// <summary>
        /// 主调度端口
        /// </summary>
        public string serverPort { get; set; }
        /// <summary>
        /// Telnet端口
        /// </summary>
        public string telnetPort { get; set; }
        /// <summary>
        /// Pro数据库ip
        /// </summary>
        public string ProIP { get; set; }
        /// <summary>
        /// Pro数据库端口
        /// </summary>
        public string ProPort { get; set; }
        /// <summary>
        /// Pro数据库链接
        /// </summary>
        public string ProSID { get; set; }
        /// <summary>
        /// Pro数据库用户名
        /// </summary>
        public string Prousr { get; set; }
        /// <summary>
        /// Pro数据库密码
        /// </summary>
        public string Propwd { get; set; }
    }
}
