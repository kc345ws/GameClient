using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCharacterCtrl : CharacterBase
{
    private Transform cardTransformParent;//卡牌的父物体
    private GameObject cardPrefab;
    private int Index = 0;
    private static Object Lock = new Object();


    private void Awake()
    {
        
    }

    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        switch (eventcode)
        {
            case CharacterEvent.INIT_LEFT_CARDLIST:
                StartCoroutine(initPlayerCard());
                break;

            case CharacterEvent.ADD_LEFT_TABLECARDS:
                addTableCard();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Bind(CharacterEvent.INIT_LEFT_CARDLIST);
        Bind(CharacterEvent.ADD_LEFT_TABLECARDS);

        cardTransformParent = transform.Find("CardPoint");
        cardPrefab = Resources.Load<GameObject>("Card/OtherCard");
    }

    /// <summary>
    /// 停顿使发牌有动画感
    /// </summary>
    /// <returns></returns>
    private IEnumerator initPlayerCard()
    {
        for (int i = 0; i < 17; i++)
        {
            createCard(i);
            lock (Lock)
            {
                Index++;
            }
            yield return new WaitForSeconds(0.1f);       
        }
    }

    private void addTableCard()
    {
        for(int i = 0; i < 3; i++)
        {
            createCard(Index);
            Index++;
        }
    }

    private void createCard(int index)
    {
        GameObject card = GameObject.Instantiate(cardPrefab, cardTransformParent);
        card.transform.localPosition = new Vector2(index * 0.15f, 0);
        card.GetComponent<SpriteRenderer>().sortingOrder = index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
