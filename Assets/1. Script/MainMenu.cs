using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject missonView, killView;
    // 게임 종료버튼을 누르면 호출
    public void ClickQuit()
    {
        //print("게임 종료 버튼 누름");
        
        //유니티 에디터에서의 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        //안드로이드
#else
Application.Quit();
#endif
    }
    // 미션 버튼 누르면 호출
    public void ClickMisson()
    {
        gameObject.SetActive(false);
        missonView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character"),new Vector3(0, -2, 0), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerController>().mainView = gameObject;
        player.GetComponent<PlayerController>().playView = missonView;
        player.GetComponent<PlayerController>().isMission = true;

        missonView.SendMessage("MissionReset");
    }
    // 킬 버튼 누르면 호출
    public void ClickKill()
    {
        gameObject.SetActive(false);
        killView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character"),new Vector3(0, -2, 0), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerController>().mainView = gameObject;
        player.GetComponent<PlayerController>().playView = killView;
        player.GetComponent<PlayerController>().isMission = false;

        killView.SendMessage("KillReset");
    }
}
