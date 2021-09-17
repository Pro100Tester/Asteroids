using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    //ref local
    [SerializeField, Tooltip("������ ��������")] private SpawnCircle SC;

    //ref
    [SerializeField, Tooltip("������ � ������� ��������� ������ ������")] private GameObject parent;
    [SerializeField, Tooltip("������ UI ������ ��� ����������� ��������� ����")] private Text textWaves;
    private ScoreBar scoreBar;
    int countWaves;

    private void Awake()
    {

        SC = GetComponentInChildren<SpawnCircle>();
        textWaves = GameObject.FindGameObjectWithTag("UI").transform.Find("Text_Waves").GetComponent<Text>();
        parent = GameObject.FindGameObjectWithTag("EnemyGameObjectParent");
        scoreBar = FindObjectOfType<ScoreBar>();
    }

    private void Start()
    {
        countWaves = 1;
        SpawnWave();
        ChangeTextWaves();

        StartCoroutine(ChangeToSpaawnUFO());
    }
    [SerializeField, Header("���� ������ ���")]
    private int chance = 10;
    IEnumerator ChangeToSpaawnUFO()
    {
        while (true)
        {

            yield return new WaitForSeconds(5f);
            if (Random.Range(0, 100) < chance) SC.SpawnEnemysCircle(Loader.loader.AliensPrefab, 1);
        }

#pragma warning disable CS0162 // ��������� ������������ ���
        yield break; // for stop
#pragma warning restore CS0162 // ��������� ������������ ���
    }
    private void FixedUpdate()
    {
        if (CheckEnemysCount() <= 1)
        {
            SpawnWave();
            countWaves += 1;
            scoreBar.ScoreAction.Invoke(10);
            ChangeTextWaves();
        }

    }
    void SpawnWave()
    {
        for (byte i = 0; i < 5; i++)
            SC.SpawnEnemysCircle(Loader.loader.AsteroidPrefab, (byte)Random.Range(1, 3));
    }
    int CheckEnemysCount()
    {
        return parent.transform.hierarchyCount;
    }

    private static string lable = "LEVEL ";
    void ChangeTextWaves()
    {
        textWaves.text = lable + countWaves;
    }
}
