using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CharacterEvent
{
    /// <summary>
    /// 初始化自己角色的手牌
    /// </summary>
    public const int INIT_MY_CARDLIST = 1000;
    public const int INIT_LEFT_CARDLIST = 1001;
    public const int INIT_RIGHT_CARDLIST = 1002;

    //给自己的角色添加底牌
    public const int ADD_MY_TABLECARDS = 1003;
    public const int ADD_LEFT_TABLECARDS = 1004;
    public const int ADD_RIGHT_TABLECARDS = 1005;
}

