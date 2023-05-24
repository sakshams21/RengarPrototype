using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClick : MonoBehaviour
{
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private Camera MainCamera;
    private static NewInputs _inputs;
    public GameObject ArrowPrefab;

    private Outline _tempHolder;
    private void Awake() => _inputs = new NewInputs();

    private void Start()
    {
        _tempHolder = null;
        _inputs.Player.RightClickMove.performed += Clicked;
        _inputs.Player.RightClickMove.Enable();
    }

    private void Update()
    {
        if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            if (hit.transform.TryGetComponent(out Outline tempOutline))
            {
                _tempHolder = tempOutline;
                _tempHolder.enabled = true;
            }
            else if(_tempHolder!=null)
            {
                _tempHolder.enabled = false;
                _tempHolder = null;
            }
        }
    }

    private void Clicked(InputAction.CallbackContext obj)
    {
        if (!Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            return;
        ArrowPrefab.SetActive(false);
        ArrowPrefab.SetActive(true);
        ArrowPrefab.transform.position = hit.point;
        PlayerMovement.AssignDestination(hit.point);
    }
}
