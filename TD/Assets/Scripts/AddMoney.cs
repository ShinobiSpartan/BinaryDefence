using UnityEngine;

public class AddMoney : MonoBehaviour
{

    public float income = 50;
    public float interval = 1;
    public float nextIncomeTime;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextIncomeTime)
        {
            GetComponent<PlayerStats>().AddIncome(income);
            nextIncomeTime = Time.deltaTime + interval;
        }
    }



}
