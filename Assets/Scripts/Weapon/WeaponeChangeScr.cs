using UnityEngine;

public class WeaponeChangeScr : MonoBehaviour
{
    public GameObject[] shields;
    public GameObject[] swords;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            switchWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            switchWeapon(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            switchWeapon(3);
    }

    void switchWeapon(int num)
    {
        for (int i = 1; i <= shields.Length; i++) 
        {
            if (i == num)
                shields[i - 1].SetActive(true);
            else 
                shields[i - 1].SetActive(false);

            if (i == num)
                swords[i - 1].SetActive(true);
            else
                swords[i - 1].SetActive(false);
        }


    }
}
