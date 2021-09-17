using UnityEngine;


public abstract class UnitShootable : UnitBase
{
    protected void SetupGun(GameObject aGun, Rigidbody2D anUnit)
    {
        _gun = aGun; _R2D = anUnit;
    }

    [SerializeField, Header("Стреляет игрок?"), Tooltip("Если true то пуля устанавливается в 6 слой (bullet from player), такая пуля не может нанести урон слою 7 (player)")]
    private bool isPlayerLayer = false;

    protected bool IsPlayer { private get { return isPlayerLayer; } set { isPlayerLayer = value; } }

    [SerializeField, Range(1f, 100f), Header("Скорость снаряда"), Tooltip("Speed of bullet")]
    private float speedOfBullet = 6f;

    protected GameObject _gun; protected Rigidbody2D _R2D;



    public void DoShot()
    {
        GameObject temp = Instantiate(Loader.loader.BulletPrefab, _gun.transform.position + _gun.transform.up * 4, _gun.transform.rotation);

        if (IsPlayer) temp.gameObject.layer = 6;// from player, but by default it's from AI        

        temp.GetComponent<Rigidbody2D>().AddForce(_gun.transform.up * speedOfBullet * Mathf.Clamp(_R2D.velocity.magnitude, 4f, 6f));
        Destroy(temp, 10f);
    }
    protected override void Start()
    {
        base.Start();
        if (gameObject.layer == 7) isPlayerLayer = true;
    }

    protected override void Awake()
    {
        base.Awake();
    }

}