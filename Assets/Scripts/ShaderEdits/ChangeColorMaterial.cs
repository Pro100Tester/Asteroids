using System.Collections;
using UnityEngine;

public class ChangeColorMaterial : MonoBehaviour
{
    private Material material;
    private const string nameOfParamMaterial = "_Color";


    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponentInChildren<SpriteRenderer>().material;
        SetColor(Color.white, 0f);

    }
    public void ChaneColor(in Color color, in float delayTime) => StartCoroutine(SetColor(color, delayTime));
    private IEnumerator SetColor(Color color, float delayTime)
    {
        material.SetColor(nameOfParamMaterial, color);
        yield return new WaitForSeconds(delayTime);
        material.SetColor(nameOfParamMaterial, Color.white);
        yield break;
    }
}
