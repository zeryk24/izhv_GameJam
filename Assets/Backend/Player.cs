using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    private player _player;

    public int HandSize { get; }
    private CardsGenerator _cardsGenerator;

    private Card[] _cards;
    public Card[] Cards { get => _cards; }

    public int MaxHealth { get; set; }

    private int _health;
    public int Health { get => _health; }

    public int CurrentMaxMana { get; set; }

    private int _currentMana;
    public int CurrentMana { get => _currentMana; }

    public Player(int handSize, int health, player player)
    {
        _player = player;
        HandSize = handSize;
        _cardsGenerator = new CardsGenerator();
        _cards = new Card[handSize];

        _health = health;
        MaxHealth = health;
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
    }

    public void HealHealth(int healthToHeal)
    {
        _health += healthToHeal;
    }

    public void ResetHealth()
    {
        _health = MaxHealth;
    }

    public void SetCurrentMaxMana(int mana)
    {
        CurrentMaxMana = mana;
        _currentMana = mana;
    }

    public void UseMana(int mana)
    {
        _currentMana -= mana;
    }

    public void FillHand()
    {
        _cards = _cardsGenerator.FillHand(HandSize, _player);
    }

    public bool GetRandomCard()
    {
        int index = IsHandFull();
        if (index != -1)
        {
            _cards[index] = _cardsGenerator.GetRandomCard(_player, index);
            return true;
        }
        return false;
    }

    public void ThrowCard(Card card)
    {
        _cards[card.HandIndex] = null;
    }

    public bool HandFull()
    {
        if (IsHandFull() != -1)
            return false;
        return true;
    }

    public bool HandEmpty()
    {
        foreach (var card in _cards)
        {
            if (card != null)
                return false;
        }
        return true;
    }

    private int IsHandFull()
    {
        for (int i = 0; i < HandSize; i++)
        {
            if (_cards[i] == null)
                return i;
        }
        return -1;
    }
}
