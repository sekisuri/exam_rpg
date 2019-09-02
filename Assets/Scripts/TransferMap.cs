using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;

    public Transform target;

    private MovingObject thePlayer;
    private CameraManager theCamera;

    public bool flag = false;

    private void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>();
        theCamera = FindObjectOfType<CameraManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

#if true
        Debug.Log("On Triger");
#endif

        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            //  

            if (flag)
            {
                SceneManager.LoadScene(transferMapName);
            }
            else
            {
                theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y,
                theCamera.transform.position.z);
                thePlayer.transform.position = target.transform.position;
            }
            
        }
    }
}
