using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus
{
    public string Description { get; }
    public int Cost { get; }
    public System.Action<player> Activate { get; }

    public Bonus(string description, int cost, System.Action<player> activate)
    {
        Description = description;
        Cost = cost;
        Activate = activate;
    }
}

public class BonusService
{
    private int _healCost = 2;
    private int _reduceRandomHandCost = 1;
    private int _increaseRandomHandDmg = 1;
    private int _increaseRandomBoardDmg = 2;
    private int _reduceRandomEnemyBoardDmg = 2;
    private int _reduceHandCost = 2;
    private int _increaseHandDmg = 2;
    private int _increaseBoardDmg = 2;
    private int _reduceEnemyBoardDmg = 3;


    private Board _board;

    public List<Bonus> Bonuses { get; set; }

    public BonusService(Board board)
    {
        _board = board;
        Bonuses = new List<Bonus>();

        CreateBonuses();
    }

    public Bonus GetRandomBonus()
    {
        int index = Random.Range(0, Bonuses.Count );
        return Bonuses[index];
    }

    private void CreateBonuses()
    {
        Bonuses.Add(new Bonus("Heal your character", _healCost, Heal));
        Bonuses.Add(new Bonus("Reduce cost of random minion in your hand", _reduceRandomHandCost, ReduceRandomHandCost));
        Bonuses.Add(new Bonus("Increase damage of random minion in your hand", _increaseRandomHandDmg, IncreaseRandomHandDmg));
        Bonuses.Add(new Bonus("Increase damage of random ally minion on board", _increaseRandomBoardDmg, IncreaseRandomBoardDmg));
        Bonuses.Add(new Bonus("Reduce damage of random enemy minion on board", _reduceRandomEnemyBoardDmg, ReduceRandomEnemyBoardDmg));
        Bonuses.Add(new Bonus("Reduce cost of all minions in your hand", _reduceHandCost, ReduceHandCost));
        Bonuses.Add(new Bonus("Increase damage of all minions in your hand", _increaseHandDmg, IncreaseHandDmg));
        Bonuses.Add(new Bonus("Increase damage of all ally minions on board", _increaseBoardDmg, IncreaseBoardDmg));
        Bonuses.Add(new Bonus("Reduce damag of all enemy minions on board", _reduceEnemyBoardDmg, ReduceEnemyBoardDmg));
    }

    private void Heal(player player)
    {
        int healthToHeal = Mathf.RoundToInt(Random.Range(3.0f, 10.0f));
        if (player == player.playerA)
        {
            _board.PlayerA.HealHealth(healthToHeal);
            _board.PlayerA.UseMana(_healCost);
        }
        else
        {
            _board.PlayerB.HealHealth(healthToHeal);
            _board.PlayerB.UseMana(_healCost);
        }

    }

    private void ReduceRandomHandCost(player player)
    {
        int randomCard = Mathf.RoundToInt(Random.Range(0.0f, _board.PlayerA.HandSize - 1));
        int reduceCost = Mathf.RoundToInt(Random.Range(1.0f, 3.0f));

        if (player == player.playerA)
        {
            var x = GetRandomHandCard(_board.PlayerA.Cards, randomCard);
            if (x != null)
                x.Cost -= reduceCost;

            _board.PlayerA.UseMana(_reduceRandomHandCost);
        }
        else
        {
            var x = GetRandomHandCard(_board.PlayerB.Cards, randomCard);
            if (x != null)
                x.Cost -= reduceCost;

            _board.PlayerB.UseMana(_reduceRandomHandCost);
        }
    }

    private void IncreaseRandomHandDmg(player player)
    {
        int randomCard = Mathf.RoundToInt(Random.Range(0.0f, _board.PlayerA.HandSize - 1));
        int increaseDmg = Mathf.RoundToInt(Random.Range(1.0f, 5.0f));

        if (player == player.playerA)
        {
            var x = GetRandomHandCard(_board.PlayerA.Cards, randomCard);
            if (x != null)
                x.Damage += increaseDmg;
            _board.PlayerA.UseMana(_increaseRandomHandDmg);
        }
        else
        {
            var x = GetRandomHandCard(_board.PlayerB.Cards, randomCard);
            if (x != null)
                x.Damage += increaseDmg;
            _board.PlayerB.UseMana(_increaseRandomHandDmg);
        }
    }

