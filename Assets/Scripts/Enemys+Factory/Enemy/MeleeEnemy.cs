using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : AbstractEnemy
{
    [Range(0.1f, 10)]
    public float attackRange = 2;
    
    [Range(0.1f, 10)]
    public float detectRange = 15;

    [Range(1, 100)]
    public int damage;

    Wander wanderState;
    RunTo runState;
    Attack attackState;
    RotateTo rotateState;

    public LayerMask targetLayer;

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, detectRange, targetLayer);

        if (cols.Length > 0)
            Target = cols[0].transform;

    }

    private new void Start()
    {
        base.Start();

        wanderState = new Wander(this);
        runState = new RunTo(this);
        attackState = new Attack(this);
        rotateState = new RotateTo(this);

        stateMachine.startingState(runState);
    }

    public override void updateState()
    {
        if (dead == true) return;

        if (Vector3.Distance(transform.forward, target.position - transform.position) < detectRange)
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
        else
        {
            stateMachine?.setState(wanderState);
        }
        
        stateMachine?.update();
    }

    public void dealDamage()
    {
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            Health targetHP = target.GetComponent<Health>();
            if (targetHP != null)
                targetHP.hpDecrease(damage);    
        }
    }
}
