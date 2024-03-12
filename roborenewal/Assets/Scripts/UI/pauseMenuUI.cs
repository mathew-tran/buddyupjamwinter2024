using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
    public static PauseMenuUI instance;
    private bool paused = false;

    [Header("Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        instance = this;
        Time.timeScale = 1;
        continueButton.onClick.AddListener(() => { ResumeGame(); });
        optionsButton.onClick.AddListener(() => { OptionsMenuUI.instance.OpenMenu(); });
        quitButton.onClick.AddListener(() => { });
    }

    public bool IsPaused() { return paused; }

    public void PauseGame() {
        OpenMenu();
        Time.timeScale = 0;
        paused = true;
    }

    public void ResumeGame() {
        CloseMenu();
        Time.timeScale = 1;
        paused = false;
    }

    public void OpenMenu() { foreach (Transform child in transform) child.gameObject.SetActive(true); ResetSelectedButton(); }
    public void CloseMenu() { foreach (Transform child in transform) child.gameObject.SetActive(false); EventSystem.current.SetSelectedGameObject(null); }

    public void ResetSelectedButton() { EventSystem.current.SetSelectedGameObject(continueButton.gameObject); }
}