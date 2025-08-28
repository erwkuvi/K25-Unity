using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class EnterMenuUI : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private InputActionAsset _inputAsset;
    
    private InputActionMap _playerInput;
    private InputActionMap _uiInput;
    private InputAction _menuInput;
    

    public string rootMenuName = "MenuPavRoot";
    private VisualElement _rootMenu;

    private bool _isWindowOpen = false;
    private void Awake()
    {
        _rootMenu = _uiDocument.rootVisualElement.Q<VisualElement>(rootMenuName);
        _rootMenu.style.display = DisplayStyle.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _playerInput = _inputAsset.FindActionMap("Player");
        _uiInput = _inputAsset.FindActionMap("UI");
        
        _menuInput = _uiInput.FindAction("Menu");
        _uiInput.Enable();
        if (_menuInput != null)
            _menuInput.performed += OnMenuClose;

    }
    private void OnDisable()
    {
        if (_menuInput != null)
            _menuInput.performed -= OnMenuClose;
        _uiInput.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OpenMenu();
    }

    private void OpenMenu()
    {
        _isWindowOpen = true;
        _rootMenu.style.display = DisplayStyle.Flex;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _playerInput.Disable();
        //_uiInput.Enable();
        
    }

    private void OnMenuClose(InputAction.CallbackContext context)
    {
        CloseMenu();
    }

    public void CloseMenu()
    {
        _isWindowOpen = false;
        _rootMenu.style.display = DisplayStyle.None;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _playerInput.Enable();
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