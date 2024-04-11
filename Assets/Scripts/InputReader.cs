using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/inputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    

    #region IPlayerActions

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
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
