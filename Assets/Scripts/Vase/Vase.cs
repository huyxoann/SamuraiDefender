using System;
using UnityEngine;
using static VaseStats;

public class Vase : MonoBehaviour
{
    Damageable damageable;
    void Awake()
    {
        damageable = GetComponent<Damageable>();
        damageable.MaxHealth = VaseStats.maxHealth;
    }


}
internal class VaseStats : ScriptableObject
{
    internal static int maxHealth = 200;
}