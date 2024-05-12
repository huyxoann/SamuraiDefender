using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthRestoreAmount = 10;
    public Vector3 spinRotationSpeed = new Vector3(0, 100, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();

        if (damageable)
        {
            if (damageable.CurrentHealth < damageable.MaxHealth)
            {
                damageable.Heal(healthRestoreAmount);
                Destroy(gameObject);
                GameManager.IncreaseScore(5);
            }else{
                return;
            }

        }
    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
