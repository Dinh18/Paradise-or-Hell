using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField]private int minute = 0;
    [SerializeField] private float second = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime;
        if(second >= 60)
        {
            minute++;
            second = 0;
        }

    }
}
