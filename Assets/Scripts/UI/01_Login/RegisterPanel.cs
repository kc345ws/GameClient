using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : UIBase {
    private Button BTN_Register;
    private Button BTN_Back;
    private InputField input_Account;
    private InputField input_PWD;
    private InputField input_Repeat;

    private AccountDto account = null;
    private SocketMsg msg = null;
    // Use this for initialization
    void Start () {
        BTN_Register = transform.Find("BTN_Register").GetComponent<Button>();
        BTN_Back = transform.Find("BTN_Back").GetComponent<Button>();
        input_Account = transform.Find("input_Account").GetComponent<InputField>();
        input_PWD = transform.Find("input_PWD").GetComponent<InputField>();
        input_Repeat = transform.Find("input_Repeat").GetComponent<InputField>();

        BTN_Register.onClick.AddListener(registerBtnClicker);
        BTN_Back.onClick.AddListener(backBtnClicker);

        SetPanelActive(false);//面板默认隐藏
    }

    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);

        switch (eventcode)
        {
            case UIEvent.REGISTER_PANEL_EVENTCODE:
                SetPanelActive(true);
                break;
        }
    }

    private void Awake()
    {
        Bind(UIEvent.REGISTER_PANEL_EVENTCODE);
        account = new AccountDto();
        msg = new SocketMsg();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        BTN_Register.onClick.RemoveAllListeners();
        BTN_Back.onClick.RemoveAllListeners();
    }

    private void registerBtnClicker()
    {
        if (string.IsNullOrEmpty(input_Account.text))
        {
            MsgCenter.Instance.Dispatch(AreoCode.UI, UIEvent.PROMPT_PANEL_EVENTCODE, "帐号不能为空");
            return;
        }
        if (input_PWD.text.Length < 6
            || input_Account.text.Length > 16)
        {
            MsgCenter.Instance.Dispatch(AreoCode.UI, UIEvent.PROMPT_PANEL_EVENTCODE, "密码必须大于6位且小于16位");
            return;
        }
        if (string.IsNullOrEmpty(input_PWD.text))
        {
            MsgCenter.Instance.Dispatch(AreoCode.UI, UIEvent.PROMPT_PANEL_EVENTCODE, "密码不能为空");
            return;
        }
        if (string.IsNullOrEmpty(input_Repeat.text) ||
            !input_PWD.text.Equals(input_Repeat.text))
        {
            MsgCenter.Instance.Dispatch(AreoCode.UI, UIEvent.PROMPT_PANEL_EVENTCODE, "两次密码输入不一致");
            return;
        }    
        account.Account = input_Account.text;
        account.Password = input_PWD.text;
        msg.OpCode = OpCode.ACCOUNT;
        msg.SubCode = AccountCode.REGISTER_CREQ;
        msg.Value = account;
        MsgCenter.Instance.Dispatch(AreoCode.NET, NetEvent.SENDMSG, msg);
    }

    private void backBtnClicker()
    {
        SetPanelActive(false);
    }


	
	
}
