using Protocol.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Protocol.Dto.Fight;

public class ButtonPanel : UIBase
{
    private Text Text_Been;
    private Text Text_Multiple;
    private Button Button_Chat;
    private Button[] btns_chat;//聊天选择按钮组
    private Image Image_ChatChoice;

    private SocketMsg socketMsg;
    private ChatDto chatDto;
    // Start is called before the first frame update
    void Start()
    {
        init();
        socketMsg = new SocketMsg();
        chatDto = new ChatDto();

        Image_ChatChoice.gameObject.SetActive(false);
        refreshView(GameModles.Instance.userDto.Been);
        Text_Multiple.text = "倍数×1";
    }

    private void init()
    {
        Text_Been = transform.Find("Text_Been").GetComponent<Text>();
        Text_Multiple = transform.Find("Text_Multiple").GetComponent<Text>();
        Button_Chat = transform.Find("Button_Chat").GetComponent<Button>();
        Image_ChatChoice = transform.Find("Image_ChatChoice").GetComponent<Image>();

        btns_chat = new Button[7];

        for(int i = 0; i < btns_chat.Length; i++)
        {
            btns_chat[i] = Image_ChatChoice.transform.Find((i + 1) + "").GetComponent<Button>();
        }

        Button_Chat.onClick.AddListener(chatBtnClicker);

        btns_chat[0].onClick.AddListener(sendChatMsg1);
        btns_chat[1].onClick.AddListener(sendChatMsg2);
        btns_chat[2].onClick.AddListener(sendChatMsg3);
        btns_chat[3].onClick.AddListener(sendChatMsg4);
        btns_chat[4].onClick.AddListener(sendChatMsg5);
        btns_chat[5].onClick.AddListener(sendChatMsg6);
        btns_chat[6].onClick.AddListener(sendChatMsg7);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Button_Chat.onClick.RemoveAllListeners();

        for (int i = 0; i < btns_chat.Length; i++)
        {
            btns_chat[i].onClick.RemoveAllListeners();
        }
    }

    private void refreshView(int beencount)
    {
        Text_Been.text = "×" + beencount;
    }

    private void setMultiple(int multiple)
    {
        Text_Multiple.text = "倍数×" + multiple;
    }

    private void chatBtnClicker()
    {
        bool active = Image_ChatChoice.gameObject.activeInHierarchy;
        Image_ChatChoice.gameObject.SetActive(!active);
    }

    private void sendChatMsg1()
    {
        socketMsg.Change(OpCode.CHAT, ChatCode.CREQ, 1);
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        Debug.Log("发送聊天信息1");
    }

    private void sendChatMsg2()
    {
        socketMsg.Change(OpCode.CHAT, ChatCode.CREQ, 2);
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        Debug.Log("发送聊天信息2");
    }

    private void sendChatMsg3()
    {
        socketMsg.Change(OpCode.CHAT, ChatCode.CREQ, 3);
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        Debug.Log("发送聊天信息3");
    }

    private void sendChatMsg4()
    {
        socketMsg.Change(OpCode.CHAT, ChatCode.CREQ, 4);
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        Debug.Log("发送聊天信息4");
    }

    private void sendChatMsg5()
    {
        socketMsg.Change(OpCode.CHAT, ChatCode.CREQ, 5);
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        Debug.Log("发送聊天信息5");
    }

    private void sendChatMsg6()
    {
        socketMsg.Change(OpCode.CHAT, ChatCode.CREQ, 6);
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        Debug.Log("发送聊天信息6");
    }

    private void sendChatMsg7()
    {
        socketMsg.Change(OpCode.CHAT, ChatCode.CREQ, 7);
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        Debug.Log("发送聊天信息7");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
