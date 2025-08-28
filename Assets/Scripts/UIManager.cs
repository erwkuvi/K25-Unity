using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Cursor = UnityEngine.Cursor;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    public string menuRootName = "MenuRoot";

    private VisualElement _menuRoot;
    private bool isMenuOpen = false; 
    
    
    [SerializeField] InputActionAsset _inputActionAsset;
    private InputActionMap _userInput;
    private InputActionMap _playerInput;
    private InputAction _menuInput;
    
    //[SerializeField] private InputActionMap _inputActionMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        
        _menuRoot = _uiDocument.rootVisualElement.Q<VisualElement>(menuRootName);

        _menuRoot.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        _userInput = _inputActionAsset.FindActionMap("UI");
        _userInput.Enable();
        _playerInput = _inputActionAsset.FindActionMap("Player");

        _menuInput = _userInput.FindAction("Menu");
        if (_menuInput != null)
            _menuInput.performed += OnMenuToggle;
        // Subscribe to input actions
    }

    private void OnDisable()
    {
        if (_menuInput != null)
            _menuInput.performed -= OnMenuToggle;
        _userInput.Disable();
    }

    private void OnMenuToggle(InputAction.CallbackContext context)
    {
        ToggleMenu();
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        
        _menuRoot.style.display = isMenuOpen ? DisplayStyle.Flex : DisplayStyle.None;
        
        Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuOpen;

        if (isMenuOpen)
        {
            _playerInput.Disable();
        }
        else
        {
            _playerInput.Enable();
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
