using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol.Dto;

public class RightStatePanel : StatePanel
{
    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        switch (eventcode)
        {
            case UIEvent.SET_RIGHT_PLAYER:
                userDto = message as UserDto;
                break;     
        }
    }

    //子类重写父类虚函数
    //若子类同名函数未添加关键字override，则直接执行父类同名函数
    //重写后的virtual函数依旧是virtual函数
    protected override void Awake()
    {
        base.Awake();
        Bind(UIEvent.SET_RIGHT_PLAYER);

        //Bind(UIEvent.PLAYER_READY,UIEvent.PLAYER_ENTER, UIEvent.PLAYER_LEAVE, UIEvent.PLAYER_HIDE_STATE);
    }

    protected override void Start()
    {
        base.Start();

        MatchRoomDto RoomDto = GameModles.Instance.matchRoomDto;
        if (RoomDto.Rightid != -1)
        {
            //如果存在左边玩家
            UserDto userDto = RoomDto.UidUdtoDic[RoomDto.Rightid];
            Dispatch(AreoCode.UI, UIEvent.SET_RIGHT_PLAYER, userDto);
            //更新玩家面板
            Dispatch(AreoCode.UI, UIEvent.PLAYER_ENTER, userDto.ID);

            //如果进入房间前该玩家已经处于准备状态
            if (RoomDto.ReadyUidlist.Contains(RoomDto.Rightid))
            {
                readystate();
            }

            //重新加载--不是必要的
            Dispatch(AreoCode.UI, UIEvent.RIGHT_PLAYER_LOADED, 1);
        }
        else
        {
            SetPanelActive(false);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Update()
    {
        base.Update();
    }
}
