using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    private int _level;

    public int BoardSize { get; }

    public Card[] PlayerACards { get; set; }
    public Card[] PlayerBCards { get; set; }

    public Player PlayerA { get; set; }
    public Player PlayerB { get; set; }

    public Board(int boardSize, int playerHandSize, int playerHealth)
    {
        BoardSize = boardSize;

        PlayerA = new Player(playerHandSize, playerHealth, player.playerA);
        PlayerB = new Player(playerHandSize, playerHealth, player.playerB);
    }

    public void NewGame(int level)
    {
        _level = level;
        PlayerA.ResetHealth();
        PlayerB.MaxHealth += 5 * (level - 1);
        PlayerB.ResetHealth();

        PlayerACards = new Card[BoardSize];
        PlayerBCards = new Card[BoardSize];

        PlayerA.FillHand();
        PlayerB.FillHand();
    }

    public void StartRound()
    {
        while (PlayerA.HandFull() != true)
            PlayerA.GetRandomCard();

        while (PlayerB.HandFull() != true)
            PlayerB.GetRandomCard();


        int currentMaxMana = Mathf.RoundToInt(Random.Range(5.0f, 10.0f));
        PlayerA.SetCurrentMaxMana(currentMaxMana);
        PlayerB.SetCurrentMaxMana(currentMaxMana + (1 * (_level-1)));
    }

    public void PlayCard(Card card, int position, player player)
    {
        if (player == player.playerA)
        {
            PlayerA.UseMana(card.Cost);

            PlayerACards[position] = card;
            PlayerA.ThrowCard(card);
        }
        else
        {
            PlayerB.UseMana(card.Cost);


            PlayerBCards[position] = card;
            PlayerB.ThrowCard(card);
        }
        card.BoardIndex = position;
    }

    public void EndRound(System.Action<Card, Card, bool> fight, System.Action<Card> hitFace)
    {
        for (int i = 0; i < BoardSize; i++)
        {
            if (PlayerACards[i] != null && PlayerBCards[i] != null)
            {
                if (PlayerACards[i].Damage > PlayerBCards[i].Damage)
                {
                    fight(PlayerACards[i], PlayerBCards[i], false);             //dies B
                    PlayerBCards[i] = null;
                }
                else if (PlayerBCards[i].Damage > PlayerACards[i].Damage)
                {
                    fight(PlayerBCards[i], PlayerACards[i], false);             //dies A
                    PlayerACards[i] = null;
                }
                else
                {
                    fight(PlayerACards[i], PlayerBCards[i], true); ;            //dies both
                    PlayerACards[i] = null;
                    PlayerBCards[i] = null;
                }
            }
            else if (PlayerACards[i] != null)
            {
                hitFace(PlayerACards[i]);           //A hits face
                PlayerB.GetDamage(PlayerACards[i].Damage);
            }
            else if (PlayerBCards[i] != null)
            {
                hitFace(PlayerBCards[i]);           //B hits face
                PlayerA.GetDamage(PlayerBCards[i].Damage);
            }
        }
    }

}
