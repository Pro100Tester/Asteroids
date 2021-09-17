using UnityEngine;


public class HealthViwer : MonoBehaviour
{
    public static HealthViwer healthViwer { get; private set; }
    private void Awake() => healthViwer = this;
    public void HideOneHP(in byte hp) => gameObject.transform.GetChild(hp).gameObject.SetActive(false);


}
