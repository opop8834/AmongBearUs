using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Misson1 : MonoBehaviour
{
    public Color red;
    public Image[] images;  //이미 빨간색이 랜덤하게 배치돼있게
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
        for (int i = 0 ; i< 4 ; i++)
        {
            images[i].color = Color.white;
        }


        //랜덤
        for (int i = 0 ; i< 4 ; i++)
        {
            int rand = Random.Range(0,7);   //0에서 6까지 랜덤
            images[rand].color = red;
        }
    }

    // X 버튼 누르면 호출
    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        player_script.MissonEnd();

    }

    //육각형 버튼 누르면 호출
    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        
        if (img.color == Color.white)
        {
            //빨간색으로
            img.color = red;
        }
        else{
            //하얀색으로
            img.color = Color.white;
        }
        
        // 미션 성공여부 체크
        int count = 0;
        for (int i = 0; i< images.Length ; i++)
        {
            if (images[i].color == Color.white)
            {
                count++;
            }
        }
        if (count == images.Length)
        {
            //성공
            Invoke("MissonSuccess", 0.2f);  //몇초정도의 지연을 만들어주고 함수호출
        }
    }

    //미션 성공하면 호출
    public void MissonSuccess()
    {
        ClickCancel();
        mission_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }

}
