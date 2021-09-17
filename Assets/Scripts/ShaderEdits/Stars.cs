using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] private Material material;

    [SerializeField]
    private float speed = 0.07f;
    private static T_length tBS, tBP;
    //BrightScale 
    //BrightPower
    List<T_length> ListOfT;
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;

        tBS = new T_length(); tBP = new T_length();
        ListOfT = new List<T_length>() { tBS, tBP };

        foreach (var thing in ListOfT)
        {
            thing.T = Random.Range(0f, 1f);
            thing.ChangeRiseOrReduce();
        }
    }

    void SetValueBS(in float t) => StarsBright.SetBrightScale(material, Mathf.Lerp(StarsBright.minbScale, StarsBright.maxbScale, t));

    void SetValueBP(in float t) => StarsBright.SetBrightPower(material, Mathf.Lerp(StarsBright.minbPower, StarsBright.maxbPower, t));


    private void FixedUpdate()
    {
        foreach (var thing in ListOfT)
        {
            if (thing.Equals(tBS))
            {
                thing.ChangeT(speed * 2f);
            }
            else
            {
                thing.ChangeT(speed);
            }
        }

        SetValueBS(ListOfT[0].T);
        SetValueBP(ListOfT[1].T);
    }

}

public class T_length
{
    private float t;
    private bool riseOrReduce;

    public float T { get { return t; } set { if (value >= -1 && value <= 2) t = value; } }
    public void ChangeT(in float speed)
    {
        if (riseOrReduce)//rise
        {
            T += Time.deltaTime * speed;
        }
        else//Reduce
        {
            T -= Time.deltaTime * speed;
        }

        if (T <= 0)
        {
            riseOrReduce = true;//rise
        }
        else if (T >= 1)
        {
            riseOrReduce = false;//Reduce
        }
    }

    public void ChangeRiseOrReduce() => riseOrReduce = Random.Range(0, 1) == 1 ? true : false; // 50%

}



