using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlatFormDeployUtility;
using PlatFormDeployModel;

namespace PlatFormDeployTools
{
    public class DelegetList
    {
        List<Delegate> delegets = new List<Delegate>();


        public delegate void AddNode();

        OprateAffairsList oplIst = new OprateAffairsList();
        void da()
        {
            oplIst.Add(new OperateAffairs() { platFormOperand = PlatFormOperand.主调度, platFormOperandType = PlatFormOperandType.添加 });
            
        }
    }
}
