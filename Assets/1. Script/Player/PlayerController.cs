using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// 설정 UI를 클릭했을때를 파악하기 위함

public class PlayerController : MonoBehaviour
{
    public Button btn;
    Animator anim;
    public GameObject joyStick, mainView, playView;
    public Sprite use, kill;
    public Text text_cool;

    GameObject coll;
    KillControll kill_script;

    public bool isCantMove, isMission;

    public Settings settings_script;
    public float speed;

    float timer;
    bool isCool,isAnim;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        Camera.main.transform.parent = transform;   //메인 카메라의 부모를 character의 자식으로 들어간다.
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);   //들어가면 위치를 초기화 시킨다.
        
        //미션이라면
        if (isMission)
        {
            btn.GetComponent<Image>().sprite = use;
            text_cool.text = "";
        }
        //킬 퀘스트라면
        else{
            kill_script = FindObjectOfType<KillControll>();
            btn.GetComponent<Image>().sprite = kill;
            timer = 5;
            isCool = true;
        }
    }
    private void Update() 
    {
        //쿨타임
        if (isCool)
        {
            timer -= Time.deltaTime;
            text_cool.text = Mathf.Ceil(timer).ToString();  //올림수로 넣어준다

            if (text_cool.text == "0")
            {
                text_cool.text = "";
                isCool = false;
            }
        }
        if (isCantMove)
        {
            joyStick.SetActive(false);
        }
        else{
            Move();
        }
        //애니메이션이 끝났다면
        if (isAnim && kill_script.kill_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            kill_script.kill_anim.SetActive(false);
            kill_script.Kill();
            isCantMove = false;
            isAnim = false;
        }
    }
    //캐릭터 움직임 관리
    void Move()
    {
        if (settings_script.isJoyStick)
        {
            joyStick.SetActive(true);

        }
        else
        {
            joyStick.SetActive(false);
            //클릭 했는지 판단
            if (Input.GetMouseButton(0))  //누르고 있는 도중에는 무조건 update
            {
#if UNITY_EDITOR
                if(!EventSystem.current.IsPointerOverGameObject())  //현재 UI를 터치하고 있는지
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;  // 화면의 중앙, 나누기2분의1보다 곱셈이 더 빠름
                    //normalized 정규화 시켜준다 방향벡터
                    transform.position += dir * speed * Time.deltaTime;
                    // speed 자연스럽게 time은 기기마다 프레임이 다르기 때문에
                    anim.SetBool("isWalk", true);

                    //왼쪽으로 이동
                    if (dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1,1,1); //좌우반전
                    }
                    //오른쪽으로 이동
                    else    {
                        transform.localScale = new Vector3(1,1,1);
                    }
                }
#else
if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  //현재 UI를 터치하고 있는지  터치는 여러개가 터치가 될수 있기때문에 그중 하나
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;  // 화면의 중앙, 나누기2분의1보다 곱셈이 더 빠름
                    //normalized 정규화 시켜준다 방향벡터
                    transform.position += dir * speed * Time.deltaTime;
                    // speed 자연스럽게 time은 기기마다 프레임이 다르기 때문에
                    anim.SetBool("isWalk", true);

                    //왼쪽으로 이동
                    if (dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1,1,1); //좌우반전
                    }
                    //오른쪽으로 이동
                    else    {
                        transform.localScale = new Vector3(1,1,1);
                    }
                }
#endif
            }

            //클릭하지 않는다면
            else{
                anim.SetBool("isWalk", false);
            }

        }

    }

    //캐릭터 삭제
    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Misson" && isMission)
        {
            coll = other.gameObject;
            btn.interactable = true;
        }
        if (other.tag == "NPC" && !isMission && !isCool)
        {
            coll = other.gameObject;
            btn.interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Misson" && isMission)
        {
            coll = null;
            btn.interactable = false;
        }
        if (other.tag == "NPC" && !isMission)
        {
            coll = null;
            btn.interactable = false;
        }
        
    }
    // Use 버튼 누르면 호출
    public void ClickButton()
    {
        //미션일때
        if (isMission)
        {
            //MissonStart를 호출
            coll.SendMessage("MissonStart");   //편하게 coll안에 missonstart 호출
        }
        //킬퀘스트 일때
        else{
            Kill();
        }
        isCantMove = true;
        btn.interactable = false;
    }
    
    void Kill()
    {
        //죽이는 애니메이션
        kill_script.kill_anim.SetActive(true);
        isAnim = true;
        //죽은 이미지 변경
        coll.SendMessage("Dead");
        //죽은 NPC는 다시 죽일 수 없게
        coll.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void MissonEnd()
    {
        isCantMove = false;
    }
}
