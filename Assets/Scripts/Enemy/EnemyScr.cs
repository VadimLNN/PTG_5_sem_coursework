using UnityEngine;
using UnityEngine.AI;

public class EnemyScr : InteractableObj
{
    // ссылка на аниматора, агента и точки пути
    Animator anim;
    NavMeshAgent agent;
    public Transform[] wayPoints;
    
    // радиусы замечания интерактивных объектов, атаки, дистанция до мастера и приспешника
    float detectRadius = 10;
    float atkRadius = 2f;
    float distToMaster, distToMinon;

    // параметры состония, индекс точки
    int state;
    int ind = 0;

    // слои игрока и приспешника
    public LayerMask playerLayer;
    public LayerMask minionLayer;
    public LayerMask targetLayer;

    // точки для определения состояния простоя или ходьбы 
    Vector3 oldPos;
    Vector3 newPos;

    // ссылка на полоску здоровья
    public GameObject hpScrollBar;
    // кол-во здоровья
    public int hp = 50;
    
    // состояние смерти
    bool isDead = false;
    
    

    void Start()
    {
        hpScrollBar.GetComponent<HealthBarScr>().maxHealth = hp;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.SetDestination(wayPoints[ind].position);
    }

    void LateUpdate()
    {
        hpScrollBar.GetComponent<HealthBarScr>().health = hp;

        if (hp <= 0)
        {
            agent.SetDestination(transform.position); 
            state = Random.Range(-2, -1);
            isDead = true;
            Destroy(this.gameObject, 5);
        }

        if (isDead == false)
        {
            // отслеживание состояний простоя и ходьбы
            newPos = transform.position;

            if (oldPos == newPos)
            {
                state = 0;
            }
            else
            {
                state = 1;
            }

            oldPos = newPos;


            // ходьба по точкам пути
            if (Vector3.Distance(transform.position, wayPoints[ind].position) < 2f)
            {
                ind++;

                if (ind >= wayPoints.Length) ind = 0;

                agent.SetDestination(wayPoints[ind].position);
            }

            // отслеживание объектов игрока и приспешников
            Collider[] colsP = Physics.OverlapSphere(transform.position, detectRadius, playerLayer);
            Collider[] colsM = Physics.OverlapSphere(transform.position, detectRadius, minionLayer);

            // 
            if (colsP.Length > 0)
                distToMaster = Vector3.Distance(transform.position, colsP[0].transform.position);
            if (colsM.Length > 0)
                distToMinon = Vector3.Distance(transform.position, colsM[0].transform.position);

            // если игрок или приспешник в радиусе 
            if (colsP.Length > 0 || colsM.Length > 0)
            {
                if (distToMaster > 0 && distToMinon > 0 && distToMaster < distToMinon ||
                    distToMaster > 0 && distToMinon == 0)
                {
                    if (Vector3.Distance(transform.position, colsP[0].transform.position) <= atkRadius)
                    {
                        state = 3;
                        agent.SetDestination(transform.position);
                    }
                    else
                        agent.SetDestination(colsP[0].transform.position);
                }
                if (distToMaster > 0 && distToMinon > 0 && distToMaster > distToMinon ||
                    distToMaster == 0 && distToMinon > 0)
                {
                    if (Vector3.Distance(transform.position, colsM[0].transform.position) <= atkRadius)
                    {
                        state = 3;
                        agent.SetDestination(transform.position);
                    }
                    else
                        agent.SetDestination(colsM[0].transform.position);
                }
            }
            else
            {
                agent.SetDestination(wayPoints[ind].position);
            }

            // 
            distToMaster = 0;
            distToMinon = 0;
        }

        // установка анимации
        anim.SetInteger("state", state);
    }
    public override void interact()
    {
        anim.SetInteger("state", 2);
    }

    public void dead()
    {
        Destroy(this.gameObject, 2);
    }
    

    void attack()
    {
        // отслеживание объектов игрока и приспешников
        Collider[] colsP = Physics.OverlapSphere(transform.position, atkRadius, playerLayer);
        Collider[] colsM = Physics.OverlapSphere(transform.position, atkRadius, minionLayer);

        // 
        if (colsP.Length > 0)
            distToMaster = Vector3.Distance(transform.position, colsP[0].transform.position);
        if (colsM.Length > 0)
            distToMinon = Vector3.Distance(transform.position, colsM[0].transform.position);

        // если игрок или приспешник в радиусе 
        if (colsP.Length > 0 || colsM.Length > 0)
        {
            if (distToMaster > 0 && distToMinon > 0 && distToMaster < distToMinon ||
                distToMaster > 0 && distToMinon == 0)
            {
                Controll c = colsP[0].transform.GetComponent<Controll>();
                if (c != null) c.takeDamage();
            }
            if (distToMaster > 0 && distToMinon > 0 && distToMaster > distToMinon ||
                distToMaster == 0 && distToMinon > 0)
            {
                MinionScr c = colsM[0].transform.GetComponent<MinionScr>();
                //if (c != null) c.takeDamage(20);
            }
        }

        distToMaster = 0;
        distToMinon = 0;
    }

    public void takeDamage(int gamage) 
    {
        hp -= gamage;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, detectRadius);
        //Gizmos.DrawWireSphere(transform.position, atkRadius);
    }
}
