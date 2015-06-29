using PlatFormDeployModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployTools
{
    public class ProjectContainer
    {
        public static List<TableContents> tableContents = new List<TableContents>();
        /// <summary>
        /// 朱调度
        /// </summary>
        public static MasterScheduler masterScheduler = new MasterScheduler();
        public static API api = new API();
        /// <summary>
        /// 认证中心
        /// </summary>
        public static CertificationCenter certificationCenter = new CertificationCenter();
        /// <summary>
        /// 子调度
        /// </summary>
        public static ChildSchedule childSchedule = new ChildSchedule();
        /// <summary>
        /// 采集器
        /// </summary>
        public static Collector collector = new Collector();
        /// <summary>
        /// 权限中心
        /// </summary>
        public static CompetenceCenter competenceCenter = new CompetenceCenter();
        /// <summary>
        /// 应用调度
        /// </summary>
        public static Dispatch dispatch = new Dispatch();
        /// <summary>
        /// 设备调度
        /// </summary>
        public static EquipmentScheduling equipmentScheduling = new EquipmentScheduling();
        /// <summary>
        /// 前置机
        /// </summary>
        public static FEP FEP = new FEP();
        public static HTTPAPI httpAPI = new HTTPAPI();
        /// <summary>
        /// 监控中心
        /// </summary>
        public static MonitoringCenter monitoringCenter = new MonitoringCenter();
    }
}