    private void IncreaseRandomBoardDmg(player player)
    {
        int randomCard = Mathf.RoundToInt(Random.Range(0.0f, _board.BoardSize - 1));
        int increaseDmg = Mathf.RoundToInt(Random.Range(1.0f, 5.0f));

        if (player == player.playerA)
        {
            var x = GetRandomBoardCard(_board.PlayerACards, randomCard);
            if (x != null)
                x.Damage += increaseDmg;
            _board.PlayerA.UseMana(_increaseRandomBoardDmg);
        }
        else
        {
            var x = GetRandomBoardCard(_board.PlayerBCards, randomCard);
            if (x != null)
                x.Damage += increaseDmg;
            _board.PlayerB.UseMana(_increaseRandomBoardDmg);
        }
    }

    private void ReduceRandomEnemyBoardDmg(player player)
    {
        int randomCard = Mathf.RoundToInt(Random.Range(0.0f, _board.BoardSize - 1));
        int reduceDmg = Mathf.RoundToInt(Random.Range(1.0f, 4.0f));

        if (player == player.playerA)
        {
            var x = GetRandomBoardCard(_board.PlayerBCards, randomCard);
            if (x != null)
                x.Damage -= reduceDmg;
            _board.PlayerA.UseMana(_reduceRandomEnemyBoardDmg);
        }
        else
        {
            var x = GetRandomBoardCard(_board.PlayerACards, randomCard);
            if (x != null)
                x.Damage -= reduceDmg;
            _board.PlayerB.UseMana(_reduceRandomEnemyBoardDmg);
        }
    }

    private void ReduceHandCost(player player)
    {
        int reduceCost = Mathf.RoundToInt(Random.Range(1.0f, 2.0f));

        if (player == player.playerA)
        {
            foreach (var card in _board.PlayerA.Cards)
            {
                if (card != null)
                    card.Cost -= reduceCost;
            }
            _board.PlayerA.UseMana(_reduceHandCost);
        }
        else
        {
            foreach (var card in _board.PlayerB.Cards)
            {
                if (card != null)
                    card.Cost -= reduceCost;
            }
            _board.PlayerB.UseMana(_reduceHandCost);
        }
    }

    private void IncreaseHandDmg(player player)
    {
        int increaseDmg = Mathf.RoundToInt(Random.Range(1.0f, 2.0f));

        if (player == player.playerA)
        {
            foreach (var card in _board.PlayerA.Cards)
            {
                if (card != null)
                    card.Damage += increaseDmg;
            }
            _board.PlayerA.UseMana(_increaseHandDmg);
        }
        else
        {
            foreach (var card in _board.PlayerB.Cards)
            {
                if (card != null)
                    card.Damage += increaseDmg;
            }
            _board.PlayerB.UseMana(_increaseHandDmg);
        }
    }

    private void IncreaseBoardDmg(player player)
    {
        int increaseDmg = Mathf.RoundToInt(Random.Range(1.0f, 2.0f));

        if (player == player.playerA)
        {
            foreach (var card in _board.PlayerACards)
            {
                if (card != null)
                    card.Damage += increaseDmg;
            }
            _board.PlayerA.UseMana(_increaseBoardDmg);
        }
        else
        {
            foreach (var card in _board.PlayerBCards)
            {
                if (card != null)
                    card.Damage += increaseDmg;
            }
            _board.PlayerB.UseMana(_increaseBoardDmg);
        }
    }

    private void ReduceEnemyBoardDmg(player player)
    {
        int reduceDmg = Mathf.RoundToInt(Random.Range(1.0f, 2.0f));

        if (player == player.playerA)
        {
            foreach (var card in _board.PlayerBCards)
            {
                if (card != null)
                    card.Damage -= reduceDmg;
            }
            _board.PlayerA.UseMana(_reduceEnemyBoardDmg);
        }
        else
        {
            foreach (var card in _board.PlayerACards)
            {
                if (card != null)
                    card.Damage -= reduceDmg;
            }
            _board.PlayerB.UseMana(_reduceEnemyBoardDmg);
        }
    }

    private Card GetRandomHandCard(Card[] hand, int random)
    {
        if (hand[random] != null)
            return hand[random];

        int i = 0;
        while (i < _board.BoardSize)
        {
            random = (random + 1) % _board.PlayerA.HandSize;

            if (hand[random] != null)
                return hand[random];

            i++;
        }
        return null;
    }

    private Card GetRandomBoardCard(Card[] board, int random)
    {
        if (board[random] != null)
            return board[random];

        int i = 0;
        while (i < _board.BoardSize)
        {
            random = (random + 1) % _board.BoardSize;

            if (board[random] != null)
                return board[random];

            i++;
        }
        return null;
    }
}
