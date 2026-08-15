using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
    [SerializeField] private UIDocument mainMenuDocument;

    private Button playButton;
    private Button settingButton;

    private void Awake() {
        VisualElement root = mainMenuDocument.rootVisualElement;

        playButton = root.Q<Button>("PlayButton");
        settingButton = root.Q<Button>("SettingButton");

        playButton.clickable.clicked += PlayGame;
        settingButton.clickable.clicked += ShowSettingsMenu;
    }

    private void PlayGame() {
        SceneManager.LoadScene("GameScene");
    }
    
    private void ShowSettingsMenu() {
        print("Showing settings menu");
    }
}
