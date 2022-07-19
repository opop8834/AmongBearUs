using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Misson5 : MonoBehaviour
{
    public Transform rotate, handle;

    public Color blue;
    public Color red;
    bool isDrag, isPlay;
    MissonControll mission_script;
    Animator anim;
    float rand;
    PlayerController player_script;
    RectTransform rect_handle;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();   //자식으로 존재
        rect_handle = handle.GetComponent<RectTransform>();
        mission_script = FindObjectOfType<MissonControll>();

    }

    private void Update() 
    {
        if (isPlay)
        {
            //드래그
            if (isDrag)
            {
                handle.position = Input.mousePosition;
                rect_handle.anchoredPosition = new Vector2(184,Mathf.Clamp(rect_handle.anchoredPosition.y, -195, 195));
                //드래그 제한


                //드래그 끝
                if (Input.GetMouseButtonUp(0))
                {
                    //성공여부 체크
                    if (rect_handle.anchoredPosition.y >-5 && rect_handle.anchoredPosition.y<5)
                    {
                        Invoke("MissonSuccess",0.2f);
                        isPlay = true;  //update 함수안에는 여러번 반복되므로 한번만 판단하게
                    }
                    isDrag = false;
                }
            }

            rotate.eulerAngles = new Vector3(0,0, 90 * rect_handle.anchoredPosition.y / 195);

            //색 변경
            if (rect_handle.anchoredPosition.y >-5 && rect_handle.anchoredPosition.y<5)
            {
                rotate.GetComponent<Image>().color = blue;
            }
            else{
                rotate.GetComponent<Image>().color = red;
            }
        }
    }

    //미션시작
    public void MissonStart()
    {
        anim.SetBool("isUp", true);
        player_script = FindObjectOfType<PlayerController>();
        // Start 함수에 넣지않는 이유는 캐릭터는 미션이 시작될때만 생성되기 때문이다
        
        //초기화
        rand = 0;

        //랜덤
        rand = Random.Range(-195, 195);
        while(rand >= -10 && rand <= 10)
        {
            rand = Random.Range(-195, 195);        
        }
        rect_handle.anchoredPosition = new Vector2(184, rand);
        isPlay = true;
    }

    // X 버튼 누르면 호출
    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        player_script.MissonEnd();

    }

    //손잡이 누르면 호출

    public void ClickHandle()
    {
        isDrag = true;
    }

    //미션 성공하면 호출
    public void MissonSuccess()
    {
        ClickCancel();
        mission_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }

}
