using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : TopDownController
{
    public void OnMove(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        CallMoveEvent(dir);
    }
}
