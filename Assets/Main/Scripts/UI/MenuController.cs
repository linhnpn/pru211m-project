using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private UIDocument _doc;
    private Button _playButton;
    private Button _settingButton;
    private Button _exitButton;
    private VisualElement _buttonsContainer;
    private VisualElement _exitPanel;
    private Button _confirmYesExit;
    private Button _confirmNoExit;

   [SerializeField]
    VisualTreeAsset _settingsTemplate;
    [SerializeField]
    private Sprite _mutedSprite;
    [SerializeField]
    private Sprite _unmutedSprite;
    [SerializeField]
    bool _mute = false;

    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _buttonsContainer = _doc.rootVisualElement.Q<VisualElement>("ButtonContainer");
        _playButton = _doc.rootVisualElement.Q<Button>("PlayButton");
        _settingButton = _doc.rootVisualElement.Q<Button>("SettingButton");
        _exitButton = _doc.rootVisualElement.Q<Button>("ExitButton");
        _exitPanel = _doc.rootVisualElement.Q<VisualElement>("ExitPanel");
        _confirmYesExit = _doc.rootVisualElement.Q<Button>("ConfirmYesExit");
        _confirmNoExit = _doc.rootVisualElement.Q<Button>("ConfirmNoExit");

        _playButton.clicked += PlayButtonOnClicked;
        _exitButton.clicked += ExitButtonOnClicked;
        _settingButton.clicked += SettingsButtonOnClick;

        _confirmYesExit.clicked += ConfirmYesExitClicked;
        _confirmNoExit.clicked += ConfirmNoExitClicked;
    }

    private void PlayButtonOnClicked()
    {
        SceneManager.LoadScene("Game");
        SceneManager.UnloadSceneAsync("Menu");
    }

    private void ExitButtonOnClicked()
    {
        _exitPanel.style.display = DisplayStyle.Flex;
        _buttonsContainer.style.display = DisplayStyle.None; 
    }

    private void SettingsButtonOnClick()
    {
        Debug.Log("lol");
        SceneManager.LoadScene("Guide");
        SceneManager.UnloadSceneAsync("Menu");
    }

    private void ConfirmYesExitClicked()
    {
        Application.Quit();
    }

    private void ConfirmNoExitClicked()
    {
        _exitPanel.style.display = DisplayStyle.None;
        _buttonsContainer.style.display = DisplayStyle.Flex;

    }
}
