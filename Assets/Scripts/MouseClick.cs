using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClick : MonoBehaviour
{
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private Camera MainCamera;
    private static NewInputs _inputs;
    public GameObject ArrowPrefab;

    private void Awake() => _inputs = new NewInputs();

    private void Start()
    {
        _inputs.Player.RightClickMove.performed += Clicked;
        _inputs.Player.RightClickMove.Enable();
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
