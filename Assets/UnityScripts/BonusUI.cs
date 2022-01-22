using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusUI : MonoBehaviour
{
    public Bonus Bonus;
    public UnityEngine.UI.Text BonusText;
    public UnityEngine.UI.Text Cost;

    public void SetBonus(Bonus bonus)
    {
        this.Bonus = bonus;
        BonusText.text = bonus.Description;
        Cost.text = "Cost: " + bonus.Cost.ToString();
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
