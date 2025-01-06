using UnityEngine;

public interface IItem
{
    public void onPickUp(GameObject player);
    public void setPosition(Vector3 position);
}
