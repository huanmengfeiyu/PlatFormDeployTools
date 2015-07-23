using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatFormDeployModel
{
   
    public enum PlatFormOperand
    {
        主调度,
        子调度,
        设备调度,
        应用调度,
        认证中心,
        权限中心,
        采集器,
        前置机,
        API,
        监控中心,
        HTTPAPI
    }
    public enum PlatFormOperandType
    {
        添加,
        修改,
        删除
    }
}
