using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/inputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    public event Action<Vector2> OnMovePlayer;
    public event Action OnActivateDash;
    private PlayerInput playerInput;


    #region IPlayerActions

    private void OnEnable()
    {
        if(playerInput == null)
        {
            playerInput = new PlayerInput();

            playerInput.Player.SetCallbacks(this);
            playerInput.UI.SetCallbacks(this);
        }

        SetGameplay();
    }

    public void SetGameplay()
    {
        playerInput.Player.Enable();
        playerInput.UI.Disable();
    }
    public void SetUI()
    {
        playerInput.Player.Disable();
        playerInput.UI.Enable();
    }

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
            Debug.Log(context.ReadValue<Vector2>());
        if(context.performed)
        {
            OnMovePlayer(context.ReadValue<Vector2>());
        }
    }

    public void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnDragonStateGamepad(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnDragonStateKeyboard(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSkillChange(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnUseSkill(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region IUIActions
    public void OnNav(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    public void OnMenu(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
