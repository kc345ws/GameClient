using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protocol.Dto.Fight;
using Protocol.Code;

public class UpPanel : UIBase
{
    private Image[] tablecards = null;//底牌
    private Button Button_Back;//返回游戏主界面
    private SceneLoadMsg sceneLoadMsg;
    private SocketMsg socketMsg;
    // Start is called before the first frame update
    void Start()
    {
        tablecards = new Image[3];
        tablecards[0] = transform.Find("Image_CardBack1").GetComponent<Image>();
        tablecards[1] = transform.Find("Image_CardBack2").GetComponent<Image>();
        tablecards[2] = transform.Find("Image_CardBack3").GetComponent<Image>();

        Button_Back = transform.Find("Button_Back").GetComponent<Button>();
        Button_Back.onClick.AddListener(backBtnClicker);

        sceneLoadMsg = new SceneLoadMsg();
        socketMsg = new SocketMsg();
    }

    private void backBtnClicker()
    {
        sceneLoadMsg.Change(1, "02_Main", ()=> {
            socketMsg.Change(OpCode.USER, UserCode.GET_USER_CREQ, "0");
            Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        });       
        Dispatch(AreoCode.SCENE, SceneCode.LOAD_SCENE, sceneLoadMsg);

        socketMsg.Change(OpCode.FIGHT, FightCode.PLAYER_LEAVE_CREQ, "玩家离开");
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);

        socketMsg.Change(OpCode.MATCH, MatchCode.LEAVE_CREQ, "发起离开匹配请求");
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Button_Back.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Execute(int eventcode, object message)
    {
        switch (eventcode)
        {
            case UIEvent.SET_TABLE_CARD:
                setTableCards(message as List<CardDto>);
                break;
        }
    }

    private void Awake()
    {
        Bind(UIEvent.SET_TABLE_CARD);
    }

    private void setTableCards(List<CardDto> cardlist)
    {
        //设置底牌
        tablecards[0].sprite = Resources.Load<Sprite>("Poker/" + cardlist[0].Name);
        tablecards[1].sprite = Resources.Load<Sprite>("Poker/" + cardlist[1].Name);
        tablecards[2].sprite = Resources.Load<Sprite>("Poker/" + cardlist[2].Name);
    }
}
