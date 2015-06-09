using PlatFormDeployModel;
using PlatFormDeployUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployTools
{
    public class FileTreat
    {
        /// <summary>
        /// 获取1级配置的文件目录
        /// </summary>
        /// <param name="path"></param>
        public void Get_TableContents(string path)
        {
            //List<TableContents> tableContentsList = new List<TableContents>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo item in directoryInfos)
                {
                    TableContents tableContent = new TableContents();
                    tableContent.FileName = item.Name;
                    tableContent.directoryPath = item.FullName;
                    ProjectContainer.tableContents.Add(tableContent);
                }
            }
            //return tableContentsList;
        }
        /// <summary>
        /// 获取2级配置的文件目录（节点级）
        /// </summary>
        /// <param name="path"></param>
        public void Get_SubTableContents(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                foreach (DirectoryInfo item in directoryInfos)
                {
                    ProjectContainer.tableContents.First(x => x.directoryPath == path).SubFileList.Add(new SubTableConents() { SubName = item.Name, SubFilePath = item.FullName });
                }
            }
        }

        /// <summary>
        /// 设置主调度
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="serverPort"></param>
        /// <param name="telnetPort"></param>
        /// <param name="SID"></param>
        /// <param name="usr"></param>
        /// <param name="pwd"></param>
        public void SetMasterScheduler(string serverIP, string serverPort, string telnetPort, string SID, string usr, string pwd)
        {
            MasterScheduler masterScheduler = new MasterScheduler();
            masterScheduler.serverIP = serverIP;
            masterScheduler.serverPort = serverPort;
            masterScheduler.telnetPort = telnetPort;
            masterScheduler.SID = SID;
            masterScheduler.usr = usr;
            masterScheduler.pwd = pwd;
            ProjectContainer.masterScheduler = masterScheduler;
        }
        /// <summary>
        /// 设置子调度
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="PORT"></param>
        /// <param name="NODEID"></param>
        /// <param name="DB"></param>
        /// <param name="USER"></param>
        /// <param name="PWD"></param>
        public void SetChildSchedule(string IP, string PORT, string NODEID, string DB, string USER, string PWD)
        {
            ChildSchedule childSchedule = new ChildSchedule();
            childSchedule.IP = IP;
            childSchedule.PORT = PORT;
            //childSchedule.NODEID = NODEID;
            childSchedule.DB = DB;
            childSchedule.USER = USER;
            childSchedule.PWD = PWD;
        }
        /// <summary>
        /// 设置设备调度
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="PORT"></param>
        /// <param name="nodeId"></param>
        public void SetEquipmentScheduling(string IP, string PORT, string nodeId)
        {
            EquipmentScheduling equipmentScheduling = new EquipmentScheduling();
            equipmentScheduling.IP = IP;
            equipmentScheduling.PORT = PORT;
            //equipmentScheduling.nodeId = nodeId;

        }
        /// <summary>
        /// 设置应用调度
        /// </summary> 
        /// <param name="IP"></param>
        /// <param name="PORT"></param>
        /// <param name="nodeId"></param>
        public void SetDispatch(string IP, string PORT, string nodeId)
        {

        }
    }
}
