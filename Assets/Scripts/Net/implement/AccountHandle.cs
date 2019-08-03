﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol.Code;
using UnityEngine;

namespace Assets.Scripts.Net.implement
{
    /// <summary>
    /// 帐号网络数据处理
    /// </summary>
    public class AccountHandle : HandleBase
    {
        private static AccountHandle instance = null;
        private SceneLoadMsg loadMsg = new SceneLoadMsg();

        
        public static AccountHandle Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new AccountHandle();
                }
                return instance;
            }
        }

        private AccountHandle() { }

        public override void OnReceive(int subcode , object message)
        {
            switch (subcode)
            {
                //服务器登陆回复
                case AccountCode.LOGIN_SRES:
                    loginResponse(message);
                    break;

                //服务器注册回复
                case AccountCode.REGISTER_SRES:
                    registerResponse(message);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 服务器登陆回复
        /// </summary>
        /// <param name="result"></param>
        private void loginResponse(object result)
        {  
            string str = result.ToString();
            if(str == "登陆成功")
            {
                loadMsg.Change(1, "02_Main", () =>
                 {
                     //TODO 从服务器获取玩家信息
                     Debug.Log("切换场景成功");
                 });
                //TODO 跳转场景 
                MsgCenter.Instance.Dispatch(AreoCode.SCENE, SceneCode.LOAD_SCENE, "123");
            }

            else
            {
                MsgCenter.Instance.Dispatch(AreoCode.UI, UIEvent.PROMPT_PANEL_EVENTCODE, str);
            }
        }

        private void registerResponse(object result)
        {
            string str = result.ToString();
            if(str == "注册成功")
            {
                //TODO 切换场景
            }

            else
            {
                MsgCenter.Instance.Dispatch(AreoCode.UI, UIEvent.PROMPT_PANEL_EVENTCODE, str);
            }
        }
    }
}
