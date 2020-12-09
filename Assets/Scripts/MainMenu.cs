﻿using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private enum MenuState{
        MAIN_MENU,
        SETTINGS,
        LEADERBOARDS,
        LEVEL_SELECT
    }
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject leaderboardMenu;
    [SerializeField] GameObject levelMenu;
    [SerializeField]private MenuState currentState;




    // Button Methods
    // public void PlayTestLevel()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }

    public void ToLevelSelect()
    {
        // DisableCurrentScene();
        print("Going to select");
        currentState = MenuState.LEVEL_SELECT;
        StartCoroutine(Fade(mainMenu, false));
        StartCoroutine(Fade(levelMenu, true));
        // levelMenu.SetActive(true);
    }

    public void ToSettings()
    {
        // DisableCurrentScene();
        print("Going to settings");
        currentState = MenuState.SETTINGS;
        StartCoroutine(Fade(mainMenu, false));
        StartCoroutine(Fade(settingsMenu, true));
        // settingsMenu.SetActive(true);
    }

    public void ToLeaderboards()
    {
        // TODO soon tm
        // DisableCurrentScene();
        print("Going to leaderboards");
        currentState = MenuState.LEADERBOARDS;
        // leaderboardMenu.SetActive(true);
    }

    public void ToMenu()
    {
        // DisableCurrentScene();
        print("Going to menu");

        switch(currentState)
        {
            case MenuState.SETTINGS:
                StartCoroutine(Fade(settingsMenu, false));
                break;
            case MenuState.LEADERBOARDS:
                StartCoroutine(Fade(leaderboardMenu, false));
                break;
            case MenuState.LEVEL_SELECT:
                StartCoroutine(Fade(levelMenu, false));
                break;
        }    
        StartCoroutine(Fade(mainMenu, true));
        currentState = MenuState.MAIN_MENU;
        // mainMenu.SetActive(true);
    }

    private void DisableCurrentScene()
    {
        switch(currentState)
        {
            case MenuState.MAIN_MENU:
                mainMenu.SetActive(false);
                break;
            case MenuState.SETTINGS:
                settingsMenu.SetActive(false);
                break;
            case MenuState.LEADERBOARDS:
                leaderboardMenu.SetActive(false);
                break;
            case MenuState.LEVEL_SELECT:
                levelMenu.SetActive(false);
                break;
        }
    }

    public void QuitGame()
    {
        // From https://forum.unity.com/threads/how-to-detect-application-quit-in-editor.344600/
        // If we are running in a standalone build of the game
#if UNITY_STANDALONE
        // Quit the application
        Application.Quit();
#endif
 
        // If we are running in the editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private IEnumerator Fade(GameObject holder, bool fadingIn)
    {
        if (fadingIn)
        {
            yield return StartCoroutine(FadeCanvas(holder.GetComponent<CanvasGroup>(), 0f, 1f, 0.5f));
        }
        else
        {
            yield return StartCoroutine(FadeCanvas(holder.GetComponent<CanvasGroup>(), 1f, 0f, 0.5f));
        }
        print("Done");
    }

    private IEnumerator FadeCanvas(CanvasGroup canvas, float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;
        float endTime = Time.time + duration;
        
        canvas.alpha = startAlpha;
        while(Time.time <= endTime)
        {
            float dt = Time.time - startTime;
            float percentage = 1/(duration/dt);
            if (startAlpha > endAlpha)
            {
                // fading out
                canvas.alpha = startAlpha - percentage;
                canvas.interactable = false;
            }
            else
            {
                // fading in
                canvas.alpha = startAlpha + percentage;
                canvas.interactable = true;
            }
            yield return new WaitForEndOfFrame();
        }
        canvas.alpha = endAlpha;
    }


}
