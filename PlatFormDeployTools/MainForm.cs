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
            PromptText.Text = @"请确认所有信息已经填写完毕，确认后点击下面的 保存 按钮";
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
                }
            }
        }

        private void save_button_Click(object sender, EventArgs e)
        {

        }

        private void jiazai_button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PathText.Text))
            {
                FileTreat fileTreat = new FileTreat();
                fileTreat.Get_TableContents(PathText.Text);
                if (ProjectContainer.tableContents != null)
                {
                    foreach (var item in ProjectContainer.tableContents)
                    {
                        fileTreat.Get_SubTableContents(item.directoryPath);
                    }
                }
                //TODO:加载各个配置参数

                var s = ProjectContainer.tableContents;

                foreach (var item in ProjectContainer.tableContents)
                {
                    for (int i = 0; i < item.SubFileList.Count; i++)
                    {
                        switch (item.FileName)
                        {
                            case "主调度":
                            case "子调度":
                            case "设备调度":
                            case "应用调度":
                            case "认证中心":
                            case "权限中心":
                            case "前置机":
                            case "监控中心":
                            case "HTTPAPI":
                                //TODO:ini文件读取
                                var path = item.SubFileList[i].SubFilePath + "\\config.ini";
                                msterIP= INIOperationClass.INIGetStringValue(path, "self", "serverIP", null);
                                msterPort = INIOperationClass.INIGetStringValue(path, "self", "serverPort", null);  
                                break;
                            case "采集器":
                            case "API":
                                //TODO: .Config读取
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void AddChildNodebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ChildNodeIDText.Text))
            {
                this.ChildNodeIDComboBox.Items.Add(this.ChildNodeIDText.Text);
                this.ChildNodeIDComboBox.SelectedItem = this.ChildNodeIDText.Text;
            }
        }

        private void SaveChildNodebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ChildNodeIDText.Text) && !string.IsNullOrWhiteSpace(this.ChildNodeIDComboBox.Text))
            {
                this.ChildNodeIDComboBox.Items.Remove(this.ChildNodeIDComboBox.Text);
                this.ChildNodeIDComboBox.Items.Add(this.ChildNodeIDText.Text);
            }
        }

        private void DeleteChildNodebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ChildNodeIDComboBox.Text))
            {
                this.ChildNodeIDComboBox.Items.Remove(this.ChildNodeIDComboBox.Text);
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
    }
}
