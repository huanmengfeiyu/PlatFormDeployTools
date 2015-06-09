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
        public static MasterScheduler masterScheduler = new MasterScheduler();
        public static List<API> apiList = new List<API>();
        public static List<CertificationCenter> certificationCenterList = new List<CertificationCenter>();
        public static List<ChildSchedule> childScheduleList = new List<ChildSchedule>();
        public static List<Collector> collectorList = new List<Collector>();
        public static List<CompetenceCenter> competenceCenterList = new List<CompetenceCenter>();
        public static List<Dispatch> dispatchList = new List<Dispatch>();
        public static List<EquipmentScheduling> equipmentSchedulingList = new List<EquipmentScheduling>();
        public static List<FEP> FEPList = new List<FEP>();
        public static List<HTTPAPI> httpAPIList = new List<HTTPAPI>();
        public static List<MonitoringCenter> monitoringCenter = new List<MonitoringCenter>();
    }
}
