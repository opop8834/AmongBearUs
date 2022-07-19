using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Misson2 : MonoBehaviour
{
    public Transform trash, handle;
    public GameObject bottom;
    public Animator anim_shake;
    MissonControll mission_script;

    bool isDrag, isPlay;
    Vector2 originPos;
    Animator anim;
    PlayerController player_script;
    RectTransform rect_handle;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();   //자식으로 존재
        rect_handle = handle.GetComponent<RectTransform>();
        mission_script = FindObjectOfType<MissonControll>();

        originPos = rect_handle.anchoredPosition;
    }

    private void Update() 
    {
        if (isPlay)
        {
            //드래그
            if (isDrag)
            {
                handle.position = Input.mousePosition;
                rect_handle.anchoredPosition = new Vector2(originPos.x,Mathf.Clamp(rect_handle.anchoredPosition.y, -135, -47));
                //드래그 제한

                anim_shake.enabled = true;

                //드래그 끝
                if (Input.GetMouseButtonUp(0))
                {
                    rect_handle.anchoredPosition = originPos;
                    isDrag = false;
                    anim_shake.enabled = false;
                }
            }

            //쓰레기 배출
            if (rect_handle.anchoredPosition.y <= -130)  //손잡이가 내려가면
            {
                bottom.SetActive(false);
            }
            else{
                bottom.SetActive(true);
            }

            //떨어지면 쓰레기 삭제
            for (int i = 0; i <trash.childCount; i++)
            {
                if (trash.GetChild(i).GetComponent<RectTransform>().anchoredPosition.y <= -600)
                {
                    Destroy(trash.GetChild(i).gameObject);
                }
            }

            //성공여부 체크
            if (trash.childCount == 0)
            {
                MissonSuccess();
                isPlay=false;
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
        for (int i = 0 ; i < trash.childCount ; i++)
        {
            Destroy(trash.GetChild(i).gameObject);
        }

        //쓰레기 스폰
        for (int i = 0 ; i < 10 ; i++)
        {
            // 사과
            GameObject trash_4 = Instantiate(Resources.Load("Trash/Trash_4"),trash) as GameObject;
            trash_4.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180),Random.Range(-180, 180));
            // 랜덤한 위치에서 생성
            trash_4.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0,180));
            // 랜덤한 각도에서 생성

            // 캔
            GameObject trash_5 = Instantiate(Resources.Load("Trash/Trash_5"),trash) as GameObject;
            trash_5.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180),Random.Range(-180, 180));
            trash_5.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0,180));     
        }
        for (int i = 0 ; i < 3 ; i++)
        {
            // 병
            GameObject trash_1 = Instantiate(Resources.Load("Trash/Trash_1"),trash) as GameObject;
            trash_1.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180),Random.Range(-180, 180));
            trash_1.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0,180));

            // 생선
            GameObject trash_2 = Instantiate(Resources.Load("Trash/Trash_2"),trash) as GameObject;
            trash_2.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180),Random.Range(-180, 180));
            trash_2.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0,180));     

            // 비닐
            GameObject trash_3 = Instantiate(Resources.Load("Trash/Trash_3"),trash) as GameObject;
            trash_3.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180),Random.Range(-180, 180));
            trash_3.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0,180));     
        }
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
