using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
    public static PauseMenuUI instance;
    private bool menuOn = false;

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

    public bool IsPaused() { return menuOn; }

    public void PauseGame() {
        OpenMenu();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuOn = true;
    }

    public void ResumeGame() {
        CloseMenu();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuOn = false;
    }

    public void OpenMenu() { foreach (Transform child in transform) child.gameObject.SetActive(true); ResetSelectedButton(); }
    public void CloseMenu() { foreach (Transform child in transform) child.gameObject.SetActive(false); EventSystem.current.SetSelectedGameObject(null); }

    public void ResetSelectedButton() { EventSystem.current.SetSelectedGameObject(continueButton.gameObject); }
}