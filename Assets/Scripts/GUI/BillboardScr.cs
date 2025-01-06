using UnityEngine;

public class BilboardScr : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);    
    }
}
