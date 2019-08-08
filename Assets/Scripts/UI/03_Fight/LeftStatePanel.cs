using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol.Dto;
using Protocol.Dto.Fight;

/// <summary>
/// 左边玩家的状态面板
/// </summary>
public class LeftStatePanel:StatePanel
{
    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        //int uid;
        switch (eventcode)
        {
            case UIEvent.SET_LEFT_PLAYER:
                userDto = message as UserDto;
                break;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Bind(UIEvent.SET_LEFT_PLAYER);
    }

    protected override void Start()
    {
        base.Start();

        MatchRoomDto RoomDto = GameModles.Instance.matchRoomDto;

        if (RoomDto.Leftid != -1)
        {
            //如果存在左边玩家
            UserDto userDto = RoomDto.UidUdtoDic[RoomDto.Leftid];
            Dispatch(AreoCode.UI, UIEvent.SET_LEFT_PLAYER, userDto);
            //更新玩家面板
            Dispatch(AreoCode.UI, UIEvent.PLAYER_ENTER, userDto.ID);

            //如果进入房间前该玩家已经处于准备状态
            if (RoomDto.ReadyUidlist.Contains(RoomDto.Leftid))
            {
                readystate();
            }

            //重新加载--不是必要的
            Dispatch(AreoCode.UI, UIEvent.LEFT_PLAYER_LOADED, 0);
        }
        else
        {
            SetPanelActive(false);
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}

