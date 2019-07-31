using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : UIBase {
    private Button startbtn;
    private Button registerbtn;
	// Use this for initialization
	void Start () {
        
        startbtn = transform.Find("BTN_Start").GetComponent<Button>();
        registerbtn = transform.Find("BTN_Register").GetComponent<Button>();

        startbtn.onClick.AddListener(startBtnClicker);
        registerbtn.onClick.AddListener(registerBtnClicker);
	}

    

    public override void OnDestroy()
    {
        base.OnDestroy();

        startbtn.onClick.RemoveAllListeners();
        registerbtn.onClick.RemoveAllListeners();
    }

    private void startBtnClicker()
    {
        MsgCenter.Instance.Dispatch(AreoCode.UI, UIEvent.START_PANEL_EVENTCODE, true);
    }

    private void registerBtnClicker()
    {
        MsgCenter.Instance.Dispatch(AreoCode.UI, UIEvent.REGISTER_PANEL_EVENTCODE, true);
    }
	
}
