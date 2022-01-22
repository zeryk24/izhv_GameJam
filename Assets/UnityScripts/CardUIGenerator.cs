using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUIGenerator : MonoBehaviour
{
    public GameObject CardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCards()
    {
        foreach (Transform child in transform)
        {
            var card = child.GetComponent<CardUI>();
            card.UpdateStats();
        }
    }
    public void ClearCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void AddCard(Card card, CardHolder cardHolder)
    {
        GameObject new_card = Instantiate(CardPrefab);
        new_card.transform.SetParent(this.transform);
        new_card.transform.localScale = new Vector3(0.8f, 1.0f, 1.0f);
        var cardComponent = new_card.GetComponent<CardUI>();
        cardComponent.Card = card;
        cardHolder.SetCard(cardComponent);

    }
}
