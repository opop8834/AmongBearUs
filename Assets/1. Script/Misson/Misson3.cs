using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Misson3 : MonoBehaviour
{
    public Text inputText, keyCode;
    Animator anim;
    PlayerController player_script;
    MissonControll mission_script;
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
        inputText.text = "";
        keyCode.text = "";

        //키코드 랜덤
        for (int i =0 ; i< 5 ; i++)
        {
            keyCode.text += Random.Range(0, 10);  //0 에서 9까지

        }
    }

    // X 버튼 누르면 호출
    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        player_script.MissonEnd();

    }

    public void ClickNumber()
    {
        if (inputText.text.Length <= 4)
        {
            inputText.text += EventSystem.current.currentSelectedGameObject.name;
        }
    }

    //계산기 삭제 버튼 누르면 호출
    public void ClickDelete()
    {
        if (inputText.text != "")
        {
            inputText.text = inputText.text.Substring(0,inputText.text.Length-1);  // 즉 마지막 글자만 지우는
        }
    }

    //체크버튼 누르면 호출
    public void ClickCheck()
    {
        if (inputText.text == keyCode.text)
        {
            MissonSuccess();
        }
    }


    //미션 성공하면 호출
    public void MissonSuccess()
    {
        ClickCancel();
        mission_script.MissionSuccess(GetComponent<CircleCollider2D>());

    }

}
