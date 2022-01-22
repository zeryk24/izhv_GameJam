using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardUI BoardUI;
    public CardUIGenerator CardGenerator;
    public BonusUI BonusUI;
    private Board board;
    BonusService bonusService;
    public AIPlayer AiPlayer;
    int level = 1;

    bool ai_played = true;
    int ai_timer = 0;
    int ai_timer_max = 800;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    // player PlayerOnTurn = player.playerA;
    // Start is called before the first frame update
    void Start()
    {
       
        board = new Board(4,4,50);
        bonusService = new BonusService(board);
        for (int i = 0; i < 4; i++)
        {
            BoardUI.Player1Board.CardHolders[i].CardPlacedEvent = HandleUICardChangedPlace;
            BoardUI.Player1Board.CardHolders[i].HasEnoughtManaCheck = HasEnoughMana;
            BoardUI.Player2Board.CardHolders[i].CardPlacedEvent = HandleUICardChangedPlace;
            BoardUI.Player2Board.CardHolders[i].HasEnoughtManaCheck = HasEnoughMana;
        }

        StartNewGame();
      
    }
    void StartNewGame()
    {
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        board.NewGame(level);
        board.StartRound();
  
        BonusUI.SetBonus(bonusService.GetRandomBonus());

        UpdateBoardGUI();
        UpdateGUIHand();
        CardGenerator.UpdateCards();
        AIMakeMove();
    }
    void UpdateBoardGUI()
    {
        BoardUI.SetHP(0, board.PlayerA.Health);
        BoardUI.SetHP(1, board.PlayerB.Health);
        BoardUI.SetMana(0, board.PlayerA.CurrentMana, board.PlayerA.CurrentMaxMana);
        BoardUI.SetMana(1, board.PlayerB.CurrentMana, board.PlayerB.CurrentMaxMana);
    }
    void UpdateGUIHand()
    {
        for (int i = 0; i < 4; i++)
        {
            if (BoardUI.Player1Hand.CardHolders[i].PlacedCard == null)
            {
                CardGenerator.AddCard(board.PlayerA.Cards[i], BoardUI.Player1Hand.CardHolders[i]);
            }
            if (BoardUI.Player2Hand.CardHolders[i].PlacedCard == null)
            {
                CardGenerator.AddCard(board.PlayerB.Cards[i], BoardUI.Player2Hand.CardHolders[i]);
            }
        }
    }
    public void ActivateBonus()
    {
        if (board.PlayerA.CurrentMana >= BonusUI.Bonus.Cost)
        {
            BonusUI.Bonus.Activate(player.playerA);
            BonusUI.SetBonus(bonusService.GetRandomBonus());
            CardGenerator.UpdateCards();
        }
        UpdateBoardGUI();

    }
    public bool IsGameFinished()
    {
        return (board.PlayerA.Health <= 0 || board.PlayerB.Health <= 0);
    }
    public void AIMakeMove()
    {
        ai_played = false;
        ai_timer = 0;
    }
    void HandleCardAttack(Card attacker, Card defender, bool bothDeath)
    {
        CardRow row;
        CardRow attacker_row;
        if (bothDeath)
        {
            BoardUI.Player1Board.CardHolders[defender.BoardIndex].DestroyCard();
            BoardUI.Player2Board.CardHolders[defender.BoardIndex].DestroyCard();
        }
        else
        {
            if (defender.Player == player.playerA)
            {
                row = BoardUI.Player1Board;
                attacker_row = BoardUI.Player2Board;
            }
            else
            {
                row = BoardUI.Player2Board;
                attacker_row = BoardUI.Player1Board;
            }
            row.CardHolders[defender.BoardIndex].DestroyCard();
            attacker_row.CardHolders[attacker.BoardIndex].PlacedCard.PlayAttackAnimation();
        }

    }
    //public bool IsPlayerOnTurn(player player)
    //{
    //    return player == PlayerOnTurn;
    //}
    void HandleHitFace(Card attacker)
    {
        CardRow row;
        if (attacker.Player == player.playerA)
        {
            row = BoardUI.Player1Board;
        }
        else
        {          
            row = BoardUI.Player2Board;
        }
       // print(attacker.BoardIndex);
        row.CardHolders[attacker.BoardIndex].PlacedCard.PlayAttackAnimation();
    }
    void HandleUICardChangedPlace(Card card, int position, player owner)
    {
     
        board.PlayCard(card, position, owner);
        UpdateBoardGUI();
    }
    bool PlayerLost()
    {
        return board.PlayerA.Health <= 0;
    }
    public void EndTurn()
    {
        board.EndRound(HandleCardAttack, HandleHitFace);
        board.StartRound();
        UpdateGUIHand();
        CardGenerator.UpdateCards();
        UpdateBoardGUI();
        if (IsGameFinished())
        {
            if (PlayerLost())
            {
                LoseScreen.SetActive(true);
            }
            else
            {
                WinScreen.SetActive(true);
            }
           
            BoardUI.Clear();
            //CardGenerator.ClearCards();
        }
        else
        {
            AIMakeMove();
        }
    }
    public void NextLevel()
    {
        level++;
        StartNewGame();
        UpdateGUIHand();
    }
    public void ResetLevel()
    {
        level = 1;
        StartNewGame();
        UpdateGUIHand();
    }
    bool HasEnoughMana(Card card, player player)
    {
        int current_mana = 0;
        if (player == player.playerA)
        {
            current_mana = board.PlayerA.CurrentMana;
        }
        else
        {
            current_mana = board.PlayerB.CurrentMana;
        }
        return card.Cost <= current_mana;
    }
    // Update is called once per frame
    void Update()
    {
        if (ai_timer < ai_timer_max)
        {
            ai_timer++;
        }
        else
        {
            if (ai_played == false)
            {
                AiPlayer.MakeMove(board, BoardUI);
                UpdateBoardGUI();
                ai_played = true;
            }
        }
    }
}
