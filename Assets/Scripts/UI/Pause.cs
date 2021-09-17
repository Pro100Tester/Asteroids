using System.Collections;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField, Header("UI в паузе"), Tooltip("(auto finding 'UI ESC menu') UI on pouse")]
    private GameObject ExitUI;

    public static Pause pause { get; private set; }


    void Awake()
    {
        pause = this;

        ExitUI = GameObject.FindGameObjectWithTag("UI").transform.Find("UI ESC menu").gameObject;

        ExitUI.SetActive(false);
    }
    public void PauseGame()
    {

        if (Time.timeScale > 0)
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
            ExitUI.SetActive(true);
        }
        else
        {

            AudioListener.pause = false;
            Time.timeScale = 1;
            ExitUI.SetActive(false);
        }
    }

    public void PlayerDead()
    {
        StartCoroutine(WaitAndPause());
    }
    private IEnumerator WaitAndPause()
    {
        yield return new WaitForSeconds(3f);
        PauseGame();
    }

}


