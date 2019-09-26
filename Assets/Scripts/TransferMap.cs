using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;

    public Transform target;
    public BoxCollider2D targetBound;

    private PlayerManager thePlayer;
    private CameraManager theCamera;
    private FadeManager theFade;


    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();
        //theFade = FindObjectOfType<FadeManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

#if true
        Debug.Log("On Triger");
#endif

        if(collision.gameObject.name == "Player")
        {
            // theFade.Fadeout();
            thePlayer.currentMapName = transferMapName;
            //theCamera.SetBound(targetBound);  

            if (true)
            {
                SceneManager.LoadScene(transferMapName);
            }
            else
            {
                theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y,
                theCamera.transform.position.z);
                thePlayer.transform.position = target.transform.position;
            }
            // theFade.FadeIn();
            
        }
    }
}
