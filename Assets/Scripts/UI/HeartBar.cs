using UnityEngine;
using UnityEngine.UI;

public class HeartBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health){
        slider.maxValue = health;
    }

    public void SetHealth(int health){
        slider.value = health;
    }
}