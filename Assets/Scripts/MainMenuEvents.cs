using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _uiDocument;
    
    private UIManager _uiManager;

    private Button _closeButton;
    private Button _resumeButton;
    private Button _quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        
        _closeButton = _uiDocument.rootVisualElement.Q("CloseButton") as Button;
        _resumeButton = _uiDocument.rootVisualElement.Q("ResumeButton") as Button;
        _quitButton = _uiDocument.rootVisualElement.Q("QuitButton") as Button;
        
        _uiManager = Object.FindObjectOfType<UIManager>();

        if (_closeButton != null)
            _closeButton.RegisterCallback<ClickEvent>(onCloseClick);
        if (_resumeButton != null)
            _resumeButton.RegisterCallback<ClickEvent>(onCloseClick);
        if (_quitButton != null)
            _quitButton.RegisterCallback<ClickEvent>(onQuitClick);
    }

    private void onCloseClick(ClickEvent evt)
    {
        _uiManager.ToggleMenu();
    }

    private void onQuitClick(ClickEvent evt)
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
                Application.Quit();
    #endif
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}