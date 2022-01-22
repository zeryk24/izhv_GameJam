using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeMove(Board board, BoardUI boardUi)
    {
        int random_card = Random.Range(0, board.PlayerB.HandSize);
        for (int i = 0; i < board.PlayerB.HandSize; i++)
        {
            int current_cost = board.PlayerB.CurrentMana;
            int index = (random_card + i) % board.PlayerB.HandSize;
            var card = board.PlayerB.Cards[index];
            print(card == null);
            if (card != null)
            {
                if (card.Cost > current_cost) continue;
                int random_spot = Random.Range(0, board.BoardSize);
                for (int j = 0; j < board.BoardSize; j++)
                {
                    int index2 = (random_spot + j) % board.BoardSize;
                    if (board.PlayerBCards[index2] == null)
                    {
                      //  print("From: " + index + " TO: "+ index2);
                        boardUi.Player2Board.CardHolders[index2].SetCard(boardUi.Player2Hand.CardHolders[index].PlacedCard);
                        break;
                    }
                }
            }
        }

    }
}
