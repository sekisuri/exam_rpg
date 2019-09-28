using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTest : MonoBehaviour
{
   

    private OrderManager theOrder;
    private NumberSystem theNumber;

    public bool flag;
    public int correctNumber;

    private void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theNumber = FindObjectOfType<NumberSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        flag = true;
        //theOrder.NotMove();
        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => !theNumber.activated);
        //theOrder.Move();

    }
}
