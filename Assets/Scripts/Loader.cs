using UnityEngine;

public class Loader : MonoBehaviour
{
    private GameObject asteroidPrefab, aliensPrefab, bulletPrefab;

    private ParticleSystem whenHitFX, whenDestroyFX;

    public GameObject AsteroidPrefab { get { return asteroidPrefab; } }
    public GameObject AliensPrefab { get { return aliensPrefab; } }
    public GameObject BulletPrefab { get { return bulletPrefab; } }
    public ParticleSystem WhenHitFX { get { return whenHitFX; } }
    public ParticleSystem WhenDestroyFX { get { return whenDestroyFX; } }

    public static Loader loader { get; private set; }

    private void Awake()
    {
        loader = this;

        if (asteroidPrefab == null)
            asteroidPrefab = Resources.Load<GameObject>("Prefabs/Asteroid");

        if (aliensPrefab == null)
            aliensPrefab = Resources.Load<GameObject>("Prefabs/UFO");

        if (bulletPrefab == null)
            bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

        if (whenHitFX == null)
            whenHitFX = Resources.Load<ParticleSystem>("FX/Arcade Spark");

        if (whenDestroyFX == null)
            whenDestroyFX = Resources.Load<ParticleSystem>("FX/CosmicReversal");

    }

}



