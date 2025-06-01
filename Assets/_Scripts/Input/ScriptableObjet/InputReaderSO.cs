using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName ="nuevo inputReader", menuName ="MainInputReaderOS", order =0)]
public class InputReaderSO : ScriptableObject, InputSystem_Actions.IPlayerActions
{

    public InputSystem_Actions inputActions;



    public delegate void inputAxisAction(Vector2 movimiento);

    public event inputAxisAction eventoMoverHorizontal;
    public event inputAxisAction eventoMirar;

    public delegate void inputTipoBoton();

    public event inputTipoBoton eventoSaltar;
    public event inputTipoBoton eventoCorrer;





    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Enable();

        inputActions.Player.AddCallbacks(this);

    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }





    public void OnMove(InputAction.CallbackContext context)
    {
        eventoMoverHorizontal?.Invoke(context.ReadValue<Vector2>());
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        eventoMirar?.Invoke(context.ReadValue<Vector2>());
    }



    public void OnJump(InputAction.CallbackContext context)
    {
        eventoSaltar?.Invoke();
    }


    public void OnSprint(InputAction.CallbackContext context)
    {
        eventoCorrer?.Invoke();
    }


}
