using System;
using UnityEngine;

public interface IInput
{
    Action<Vector2> OnMovementInput { get; set; }
    Action OnShootInput { get; set; }

}


