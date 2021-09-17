/*
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Olds
{
    public class old_SpawnerFromWalls : MonoBehaviour
    {
        [SerializeField, Header("Стороны карт"), TooltipAttribute("Обязательно должны быть у GameObject именно такие слова up down right left")]
        private List<GameObject> mapSides;

        [SerializeField] private GameObject asteroid;
        [SerializeField] private GameObject alians;
        [SerializeField, Header("Сила броска астероида")] private float force = 150f;

        [SerializeField, Header("Нейминг сторон"), TooltipAttribute("Проверочное имя сторон, которое будет служить проверкой для размещения спавнеров")]
        private string left = "left", right = "right", bottom = "bottom", top = "top";


        private void Awake()
        {
            if (asteroid == null)
                asteroid = Resources.Load("Prefabs/Asteroid") as GameObject;
        }


        // Update is called once per frame
        void Start()
        {
            CreateSpawners();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                DOIT();
        }


        GameObject[] spawners = new GameObject[4];

        private void CreateSpawners()
        {
            for (int i = 0; i < mapSides.Count; i++)
            {

                //Vector3 localDirection = mapSides[i].transform.TransformDirection(Side(mapSides[i].gameObject.name) * 10f);
                //GameObject gameObject = Instantiate(, (mapSides[i].transform.position + localDirection), mapSides[i].transform.rotation);
                GameObject gameObject = new GameObject();
                spawners[i] = gameObject;
                gameObject.name = "Generated Spawner ";

                //gameObject.transform.position = (mapSides[i].transform.position + Vector3.left * 10f);
                gameObject.transform.position = mapSides[i].transform.position;
                gameObject.transform.rotation = Quaternion.Euler(SideLook(mapSides[i].gameObject.name, gameObject));
                //gameObject.AddComponent<Spawner>();
                //gameObject.GetComponent<Spawner>().Spawn(asteroid, UnityEngine.Random.Range(1, 15), force);

                //Destroy(gameObject);
            }
            DOIT();

        }
        //mapSides[i].transform.worldToLocalMatrix.MultiplyPoint3x4(new Vector3(0, UnityEngine.Random.Range(-.5f,.5f),0f))


        Vector3 fG;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawCube(fG, Vector3.one);

        }

        void DOIT()
        {

            foreach (GameObject spawner in spawners)
            {
                Vector3 vector;
                float limit;
                GameObject obj_side;

                if (spawner.name.Contains(left) || spawner.name.Contains(right))
                {
                    if (spawner.name.Contains(left))
                    {
                        obj_side = mapSides.Find(x => x.name.Contains(left));
                    }
                    else
                    {
                        obj_side = mapSides.Find(x => x.name.Contains(right));
                    }

                    limit = obj_side.transform.localScale.y - 5;

                    vector = new Vector3(
                    0f,
                    UnityEngine.Random.Range(-limit, limit),
                    0f);
                }
                else
                {
                    if (spawner.name.Contains(bottom))
                    {
                        obj_side = mapSides.Find(x => x.name.Contains(bottom));
                    }
                    else
                    {
                        obj_side = mapSides.Find(x => x.name.Contains(top));
                    }
                    limit = obj_side.transform.localScale.y - 5;
                    vector = new Vector3(
                    UnityEngine.Random.Range(-limit, limit),
                    0f,
                    0f);
                }

                print(obj_side.GetComponent<BoxCollider2D>().bounds.ClosestPoint(vector));
                fG = (obj_side.GetComponent<BoxCollider2D>().bounds.ClosestPoint(vector));
                //spawner.transform.position = obj_side.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);


                spawner.transform.position = obj_side.GetComponent<BoxCollider2D>().bounds.ClosestPoint(vector);

                //spawner.GetComponent<Spawner>().Spawn(asteroid, 10, force, true);
            }
        }






        Vector3 Side(string s)
        {
            if (s.Contains("left"))
            {
                return Vector3.left;
            }
            else if (s.Contains("right"))
            {
                return Vector3.right;
            }
            else if (s.Contains("bottom"))
            {
                return Vector3.left;
            }
            else if (s.Contains("top"))
            {
                return Vector3.right;
            }
            throw new Exception("Не не правильный нейминг объектов в SpawnController");

        }

        Vector3 SideLook(string s, GameObject obj)
        {
            float shift = -3f;
            if (s.Contains(left))
            {
                obj.name += left;
                obj.transform.localPosition += new Vector3(shift, 0, 0);
                return new Vector3(0, 0, 0);
            }
            else if (s.Contains(right))
            {
                obj.name += right;
                obj.transform.localPosition += new Vector3(shift, 0, 0);
                return new Vector3(0, 0, 180);
            }
            else if (s.Contains(bottom))
            {
                obj.name += bottom;
                obj.transform.localPosition += new Vector3(0, shift, 0);
                return new Vector3(0, 0, 90);
            }
            else if (s.Contains(top))
            {
                obj.name += top;
                obj.transform.localPosition += new Vector3(0, shift, 0);
                return new Vector3(0, 0, -90);
            }
            throw new Exception("Не не правильный нейминг объектов в SpawnController");



        }


    }

}
*/