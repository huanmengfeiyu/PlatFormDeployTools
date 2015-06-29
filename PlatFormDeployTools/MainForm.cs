using PlatFormDeployModel;
using PlatFormDeployUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatFormDeployTools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //PromptText.Text = @"请确认所有信息已经填写完毕，确认后点击下面的 保存 按钮";

            panel2.Enabled = false;
            jiazai_button.Enabled = false;
            #region ceshishuju
            PathText.Text = @"C:\Users\FDFF\Desktop\平台部署成品——标准文件结构";
            #endregion

        }

        private void LiuLanBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog.SelectedPath.Trim() != "")
                {
                    PathText.Text = folderBrowserDialog.SelectedPath.Trim();
                    //TODO: 浏览的处理
                    
                    jiazai_button.Enabled = true;
                }
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 0; i++)
            {

            }
        }

        private void jiazai_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PathText.Text))
            {
                MessageBox.Show("请选择路径");
                return;
            }
            FileTreat fileTreat = new FileTreat();
            fileTreat.Get_TableContents(PathText.Text);
            if (ProjectContainer.tableContents != null)
            {
                foreach (var item in ProjectContainer.tableContents)
                {
                    fileTreat.Get_SubTableContents(item.directoryPath);
                }
            }

            var s = ProjectContainer.tableContents;
            string path = string.Empty;
            #region ReadFile
            foreach (var item in ProjectContainer.tableContents)
            {
                for (int i = 0; i < item.SubFileList.Count; i++)
                {
                    switch (item.FileName)
                    {
                        case "主调度":
                            path = item.SubFileList[i].SubFilePath + "\\config.ini";
                            ProjectContainer.masterScheduler.serverIP = INIOperationClass.INIGetStringValue(path, "self", "serverIP", null);
                            ProjectContainer.masterScheduler.serverPort = INIOperationClass.INIGetStringValue(path, "self", "serverPort", null);
                            ProjectContainer.masterScheduler.Prousr = INIOperationClass.INIGetStringValue(path, "db", "usr", null);
                            ProjectContainer.masterScheduler.Propwd = INIOperationClass.INIGetStringValue(path, "db", "pwd", null);
                            ProjectContainer.masterScheduler.telnetPort = INIOperationClass.INIGetStringValue(path, "self", "telnetPort", null);
                            string maSID = INIOperationClass.INIGetStringValue(path, "db", "SID", null);
                            int a = maSID.IndexOf(':');//“：”的位置
                            int b = maSID.IndexOf('/');//"/"的位置
                            if (a != -1)
                            {
                                ProjectContainer.masterScheduler.ProIP = maSID.Substring(0, a);
                                ProjectContainer.masterScheduler.ProPort = maSID.Substring(a + 1, b - a - 1);
                                ProjectContainer.masterScheduler.ProSID = maSID.Substring(b + 1);
                            }
                            else
                            {
                                ProjectContainer.masterScheduler.ProIP = maSID.Substring(0, b);
                                ProjectContainer.masterScheduler.ProSID = maSID.Substring(b + 1);
                            }
                            break;
                        case "子调度":
                            path = item.SubFileList[i].SubFilePath + "\\config.ini";
                            ProjectContainer.childSchedule.NODEIDList.Add(INIOperationClass.INIGetStringValue(path, "Sub Dispatch", "NODEID", null));
                            break;
                        case "设备调度":
                            path = item.SubFileList[i].SubFilePath + "\\config.ini";
                            ProjectContainer.equipmentScheduling.nodeIdList.Add(INIOperationClass.INIGetStringValue(path, "self", "nodeId", null));
                            break;
                        case "应用调度":
                            path = item.SubFileList[i].SubFilePath + "\\config.ini";
                            ProjectContainer.dispatch.nodeIdList.Add(INIOperationClass.INIGetStringValue(path, "self", "nodeId", null));
                            break;
                        case "认证中心":
                            path = item.SubFileList[i].SubFilePath + "\\config.ini";
                            ProjectContainer.certificationCenter.NodeIDList.Add(INIOperationClass.INIGetStringValue(path, "Node", "ID", null));
                            ProjectContainer.certificationCenter.DcIP = INIOperationClass.INIGetStringValue(path, "DB", "Address", null);
                            ProjectContainer.certificationCenter.DcPort = INIOperationClass.INIGetStringValue(path, "DB", "Port", null);
                            ProjectContainer.certificationCenter.DcSID = INIOperationClass.INIGetStringValue(path, "DB", "Table", null);
                            ProjectContainer.certificationCenter.DcUsr = INIOperationClass.INIGetStringValue(path, "DB", "User", null);
                            ProjectContainer.certificationCenter.DcPwd = INIOperationClass.INIGetStringValue(path, "DB", "Password", null);
                            break;
                        case "权限中心":
                            path = item.SubFileList[i].SubFilePath + "\\config.ini";
                            ProjectContainer.competenceCenter.nodeidList.Add(INIOperationClass.INIGetStringValue(path, "db", "nodeId", null));
                            break;
                        case "前置机":
                            path = item.SubFileList[i].SubFilePath + "\\config.ini";
                            FEPNode fepNode = new FEPNode();
                            fepNode.managerip = INIOperationClass.INIGetStringValue(path, "front server", "managerip", null);
                            fepNode.managerport = INIOperationClass.INIGetStringValue(path, "front server", "managerport", null);
                            fepNode.nodeid = INIOperationClass.INIGetStringValue(path, "front server", "nodeid", null);
                            ProjectContainer.FEP.NodeList.Add(fepNode);
                            break;
                        case "监控中心":
                            break;
                        case "HttpAPI":
                            path = item.SubFileList[i].SubFilePath + "\\config.ini";
                            ProjectContainer.httpAPI.PortList.Add(INIOperationClass.INIGetStringValue(path, "Setting", "Port", null));
                            break;
                        case "采集器":
                            path = item.SubFileList[i].SubFilePath + "\\Tendency.DataCollector.exe";
                            ProjectContainer.collector.NodeIdList.Add(CfgOperationClass.GetAppSettings(path, "NodeId"));
                            break;
                        case "API":
                            path = item.SubFileList[i].SubFilePath + "\\Tendency.ApiDataCenter.exe";
                            ProjectContainer.api.NodeIdList.Add(CfgOperationClass.GetAppSettings(path, "NodeId"));
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion
            #region 界面数据显示
            masterIPText.Text = ProjectContainer.masterScheduler.serverIP;
            masterPortText.Text = ProjectContainer.masterScheduler.serverPort;
            ProIPText.Text = ProjectContainer.masterScheduler.ProIP;
            ProPortText.Text = ProjectContainer.masterScheduler.ProPort;
            ProSIDText.Text = ProjectContainer.masterScheduler.ProSID;
            ProUsrText.Text = ProjectContainer.masterScheduler.Prousr;
            ProPwdText.Text = ProjectContainer.masterScheduler.Propwd;
            DCIPText.Text = ProjectContainer.certificationCenter.DcIP;
            DCPortText.Text = ProjectContainer.certificationCenter.DcPort;
            DCSIDText.Text = ProjectContainer.certificationCenter.DcSID;
            DcUsrText.Text = ProjectContainer.certificationCenter.DcUsr;
            DcPwdText.Text = ProjectContainer.certificationCenter.DcPwd;
            TelNetPortText.Text = ProjectContainer.masterScheduler.telnetPort;

            ChildNodeIDComboBox.Items.AddRange(ProjectContainer.childSchedule.NODEIDList.ToArray());
            CertificationNodeComboBox.Items.AddRange(ProjectContainer.certificationCenter.NodeIDList.ToArray());
            HttpAPIPortComboBox.Items.AddRange(ProjectContainer.httpAPI.PortList.ToArray());
            CollectorNodeComboBox.Items.AddRange(ProjectContainer.collector.NodeIdList.ToArray());
            CompetenceNodeComboBox.Items.AddRange(ProjectContainer.competenceCenter.nodeidList.ToArray());
            DispatchNodeComboBox.Items.AddRange(ProjectContainer.dispatch.nodeIdList.ToArray());
            APINodeComboBox.Items.AddRange(ProjectContainer.api.NodeIdList.ToArray());
            EquipmentNodeComboBox.Items.AddRange(ProjectContainer.equipmentScheduling.nodeIdList.ToArray());
            //FEPNodeComboBox.Items.AddRange(ProjectContainer.FEP.NodeList .ToArray());

            for (int i = 0; i < ProjectContainer.FEP.NodeList.Count; i++)
            {
                FEPNodeComboBox.Items.Add(ProjectContainer.FEP.NodeList[i].nodeid);
            }

            panel2.Enabled = true;
            jiazai_button.Enabled = true;
            #endregion
        }

        private void AddChildNodebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ChildNodeIDText.Text))
            {
                this.ChildNodeIDComboBox.Items.Add(this.ChildNodeIDText.Text);
                this.ChildNodeIDComboBox.SelectedItem = this.ChildNodeIDText.Text;
                ProjectContainer.childSchedule.NODEIDList.Add(this.ChildNodeIDText.Text);
            }
        }

        private void SaveChildNodebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ChildNodeIDText.Text) && !string.IsNullOrWhiteSpace(this.ChildNodeIDComboBox.Text))
            {
                //this.ChildNodeIDComboBox.Items.Remove(this.ChildNodeIDComboBox.Text);
                //this.ChildNodeIDComboBox.Items.Add(this.ChildNodeIDText.Text);
                ProjectContainer.childSchedule.NODEIDList.Remove(this.ChildNodeIDComboBox.Text);
                ProjectContainer.childSchedule.NODEIDList.Add(this.ChildNodeIDText.Text);
                this.ChildNodeIDComboBox.Items.Clear();
                this.ChildNodeIDComboBox.Items.AddRange(ProjectContainer.childSchedule.NODEIDList.ToArray());
            }
        }

        private void DeleteChildNodebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ChildNodeIDComboBox.Text))
            {
                this.ChildNodeIDComboBox.Items.Remove(this.ChildNodeIDComboBox.Text);
                ProjectContainer.childSchedule.NODEIDList.Remove(this.ChildNodeIDComboBox.Text);
            }
        }

        private void AddCertificationNodeButtton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.CertificationNodeText.Text))
            {
                this.CertificationNodeComboBox.Items.Add(this.CertificationNodeText.Text);
                this.CertificationNodeComboBox.SelectedItem = this.CertificationNodeText.Text;
            }
        }

        private void SaveCertificationNodebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.CertificationNodeComboBox.Text) && !string.IsNullOrWhiteSpace(this.CertificationNodeText.Text))
            {
                this.CertificationNodeComboBox.Items.Remove(this.CertificationNodeComboBox.Text);
                this.CertificationNodeComboBox.Items.Add(this.CertificationNodeText.Text);
            }
        }

        private void DeleteCertificationNodeButtton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.CertificationNodeComboBox.Text))
            {
                this.CertificationNodeComboBox.Items.Remove(this.CertificationNodeComboBox.Text);
            }
        }

        private void AddHttpAPIPortButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.HttpAPIPortText.Text))
            {
                this.HttpAPIPortComboBox.Items.Add(this.HttpAPIPortText.Text);
                this.HttpAPIPortComboBox.SelectedItem = this.HttpAPIPortText.Text;
            }
        }

        private void SaveHttpAPIPortButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.HttpAPIPortComboBox.Text) && !string.IsNullOrWhiteSpace(this.HttpAPIPortText.Text))
            {
                this.HttpAPIPortComboBox.Items.Remove(this.HttpAPIPortComboBox.Text);
                this.HttpAPIPortComboBox.Items.Add(this.HttpAPIPortText.Text);
            }
        }

        private void DeleteHttpAPIPortButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.HttpAPIPortComboBox.Text))
            {
                this.HttpAPIPortComboBox.Items.Remove(this.HttpAPIPortComboBox.Text);
            }
        }

        private void AddCollectorNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CollectorNodeText.Text))
            {
                this.CollectorNodeComboBox.Items.Add(CollectorNodeText.Text);
            }
        }

        private void SaveCollectorNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CollectorNodeComboBox.Text) && !string.IsNullOrWhiteSpace(CollectorNodeText.Text))
            {
                this.CollectorNodeComboBox.Items.Remove(CollectorNodeComboBox.Text);
                this.CollectorNodeComboBox.Items.Add(CollectorNodeText.Text);
            }
        }

        private void DeleteCollectorNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CollectorNodeComboBox.Text))
            {
                this.CollectorNodeComboBox.Items.Remove(CollectorNodeComboBox.Text);
            }
        }

        private void AddCompetenceNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CompetenceNodeText.Text))
            {
                CompetenceNodeComboBox.Items.Add(CompetenceNodeText.Text);
            }
        }

        private void SaveCompetenceNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CompetenceNodeText.Text) && !string.IsNullOrWhiteSpace(CompetenceNodeComboBox.Text))
            {
                CompetenceNodeComboBox.Items.Remove(CompetenceNodeComboBox.Text);
                CompetenceNodeComboBox.Items.Add(CompetenceNodeText.Text);
            }
        }

        private void DeleteCompetenceNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CompetenceNodeComboBox.Text))
            {
                CompetenceNodeComboBox.Items.Remove(CompetenceNodeComboBox.Text);
            }
        }

        private void AddDispatchNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DispatchNodeText.Text))
            {
                DispatchNodeComboBox.Items.Add(DispatchNodeText.Text);
            }
        }

        private void SaveDispatchNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DispatchNodeComboBox.Text) && !string.IsNullOrWhiteSpace(DispatchNodeText.Text))
            {
                DispatchNodeComboBox.Items.Remove(DispatchNodeComboBox.Text);
                DispatchNodeComboBox.Items.Add(DispatchNodeText.Text);
            }
        }

        private void DeleteDispatchNodeButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DispatchNodeComboBox.Text))
            {
                DispatchNodeComboBox.Items.Remove(DispatchNodeComboBox.Text);
            }
        }

        private void AddFEPNodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FEPNodeText.Text))
            {
                MessageBox.Show("前置机节点不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(FEPWebIPText.Text))
            {
                MessageBox.Show("前置机WEB IP不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(FEPWEBPortText.Text))
            {
                MessageBox.Show("前置机WEB 端口不能为空");
                return;
            }
            FEPNodeComboBox.Items.Add(FEPNodeText.Text);
        }

        private void SaveFEPNodeButton_Click(object sender, EventArgs e)
        {

        }

        private void DeleteFEPNodeButton_Click(object sender, EventArgs e)
        {

        }

        private void AddAPINodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(APINodeText.Text))
            {
                MessageBox.Show("API节点不能为空");
                return;
            }
            APINodeComboBox.Items.Add(APINodeText.Text);
        }

        private void SaveAPINodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(APINodeText.Text))
            {
                MessageBox.Show("API节点不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(APINodeComboBox.Text))
            {
                MessageBox.Show("请选择要修改的API节点");
                return;
            }
            APINodeComboBox.Items.Remove(APINodeComboBox.Text);
            APINodeComboBox.Items.Add(APINodeText.Text);
        }

        private void DeleteAPINodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(APINodeComboBox.Text))
            {
                MessageBox.Show("请选择要删除的API节点");
                return;
            }
            APINodeComboBox.Items.Remove(APINodeComboBox.Text);
        }

        private void AddEquipmentNodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EquipmentNodeText.Text))
            {
                MessageBox.Show("请输入要添加的设备调度节点ID");
                return;
            }
            EquipmentNodeComboBox.Items.Add(EquipmentNodeText.Text);
        }

        private void SaveEquipmentNodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EquipmentNodeText.Text))
            {
                MessageBox.Show("请输入设备调度节点ID");
                return;
            }
            if (string.IsNullOrWhiteSpace(EquipmentNodeComboBox.Text))
            {
                MessageBox.Show("请选择要修改的设备调度节点ID");
                return;
            }
            EquipmentNodeComboBox.Items.Remove(EquipmentNodeComboBox.Text);
            EquipmentNodeComboBox.Items.Add(EquipmentNodeText.Text);
        }

        private void DeleteEquipmentNodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EquipmentNodeComboBox.Text))
            {
                MessageBox.Show("请选择要删除的设备调度节点ID");
                return;
            }
            EquipmentNodeComboBox.Items.Remove(EquipmentNodeComboBox.Text);
        }

        private void FEPNodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        string msterIP;
        string msterPort;
        string telnetPort;
        string dcIP;
        string dcPort;
        string dcSid;
        string dcusr;
        string dcpwd;
        string proIP;
        string proPort;
        string proSid;
        string proUsr;
        string proPwd;

        private void BtnNewBuild_Click(object sender, EventArgs e)
        {
            //TODO：解压包并复制到目录
        }
               
    }
}
