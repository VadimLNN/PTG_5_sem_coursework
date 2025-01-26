using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    public bool cursorLock = true;

    void Start() => lockCursor();

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    if (cursorLock)
        //        lockCursor();
        //    else
        //        unlockCursor();
    }

    public void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLock = true;
    }

    public void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLock = false;
    }
}
