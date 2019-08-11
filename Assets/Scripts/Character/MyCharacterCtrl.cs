using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自己角色的控制器
/// </summary>
public class MyCharacterCtrl : CharacterBase
{
    private List<CardDto> myCardList;
    private Transform cardTransformParent;//卡牌的父物体
    private GameObject cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        myCardList = new List<CardDto>();
        cardTransformParent = transform.Find("CardPoint");
        cardPrefab = Resources.Load<GameObject>("Card/MyCard");
    }

    /// <summary>
    /// 停顿使发牌有动画感
    /// </summary>
    /// <returns></returns>
    private IEnumerator initPlayerCard(List<CardDto> cardList)
    {
        for(int i = 0; i < 17; i++)
        {
            createCard(cardList[i], i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void createCard(CardDto cardDto, int index)
    {
        GameObject card = GameObject.Instantiate(cardPrefab, cardTransformParent);
        card.transform.localPosition = new Vector2(index * 0.2f, 0);
        card.name = cardDto.Name;
        CardCtrl cardCtrl = card.GetComponent<CardCtrl>();
        cardCtrl.Init(cardDto, true, index);

        myCardList.Add(cardDto);
    }

    private void Awake()
    {
        Bind(CharacterEvent.INIT_MY_CARDLIST);
    }

    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        switch (eventcode)
        {
            case CharacterEvent.INIT_MY_CARDLIST:
                StartCoroutine(initPlayerCard(message as List<CardDto>));
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
