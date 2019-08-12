using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protocol.Dto;
using System;
using Protocol.Dto.Fight;

/// <summary>
/// 所有人都共同功能的提取
/// </summary>
public class StatePanel : UIBase
{
    protected UserDto userDto;

    protected Image Image_chat;
    protected Image Image_identity;
    protected Text Text_Ready;
    protected Text Text_chat;

    [Range(1,3)]
    protected float Showtime = 2f;
    protected float Timer = 0f;
    protected bool isShow = false;

    //protected delegate void pocessPlayerEventDelegate();

    public override void Execute(int eventcode, object message)
    {
        int uid;
        switch (eventcode)
        {
            case UIEvent.PLAYER_READY:
                uid = (int)message;
                /*if (uid == userDto.ID)//为了区别左右玩家
                {
                    //如果收到左边玩家准备的服务器广播
                    //Text_Ready.gameObject.SetActive(true);
                    readystate();
                }*/
                processPlayerEvent(uid,
                    () => { readystate(); });
                break;

            //显示状态面板
            case UIEvent.PLAYER_ENTER:
                uid = (int)message;
                /*if (uid == userDto.ID)//为了区别左右玩家
                {
                    //如果收到左边玩家准备的服务器广播
                    SetPanelActive(true);
                }*/
                processPlayerEvent(uid,
                    () => { SetPanelActive(true); });
                break;

            //隐藏状态面板
            case UIEvent.PLAYER_LEAVE:
                uid = (int)message;
                /*if (uid == userDto.ID)//为了区别左右玩家
                {
                    //如果收到左边玩家准备的服务器广播
                    SetPanelActive(false);
                }*/
                processPlayerEvent(uid,
                    () => { SetPanelActive(false); });
                break;

            case UIEvent.PLAYER_HIDE_STATE://隐藏准备文字
                //uid = (int)message;
                    Text_Ready.gameObject.SetActive(false);
                /*processPlayerEvent(uid,
                    () => { Text_Ready.gameObject.SetActive(true); });*/
                break;

            case UIEvent.PLAYER_CHAT:
                ChatDto chatDto = message as ChatDto;
                processPlayerEvent(chatDto.UserID,()=> {
                    setChatText(chatDto.ChatText);
                });
                break;

            case UIEvent.PLAYER_CHANGE_IDENTITY:
                uid = (int)message;
                processPlayerEvent(uid,
                    () =>
                    {
                        setIdentity(1);
                    });
                break;
        }
    }

    protected virtual void processPlayerEvent(int uid, Action action)
    {
        if(userDto !=null && userDto.ID == uid)
        {
            action.Invoke();
        }
    }

    protected virtual void readystate()
    {
        Text_Ready.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Image_chat = transform.Find("Image_chat").GetComponent<Image>();
        Image_identity = transform.Find("Image_identity").GetComponent<Image>();
        Text_Ready = transform.Find("Text_Ready").GetComponent<Text>();
        Text_chat = Image_chat.transform.Find("Text_chat").GetComponent<Text>();

        setIdentity(0);

        Image_chat.gameObject.SetActive(false);
        Text_Ready.gameObject.SetActive(false);
        Text_chat.gameObject.SetActive(false);
        //SetPanelActive(false);
    }

    protected virtual void Awake()
    {
        Bind(UIEvent.PLAYER_READY, UIEvent.PLAYER_ENTER, UIEvent.PLAYER_LEAVE, UIEvent.PLAYER_HIDE_STATE);
        Bind(UIEvent.PLAYER_CHAT);
        Bind(UIEvent.PLAYER_CHANGE_IDENTITY);
    }

    /// <summary>
    /// 外界调用设置聊天内容
    /// </summary>
    /// <param name="str">聊天内容</param>
    protected void setChatText(string str)
    {
        Text_chat.text = str;
        setImageChatActive(true);
        isShow = true;
    }

    protected void setImageChatActive(bool active)
    {
        Image_chat.gameObject.SetActive(active);
        Text_chat.gameObject.SetActive(active);
    }

    /// <summary>
    /// 设置身份
    /// </summary>
    /// <param name="identity">0为农民，1为地主</param>
    protected void setIdentity(int identity)
    {
        string path;
        if(identity == 0)
        {
            path = "Identity/Farmer";
        }
        else
        {
            path = "Identity/Landlord";
        }

        Image_identity.sprite = Resources.Load<Sprite>(path);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isShow)
        {
            Timer += Time.deltaTime;
            if(Timer >= Showtime)
            {
                Timer = 0f;
                isShow = false;
                setImageChatActive(false);
            }
        }
    }
}
