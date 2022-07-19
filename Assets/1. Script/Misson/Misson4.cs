using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Misson4 : MonoBehaviour
{
    public Color blue;
    public Transform numbers;
    Animator anim;
    PlayerController player_script;
    MissonControll mission_script;
    int count;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();   //자식으로 존재
        mission_script = FindObjectOfType<MissonControll>();
    }

    //미션시작
    public void MissonStart()
    {
        anim.SetBool("isUp", true);
        player_script = FindObjectOfType<PlayerController>();
        // Start에 넣지않는 이유는 캐릭터는 미션이 시작될때 생성되기 때문이다

        //초기화
        for (int i =0 ; i< numbers.childCount ; i++)
        {
            numbers.GetChild(i).GetComponent<Image>().color = Color.white;
            numbers.GetChild(i).GetComponent<Button>().enabled = true;

        }
        //숫자 랜덤 배치
        for (int i =0 ; i<10 ; i++)
        {
            Sprite temp = numbers.GetChild(i).GetComponent<Image>().sprite;

            int rand = Random.Range(0, 10);
            numbers.GetChild(i).GetComponent<Image>().sprite = numbers.GetChild(rand).GetComponent<Image>().sprite;
            numbers.GetChild(rand).GetComponent<Image>().sprite = temp;

        }
        count = 1;
    }

    // X 버튼 누르면 호출
    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        player_script.MissonEnd();

    }

    //숫자버튼 누르면 호출
    public void ClickNumber()
    {
        if (count.ToString() == EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name)  //지금 누른 현재 이미지 숫자 추출
        {
            // 색 변경
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = blue;

            //버튼 비활성화
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
            count++;

            //성공여부 체크
            if (count == 11)
            {
                Invoke("MissonSuccess", 0.2f);
            }
        }

    }

    //미션 성공하면 호출
    public void MissonSuccess()
    {
        ClickCancel();
        mission_script.MissionSuccess(GetComponent<CircleCollider2D>());

    }

}
