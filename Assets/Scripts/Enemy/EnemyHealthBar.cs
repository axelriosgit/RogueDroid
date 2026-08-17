using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        Debug.Log("SLIDER ASIGNADO: " + slider);
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        Debug.Log("BARRA MAX: " + maxHealth);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        Debug.Log("BARRA RECIBE VIDA: " + health);
        Debug.Log("VALOR REAL: " + slider.value);
    }
}