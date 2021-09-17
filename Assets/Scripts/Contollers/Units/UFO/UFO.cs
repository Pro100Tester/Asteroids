using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(UFOMovement), typeof(UFOAimAndShoot))]

public class UFO : UnitShootable
{
    [SerializeField, Header("Скорость стрельбы")]
    private float shootDelay = 1;

    [SerializeField, Tooltip("Target to aim (auto, finding tag 'player'')"), Header("Целится в объект")]
    private GameObject player;

    [SerializeField, Range(2f, 10f), Tooltip("Расстояния до астероида для того что бы начать манёвр"), Header("Расстояния для манёвра")]
    private float avoidRadius = 5;


    //new gameobjects
    // Создаётся дополнительный GameObject что бы
    // разместить в нём пушку для стрельбы
    private GameObject cannon;

    // Создаётся дополнительный CircleCollider2D что бы
    // сделать коллайдер триггером для укранянеия от астероидов
    private CircleCollider2D CC2D_avoid_asteroid;
    //ref
    private Rigidbody2D myRb2D;


    [SerializeField, Header("Идёт ли стрельба сейчас?")]
    private bool doesShooting;

    //cannon, gameObject.GetComponent<Rigidbody2D>()
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
        CreateCannon();

        myRb2D = gameObject.GetComponent<Rigidbody2D>();
        CreateAvoidCircle();

        SetupGun(cannon, myRb2D);
    }
    protected override void Start()
    {
        base.Start();
    }
    private void CreateCannon()
    {
        cannon = Instantiate(new GameObject(), gameObject.transform);
        cannon.name = "cannon";
    }

    void OnBecameVisible()
    {
        StartCoroutine(ShootToPlayer());
    }
    void OnBecameInvisible()
    {
        doesShooting = false;
        StopCoroutine(ShootToPlayer());
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.layer == 7)//player
        {
            TakeDamage(1);
        }
    }


    #region Shooting part
    IEnumerator ShootToPlayer()
    {
        if (doesShooting) yield break;
        doesShooting = true;
        while (doesShooting)
        {
            if (player == null)// если игрок умер
                yield break;
            AimToPlayer();
            DoShot();

            yield return new WaitForSeconds(shootDelay);
        }
        doesShooting = false;
        yield break;
    }

    void AimToPlayer()
    {
        // игрок мог быть уже убит, поэтому try
        try
        {
            // Оружие будет направлено в сторону игрока
            Vector3 relativePos = player.gameObject.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.back);
            rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            cannon.transform.rotation = rotation;
        }
        catch { }

    }
    #endregion
    #region Movement part
    private void CreateAvoidCircle()
    {
        CC2D_avoid_asteroid = gameObject.AddComponent<CircleCollider2D>();
        CC2D_avoid_asteroid.radius = avoidRadius;
        CC2D_avoid_asteroid.isTrigger = true;
    }

    [SerializeField, Header("Ограницение физической скорости объекта")]
    private float maxSpeed = 15f;
    void FixedUpdate()
    {
        if (myRb2D.velocity.magnitude > maxSpeed)
        {
            myRb2D.velocity = myRb2D.velocity.normalized * maxSpeed;
        }
    }


    [SerializeField, Header("Физическая сила отскока"), Tooltip("Сила отбрасывания текущего объекта от претпядствий")]
    private byte forceAvoid = 5;

    private void OnTriggerStay2D(Collider2D collision)
    {
        // уклоняется от врагов, то есть астероидов
        if (collision.gameObject.layer == 9)//enemy
        {
            Vector3 pos = collision.gameObject.transform.position;
            Vector3 myPos = gameObject.transform.position;

            if (pos.y > myPos.y)
            {

                myRb2D.AddForce(Vector2.down * forceAvoid, ForceMode2D.Impulse);
            }
            if (pos.y > myPos.y)
            {
                myRb2D.AddForce(Vector2.up * forceAvoid, ForceMode2D.Impulse);
            }
            if (pos.x > myPos.x)
            {
                myRb2D.AddForce(Vector2.left * forceAvoid, ForceMode2D.Impulse);
            }
            if (pos.x < myPos.x)
            {
                myRb2D.AddForce(Vector2.right * forceAvoid, ForceMode2D.Impulse);
            }
        }

    }

    #endregion
}
