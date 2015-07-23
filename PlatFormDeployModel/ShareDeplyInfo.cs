using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatFormDeployModel
{
    public class ShareDeplyInfo
    {
        /// <summary>
        /// 主调度IP
        /// </summary>
        public string masterServerIP { get; set; }

        /// <summary>
        /// 主调度端口
        /// </summary>
        public string masterServerPort { get; set; }

        /// <summary>
        /// dc数据库IP
        /// </summary>
        public string DcIP { get; set; }

        /// <summary>
        /// dc 数据库端口
        /// </summary>
        public string DcPort { get; set; }

        /// <summary>
        /// dc 数据库SID
        /// </summary>
        public string DcSID { get; set; }

        /// <summary>
        /// dc 数据库用户名
        /// </summary>
        public string DcUsr { get; set; }

        /// <summary>
        /// dc数据库密码
        /// </summary>
        public string DcPwd { get; set; }

        /// <summary>
        /// Pro数据库ip
        /// </summary>
        public string ProIP { get; set; }

        /// <summary>
        /// Pro数据库端口
        /// </summary>
        public string ProPort { get; set; }

        /// <summary>
        /// Pro数据库SID
        /// </summary>
        public string ProSID { get; set; }

        /// <summary>
        /// Pro数据库用户名
        /// </summary>
        public string ProUSER { get; set; }
        /// <summary>
        /// Pro数据库密码
        /// </summary>
        public string ProPWD { get; set; }
    }
}
