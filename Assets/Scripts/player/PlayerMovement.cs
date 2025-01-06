using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Moving
    [Range(1f, 100f)]
    public float runSpeed = 10;
    [Range(1f, 100f)]
    public float sideStepSpeed = 5;

    [Range(0.1f, 10f)]
    public float acceleration = 3f;
    [Range(0.1f, 10f)]
    public float deceleration = 5f;

    float maxXSpeed;
    float xSpeed = 0;
    float maxZSpeed;
    float zSpeed = 0;

    Rigidbody rb;
    Vector3 V;

    // Animations
    public PlayerAnimations pa;

    // Jump
    [Range(3f, 15f)]
    public float jumpForce = 5f;

    bool onGround = true;
    
    // Attack
    public LayerMask enemyLayer;
    float atkRadius = 1.5f;
    bool attacking = false;

    // Minions
    public GameObject minions;
    MinionCrowd minionsCrowd;
    float sendRate = 10f;
    float nextSend = 0f;
    float nextSendBack = 0f;
    bool ordering = false;

    // Interactions
    public LayerMask interactable;
    float detectRadius = 1.5f;

    bool dead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        minionsCrowd = minions.GetComponent<MinionCrowd>();

        maxZSpeed = runSpeed;
        maxXSpeed = sideStepSpeed;
    }

    void Update()
    {
        if (dead) return;

        HandleJump();
        HandleAttack();
        HandleOrders();
        HandleInteract();
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        bool sprint = (Input.GetKey(KeyCode.LeftShift));

        if (sprint)
            maxZSpeed = runSpeed * 1.5f;
        else
            maxZSpeed = runSpeed;


        if (x != 0)
            xSpeed = Mathf.Lerp(xSpeed, x * maxXSpeed, acceleration * Time.deltaTime);
        else
            if (xSpeed != 0)
            xSpeed = Mathf.Lerp(xSpeed, x * maxXSpeed, deceleration * Time.deltaTime);

        if (z != 0)
            zSpeed = Mathf.Lerp(zSpeed, z * maxZSpeed, acceleration * Time.deltaTime);
        else
            if (zSpeed != 0)
            zSpeed = Mathf.Lerp(zSpeed, z * maxZSpeed, deceleration * Time.deltaTime);

        pa.setAnimatorParameters(xSpeed / sideStepSpeed, zSpeed / runSpeed);

        V = new Vector3(x, 0, z).normalized;
        V.x *= xSpeed < 0 ? -xSpeed : xSpeed;
        V.z *= zSpeed < 0 ? -zSpeed : zSpeed;

        V = transform.TransformDirection(V);

        V.y = rb.velocity.y;

        rb.velocity = V;
    }

    void HandleJump()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, 1.5f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pa.jump();
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        pa.setOnGround(onGround);
    }

    void HandleOrders()
    {
        pa.setOrder(0);

        if (minionsCrowd != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                pa.setOrder(1);
                ordering = true;

                if (Time.time >= nextSend)
                {
                    nextSend = Time.time + 1 / sendRate;
                    minionsCrowd.GoForward();
                }
            }

            if (Input.GetKey(KeyCode.Q))
            {
                pa.setOrder(2);
                ordering = true;

                if (Time.time >= nextSendBack)
                {
                    nextSendBack = Time.time + 1 / sendRate;
                    minionsCrowd.GoBackOne();
                }
            }
        }
    }
    void AllBack()
    {
        minionsCrowd.GoBackAll();
    }

    void HandleInteract()
    {
        // проверка объекта для интеракции в радиусе
        Collider[] cols = Physics.OverlapSphere(transform.position, detectRadius, interactable);
        // если объект попал в радиус
        if (cols.Length > 0 && Input.GetKey(KeyCode.F))
        {
            pa.interact();
            InteractableObj c = cols[0].transform.GetComponent<InteractableObj>();
            if (c != null)
                c.interact();
        }
    }
    void stopInteraction() => pa.stopInteraction();

    void HandleAttack()
    {
        if (Input.GetMouseButton(0))
        {
            pa.Attack();
            attacking = true;
        }
    }
    void attack()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, atkRadius, enemyLayer);

        if (cols.Length > 0)
        {
            Health targetHP = cols[0].GetComponent<Health>();
            if (targetHP != null)
                targetHP.hpDecrease(100);
        }
    }
    void stopAttack() => pa.stopAttack();


    public void death()
    {
        dead = true;
        pa.SetDeath();

        StartCoroutine(despawn());
    }
    IEnumerator despawn()
    {
        yield return new WaitForSeconds(4);

        Destroy(gameObject);
    }
}
