using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protocol.Dto;
using Protocol.Code;

public class MyStatePanel : StatePanel
{
    private Button Button_Ready;//准备
    private Button Button_Grab;//抢
    private Button Button_NoGrab;//不抢
    private Button Button_Deal;//出牌
    private Button Button_NoDeal;//不出

    private SocketMsg SocketMsg;

    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        bool active;
        switch (eventcode)
        {
            case UIEvent.SET_MY_PLAYER_STATE:
                userDto = message as UserDto;
                break;

            case UIEvent.SHOW_GRAB_BUTTON:
                active = (bool)message;
                Button_Grab.gameObject.SetActive(active);
                Button_NoGrab.gameObject.SetActive(active);
                break;

            case UIEvent.SHOW_DEAL_BUTTON:
                active = (bool)message;
                Button_Deal.gameObject.SetActive(active);
                Button_NoDeal.gameObject.SetActive(active);
                break;

            case UIEvent.LEFT_PLAYER_LOADED:
                refreshPlayer((int)message);
                break;

            case UIEvent.RIGHT_PLAYER_LOADED:
                refreshPlayer((int)message);
                break;
        }

    }

    //当左右玩家第一次加载完毕后再次加载一遍，防止加载失败 0为左 1为右
    private void refreshPlayer(int usertype)
    {
        MatchRoomDto RoomDto = GameModles.Instance.matchRoomDto;
        if (RoomDto.Leftid != -1 && usertype == 0)
        {
            //如果存在左边玩家
            UserDto leftuserDto = RoomDto.UidUdtoDic[RoomDto.Leftid];
            Dispatch(AreoCode.UI, UIEvent.SET_LEFT_PLAYER, leftuserDto);
            Debug.Log("左边玩家信息重新加载完毕");
        }
        else if (RoomDto.Rightid != -1 && usertype == 1)
        {
            //如果存在右边玩家
            UserDto rightuserDto = RoomDto.UidUdtoDic[RoomDto.Rightid];
            Dispatch(AreoCode.UI, UIEvent.SET_RIGHT_PLAYER, rightuserDto);
            Debug.Log("右边玩家信息重新加载完毕");
        }
    }


    //重新加载--不是必要的
    protected override void readystate()
    {
        base.readystate();
        Button_Ready.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SocketMsg = new SocketMsg();
        Button_Ready = transform.Find("Button_Ready").GetComponent<Button>();
        Button_Grab = transform.Find("Button_Grab").GetComponent<Button>();
        Button_NoGrab = transform.Find("Button_NoGrab").GetComponent<Button>();
        Button_Deal = transform.Find("Button_Deal").GetComponent<Button>();
        Button_NoDeal = transform.Find("Button_NoDeal").GetComponent<Button>();

        Button_Ready.onClick.AddListener(readyBtnClicker);
        Button_Grab.onClick.AddListener(grabBtnClicker);
        Button_NoGrab.onClick.AddListener(nograbBtnClicker);
        Button_Deal.onClick.AddListener(dealBtnClicker);
        Button_NoDeal.onClick.AddListener(nodealBtnClicker);

        Dispatch(AreoCode.UI, UIEvent.SET_MY_PLAYER_STATE, GameModles.Instance.userDto);    

        Button_Grab.gameObject.SetActive(false);
        Button_NoGrab.gameObject.SetActive(false);
        Button_Deal.gameObject.SetActive(false);
        Button_NoDeal.gameObject.SetActive(false);
        //Dispatch(AreoCode.UI, UIEvent.PROMPT_PANEL_EVENTCODE, "进入房间成功");
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        Button_Ready.onClick.RemoveAllListeners();
        Button_Grab.onClick.RemoveAllListeners();
        Button_NoGrab.onClick.RemoveAllListeners();
        Button_Deal.onClick.RemoveAllListeners();
        Button_NoDeal.onClick.RemoveAllListeners();
    }

    private void readyBtnClicker()
    {
        //隐藏准备按钮   
        Button_Ready.gameObject.SetActive(false);
        //显示已准备文字
        Text_Ready.gameObject.SetActive(true);
        //向服务器发送玩家准备
        SocketMsg.Change(OpCode.MATCH, MatchCode.READY_CREQ, GameModles.Instance.userDto.ID);
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, SocketMsg);      
    }

    private void grabBtnClicker()
    {

    }

    private void nograbBtnClicker()
    {

    }

    private void dealBtnClicker()
    {

    }

    private void nodealBtnClicker()
    {

    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Awake()
    {
        base.Awake();
        Bind(UIEvent.SHOW_GRAB_BUTTON, UIEvent.SHOW_DEAL_BUTTON,UIEvent.SET_MY_PLAYER_STATE);
        Bind(UIEvent.LEFT_PLAYER_LOADED, UIEvent.RIGHT_PLAYER_LOADED);
    }
}
