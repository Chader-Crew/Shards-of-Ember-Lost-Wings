using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe "parser" das informacoes do input system. Tem eventos customizados que apenas passam as partes importantes do contexto.
[CreateAssetMenu(menuName = "ScriptableObjects/inputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    private PlayerInput playerInput;

    //eventos
    public event Action<Vector2> OnMoveEvent;
    public event Action OnDashEvent;
    public event Action OnAttackEvent;
    public event Action<int> OnDragonStateEvent;    //0,1,2 sao cada um dos estados
    public event Action<Vector2> OnSkillChangeEvent;
    public event Action OnSkillUseEvent;
    public event Action OnPauseEvent;


    //inicializacao
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

    //troca de action map
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

    //interfaces (funcoes callback)
    #region IPlayerActions
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
        Debug.Log(context.ReadValue<Vector2>());
        OnMoveEvent(context.ReadValue<Vector2>());
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
