using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.

public class Menu : MonoBehaviour
{
#pragma warning disable IDE0044 
    [SerializeField] private GameObject Ship;
#pragma warning restore IDE0044 

    public static Menu menu { get; private set; }
    private void Awake() => menu = this;

    public void HitPlay()
    {

        StopAllCoroutines();
        StartCoroutine(Animate());
    }
    public void Restart()
    {
        if (Time.timeScale == 0)// isPaused?
        {

            AudioListener.pause = false;
            menu.HitPlay();
        }

    }

    public void ExitFormGame()
    {
        Application.Quit();
    }
    IEnumerator Animate()
    {
        if (Ship != null)
        {
            Ship.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(1);
        }

        if (SceneManager.GetSceneAt(0).isLoaded)
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }

        SceneManager.LoadScene(1);
        yield break;
    }
}

