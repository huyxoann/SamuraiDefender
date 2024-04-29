using System;
using UnityEngine;

internal class PlayerStats : ScriptableObject
{
    internal static int maxHealth = 100;
    internal static int currentHealth;
    internal static float maxStamina = 100f;
    internal static float currentStamina;
    internal static float staminaRunDrainRate = 25f;
    internal static float staminaJumpDrainRate = 0.5f;
    internal static float staminaRegenRate = 40f;
    internal static float staminaKatanaDrainRate = 15f;
    internal static float staminaRegenCountdownTime = 0.5f;
    internal static int level = 0;
    internal static int currentExp=0;
    internal static int[] expLimit= {10, 20, 50, 100, 200, 500};

    internal static float walkSpeed = 4f;
    internal static float runningSpeed = 7f;
    internal static float airSpeed = 5f;
    internal static float jumpImpulse = 5f;

    internal static bool _isMoving = false;
    internal static bool _isRunning = false;
    internal static bool _isJumping = false;

}