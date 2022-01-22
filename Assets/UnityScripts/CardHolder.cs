using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardHolder : MonoBehaviour, IDropHandler
{
    public CardUI PlacedCard = null;
    private GameObject PlacedCardObj = null;
    public System.Action<Card, int, player> CardPlacedEvent = null;
    public System.Func<Card, player, bool> HasEnoughtManaCheck;
    public int Position;
    public player Owner = player.playerB;
    public bool Hand = false;
    // Start is called before the first frame update
    void Start()
    {
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetCardPosition()
    {
        return transform.position;
    }
    public void SetCard(CardUI card)
    {
        if (card != null)
        {
            card.SetCardHolder(this);
            PlacedCard = card;
            PlacedCardObj = card.gameObject;
            if (!Hand && CardPlacedEvent != null)
            {
               CardPlacedEvent(card.Card, Position, Owner);
            }
        }

    }
    public void ClearCard()
    {
        PlacedCard = null;
        PlacedCardObj = null;
    }
    public void DestroyCard()
    {
        if (PlacedCard != null)
        {
            PlacedCard.Die();
        }
        ClearCard();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        PlacedCardObj = droppedObject;
        var card = droppedObject.GetComponent<CardUI>();
        if (card.cardHolder != null) {
            if (Hand)
            {
                return;
            }
            else
            {
                if (card.cardHolder.Owner != Owner) return;
                if (!card.cardHolder.Hand) return;
                if (!HasEnoughtManaCheck(card.Card, Owner)) return;
                SetCard(card);
            }
        }
        else {
            SetCard(card);
        }
    }
}
