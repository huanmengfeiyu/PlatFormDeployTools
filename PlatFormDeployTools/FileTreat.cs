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
                    TableContent tableContent = new TableContent();
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
                    if (item.Name.StartsWith("D"))
                    {
                        return;
                    }
                    ProjectContainer.tableContents.First(x => x.directoryPath == path).SubFileList.Add(new SubTableConent() { SubName = item.Name, SubFilePath = item.FullName });
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
        public static void SetMasterScheduler(OperateAffairs operateAffair)
        {
            TableContent zhuDiaoDu = ProjectContainer.tableContents.Find(x => x.FileName.Equals("主调度"));
            INIOperationClass.INIWriteValue(zhuDiaoDu.SubFileList[0].SubFilePath + "\\config.ini", "self", "serverIP",ProjectContainer.shareDeployInfo.masterServerIP);
            INIOperationClass.INIWriteValue(zhuDiaoDu.SubFileList[0].SubFilePath + "\\config.ini", "self", "serverPort", ProjectContainer.shareDeployInfo.masterServerPort);
            INIOperationClass.INIWriteValue(zhuDiaoDu.SubFileList[0].SubFilePath + "\\config.ini", "self", "telnetPort", (operateAffair.platFormDeployInfo as MasterScheduler).telnetPort);
            INIOperationClass.INIWriteValue(zhuDiaoDu.SubFileList[0].SubFilePath+"\\config.ini","db","SID",)
        }
        /// <summary>
        /// 设置子调度
        /// </summary>+
        /// <param name="IP"></param>
        /// <param name="PORT"></param>
        /// <param name="NODEID"></param>
        /// <param name="DB"></param>
        /// <param name="USER"></param>
        /// <param name="PWD"></param>
        public static void SetChildSchedule(OperateAffairs operateAffair)    
        {
            TableContent ziDiaoDu = ProjectContainer.tableContents.Find(x => x.FileName.Equals("子调度"));
            if (operateAffair.platFormOperandType == PlatFormOperandType.添加)
            {
                FileOperate.CopyFolder(ziDiaoDu.SubFileList[0].SubFilePath, ziDiaoDu.directoryPath + "\\" + (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[0]);
                INIOperationClass.INIWriteValue(ziDiaoDu.directoryPath + "\\" + (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[0] + "\\config.ini", "Sub Dispatch", "NODEID", (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[0]);
            }
            else if (operateAffair.platFormOperandType == PlatFormOperandType.修改)
            {
                FileOperate.RenameDir(ziDiaoDu.directoryPath + "\\" + (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[0], ziDiaoDu.directoryPath + "\\" + (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[1]);
                INIOperationClass.INIWriteValue(ziDiaoDu.directoryPath + "\\" + (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[1] + "\\config.ini", "Sub Dispatch", "NODEID", (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[1]);
            }
            else if (operateAffair.platFormOperandType == PlatFormOperandType.删除)
            {
                FileOperate.RenameDir(ziDiaoDu.directoryPath + "\\" + (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[0], ziDiaoDu.directoryPath + "\\" + "D" + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + (operateAffair.platFormDeployInfo as ChildSchedule).NODEIDList[0]);
            }
            else
            {

            }
            //ChildSchedule childSchedule = new ChildSchedule();
            //childSchedule.IP = IP;
            //childSchedule.PORT = PORT;
            ////childSchedule.NODEID = NODEID;
            //childSchedule.DB = DB;
            //childSchedule.USER = USER;
            //childSchedule.PWD = PWD;
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

        public static void Execute()
        {
            if (0 < ProjectContainer.oprateAffairsList.Count)
            {
                for (int i = 0; i < ProjectContainer.oprateAffairsList.Count; i++)
                {
                    switch (ProjectContainer.oprateAffairsList[i].platFormOperand)
                    {
                        case PlatFormOperand.主调度:
                            SetMasterScheduler(ProjectContainer.oprateAffairsList[i]);
                            break;
                        case PlatFormOperand.子调度:
                            SetChildSchedule(ProjectContainer.oprateAffairsList[i]);
                            break;
                        case PlatFormOperand.设备调度:
                            break;
                        case PlatFormOperand.应用调度:
                            break;
                        case PlatFormOperand.认证中心:
                            break;
                        case PlatFormOperand.权限中心:
                            break;
                        case PlatFormOperand.采集器:
                            break;
                        case PlatFormOperand.前置机:
                            break;
                        case PlatFormOperand.API:
                            break;
                        case PlatFormOperand.监控中心:
                            break;
                        case PlatFormOperand.HTTPAPI:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
