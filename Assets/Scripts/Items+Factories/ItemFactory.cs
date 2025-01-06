using UnityEngine;

public abstract class ItemFactory : MonoBehaviour
{
    public abstract IItem getItem();
}
