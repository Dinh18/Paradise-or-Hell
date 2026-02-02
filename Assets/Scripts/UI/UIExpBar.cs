using UnityEngine;
using UnityEngine.UI;

public class UIExpBa : MonoBehaviour
{
    private Slider expBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        expBar = GetComponent<Slider>();
        expBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        PlayerExperience.OnChangeExp += ChangeValue;
    }

    private void ChangeValue(int currExp, int expToUpLevel)
    {
        expBar.value = (float) currExp/expToUpLevel;
    }
}
