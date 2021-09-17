using UnityEngine;
using System;

public abstract class UnitBase : MonoBehaviour
{
    [SerializeField, Header("Выбр тип юнита (HP)"), Tooltip("Устанавливает здороъе в зависимоти от типа юнита")]
    private Hp.typeOfUnit ChoseTypeOfUnit;

    [SerializeField, Header("Здоровье объекта"), Tooltip("(auto) HP of gameobject")]
    private byte health;
    protected byte Health { get { return health; } }
    //ref
    private ChangeColorSprite _ChangeColorSprite;

    protected virtual void Awake()
    {
        AddChangeColorSprite();
    }

    private void AddChangeColorSprite()
    {
        if (GetComponent<SpriteRenderer>())
        {
            _ChangeColorSprite = gameObject.AddComponent<ChangeColorSprite>();
        }
        else if (GetComponentInChildren<SpriteRenderer>())
        {
            _ChangeColorSprite = GetComponentInChildren<SpriteRenderer>().gameObject.AddComponent<ChangeColorSprite>();
        }
        else
        {
            throw new System.Exception("SpriteRenderer not found");
        }
    }
    protected virtual void Start()
    {
        SetHealth();

    }
    private void SetHealth()
    {
        health = Hp.Set(ChoseTypeOfUnit);
    }

    protected void TakeDamage(in byte dmg)
    {
        health -= dmg;

        _ChangeColorSprite.ChangeColor(Color.red, .3f);
        if (health <= 0) DoDie();
    }

    private void DoDie()
    {
        ParticleSystem temp = Instantiate(Loader.loader.WhenDestroyFX, transform.position, transform.rotation);
        Destroy(temp.gameObject, 5f);
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 10)//bullet from player or bullet from AI
        {
            TakeDamage(1);
        }

    }

}
