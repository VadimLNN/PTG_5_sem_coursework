using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour, IItem
{
    [Range(1, 100)]
    [SerializeField] int amount;

    public void onPickUp(GameObject player)
    {
        if(player.GetComponent<Health>().changeHealth(amount))
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            onPickUp(other.gameObject);
    }

    public void setPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
