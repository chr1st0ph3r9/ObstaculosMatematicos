using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName ="nuevo inputReader", menuName ="MainInputReaderOS", order =0)]
public class InputReaderSO : ScriptableObject, InputSystem_Actions.IPlayerActions
{

    public InputSystem_Actions inputActions;



    public delegate void inputAxisAction(Vector2 movimiento);

    public event inputAxisAction eventoMoverHorizontal;
    public event inputAxisAction eventoMirar;

    public delegate void inputTipoAction();





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






    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

 

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }


}
