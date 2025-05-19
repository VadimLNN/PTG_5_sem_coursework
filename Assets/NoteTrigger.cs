using UnityEngine;
using UnityEngine.UI;

public class NoteTrigger : MonoBehaviour
{
    public GameObject notePanel;      // ������ UI � ������� �������
    public Text noteText;             // ��������� ������ (UI Text ��� TMP_Text)
    [TextArea]
    public string message;            // ����� �������

    private bool isPlayerNearby = false;

    void Start()
    {
        if (notePanel != null)
            notePanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            ShowNote();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            HideNote();
        }
    }

    void ShowNote()
    {
        if (notePanel != null && noteText != null)
        {
            noteText.text = message;
            notePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void HideNote()
    {
        if (notePanel != null)
        {
            notePanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
