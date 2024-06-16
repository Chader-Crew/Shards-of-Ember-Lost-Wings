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
    public event Action OnItemInteractEvent;
    public event Action<int> OnInventoryInteractEvent;


    //inicializacao
    public void Initialize()
    {
        ClearEvents();
        if(playerInput == null)
        {
            playerInput = new PlayerInput();

            playerInput.Player.SetCallbacks(this);
            playerInput.UI.SetCallbacks(this);
        }

        SetGameplay();
    }

    private void ClearEvents()
    {
        OnMoveEvent = (v)=>{};
        OnDashEvent = ()=>{};
        OnAttackEvent = ()=>{};
        OnDragonStateEvent = (x)=>{};
        OnSkillChangeEvent = (v)=>{};
        OnSkillUseEvent = ()=>{};
        OnPauseEvent = ()=>{};
        //OnItemInteractEvent = ()=>{};
        OnInventoryInteractEvent = (x)=>{};
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
        OnMoveEvent(context.ReadValue<Vector2>());
    }

    public void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {   
        if(context.performed){
            OnAttackEvent?.Invoke();
        }
    }

    public void OnInventoryInteract(UnityEngine.InputSystem.InputAction.CallbackContext context){
        if(context.performed){
            switch(context.ReadValue<Vector2>()){
                case Vector2 v when v.Equals(Vector3.up):
                    OnInventoryInteractEvent(1);
                    break;
                case Vector2 v when v.Equals(Vector3.right):
                    OnInventoryInteractEvent(3);
                    break;
                case Vector2 v when v.Equals(Vector3.down):
                    OnInventoryInteractEvent(2);
                    break;
                case Vector2 v when v.Equals(Vector3.left):
                    OnInventoryInteractEvent(0);
                    break;
            }
        }
    }

    public void OnItemInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.performed){
            OnItemInteractEvent?.Invoke();
        }
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
        if(context.performed)
        {
            OnSkillUseEvent();
        }
    }

    public void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.performed){
            OnPauseEvent();
        }
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
