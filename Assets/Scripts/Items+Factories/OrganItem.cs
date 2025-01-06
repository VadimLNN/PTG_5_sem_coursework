using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganItem : MonoBehaviour, IItem
{
    [SerializeField] ItemTypes itemType;
    [Range(1, 1000)]
    [SerializeField] int amount = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            onPickUp(other.gameObject);
    }

    public void onPickUp(GameObject player)
    {
        player.GetComponent<Inventory>().addItem(itemType, amount);
        Destroy(gameObject);
    }

    public void setPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
