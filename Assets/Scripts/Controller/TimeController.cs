using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]private int minute = 0;
    [SerializeField] private float second = 0;
    private SpawnerController spawnerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnerController = FindAnyObjectByType<SpawnerController>();
    }

    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime;
        if(second >= 60)
        {
            minute++;
            if(minute == 5) spawnerController.UpdateWave(2,20);
            else if(minute == 10) spawnerController.UpdateWave(3,20);
            second = 0;
        }

    }
}
