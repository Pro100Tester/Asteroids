using System.Collections;
using UnityEngine;

public class ChangeColorSprite : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetColor(Color.white, 0f);
    }

    public void ChangeColor(in Color color, in float delayTime) => StartCoroutine(SetColor(color, delayTime));
    private IEnumerator SetColor(Color color, float delayTime)
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(delayTime);
        spriteRenderer.color = Color.white;
        yield break;
    }
}
