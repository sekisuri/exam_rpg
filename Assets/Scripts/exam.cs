using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exam : MonoBehaviour
{
    BGMManager bgm;

    public int playTrack;
    private void Start()
    {
        bgm = FindObjectOfType<BGMManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bgm.BGMPlay(playTrack);
        this.gameObject.SetActive(false);
    }

    
}
