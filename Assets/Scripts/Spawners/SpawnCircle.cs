using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SpawnCircle : MonoBehaviour
{
    public static SpawnCircle spawnCircle { get; private set; }


    [SerializeField] private CircleCollider2D cc2d;
    [SerializeField, Header("Сила броска астероида")] private float force = 150f;

    private Vector3[] dirs = new Vector3[4] { Vector3.left, Vector3.right, Vector3.up, Vector3.down };
    [SerializeField] private GameObject Saved;

    //public Vector3 Vec, pos;
    //[Range(0f,1f)]
    //public float t;

    private void Awake()
    {
        spawnCircle = this;

        Parent = GameObject.FindGameObjectWithTag("EnemyGameObjectParent");

        cc2d = GetComponent<CircleCollider2D>();
        cc2d.isTrigger = true;
        CreateSpawner();
    }

    private void CreateSpawner()
    {
        Saved = new GameObject();


        Saved.name = "Generated Spawner ";

        //gameObject.transform.position = (mapSides[i].transform.position + Vector3.left * 10f);
        Saved.transform.position = new Vector3(cc2d.bounds.min.x, cc2d.bounds.min.y, 0f);



        Vector2 sd = cc2d.bounds.center;


        Saved.transform.LookAt(sd, Vector3.forward);
        //Saved.AddComponent<Spawner>();
        //gameObject.GetComponent<Spawner>().Spawn(asteroidPrefab, 10, force,true);
    }
    //Возращяет крайний вектор объекта 
    private Vector3 LerpBounds(Vector3 dir, float t)
    {
        Vector3 b1, b2;
        if (dir == Vector3.right)
        {
            b1 = new Vector3(cc2d.bounds.max.x, cc2d.bounds.min.y, 0f);
            b2 = new Vector3(cc2d.bounds.max.x, cc2d.bounds.max.y, 0f);
        }
        else if (dir == Vector3.left)
        {
            b1 = new Vector3(cc2d.bounds.min.x, cc2d.bounds.min.y, 0f);
            b2 = new Vector3(cc2d.bounds.min.x, cc2d.bounds.max.y, 0f);
        }
        else if (dir == Vector3.down)
        {
            b1 = new Vector3(cc2d.bounds.min.x, cc2d.bounds.min.y, 0f);
            b2 = new Vector3(cc2d.bounds.max.x, cc2d.bounds.min.y, 0f);
        }
        else if (dir == Vector3.up)
        {
            b1 = new Vector3(cc2d.bounds.min.x, cc2d.bounds.max.y, 0f);
            b2 = new Vector3(cc2d.bounds.max.x, cc2d.bounds.max.y, 0f);
        }
        else
        {
            throw new System.Exception("Error Vector not found");
        }
        return Vector3.Lerp(b1, b2, t);
    }


    public void SpawnEnemysCircle(GameObject prefab, byte countEnemys)
    {

        Saved.transform.position = LerpBounds(dirs[Random.Range(0, 4)], Random.Range(0f, 1f));
        Saved.transform.LookAt(cc2d.bounds.center, Vector3.forward);

        //Saved.GetComponent<Spawner>().Spawn(prefab, countEnemys, force, false);
        Spawn(prefab, countEnemys, force, false);
    }

    #region Spaawner

    [Tooltip("Объект к которому будут закреплены враги")]
    public GameObject Parent;


    private float distance = 15.0f;
    public void Spawn(GameObject prefab, int spawnCount, float force, bool rightOrForward)
    {
        Vector3 dir;
        if (rightOrForward)
        {
            dir = Saved.transform.right;
        }
        else
        {
            dir = Saved.transform.forward;
        }

        for (int i = 0; i < spawnCount; i++)
        {

            Vector3 direction = Random.insideUnitCircle.normalized * distance;
            Vector3 spawnPoint = Saved.transform.position + direction;

            float r = Random.Range(-distance, distance);
            Quaternion rotation = Quaternion.AngleAxis(r, Vector3.forward);

            GameObject obj = Instantiate(prefab, spawnPoint, rotation);
            obj.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);
            obj.transform.SetParent(Parent.transform);
        }
    }
    #endregion
}