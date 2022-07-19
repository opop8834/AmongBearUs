using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControll : MonoBehaviour
{
    public Sprite[] idles, dead;
    SpriteRenderer sr;

    int rand;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        rand = Random.Range(0, 5);
        sr.sprite = idles[rand];
    }
    //죽은 이미지
    public void Dead()
    {
        sr.sprite = dead[rand];
        sr.sortingOrder = -1;
    }

}
