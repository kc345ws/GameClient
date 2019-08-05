using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanel : UIBase {
    private Button Button_Match;//快速匹配按钮
    private Button Button_Cancel;//取消匹配按钮
    private Button Button_Enter;//进入房间按钮
    private Text Text_Matching;//正在匹配文字
    private Image Image_Logo;//图片
    private bool isShow = false;
    private int Dotcount = 0;
    private string defaultstr;
    private float Timer = 0f;
    // Use this for initialization

    void Start () {
        Button_Match = transform.Find("Button_Match").GetComponent<Button>();
        Button_Cancel = transform.Find("Button_Cancel").GetComponent<Button>();
        Button_Enter = transform.Find("Button_Enter").GetComponent<Button>();
        Text_Matching = transform.Find("Text_Matching").GetComponent<Text>();
        Image_Logo = transform.Find("Image_Logo").GetComponent<Image>();

        Button_Match.onClick.AddListener(matchBtnClicker);
        Button_Cancel.onClick.AddListener(cancelBtnClicker);
        defaultstr = Text_Matching.text;

        setObjectActive(isShow);
        Button_Enter.gameObject.SetActive(false);
    }

    public override void OnDestroy()
    {
        Button_Match.onClick.RemoveAllListeners();
        Button_Cancel.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update () {
        if (isShow)
        {
            Timer += Time.deltaTime;
            if(Timer >= 0.5f){
                matchAnimation();
                Timer = 0f;
            }         
        }
	}

    private void setObjectActive(bool active)
    {
        Image_Logo.gameObject.SetActive(active);
        Text_Matching.gameObject.SetActive(active);
        Button_Cancel.gameObject.SetActive(active);
    }

    private void matchBtnClicker()
    {
        //TODO 向服务器发送匹配请求
        isShow = !isShow;
        if (isShow == false)
        {
            Button_Enter.gameObject.SetActive(false);
        }
        else
        {
            Button_Enter.gameObject.SetActive(true);
        }
        setObjectActive(isShow);
    }

    private void matchAnimation()
    {  
        Text_Matching.text += ".";
        Dotcount++;
        if(Dotcount >= 5)
        {
            Text_Matching.text = defaultstr;
            Dotcount = 0;
        }      
    }

    private void cancelBtnClicker()
    {
        //TODO 向服务器发送取消匹配请求
    }
}
