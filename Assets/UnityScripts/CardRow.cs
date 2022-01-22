using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRow : MonoBehaviour
{
    public CardHolder[] CardHolders = new CardHolder[4];
    public player Owner = player.playerB;
    public bool IsHand;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            CardHolders[i].Position = i;
            CardHolders[i].Owner = Owner;
            CardHolders[i].Hand = IsHand;
        }   
    }

    public void Clear()
    {
        for (int i = 0; i < CardHolders.Length; i++)
        {
            CardHolders[i].DestroyCard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
