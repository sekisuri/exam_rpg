using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Dialogue dialogue1;
    public Dialogue dialogue2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;

    private bool flag = false;
    private void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            flag = true;

            StartCoroutine(EventCoroutine());


        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();
        //theOrder.NotMove();
        theDM.ShowDialogue(dialogue1);

        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move("player","RIGHT");
        theOrder.Move("player", "RIGHT");
        theOrder.Move("player", "UP");

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue2);
    }
}
