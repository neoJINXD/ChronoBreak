using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField] Slider fovSlider;
    [SerializeField] Slider sensSlider;
    [SerializeField] InputField fovInputField;
    [SerializeField] InputField sensInputField;
    [SerializeField] Toggle hcToggle;

    
    [SerializeField] TMP_Text natureTime;
    [SerializeField] TMP_Text hcNatureTime;
    [SerializeField] TMP_Text city1Time;
    [SerializeField] TMP_Text hcCity1Time;
    [SerializeField] TMP_Text city2Time;
    [SerializeField] TMP_Text hcCity2Time;
    [SerializeField] TMP_Text city3Time;
    [SerializeField] TMP_Text hcCity3Time;


    void Start() 
    {
        fovSlider.value = GameManager.instance.fov;
        sensSlider.value = GameManager.instance.sensitivity;
        UpdateFOVFromSlider();
        UpdateSensFromSlider();
        
    }

    void Update() 
    {
        // print(natureTime.text);
        natureTime.text = ConvertTextToTime(GameManager.instance.scores["Nature1"]);    
        hcNatureTime.text = ConvertTextToTime(GameManager.instance.scores["HCNature1"]);    
        city1Time.text = ConvertTextToTime(GameManager.instance.scores["City1"]);    
        hcCity1Time.text = ConvertTextToTime(GameManager.instance.scores["HCCity1"]);    
        city2Time.text = ConvertTextToTime(GameManager.instance.scores["City2"]);    
        hcCity2Time.text = ConvertTextToTime(GameManager.instance.scores["HCCity2"]);    
        city3Time.text = ConvertTextToTime(GameManager.instance.scores["City3"]);    
        hcCity3Time.text = ConvertTextToTime(GameManager.instance.scores["HCCity3"]);    
    }

    // Button Methods

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
                GameManager.instance.SaveData(new PlayerData(GameManager.instance));
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
            holder.SetActive(true);
        }
        else
        {
            yield return StartCoroutine(FadeCanvas(holder.GetComponent<CanvasGroup>(), 1f, 0f, 0.5f));
            holder.SetActive(false);
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

    // Settings Methods

    public void FOVSlider(float fov)
    {
        GameManager.instance.fov = fov;
        print(fov);
    }
    public void SensSlider(float sensitivity)
    {
        GameManager.instance.sensitivity = sensitivity;
        print(sensitivity);
    }

    public void UpdateFOVFromValue() 
    {
        // fovInputField.text = val;
        // fovSlider.value = float.Parse(val);
        fovSlider.value = float.Parse(fovInputField.text);
    }
    public void UpdateSensFromValue() 
    {
        // sensInputField.text = val;
        // sensSlider.value = float.Parse(val);
        sensSlider.value = float.Parse(sensInputField.text);
    }
    public void UpdateFOVFromSlider() 
    {
        // fovInputField.text = val.ToString();
        // fovSlider.value = val;
        fovInputField.text = fovSlider.value.ToString();
    }
    public void UpdateSensFromSlider() 
    {
        // sensInputField.text = val.ToString();
        // sensSlider.value = val;;
        sensInputField.text = sensSlider.value.ToString();
    }

    public void ToggleHardcore()
    {
        GameManager.instance.hardcoreMode = hcToggle.isOn;
    }

    private string ConvertTextToTime(float time)
    {
        float min = (int) (time / 60);
        float sec = (int) (time - min * 60);
        float ms = (int) ((time - min * 60 - sec) * 100);

        // Formatting
        string minutesText = min < 10 ? minutesText = "0" + min.ToString() : min.ToString();
        string secondsText = sec < 10 ? secondsText = "0" + sec.ToString() : sec.ToString();
        string msText = ms < 10 ? msText = "0" + ms.ToString() : ms.ToString();

        // Update timer text label
        return minutesText + ":" + secondsText + "." + msText;
    }

}
