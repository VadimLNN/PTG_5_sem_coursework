using UnityEngine;
using UnityEngine.AI;

public class GoatSheepControllerScr : MonoBehaviour
{
    // ������ �� ���������, ������
    Animator anim;
    NavMeshAgent agent;

    // ����� ��� ����������� ��������� ������� ��� ������ 
    Vector3 oldPos;
    Vector3 newPos;

    // ��������� ��������, ���-�� ��������
    int state;
    public int hp = 15;

    // ��������� ����� 
    bool isDead = false;

    // ������ �� ������� ��������
    public GameObject hpScrollBar;

    void Start()
    {
        hpScrollBar.GetComponent<HealthBarScr>().maxHealth = hp;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        hpScrollBar.GetComponent<HealthBarScr>().health = hp;

        if (hp <= 0)
        {
            agent.SetDestination(transform.position);
            state = -1;
            isDead = true;
            Destroy(this.gameObject, 2);
        }

        if (isDead == false)
        {
            // ������������ ��������� ������� � ������
            newPos = transform.position;

            if (oldPos == newPos)
                state = 0;
            else
                state = 1;

            oldPos = newPos;
        }

        // ��������� ��������
        anim.SetInteger("state", state);
    }

    public void takeDamage(int gamage)
    {
        hp -= gamage;
    }
}
