using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protocol.Dto.Fight;

public class UpPanel : UIBase
{
    private Image[] tablecards = null;//底牌
    // Start is called before the first frame update
    void Start()
    {
        tablecards = new Image[3];
        tablecards[0] = transform.Find("Image_CardBack1").GetComponent<Image>();
        tablecards[1] = transform.Find("Image_CardBack2").GetComponent<Image>();
        tablecards[2] = transform.Find("Image_CardBack3").GetComponent<Image>();
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
                setTableCards(message as CardDto[]);
                break;
        }
    }

    private void Awake()
    {
        Bind(UIEvent.SET_TABLE_CARD);
    }

    private void setTableCards(CardDto[] cards)
    {
        //TODO 设置底牌
        tablecards[0].sprite = Resources.Load<Sprite>("Poker/" + cards[0].Name);
        tablecards[1].sprite = Resources.Load<Sprite>("Poker/" + cards[1].Name);
        tablecards[2].sprite = Resources.Load<Sprite>("Poker/" + cards[2].Name);
    }
}
