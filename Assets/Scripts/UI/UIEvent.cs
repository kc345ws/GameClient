﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 100-200
/// </summary>
public static class UIEvent{
    //开始面板事件
    public const int START_PANEL_EVENTCODE = 100;
    //注册面板事件
    public const int REGISTER_PANEL_EVENTCODE = 101;
    //提示面板事件
    public const int PROMPT_PANEL_EVENTCODE = 102;
    //角色信息面板事件
    public const int INFO_PANEL_EVENTCODE = 103;
    //显示进入房间按钮
    public const int SHOW_ROOM_ENTER_BUTTON = 104;
    //显示创建面板
    public const int SHOW_CREATE_PANEL = 105;


    //设置底牌
    public const int SET_TABLE_CARD = 106;
  
    //玩家准备
    public const int PLAYER_READY = 107;
    //玩家进入
    public const int PLAYER_ENTER = 108;
    //玩家离开
    public const int PLAYER_LEAVE = 109;
    //玩家改变身份
    public const int PLAYER_CHANGE_IDENTITY = 110;
    //设置左边玩家的数据
    public const int SET_LEFT_PLAYER = 111; 
    //角色聊天
    public const int PLAYER_CHAT = 112;
    //开始游戏,隐藏角色面板
    public const int PLAYER_HIDE_STATE = 113;
    //设置右边玩家的数据
    public const int SET_RIGHT_PLAYER = 114;
    //显示抢按钮
    public const int SHOW_GRAB_BUTTON = 115;
    //显示出牌按钮
    public const int SHOW_DEAL_BUTTON = 116;
    //设置自身用户信息
    public const int SET_MY_PLAYER_STATE = 117;

    //左边玩家加载完成
    public const int LEFT_PLAYER_LOADED = 118;
    //右边玩家加载完成
    public const int RIGHT_PLAYER_LOADED = 119;

    //显示结束面板
    public const int SHOW_OVER_PANEL = 120;

    //设置结束面板信息
    public const int SET_OVER_PANEL_MESSAGE = 121;
}
