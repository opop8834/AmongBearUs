using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Misson6 : MonoBehaviour
{
    public bool[] isColor = new bool [4];
    public RectTransform[] rights;
    public LineRenderer[] lines;
    Vector2 clickPos;
    LineRenderer line;
    bool isDrag;
    Animator anim;
    MissonControll mission_script;
    Color leftC, rightC;
    PlayerController player_script;

    float leftY, rightY;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();   //자식으로 존재
        mission_script = FindObjectOfType<MissonControll>();

    }

    private void Update() 
    {
        //드래그
        if (isDrag)
        {
            line.SetPosition(1, new Vector3((Input.mousePosition.x - clickPos.x)*1920f / Screen.width, (Input.mousePosition.y - clickPos.y)*1080f / Screen.height, -10));
            //해상도에 맞게 드래그된다.
            //드래그 끝
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // ray는 박스콜라이더 2d가 아니고 그냥 박스콜라이더를 사용한다
                RaycastHit hit;
                //오른쪽 선에 닿았다면
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject rightLine = hit.transform.gameObject;

                    //오른쪽 선 y값
                    rightY = rightLine.GetComponent<RectTransform>().anchoredPosition.y;

                    //오른쪽 선 색상
                    rightC = rightLine.GetComponent<Image>().color;

                    line.SetPosition(1, new Vector3(500, rightY - leftY ,-10));

                    //색 비교
                    if (leftC == rightC)
                    {
                        switch(leftY)
                        {
                            case 225: isColor[0] = true;
                                break;
                            case 75: isColor[1] = true;
                                break;
                            case -75: isColor[2] = true;
                                break;
                            case -225: isColor[3] = true;
                                break;
                        }
                    }
                    else{
                        switch(leftY)
                        {
                            case 225: isColor[0] = false;
                                break;
                            case 75: isColor[1] = false;
                                break;
                            case -75: isColor[2] = false;
                                break;
                            case -225: isColor[3] = false;
                                break;
                        }
                    }

                    //성공여부 체크
                    if (isColor[0]&&isColor[1]&&isColor[2]&&isColor[3])
                    {
                        Invoke("MissonSuccess", 0.2f);
                    }
                }
                //닿지 않았다면
                else{
                    line.SetPosition(1,new Vector3(0,0,-10));
                }
                isDrag = false;
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
        for (int i =0 ; i < 4 ; i++)
        {
            isColor[i] = false;
            lines[i].SetPosition(1, new Vector3(0,0,-10));
        }

        //랜덤 배치
        for (int i =0 ; i<rights.Length ; i++)
        {
            Vector3 temp = rights[i].anchoredPosition;
            int rand = Random.Range(0,4);
            rights[i].anchoredPosition = rights[rand].anchoredPosition;
            rights[rand].anchoredPosition = temp;
        }
    }

    // X 버튼 누르면 호출
    public void ClickCancel()
    {
        anim.SetBool("isUp", false);
        player_script.MissonEnd();

    }

    //선 누르면 호출
    public void ClickLine(LineRenderer click)
    {
        clickPos = Input.mousePosition;
        line = click;
        
        //왼쪽 선 y값
        leftY = click.transform.parent.GetComponent<RectTransform>().anchoredPosition.y;
        
        //왼쪽 선 색상
        leftC = click.transform.parent.GetComponent<Image>().color;

        isDrag = true;
    }

    //미션 성공하면 호출
    public void MissonSuccess()
    {
        ClickCancel();
        mission_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }

}
