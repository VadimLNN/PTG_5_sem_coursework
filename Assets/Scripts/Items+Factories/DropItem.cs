using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public OrganItem item;
    public QuestManager manager;
    
    public void dropItem(Vector3 point)
    {
        OrganItem droppedItem = Instantiate(item);



        item.setPosition(transform.position);
    }
}
