using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    //ref
    [SerializeField]
    private UnityEngine.UI.Text textScore;

    //values
    [SerializeField] private int score;
    public System.Action<int> ScoreAction;

    private void Awake()
    {
        ScoreAction += ScoreEdit;
        textScore = GameObject.FindGameObjectWithTag("UI").transform.Find("Text_Score").GetComponent<UnityEngine.UI.Text>();
    }
    void ScoreEdit(int value)
    {
        textScore.text = "SCORE: " + (score += value);
    }
}
