using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : UIBase {
    private Button BTN_Login;
    private Button BTN_Back;
    private InputField input_Account;
    private InputField input_PWD;
    // Use this for initialization
    void Start () {
        BTN_Login = transform.Find("BTN_Login").GetComponent<Button>();
        BTN_Back = transform.Find("BTN_Back").GetComponent<Button>();
        input_Account = transform.Find("input_Account").GetComponent<InputField>();
        input_PWD = transform.Find("input_PWD").GetComponent<InputField>();

        BTN_Login.onClick.AddListener(loginBtnClicker);
        BTN_Back.onClick.AddListener(backBtnClicker);

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        Bind(UIEvent.START_PANEL_EVENTCODE);
    }

    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        switch (eventcode)
        {
            case UIEvent.START_PANEL_EVENTCODE:
                SetPanelActive((bool)message);
                break;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        BTN_Login.onClick.RemoveAllListeners();
        BTN_Back.onClick.RemoveAllListeners();
    }

    private void loginBtnClicker()
    {
        if (string.IsNullOrEmpty(input_Account.text)
            ||string.IsNullOrEmpty(input_PWD.text)
            || input_PWD.text.Length < 6 || input_PWD.text.Length > 16)
        {
            return;
        }
    }

    private void backBtnClicker()
    {
        SetPanelActive(false);
    }
}
