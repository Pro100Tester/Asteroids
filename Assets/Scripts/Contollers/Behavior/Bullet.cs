using UnityEngine;
public class Bullet : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.audioManager.PlayOneShot("pew");
    }

    ParticleSystem temp;
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        temp = Instantiate(Loader.loader.WhenHitFX, transform.position, transform.rotation);
        Destroy(temp.gameObject, 2f);
        Destroy(gameObject);


    }



}
