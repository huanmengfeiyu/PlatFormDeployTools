using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    public class TableContent
    {
        public string FileName { get; set; }
        public string directoryPath { get; set; }
        public List<SubTableConent> SubFileList { get; set; }
        public TableContent()
        {
            SubFileList = new List<SubTableConent>();
        }
    }
    public class SubTableConent
    {
        public string SubName { get; set; }
        public string SubFilePath { get; set; }
    }
}
