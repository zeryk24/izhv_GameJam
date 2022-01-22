using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUI : MonoBehaviour
{
    public CardRow Player1Hand;
    public CardRow Player2Hand;
    public CardRow Player1Board;
    public CardRow Player2Board;

    public UnityEngine.UI.Text Player1Mana;
    public UnityEngine.UI.Text Player2Mana;
    public UnityEngine.UI.Text Player1HP;
    public UnityEngine.UI.Text Player2HP;

    public void SetMana(int player, int amount, int max)
    {
        string text = "Mana: " + amount + " / " + max;
        if (player == 0)
        {
            Player1Mana.text = text; 
        }
        else Player2Mana.text = text;
    }
    public void SetHP(int player, int amount)
    {
        string text = "HP: " + amount;
        if (player == 0)
        {
            Player1HP.text = text;
        }
        else Player2HP.text = text;
    }
    public void Clear()
    {
        Player1Hand.Clear();
        Player2Hand.Clear();
        Player1Board.Clear();
        Player2Board.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
