using UnityEngine;
using UnityEngine.AI;

public class GoatSheepControllerScr : MonoBehaviour
{
    // ссылка на аниматора, агента
    Animator anim;
    NavMeshAgent agent;

    // точки для определения состояния простоя или ходьбы 
    Vector3 oldPos;
    Vector3 newPos;

    // параметры состония, кол-во здоровья
    int state;
    public int hp = 15;

    // состояние жизни 
    bool isDead = false;

    // ссылка на полоску здоровья
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
            // отслеживание состояний простоя и ходьбы
            newPos = transform.position;

            if (oldPos == newPos)
                state = 0;
            else
                state = 1;

            oldPos = newPos;
        }

        // установка анимации
        anim.SetInteger("state", state);
    }

    public void takeDamage(int gamage)
    {
        hp -= gamage;
    }
}
