using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MinionScr : MonoBehaviour
{
    // ссылка на нав. агента, аниматора 
    public NavMeshAgent agent;
    Animator anim;

    // слой врагов 
    public LayerMask enemyLayer;

    // параметр состояния и времени на осмотр перед возвращением 
    int state;
    float inspectionTime = 0.5f;

    // радиус атаки, замечания, здоровья
    float atkRadius = 0.7f;
    float detectRadius = 5f;

    // точки для определения состояния простоя или ходьбы 
    Vector3 assignmentPnt;

    // состояния "на задании", "смерти"
    public bool isOnAssignment = false;
    bool isDead = false;
    bool isFighting = false;

    Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();  
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (isDead == true) return;
        
        Vector3 posNow = transform.position;
        float razbros = 0.1f;
        if (assignmentPnt.x - razbros <= posNow.x && posNow.x <= assignmentPnt.x + razbros &&
            assignmentPnt.z - razbros <= posNow.z && posNow.z <= assignmentPnt.z + razbros)
        {
            state = 0;

            // отсчёт времени до прекращения состояния "на задании" 
            if (state == 0 && isOnAssignment == true)
            {
                inspectionTime -= Time.deltaTime;
            }

            if (inspectionTime <= 0 && isOnAssignment == true)
            {
                isOnAssignment = false;
                inspectionTime = 0.5f;
            }
        }
        else
            state = 1;


        // отслеживание врага
        Collider[] cols = Physics.OverlapSphere(transform.position, detectRadius, enemyLayer);

        // если враг в радиусе 
        if (cols.Length > 0 && isOnAssignment == true)
        {
            isFighting = true;
            if (Vector3.Distance(transform.position, cols[0].transform.position) <= atkRadius)
            {
                state = 2;
                agent.SetDestination(transform.position);
            }
            else
                agent.SetDestination(cols[0].transform.position);
        }
        else
            isFighting = false;


        // установка анимации
        anim.SetInteger("state", state);
        anim.SetFloat("speed", Vector3.Magnitude(rb.velocity));
    }
    
    public void FollowOrder(Vector3 point) 
    {
        if (isDead == false)
        {
            assignmentPnt = point;
        
            // пробежка до задания и установка состояния 
            agent.SetDestination(point);
            isOnAssignment = true;
        }
    }
    public void FollowMaster(Vector3 point)
    {
        if (isDead == false)
        {
            assignmentPnt = point;
            // преследование мастера
            agent.SetDestination(point);
            isOnAssignment = false;
            isFighting = false;
        }
    }

    public bool GetIsOnAssignment()
    {
        // возвращение состояния на задании
        return isOnAssignment;
    }
    public void stopFollowOrder()
    {
        // прекрашение состояния "на задании"
        isOnAssignment = false;
        isFighting = false;
    }


    void dealDamage()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, atkRadius, enemyLayer);

        if (cols.Length > 0)
        {
            Health targetHP = cols[0].GetComponent<Health>();
            if (targetHP != null)
                targetHP.hpDecrease(3);
        }
    }
    public void death()
    {
        isDead = true;
        agent.SetDestination(transform.position);
        anim.SetTrigger("death");

        StartCoroutine(despawn());
    }
    IEnumerator despawn()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        // радиус атаки
        //Gizmos.DrawWireSphere(transform.position, atkRadius);
        //Gizmos.DrawWireSphere(transform.position, detectRadius);

    }
}
