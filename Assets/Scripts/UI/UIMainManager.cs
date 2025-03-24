using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIMainManager : MonoBehaviour
{
    private IMenu[] m_menuList;

    private GameManagerUI m_gameManager;

    private void Awake()
    {
        m_menuList = GetComponentsInChildren<IMenu>(true);
    }

    void Start()
    {
        for (int i = 0; i < m_menuList.Length; i++)
        {
            m_menuList[i].Setup(this);
        }
    }

    internal void ShowMainMenu()
    {
        m_gameManager.ClearLevel();
        m_gameManager.SetState(GameManagerUI.eStateGame.MAIN_MENU);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_gameManager.State == GameManagerUI.eStateGame.GAME_STARTED)
            {
                m_gameManager.SetState(GameManagerUI.eStateGame.PAUSE);
            }
            else if (m_gameManager.State == GameManagerUI.eStateGame.PAUSE)
            {
                m_gameManager.SetState(GameManagerUI.eStateGame.GAME_STARTED);
            }
        }
    }

    internal void Setup(GameManagerUI gameManager)
    {
        m_gameManager = gameManager;
        m_gameManager.StateChangedAction += OnGameStateChange;
    }

    private void OnGameStateChange(GameManagerUI.eStateGame state)
    {
        switch (state)
        {
            case GameManagerUI.eStateGame.SETUP:
                break;
            case GameManagerUI.eStateGame.MAIN_MENU:
                ShowMenu<UIPanelMain>();
                break;
            case GameManagerUI.eStateGame.GAME_STARTED:
                ShowMenu<UIPanelGame>();
                break;
            case GameManagerUI.eStateGame.PAUSE:
                ShowMenu<UIPanelPause>();
                break;
            case GameManagerUI.eStateGame.GAME_OVER:
                ShowMenu<UIPanelGameOver>();
                break;
        }
    }

    private void ShowMenu<T>() where T : IMenu
    {
        for (int i = 0; i < m_menuList.Length; i++)
        {
            IMenu menu = m_menuList[i];
            if(menu is T)
            {
                menu.Show();
            }
            else
            {
                menu.Hide();
            }            
        }
    }

    internal Text GetLevelConditionView()
    {
        UIPanelGame game = m_menuList.Where(x => x is UIPanelGame).Cast<UIPanelGame>().FirstOrDefault();
        if (game)
        {
            return game.LevelConditionView;
        }

        return null;
    }

    internal void ShowPauseMenu()
    {
        m_gameManager.SetState(GameManagerUI.eStateGame.PAUSE);
    }

    internal void LoadLevelMoves()
    {
        m_gameManager.LoadLevel(GameManagerUI.eLevelMode.MOVES);
    }

    internal void LoadLevelTimer()
    {
        m_gameManager.LoadLevel(GameManagerUI.eLevelMode.TIMER);
    }

    internal void ShowGameMenu()
    {
        m_gameManager.SetState(GameManagerUI.eStateGame.GAME_STARTED);
    }
}
