using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerUi : MonoBehaviour
{
    public GameObject playGameObj, StartGame,GameOver, GameWin;
    public GameObject autoWin, autoLost;
    private static GameManagerUi instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        
    }
    private void Start()
    {
        StartGame.SetActive(true);
        GameWin.SetActive(false);
        GameOver.SetActive(false);
        autoLost.SetActive(false);
        autoWin.SetActive(false);

    }
    public void Playgame()
    {
        playGameObj.SetActive(true);
        StartGame.SetActive(false);
        GameWin.SetActive(false);
        GameOver.SetActive(false);
    }
    
    public void replay()
    {
        SceneManager.LoadScene(1);
    }
    public void home()
    {
        SceneManager.LoadScene(1);
    }
    public void gameOver()
    {
     StartGame.SetActive(false);
        GameWin.SetActive(false);
        GameOver.SetActive(true);
    }

    public void AutoWin()
    {
        autoWin.SetActive(true);
        autoLost.SetActive(false);

    }
    public void AutoLost()
    {
        autoWin.SetActive(false);
        autoLost.SetActive(true);
    }

}
