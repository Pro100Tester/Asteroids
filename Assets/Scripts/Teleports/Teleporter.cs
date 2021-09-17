using System.Collections;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour
{
    [SerializeField, Header("Объект к которому нужно телеполртироваться")]
    private GameObject revertSide;

    [SerializeField]
    private bool caresAboutWasFirstContact = true;





    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>()) Destroy(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)

    {
        if (collision.isTrigger)
            return;

        if (caresAboutWasFirstContact)
        {
            //если стоит скрипт на "игнорирование первого телепорта"
            //если скрипта нету или FirstContact==ture телепортирует иначе нет
            if (CheckisAbleToTeleport(collision, 0))
                return;
        }


        float pos;
        pos = transform.worldToLocalMatrix.MultiplyPoint3x4(collision.transform.position).x;
        if (pos < 0) Teleport(collision.transform);

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //ставит у объекта со скриптом что уже был первый контакт
        CheckisAbleToTeleport(collision, 1);
    }
    // Даёт возможноть проигнорировать первое вхождение в телепорт
    bool CheckisAbleToTeleport(Collider2D c2D, byte stage)
    {
        var isTeleportAble = c2D.GetComponent<TeleportAble>();
        // если скрипта нету || если уже был первый контакт
        if (isTeleportAble == null || isTeleportAble.FirstContact == true)
            return false;

        if (stage == 0)// OnTiggerStay
        {
            if (isTeleportAble.FirstContact == false)
                return true;

        }
        else if (stage == 1)// OnTriggerExit
        {
            isTeleportAble.FirstContact = true;
        }
        return false;

    }

    void Teleport(Transform obj)
    {
        // positon of obj
        Vector2 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        localPos = new Vector2(-localPos.x, -localPos.y);
        StartCoroutine(DisableCol(obj));
        obj.position = revertSide.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        // rotation of obj
        /*
        Quaternion q = revertSide.transform.rotation * Quaternion.Inverse(transform.rotation);// * Quaternion.Euler(0, 180, 0);
        obj.rotation = q * obj.rotation;
        */
    }
    IEnumerator DisableCol(Transform obj)
    {

        Collider2D colider = obj.gameObject.GetComponent<Collider2D>();
        colider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        colider.enabled = true;
        yield break;
    }
}



