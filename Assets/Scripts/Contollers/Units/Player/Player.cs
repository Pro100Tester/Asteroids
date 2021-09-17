using UnityEngine;


class Player : UnitShootable
{
    [Header("Свойства каробля")]
    [Space(2f)]

    [SerializeField, Range(1f, 10f), Header("Скорость")]
    private float speedOfShip;


    [SerializeField, Range(1f, 10f), Header("Разворот")]
    private float rotatinoOfShip;

    //ref
    private Rigidbody2D rb2d;
    private IInput input;


    protected override void Awake()
    {
        base.Awake();

        input = FindObjectOfType<PlayerInput>().GetComponent<IInput>();
        rb2d = GetComponent<Rigidbody2D>();

        SetupGun(gameObject, rb2d);
    }
    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        input.OnMovementInput += ForceMovements;

        input.OnShootInput += DoShot;
    }

    private void OnDisable()
    {
        input.OnMovementInput -= ForceMovements;

        input.OnShootInput -= DoShot;
    }

    private void ForceMovements(Vector2 input)
    {
        if (input.y == Vector2.up.y)
            rb2d.AddForce(rb2d.gameObject.transform.up * speedOfShip);

        if (input.x == Vector2.left.x)
        {
            //rb2d.AddTorque(1f * rotatinoOfShip);
            gameObject.transform.Rotate(0, 0, rotatinoOfShip);
        }
        else if (input.x == Vector2.right.x)
        {
            gameObject.transform.Rotate(0, 0, -rotatinoOfShip);
            //rb2d.AddTorque(-1f * rotatinoOfShip);
        }
    }
    private void OnDestroy()
    {
        try
        {
            Pause.pause.PlayerDead();
        }
        catch { }

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.layer == 9 )//enemy
        {
            TakeDamage(1);
        }
            HealthViwer.healthViwer.HideOneHP(Health);
    }

}
