using Assets.Scripts.Net;
using Protocol.Code;
using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : HandlerBase
{
    private static FightHandler instance = new FightHandler();
    public static FightHandler Instance { get
        {
            lock (instance)
            {
                if(instance == null)
                {
                    instance = new FightHandler();
                }
                return instance;
            }
        } }

    private FightHandler() { }

    public override void OnReceive(int subcode, object message)
    {
        switch (subcode)
        {

            case FightCode.DEAL_SRES:
                break;

            case FightCode.GET_CARD_SRES://服务器给客户端发牌
                processGetCard(message as List<CardDto>);
                break;

            case FightCode.PASS_SRES:
                break;

            case FightCode.TURN_LANDLORD_SBOD:
                processTurnLandlord((int)message);
                break;

            case FightCode.GRAB_LANDLORD_SBOD:
                processGrabLandlord(message as LandLordDto);
                break;
        }
    }

    private void processGrabLandlord(LandLordDto landLordDto)
    {
        //播放抢地主声音
        Dispatch(AreoCode.AUDIO, AudioEvent.PLAY_EFFECT_AUDIO, "Fight/Woman_Order");
        //设置地主身份
        Dispatch(AreoCode.UI, UIEvent.PLAYER_CHANGE_IDENTITY, landLordDto.UserId);
        //设置底牌
        Dispatch(AreoCode.UI, UIEvent.SET_TABLE_CARD, landLordDto.TableCardList);
        //给地主发底牌
        addTableCard(landLordDto);
    }

    private void addTableCard(LandLordDto landLordDto)
    {
        int uid = landLordDto.UserId;
        if (uid == GameModles.Instance.userDto.ID)
        {
            Dispatch(AreoCode.CHARACTER, CharacterEvent.ADD_MY_TABLECARDS, landLordDto.TableCardList);
        }
        else if(uid == GameModles.Instance.matchRoomDto.Leftid)
        {
            Dispatch(AreoCode.CHARACTER, CharacterEvent.ADD_LEFT_TABLECARDS, landLordDto.TableCardList);
        }
        else if(uid == GameModles.Instance.matchRoomDto.Rightid)
        {
            Dispatch(AreoCode.CHARACTER, CharacterEvent.ADD_RIGHT_TABLECARDS, landLordDto.TableCardList);
        }
    }

    /// <summary>
    /// 是否是第一个人开始叫地主
    /// </summary>
    private bool isfirstturn = true;
    //轮换抢地主(不叫)
    private void processTurnLandlord(int uid)
    {
        if (isfirstturn)
        {
            isfirstturn = false;//第一次时不播放不叫的音效
        }
        else
        {
            Dispatch(AreoCode.AUDIO, AudioEvent.PLAY_EFFECT_AUDIO, "Fight/Woman_NoOrder");
        }

        if(uid == GameModles.Instance.userDto.ID)
        {
            Dispatch(AreoCode.UI, UIEvent.SHOW_GRAB_BUTTON, true);
        }
    }

    private void processGetCard(List<CardDto> cardList)
    {
        //给自己创建牌
        Dispatch(AreoCode.CHARACTER, CharacterEvent.INIT_MY_CARDLIST, cardList);

        Dispatch(AreoCode.CHARACTER, CharacterEvent.INIT_LEFT_CARDLIST, "初始化左边");
        Dispatch(AreoCode.CHARACTER, CharacterEvent.INIT_RIGHT_CARDLIST, "初始化右边");
    }

}
