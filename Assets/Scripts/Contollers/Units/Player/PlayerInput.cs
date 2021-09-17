using Extended;
using System;
using UnityEngine;


public class PlayerInput : MonoBehaviour, IInput
{

    public static PlayerInput playerInput { get; private set; }

    public Action<Vector2> OnMovementInput { get; set; }
    public Action OnShootInput { get; set; }

    // buttons
    public static bool up, left, right, down, isAnyMoveKeysPressing, shoot, esc, r;


    private Vector2 moveVector;
    private Vector2 MoveVector
    {
        get
        {

            if (isAnyMoveKeysPressing)
            {
                if (up)
                {
                    moveVector += Vector2.up;
                }
                if (left)
                {
                    moveVector += Vector2.left;
                }
                if (right)
                {
                    moveVector += Vector2.right;
                }
                if (!left && !right)
                {
                    moveVector.x = 0;
                }
                /*
                if (down)
                {
                    moveVector += Vector2.down;
                }*/
            }
            else
            {
                moveVector = Vector2.zero;
            }

            return moveVector.Clamp(-Vector2.one, Vector2.one);
        }


    }
    private void Awake() => playerInput = this;



    private void KeyPressing()
    {
        up = Input.GetKey(KeyCode.W) ? true : false;
        left = Input.GetKey(KeyCode.A) ? true : false;
        right = Input.GetKey(KeyCode.D) ? true : false;
        r = Input.GetKey(KeyCode.R) ? true : false;
        //down = Input.GetKey(KeyCode.S) ? true : false;

        esc = Input.GetKeyDown(KeyCode.Escape) ? true : false;

        shoot = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) ? true : false;

        //performents
        isAnyMoveKeysPressing = (up || left || right) ? true : false;//|| down
    }
    private void FixedUpdate()
    {
        OnMovementInput?.Invoke(MoveVector);
    }
    private void Update()
    {
        KeyPressing();

        if (r) Menu.menu.Restart();

        if (shoot) OnShootInput?.Invoke();

        if (esc) Pause.pause.PauseGame();
    }




}











