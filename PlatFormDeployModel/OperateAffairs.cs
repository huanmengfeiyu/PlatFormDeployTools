using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatFormDeployModel
{
    public class OperateAffairs
    {
        public PlatFormOperand platFormOperand;
        public PlatFormOperandType platFormOperandType;
        public IPlatFormDeployInfo platFormDeployInfo;
        public int resultCode;
    }
    public class OprateAffairsList:List<OperateAffairs>
    {

    }
}