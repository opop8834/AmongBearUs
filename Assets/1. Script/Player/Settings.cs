using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool isJoyStick;
    public Image touchBtn, JoyStickBtn;
    public Color blue;

    GameObject mainView, playView;

    public PlayerController player_script;

    private void Start() {
        mainView = player_script.mainView;
        playView = player_script.playView;
    }

    // 설정 버튼을 누르면 호출
    public void ClickSetting()
    {
        gameObject.SetActive(true);
        player_script.isCantMove = true;
    }

    // 게임으로 돌아가기 버튼을 누르면 호출
    public void ClickBack()
    {
        gameObject.SetActive(false);
        player_script.isCantMove = false;
    }

    //터치 이동을 누르면 호출
    public void ClickTouch()
    {
        isJoyStick = false;
        touchBtn.color = blue;
        JoyStickBtn.color = Color.white;
    }

    public void ClickJoyStick()
    {
        isJoyStick = true;
        touchBtn.color = Color.white;
        JoyStickBtn.color = blue;
    }

    //게임 나가기 버튼을 누르면 호출
    public void ClickQuit()
    {
        mainView.SetActive(true);
        playView.SetActive(false);

        //캐릭터 삭제
        player_script.DestroyPlayer();

    }
}
