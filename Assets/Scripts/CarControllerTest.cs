using UnityEngine;
using UnityEngine.InputSystem;

public class CarControllerTest : MonoBehaviour
{
    private NewInputs _customInputs;
    
    void Awake()
    {
        _customInputs = new NewInputs();
        _customInputs.Player.Move.performed += MoveCar;
    }

    private void MoveCar(InputAction.CallbackContext obj)
    {
        
    }
}
