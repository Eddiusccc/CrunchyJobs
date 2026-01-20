using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;
    public int objectivesCompleted = 0;
    public int jobPointsTotal = 0;
    public int jobPointsCurrent = 0;
    public int level = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetObjective();
    }

    void SetObjective()
    {
        float modif = 1 + (0.1f * (level - 1));
        jobPointsTotal = Mathf.RoundToInt(100 * level * modif);
    }

    public void AddJobPoints(int points)
    {
        jobPointsCurrent += points;
        if (jobPointsCurrent >= jobPointsTotal)
        {
            objectivesCompleted++;
            level++;
            Debug.Log("Objective Completed!");
        }
    }
}
