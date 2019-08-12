﻿using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol.Constants;

/// <summary>
/// 自己角色的控制器
/// </summary>
public class MyCharacterCtrl : CharacterBase
{
    private List<CardDto> myCardList;
    private List<CardCtrl> CardCtrllist;
    private Transform cardTransformParent;//卡牌的父物体
    private GameObject cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Bind(CharacterEvent.INIT_MY_CARDLIST);
        Bind(CharacterEvent.ADD_MY_TABLECARDS);

        myCardList = new List<CardDto>();
        CardCtrllist = new List<CardCtrl>();
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
        CardCtrllist.Add(cardCtrl);
    }

    private void addTableCard(List<CardDto> cardlist)
    {
        int index = myCardList.Count;
        foreach (var item in cardlist)
        {
            myCardList.Add(item);
        }
        CardWeight.SortCard(ref myCardList);

        //复用先前创建的牌
        for(int i = 0; i < 17; i++)
        {
            CardCtrllist[i].gameObject.SetActive(true);
            CardCtrllist[i].Init(myCardList[i], true, i);
        }

        for (int i = index; i < 20; i++)
        {
            GameObject card = GameObject.Instantiate(cardPrefab, cardTransformParent);
            card.transform.localPosition = new Vector2(index * 0.2f, 0);
            card.name = myCardList[i].Name;
            CardCtrl cardCtrl = card.GetComponent<CardCtrl>();
            cardCtrl.Init(myCardList[i], true, index);

            CardCtrllist.Add(cardCtrl);
            index++;
        }
    }

    private void Awake()
    {     
    }

    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        switch (eventcode)
        {
            case CharacterEvent.INIT_MY_CARDLIST:
                StartCoroutine(initPlayerCard(message as List<CardDto>));
                break;

            case CharacterEvent.ADD_MY_TABLECARDS:
                addTableCard(message as List<CardDto>);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
