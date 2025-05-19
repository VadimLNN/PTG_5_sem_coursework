using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public LayerMask npc;
    public Camera cam;

    public int intellect = 1;
    public int power = 1;

    void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            if (Physics.Raycast(ray, out hit, 100, npc))
                hit.transform.GetComponent<NPCScript>().interact();
    }
}
