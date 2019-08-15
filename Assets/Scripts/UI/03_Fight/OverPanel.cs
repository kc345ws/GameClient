using Protocol.Code;
using Protocol.Constants;
using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverPanel : UIBase
{
    private Image Image_BG;
    private Text Text_Result;
    private Text Text_BeenResult;
    private Button Button_Back;

    private SceneLoadMsg sceneLoadMsg;
    private SocketMsg socketMsg;
    // Start is called before the first frame update
    void Start()
    {
        Bind(UIEvent.SHOW_OVER_PANEL);
        //Bind(FightCode.GAME_OVER_SBOD);
        Bind(UIEvent.SET_OVER_PANEL_MESSAGE);

        sceneLoadMsg = new SceneLoadMsg();
        socketMsg = new SocketMsg();

        Image_BG = transform.Find("Image_BG").GetComponent<Image>();
        Text_Result = transform.Find("Text_Result").GetComponent<Text>();
        Text_BeenResult = transform.Find("Text_BeenResult").GetComponent<Text>();
        Button_Back = transform.Find("Button_Back").GetComponent<Button>();

        Button_Back.onClick.AddListener(backBtnClicker);

        SetPanelActive(false);
    }

    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        switch (eventcode)
        {
            case UIEvent.SHOW_OVER_PANEL:
                SetPanelActive((bool)message);
                break;

            /*case FightCode.GAME_OVER_SBOD:
                setMessage(message as OverDto);
                break;*/
            case UIEvent.SET_OVER_PANEL_MESSAGE:
                setMessage(message as OverDto);
                break;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Button_Back.onClick.RemoveAllListeners();
    }

    private void backBtnClicker()
    {
        sceneLoadMsg.Change(1, "02_Main", () => {
            socketMsg.Change(OpCode.USER, UserCode.GET_USER_CREQ, "0");
            Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
        });
        Dispatch(AreoCode.SCENE, SceneCode.LOAD_SCENE, sceneLoadMsg);

        socketMsg.Change(OpCode.FIGHT, FightCode.PLAYER_LEAVE_CREQ, "玩家离开");
        Dispatch(AreoCode.NET, NetEvent.SENDMSG, socketMsg);
    }

    private void setMessage(OverDto overDto)
    {
        bool isfound = false;
        if(overDto.WinIdentity == PlayerIdentity.FRAMER)
        {
            //农民胜利
            Text_Result.text = "农民胜利";
            foreach (var item in overDto.WinidList)
            {
                if(item.UserID == GameModles.Instance.userDto.ID)
                {
                    Text_BeenResult.text = "欢乐豆:+" + overDto.BennCount;
                    isfound = true;
                    Dispatch(AreoCode.AUDIO, AudioEvent.PLAY_EFFECT_AUDIO, "Fight/MusicEx_Win");
                    break;
                }
            }
            if (!isfound)
            {
                Text_BeenResult.text = "欢乐豆:-" + overDto.BennCount;
                Dispatch(AreoCode.AUDIO, AudioEvent.PLAY_EFFECT_AUDIO, "Fight/MusicEx_Lose");
            }               
        }
        else
        {
            Text_Result.text = "地主胜利";
            foreach (var item in overDto.WinidList)
            {
                if (item.UserID == GameModles.Instance.userDto.ID)
                {
                    Text_BeenResult.text = "欢乐豆:+" + overDto.BennCount;
                    isfound = true;
                    Dispatch(AreoCode.AUDIO, AudioEvent.PLAY_EFFECT_AUDIO, "Fight/MusicEx_Win");
                    break;
                }
            }
            if (!isfound)
            {
                Text_BeenResult.text = "欢乐豆:-" + overDto.BennCount;
                Dispatch(AreoCode.AUDIO, AudioEvent.PLAY_EFFECT_AUDIO, "Fight/MusicEx_Lose");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
