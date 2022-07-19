using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 스틱 드래그 + 제한
// 2. 드래그 한만큼 캐릭터 이동
public class JoyStick : MonoBehaviour
{
    Animator anim;
    public RectTransform stick, backGround;
    bool isDrag;
    public float speed;
    float limit;
    PlayerController playercontroller;

    private void Start() 
    {
        anim = GetComponent<Animator>();
        limit = backGround.rect.width * 0.5f; 
        //원의 반지름 만큼 제한
    }
    private void Update() 
    {
        //드래그 하는 동안
        if (isDrag)
        {
            Vector2 vec = Input.mousePosition - backGround.position;
            // 큰 동그라미를 기준으로 얼마큼 떨어졌는지 계산
            stick.localPosition = Vector2.ClampMagnitude(vec, limit);
            //제한을 둔 값을 다시 포지션에 넣는다        

            Vector3 dir = (stick.position - backGround.position).normalized;
            transform.position += dir * speed * Time.deltaTime;   

            anim.SetBool("isWalk", true);

            //왼쪽으로 이동
            if (dir.x < 0)
            {
                transform.localScale = new Vector3(-1,1,1); //좌우반전
            }
            //오른쪽으로 이동
            else{
                transform.localScale = new Vector3(1,1,1);
            }

            //드래그가 끝나면
            if (Input.GetMouseButtonUp(0))
            {
                stick.localPosition = new Vector3(0, 0, 0);
                // 그냥 position은 씬에서 자기의 위치 local은 부모를 기준으로 한 포지션
                //stick 이기 때문에 그 기준으로 돌아옴
                anim.SetBool("isWalk", false);
                isDrag = false;
            }
        }
    }
    //스틱을 누르면 실행될 함수
    public void ClickStick()
    {
        isDrag = true;
    }

}
