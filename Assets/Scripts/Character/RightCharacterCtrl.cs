using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCharacterCtrl : CharacterBase
{
    private Transform cardTransformParent;//卡牌的父物体
    private GameObject cardPrefab;

    private void Awake()
    {
        Bind(CharacterEvent.INIT_RIGHT_CARDLIST);
    }

    public override void Execute(int eventcode, object message)
    {
        base.Execute(eventcode, message);
        switch (eventcode)
        {
            case CharacterEvent.INIT_RIGHT_CARDLIST:
                StartCoroutine(initPlayerCard());
                break;
        }
    }

    private IEnumerator initPlayerCard()
    {
        for (int i = 0; i < 17; i++)
        {
            createCard(i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void createCard(int index)
    {
        GameObject card = GameObject.Instantiate(cardPrefab, cardTransformParent);
        card.transform.localPosition = new Vector2(index * 0.15f, 0);
        card.GetComponent<SpriteRenderer>().sortingOrder = index;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardTransformParent = transform.Find("CardPoint");
        cardPrefab = Resources.Load<GameObject>("Card/OtherCard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
