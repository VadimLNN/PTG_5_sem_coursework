using UnityEngine;

public class CameraLookingScr : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public float smoothing = 2.0f;
    Vector2 mouseLook;
    Vector2 smoothV;
    GameObject character;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        character = this.transform.parent.gameObject;
    }

    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        //transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

    }
}
