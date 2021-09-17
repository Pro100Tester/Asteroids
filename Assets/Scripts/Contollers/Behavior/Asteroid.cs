using UnityEngine;
[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb2d;
    private Sprite ExtraLife;
    private ScoreBar scoreBar;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        scoreBar = FindObjectOfType<ScoreBar>();


    }

    private void Start()
    {
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = AsteroidLoader.Sprites[Random.Range(0, AsteroidLoader.Sprites.Length)];
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        }
        AsteroidType();

    }

    private void AsteroidType()
    {
        if (spriteRenderer.sprite.name.Contains(AsteroidLoader.big))
        {
            ExtraLife = AsteroidSpriteKeeper.mediumSprites[Random.Range(0, AsteroidSpriteKeeper.mediumSprites.Count)];
            circleCollider.radius = AsteroidLoader.forBig;
            rb2d.mass = 2f;
        }
        else if (spriteRenderer.sprite.name.Contains(AsteroidLoader.medium))
        {
            ExtraLife = AsteroidSpriteKeeper.smallSprites[Random.Range(0, AsteroidSpriteKeeper.mediumSprites.Count)];
            circleCollider.radius = AsteroidLoader.forMedium;
            rb2d.mass = 1f;
            gameObject.transform.localScale = Vector3.one * 2;
        }
        else
        {
            ExtraLife = null;
            circleCollider.radius = AsteroidLoader.forSmall;
            rb2d.mass = .5f;
            gameObject.transform.localScale = Vector3.one * 3;
        }
    }

    private void Split()
    {
        rb2d.AddRelativeForce(transform.right * 250f);
        GameObject temp = Instantiate(gameObject, SpawnCircle.spawnCircle.Parent.transform);
        temp.GetComponent<TeleportAble>().FirstContact = true;
        temp.GetComponent<Rigidbody2D>().velocity = rb2d.velocity;
        temp.GetComponent<Rigidbody2D>().AddRelativeForce(-temp.gameObject.transform.right * 250f);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.layer == 7)//player
        {
            AudioManager.audioManager.Play("asteroidDestroyed");
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 6)//bullet from player
        {
            AudioManager.audioManager.Play("asteroidDestroyed");
            scoreBar.ScoreAction?.Invoke(1);
            if (ExtraLife != null)
            {
                spriteRenderer.sprite = ExtraLife;
                AsteroidType();
                Split();
            }
            else
            {
                Destroy(gameObject);
            }



        }
    }
}

