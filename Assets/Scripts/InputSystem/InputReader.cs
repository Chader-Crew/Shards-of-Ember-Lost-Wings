using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Classe "parser" das informacoes do input system. Tem eventos customizados que apenas passam as partes importantes do contexto.
[CreateAssetMenu(menuName = "ScriptableObjects/inputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    private PlayerInput playerInput;


    //eventos
    public static event Action<Vector2> OnMoveEvent;
    public static event Action OnDashEvent;
    public static event Action OnAttackEvent;
    public static event Action<int> OnDragonStateEvent;    //0,1,2 sao cada um dos estados
    public static event Action<int> OnSkillSelectEvent;
    public static event Action OnConfirmSkillEvent;
    public static event Action OnSkillUseEvent;
    public static event Action OnPauseEvent;
    public static event Action OnCheatEvent;
    public static event Action OnItemInteractEvent;
    public static event Action<int> OnInventoryInteractEvent;


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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        SetGameplay();
    }

    private void ClearEvents()
    {
        OnMoveEvent = (v)=>{};
        OnDashEvent = ()=>{};
        OnAttackEvent = ()=>{};
        OnDragonStateEvent = (x)=>{};
        OnSkillSelectEvent = (v)=>{};
        OnConfirmSkillEvent = ()=>{};
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
         if(context.started)
        {
            OnDashEvent();
        }
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
                    OnInventoryInteractEvent(0);
                    break;
                case Vector2 v when v.Equals(Vector3.right):
                    OnInventoryInteractEvent(1);
                    break;
                case Vector2 v when v.Equals(Vector3.down):
                    OnInventoryInteractEvent(2);
                    break;
                case Vector2 v when v.Equals(Vector3.left):
                    OnInventoryInteractEvent(3);
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
        if (context.performed)
        {
            OnDragonStateEvent((int)context.ReadValue<float>());
        }
    }

    public void OnDragonStateKeyboard(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnDragonStateEvent(1);
        }else if(context.canceled)
        {
            OnDragonStateEvent(-1);
        }
    }
    public void OnSkillChange(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.performed)
        {        
            switch(MathF.Ceiling((Vector2.SignedAngle(Vector2.up, context.ReadValue<Vector2>())+ 180)/90 +.5f))
            {
                case 1:
                case 5:
                    OnSkillSelectEvent(3);
                    Debug.Log("Skill Change Call (4)");
                    break;
                case 2:
                    OnSkillSelectEvent(2);
                    Debug.Log("Skill Change Call (3)");
                    break;
                case 3:
                    OnSkillSelectEvent(0);
                    Debug.Log("Skill Change Call (1)");
                    break;
                case 4:
                    OnSkillSelectEvent(1);
                    Debug.Log("Skill Change Call (2)");
                    break;

                default:
                    break;
            }
        }
        if(context.canceled)
        {
            OnConfirmSkillEvent();
        }
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
        if(context.performed)
        {
            SkillTreeUI.Instance.ToggleUI(); //a gente precisa de um gerente de UI lmao
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
    public void OnCheat(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.performed){
            PlayerController.Instance.GainShards(100);
        }
    }
    #endregion
}
