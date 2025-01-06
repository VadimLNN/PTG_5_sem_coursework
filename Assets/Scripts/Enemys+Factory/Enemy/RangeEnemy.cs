using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : AbstractEnemy
{
    [Range(1f, 50)]
    public float attackRange = 10;
    [Range(1f, 50)]
    public float saveDist = 8;

    [Range(1, 100)]
    public int damage;

    RunTo runState;
    RunOut runOutState;
    Attack attackState;
    Stunned stunnedState;
    RotateTo rotateState;

    public GameObject enemyProjectile;
    public Transform shotPoint;


    private new void Start()
    {
        base.Start();

        runState = new RunTo(this);
        runOutState = new RunOut(this);
        attackState = new Attack(this);
        stunnedState = new Stunned(this);
        rotateState = new RotateTo(this);

        stateMachine.startingState(runState);
    }

    public override void updateState()
    {
        if (dead == true) return;

        if (stunned == true)
        {
            stateMachine?.setState(stunnedState);
        }


        if (Vector3.Distance(transform.position, target.position) < saveDist)
        {
            stateMachine?.setState(runOutState);
        }
        else
        {
            if (Vector3.Angle(transform.forward, target.position - transform.position) > 20)
            {
                stateMachine?.setState(rotateState);
            }
            else if (Vector3.Distance(transform.position, target.position) > attackRange)
            {
                stateMachine?.setState(runState);
            }
            else if (Vector3.Angle(transform.forward, target.position - transform.position) > 10)
            {
                stateMachine?.setState(rotateState);
            }
            else
                stateMachine?.setState(attackState);
        }

        stateMachine?.update();
    }

    public void dealDamage()
    {
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            Instantiate(enemyProjectile, shotPoint.position, shotPoint.rotation);
        }
    }
}
