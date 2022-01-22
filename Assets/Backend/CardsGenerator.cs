using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGenerator
{
    public Card[] FillHand(int handSize, player player)
    {
        Card[] cards = new Card[handSize];
        for (int i = 0; i < handSize; i++)
        {
            cards[i] = GetRandomCard(player, i);
        }
        return cards;
    }

    public Card GetRandomCard(player player, int handIndex)
    {
        int cost = Mathf.RoundToInt(Random.Range(1f,10f));
        int damage = cost * 2;
        return new Card(damage, cost, player, handIndex);
    }
}
