using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol.Code;

namespace Assets.Scripts.Net
{
    /// <summary>
    /// 网络消息处理基类
    /// </summary>
    public abstract class HandleBase
    {
        public abstract void OnReceive(int subcode, object message);

        public void Dispatch(int opcode , int subcode , object message)
        {
            MsgCenter.Instance.Dispatch(opcode, subcode, message);
        }
    }
}
