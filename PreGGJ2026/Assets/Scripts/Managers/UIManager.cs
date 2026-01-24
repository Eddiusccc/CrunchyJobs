using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    #region TEXT ELEMENTS
    public TMP_Text workerName;
    public TMP_Text[] stats;
    public TMP_Text state;
    public Slider objectiveBar;
    #endregion
    Worker currentWorker;
    private void Start()
    {
        UpdateBar(0);
    }
    private void Update()
    {
        if (currentWorker != null)
        {
            UpdateWorkerUI();
        }
    }

    void UpdateWorkerUI()
    {
        workerName.text = currentWorker.workerName;
        for (int i = 0; i < currentWorker.workerStats.Length; i++)
        {
            if (i < 2)
            {
                stats[i].text = $"{currentWorker.workerStats[i].statName}: " +
                    $"{currentWorker.workerStats[i].currentValue}/{currentWorker.workerStats[i].maxValue}";
            }
            else
            {
                stats[i].text = $"{currentWorker.workerStats[i].statName}: " +
                     $"{currentWorker.workerStats[i].currentValue}";
            }
        }
        state.text = $"Estado: {currentWorker.currentState}";
    }
    public void UpdateBar(float newValue)
    {
        objectiveBar.value = Mathf.Clamp(newValue, objectiveBar.minValue, objectiveBar.maxValue);
    }
    public void AssignWorker(Worker worker)
    {
        currentWorker = worker;
        UpdateWorkerUI();
    }



    private void Awake()
    {
        instance = this;
    }
}
