using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestIdleState : CharStateBase
{
    public InputAction moveAction;
    public override void OnEnterState()
    {
        
    }

    public override void BeforeChangeState()
    {
        throw new System.NotImplementedException();
    }
}
