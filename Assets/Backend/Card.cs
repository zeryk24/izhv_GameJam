using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum player
{
    playerA,
    playerB
}

public class Card
{
    public int Damage { get; set; }
    public int Cost { get; set; }

    private int _boardIndex;
    public int BoardIndex
    {
        get => _boardIndex;
        set 
        { 
            _boardIndex = value;
            _handIndex = -1;
        }
    }

    private int _handIndex;
    public int HandIndex { get => _handIndex; }
    public player Player { get; }

    public Card(int damage, int cost, player player, int handIndex)
    {
        Damage = damage;
        Cost = cost;
        BoardIndex = -1;
        Player = player;
        _handIndex = handIndex;
    }
}

