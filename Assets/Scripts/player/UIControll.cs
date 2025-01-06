using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControll : MonoBehaviour
{
    public GameObject questPnl;
    bool visible = false;


    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
            visible = ! visible;
            
        questPnl.SetActive(visible);
    }
}
