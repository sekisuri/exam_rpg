using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    BGMManager bgm;

    public int playTrack;
    private void Start()
    {
        bgm = FindObjectOfType<BGMManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(abc());
    }

    IEnumerator abc()
    {
        bgm.FadeOutBGM();
        yield return new WaitForSeconds(3f);
        bgm.FadeInBGM();
    }
}
