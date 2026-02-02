using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    private Slider healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        PlayerHealth.OnHealthChange += ChangeValue;
    }

    private void ChangeValue(int currHealth, int maxHealth)
    {
        healthBar.value = (float)currHealth/maxHealth;
    }



}
