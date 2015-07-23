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

            panel2.Enabled = false;
            BtnNewBuild.Enabled = false;
            jiazai_button.Enabled = false;

            PathText.Text = GlobleConfig.LastDirectory;


        }

        private void LiuLanBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog.SelectedPath.Trim() != "")
                {
                    PathText.Text = folderBrowserDialog.SelectedPath.Trim();

                    GlobleConfig.LastDirectory = PathText.Text;
                    BtnNewBuild.Enabled = true;
                    jiazai_button.Enabled = true;
                }
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
                            ProjectContainer.shareDeployInfo.masterServerIP = INIOperationClass.INIGetStringValue(path, "self", "serverIP", null);
                            ProjectContainer.shareDeployInfo.masterServerPort = INIOperationClass.INIGetStringValue(path, "self", "serverPort", null);
                            ProjectContainer.shareDeployInfo.ProUSER = INIOperationClass.INIGetStringValue(path, "db", "usr", null);
                            ProjectContainer.shareDeployInfo.ProPWD = INIOperationClass.INIGetStringValue(path, "db", "pwd", null);
                            ProjectContainer.masterScheduler.telnetPort = INIOperationClass.INIGetStringValue(path, "self", "telnetPort", null);
                            string maSID = INIOperationClass.INIGetStringValue(path, "db", "SID", null);
                            int a = maSID.IndexOf(':');//“：”的位置
                            int b = maSID.IndexOf('/');//"/"的位置
                            if (a != -1)
                            {
                                ProjectContainer.shareDeployInfo.ProIP = maSID.Substring(0, a);
                                ProjectContainer.shareDeployInfo.ProPort = maSID.Substring(a + 1, b - a - 1);
                                ProjectContainer.shareDeployInfo.ProSID = maSID.Substring(b + 1);
                            }
                            else
                            {
                                ProjectContainer.shareDeployInfo.ProIP = maSID.Substring(0, b);
                                ProjectContainer.shareDeployInfo.ProPort = "1521";
                                ProjectContainer.shareDeployInfo.ProSID = maSID.Substring(b + 1);
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
                            ProjectContainer.shareDeployInfo.DcIP = INIOperationClass.INIGetStringValue(path, "DB", "Address", null);
                            ProjectContainer.shareDeployInfo.DcPort = INIOperationClass.INIGetStringValue(path, "DB", "Port", null);
                            ProjectContainer.shareDeployInfo.DcSID = INIOperationClass.INIGetStringValue(path, "DB", "Table", null);
                            ProjectContainer.shareDeployInfo.DcUsr = INIOperationClass.INIGetStringValue(path, "DB", "User", null);
                            ProjectContainer.shareDeployInfo.DcPwd = INIOperationClass.INIGetStringValue(path, "DB", "Password", null);
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
            masterIPText.Text = ProjectContainer.shareDeployInfo.masterServerIP;
            masterPortText.Text = ProjectContainer.shareDeployInfo.masterServerPort;
            ProIPText.Text = ProjectContainer.shareDeployInfo.ProIP;
            ProPortText.Text = ProjectContainer.shareDeployInfo.ProPort;
            ProSIDText.Text = ProjectContainer.shareDeployInfo.ProSID;
            ProUsrText.Text = ProjectContainer.shareDeployInfo.ProUSER;
            ProPwdText.Text = ProjectContainer.shareDeployInfo.ProPWD;
            DCIPText.Text = ProjectContainer.shareDeployInfo.DcIP;
            DCPortText.Text = ProjectContainer.shareDeployInfo.DcPort;
            DCSIDText.Text = ProjectContainer.shareDeployInfo.DcSID;
            DcUsrText.Text = ProjectContainer.shareDeployInfo.DcUsr;
            DcPwdText.Text = ProjectContainer.shareDeployInfo.DcPwd;
            TelNetPortText.Text = ProjectContainer.masterScheduler.telnetPort;

            ChildNodeIDComboBox.Items.AddRange(ProjectContainer.childSchedule.NODEIDList.ToArray());
            CertificationNodeComboBox.Items.AddRange(ProjectContainer.certificationCenter.NodeIDList.ToArray());
            HttpAPIPortComboBox.Items.AddRange(ProjectContainer.httpAPI.PortList.ToArray());
            CollectorNodeComboBox.Items.AddRange(ProjectContainer.collector.NodeIdList.ToArray());
            CompetenceNodeComboBox.Items.AddRange(ProjectContainer.competenceCenter.nodeidList.ToArray());
            DispatchNodeComboBox.Items.AddRange(ProjectContainer.dispatch.nodeIdList.ToArray());
            APINodeComboBox.Items.AddRange(ProjectContainer.api.NodeIdList.ToArray());
            EquipmentNodeComboBox.Items.AddRange(ProjectContainer.equipmentScheduling.nodeIdList.ToArray());

            for (int i = 0; i < ProjectContainer.FEP.NodeList.Count; i++)
            {
                FEPNodeComboBox.Items.Add(ProjectContainer.FEP.NodeList[i].nodeid);
            }

            #endregion
            panel2.Enabled = true;
        }

        private void AddChildNodebutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.ChildNodeIDText.Text))
            {
                MessageBox.Show("子调度修改栏不能为空");
                return;
            }
            this.ChildNodeIDComboBox.Items.Add(this.ChildNodeIDText.Text);
            this.ChildNodeIDComboBox.SelectedItem = this.ChildNodeIDText.Text;
            ProjectContainer.childSchedule.NODEIDList.Add(this.ChildNodeIDText.Text);

            ChildSchedule child = new ChildSchedule();
            child.NODEIDList.Add(this.ChildNodeIDText.Text);
            ProjectContainer.oprateAffairsList.Add(new OperateAffairs() { platFormDeployInfo = child, platFormOperand = PlatFormOperand.子调度, platFormOperandType = PlatFormOperandType.添加 });
        }

        private void SaveChildNodebutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.ChildNodeIDText.Text))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(this.ChildNodeIDComboBox.Text))
            {
                return;
            }

            ChildSchedule child = new ChildSchedule();
            child.NODEIDList.Add(ChildNodeIDComboBox.Text);
            child.NODEIDList.Add(ChildNodeIDText.Text);
            ProjectContainer.oprateAffairsList.Add(new OperateAffairs() { platFormDeployInfo = child, platFormOperand = PlatFormOperand.子调度, platFormOperandType = PlatFormOperandType.修改 });

            ProjectContainer.childSchedule.NODEIDList.Remove(this.ChildNodeIDComboBox.Text);
            ProjectContainer.childSchedule.NODEIDList.Add(this.ChildNodeIDText.Text);
            this.ChildNodeIDComboBox.Items.Clear();
            this.ChildNodeIDComboBox.Items.AddRange(ProjectContainer.childSchedule.NODEIDList.ToArray());
        }

        private void DeleteChildNodebutton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.ChildNodeIDComboBox.Text))
            {
                return;
            }
            ChildSchedule child = new ChildSchedule();
            child.NODEIDList.Add(ChildNodeIDComboBox.Text);
            ProjectContainer.oprateAffairsList.Add(new OperateAffairs() { platFormDeployInfo = child, platFormOperand = PlatFormOperand.子调度, platFormOperandType = PlatFormOperandType.删除 });

            this.ChildNodeIDComboBox.Items.Remove(this.ChildNodeIDComboBox.Text);
            ProjectContainer.childSchedule.NODEIDList.Remove(this.ChildNodeIDComboBox.Text);
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

        private void BtnNewBuild_Click(object sender, EventArgs e)
        {
            //TODO：解压包并复制到目录
            panel2.Enabled = true;
        }

        private void btnTreat_Click(object sender, EventArgs e)
        {
            #region not null check
            if (string.IsNullOrWhiteSpace(DCIPText.Text))
            {
                MessageBox.Show("请设置数据库dc的IP");
                return;
            }
            if (string.IsNullOrWhiteSpace(DCPortText.Text))
            {
                MessageBox.Show("请设置数据库dc的端口");
                return;
            }
            if (string.IsNullOrWhiteSpace(DCSIDText.Text))
            {
                MessageBox.Show("请设置数据库dc的SID");
                return;
            }
            if (string.IsNullOrWhiteSpace(DcUsrText.Text))
            {
                MessageBox.Show("请输入数据库dc的用户名");
                return;
            }
            if (string.IsNullOrWhiteSpace(DcPwdText.Text))
            {
                MessageBox.Show("请输入数据库dc的密码");
                return;
            }
            if (string.IsNullOrWhiteSpace(ProIPText.Text))
            {
                MessageBox.Show("请设置数据库Pro的IP");
                return;
            }
            if (string.IsNullOrWhiteSpace(ProPortText.Text))
            {
                MessageBox.Show("请设置数据库Pro的端口");
                return;
            }
            if (string.IsNullOrWhiteSpace(ProSIDText.Text))
            {
                MessageBox.Show("请设置数据库Pro的SID");
                return;
            }
            if (string.IsNullOrWhiteSpace(ProUsrText.Text))
            {
                MessageBox.Show("请输入数据库Pro的用户名");
                return;
            }
            if (string.IsNullOrWhiteSpace(ProPwdText.Text))
            {
                MessageBox.Show("请输入数据库Pro的密码");
                return;
            }
            if (string.IsNullOrWhiteSpace(masterIPText.Text))
            {
                MessageBox.Show("请设置主调度IP");
                return;
            }
            if (string.IsNullOrWhiteSpace(masterPortText.Text))
            {
                MessageBox.Show("请输入主调度端口");
                return;
            }
            if (string.IsNullOrWhiteSpace(TelNetPortText.Text))
            {
                MessageBox.Show("请输入TelNet端口");
                return;
            }
            #endregion

            ProjectContainer.shareDeployInfo.DcIP = DCIPText.Text;
            ProjectContainer.shareDeployInfo.DcPort = DCPortText.Text;
            ProjectContainer.shareDeployInfo.DcSID = DCSIDText.Text;
            ProjectContainer.shareDeployInfo.DcUsr = DcUsrText.Text;
            ProjectContainer.shareDeployInfo.DcPwd = DcPwdText.Text;

            ProjectContainer.shareDeployInfo.ProIP = ProIPText.Text;
            ProjectContainer.shareDeployInfo.ProPort = ProPortText.Text;
            ProjectContainer.shareDeployInfo.ProSID = ProSIDText.Text;
            ProjectContainer.shareDeployInfo.ProUSER = ProUsrText.Text;
            ProjectContainer.shareDeployInfo.ProPWD = ProPwdText.Text;

            ProjectContainer.shareDeployInfo.masterServerIP = masterIPText.Text;
            ProjectContainer.shareDeployInfo.masterServerPort = masterPortText.Text;
            //ProjectContainer.masterScheduler.telnetPort = TelNetPortText.Text;

            MasterScheduler master = new MasterScheduler();
            master.telnetPort = TelNetPortText.Text;
            ProjectContainer.oprateAffairsList.Add(new OperateAffairs() { platFormDeployInfo = master, platFormOperand = PlatFormOperand.主调度, platFormOperandType = PlatFormOperandType.修改 });

            FileTreat.Execute();
        }

    }
}
