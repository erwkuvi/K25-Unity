using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class EntryMenuEvents : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button _closeButton;
    private Button _noButton;
    private Button _yesButton;

            
    private EnterMenuUI _menuManager;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _closeButton = _uiDocument.rootVisualElement.Q("CloseButton") as Button;
        _noButton = _uiDocument.rootVisualElement.Q("NoButton") as Button;
        _yesButton = _uiDocument.rootVisualElement.Q("YesButton") as Button;
        
        //_menuManager = FindObjectOfType<EnterMenuUI>();
        _menuManager = Object.FindObjectOfType<EnterMenuUI>();
        
        if (_yesButton != null)
            _yesButton.RegisterCallback<ClickEvent>(OnYesClick);
        if (_noButton != null)
            _noButton.RegisterCallback<ClickEvent>(OnCloseClick);
        if (_closeButton != null)
            _closeButton.RegisterCallback<ClickEvent>(OnCloseClick);
    }

    private void OnDisable()
    {
        _yesButton.UnregisterCallback<ClickEvent>(OnYesClick);
        _noButton.UnregisterCallback<ClickEvent>(OnCloseClick);
        _closeButton.UnregisterCallback<ClickEvent>(OnCloseClick);
        
    }
    private void OnYesClick(ClickEvent e)
    {
        if (_menuManager != null)
            _menuManager.CloseMenu();
        //Go to scene scene "indoor"
        SceneManager.LoadScene("indoor");

    }
    
    private void OnCloseClick(ClickEvent e)
    {
        if (_menuManager != null)
            _menuManager.CloseMenu();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
