using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;


    private void Awake() {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        detectedColliders.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        detectedColliders.Remove(other);
    }
}
