using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    //text for the moneyText
    public Text moneyText;
    
    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + PlayerStats.money.ToString();
    }
}
