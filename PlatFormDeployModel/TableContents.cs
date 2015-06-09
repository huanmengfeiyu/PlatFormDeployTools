using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatFormDeployModel
{
    public class TableContents
    {
        public string FileName { get; set; }
        public string directoryPath { get; set; }
        public List<SubTableConents> SubFileList { get; set; }
        public TableContents()
        {
            SubFileList = new List<SubTableConents>();
        }
    }
    public class SubTableConents
    {
        public string SubName { get; set; }
        public string SubFilePath { get; set; }
    }
}
