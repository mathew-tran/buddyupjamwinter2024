using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour {
    public static OptionsMenuUI instance;
    public enum Difficulty { Easy, Normal, Hard }
    private bool menuOn = false;

    [Header("Audio Settings")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Controls Settings")]
    [SerializeField] private GameObject keyboardRebinds;
    [SerializeField] private GameObject gamepadRebinds;
    [SerializeField] private GameObject defaultSelectButtonForKeyboards;
    [SerializeField] private GameObject defaultSelectButtonForGamepads;
    public GameObject waitingForInputScreen;

    private void Awake() { instance = this; }
    private void Start() {
        CloseMenu();
        PauseMenuUI.instance.CloseMenu();

        musicVolumeSlider.value = musicVolumeSlider.maxValue;
        sfxVolumeSlider.value = sfxVolumeSlider.maxValue;

        if (Input.GetJoystickNames() != null) { keyboardRebinds.SetActive(true); gamepadRebinds.SetActive(false); }
        else { keyboardRebinds.SetActive(false); gamepadRebinds.SetActive(true); }
    }
    private void Update() {


        if (Input.GetJoystickNames() != null && gamepadRebinds.activeSelf) {
            keyboardRebinds.SetActive(true); gamepadRebinds.SetActive(false);
            if (menuOn) ResetSelectedButton();
        } else if (Input.GetJoystickNames() == null && keyboardRebinds.activeSelf) {
            keyboardRebinds.SetActive(false); gamepadRebinds.SetActive(true);
            if (menuOn) ResetSelectedButton();
        }
    }

    public void OpenMenu() {
        menuOn = true;
        PauseMenuUI.instance.CloseMenu();
        ResetSelectedButton();
        foreach (Transform child in transform) child.gameObject.SetActive(true);
    }
    public void CloseMenu() {
        menuOn = false;
        PauseMenuUI.instance.OpenMenu();
        foreach (Transform child in transform) child.gameObject.SetActive(false);
    }
    public bool IsMenuOn() { return menuOn; }

    public void ResetSelectedButton() {
        if (Input.GetJoystickNames() != null) EventSystem.current.SetSelectedGameObject(defaultSelectButtonForKeyboards);
        else EventSystem.current.SetSelectedGameObject(defaultSelectButtonForGamepads);
    }
}