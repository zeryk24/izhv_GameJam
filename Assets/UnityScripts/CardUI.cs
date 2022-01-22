using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    public CardHolder cardHolder = null;
    public Card Card = null;
    public UnityEngine.UI.Text AttackText;
    public UnityEngine.UI.Text CostText;
    public Sprite[] imgs = new Sprite[10];
    Image card_image;
    Animator animator;
    public Vector3 target_position;
    bool position_reached = false;
    public void PlayAttackAnimation()
    {
        if (Card.Player == player.playerA)
        {
            animator.Play("Attack");
        }
        else
        {
            animator.Play("Attack2");
        }
    }
    public void Die()
    {
        if (animator != null)
        {
            if (Card.Player == player.playerA)
            {
                animator.Play("Die1");
            }
            else
            {
                animator.Play("Die2");
            }
        }
        Destroy(this.gameObject, 1.0f);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
       // if (!IsPlayerOnTurn(Card.Player)) return;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
       // if (!IsPlayerOnTurn(Card.Player)) return;
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
      //  if (!IsPlayerOnTurn(Card.Player)) return;
        if (cardHolder != null)
        {
            transform.position = cardHolder.GetCardPosition();
        }
    }
    public void SetCardHolder(CardHolder cardHolder)
    {
        if (this.cardHolder != null) this.cardHolder.ClearCard();
        this.cardHolder = cardHolder;
        target_position = cardHolder.GetCardPosition();
        position_reached = false;
    }
    void UpdateImage()
    {
        if (Card.Cost >= 0 && Card.Cost <= 10)
        {
            card_image.sprite = imgs[Card.Cost-1];
        }
    }
    public void UpdateStats()
    {
        if (Card != null)
        {
            AttackText.text = Card.Damage.ToString();
            CostText.text = Card.Cost.ToString();
        
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponentInChildren<Animator>();
        card_image =  transform.GetChild(0).GetChild(0).GetComponent<Image>();
        UpdateStats();
        UpdateImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (position_reached == false)
        {
            float distance = Vector3.Distance(transform.position, target_position);
            if (distance < 3.0f)
            {
                transform.position = target_position;
                position_reached = true;
            }
            else
            {
                var dir = (target_position - transform.position).normalized;
                transform.position += dir * 4.0f;
            }
        }
    }
}
