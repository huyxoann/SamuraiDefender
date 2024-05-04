using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 10;

    private void  OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();

        if(damageable != null){
            bool gotHit = damageable.TakeDamage(damage);
            if(gotHit){
                Debug.Log(other.name + "hit for "+damage);
            }
        }
    }
}
