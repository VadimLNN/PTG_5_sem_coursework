using UnityEngine;
using UnityEngine.AI;

public class Controll : MonoBehaviour
{
    // ссылки на физическое тело, состояние и аниматора
    Rigidbody rb;
    int state = 0;
    Animator anim;

    // слой для интерактивных объектов, врагов и радиус действия
    public LayerMask interactable;
    public LayerMask enemyLayer;
    float detectRadius = 1.5f;

    // скорость передвижения
    float speed = 2.5f;

    // Характеристики для прыжка
    [Range(1f, 10f)]
    public float jumpForce = 5;
    public bool onGround;

    // состояние атаки, полёта, ползанья и блока
    bool attacking = false;
    bool isFlying = false;
    bool isCrouch = false;
    bool isBlock = false;


    // для работы с миньонами
    public GameObject minions;
    MinionCrowd minionsCrowd;

    float sendRate = 10f;
    float nextSend = 0f;
    float nextSendBack = 0f;


    // 
    int hp = 100;
    float atkRadius = 1.5f;



    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        onGround = true;

        minionsCrowd = minions.GetComponent<MinionCrowd>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            onGround = true;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            onGround = false;
    }

    void LateUpdate()
    {
        // установка анимации простоя
        state = 0;

        // если нет анимации атаки
        if (attacking == false)
        {
            // установка состояния на кортах
            if (Input.GetKeyDown(KeyCode.C))
                isCrouch = !isCrouch;

            // при блоке движение в блоке
            if (isBlock == false)
            {
                // установка анимации и передвижения вперёд, назад 
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (isCrouch == true)
                    {
                        state = 111;
                        rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed / 1.3f);
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            state = 11;                                                      // умножение скорости X2 тк бег
                            rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed * 2);
                        }
                        else
                        {
                            state = 1;
                            rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);
                        }
                    }
                }
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    if (isCrouch == true)
                    {
                        state = 222;
                        rb.MovePosition(transform.position - transform.forward * Time.fixedDeltaTime * speed / 1.3f);
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            state = 22;                                                         // снижение скорости тк движение назад
                            rb.MovePosition(transform.position - transform.forward * Time.fixedDeltaTime * speed);
                        }
                        else
                        {
                            state = 2;                                                           // снижение скорости тк движение назад
                            rb.MovePosition(transform.position - transform.forward * Time.fixedDeltaTime * (speed / 2));
                        }
                    }
                }

                // ходьба в стороны
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        state = 33;                                                        
                        rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime * speed * 2);
                    }
                    else
                    {
                        state = 3;                                                           
                        rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime * speed);
                    }
                
                    //state = 3;
                    //Quaternion deltaRotation = Quaternion.Euler(Vector3.up * Time.deltaTime * ang_speed); ;
                    //rb.MoveRotation(rb.rotation * deltaRotation);
                }
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        state = 44;
                        rb.MovePosition(transform.position - transform.right * Time.fixedDeltaTime * speed * 2);
                    }
                    else
                    {
                        state = 4;
                        rb.MovePosition(transform.position - transform.right * Time.fixedDeltaTime * speed);
                    }

                    //state = 4;
                    //Quaternion deltaRotation = Quaternion.Euler(Vector3.up * Time.deltaTime * -ang_speed);
                    //rb.MoveRotation(rb.rotation * deltaRotation);
                }
            }

            // команда вперёд на пкм, если персонаж стоит, идёт вперёд
            if (onGround == true //&& (state == 0 || state == 1 || state == 2 || state == 3 || state == 4) 
                && Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftControl))
            {
                state = 5;
                if (Time.time >= nextSend)
                {
                    nextSend = Time.time + 1 / sendRate;
                    minionsCrowd.GoForward();
                }
            }
            // атака мечом на пкм + ctrl, если персонаж стоит или ходит
            if (onGround == true //&& (state == 0 || state == 1 || state == 2 || state == 3 || state == 4) 
                && Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftControl))
                state = 55;
            
            // команда назад на лкм, если персонаж стоит или ходит
            if (onGround == true //&& (state == 0 || state == 1 || state == 2 || state == 3 || state == 4) 
                && Input.GetKey(KeyCode.Mouse1))
            {
                state = 6;
                if (Time.time >= nextSendBack)
                {
                    nextSendBack = Time.time + 1 / sendRate;
                    minionsCrowd.GoBackOne();
                }
            }

            // блок на лкм + ctrl, если персонаж стоит, идёт вперёд
            if (onGround == true //&& (state == 0 || state == 1 || state == 2 || state == 3 || state == 4) 
                && Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.LeftControl))
            {
                isBlock = true;
                state = 66;
            }
            else 
                isBlock = false;
            

            // прыжок (взлёт)
            if (onGround == true && Input.GetKey(KeyCode.Space))
            {
                state = 100;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            // полёт
            if (onGround == false)
            {
                state = 101;
                isFlying = true;
            }
            // приземление
            if (onGround == true && isFlying == true)
            {
                isFlying = false;
                state = 102;
            }

            // проверка объекта для интеракции в радиусе
            Collider[] cols = Physics.OverlapSphere(transform.position, detectRadius, interactable);
            // если объект попал в радиус
            if (cols.Length > 0 && Input.GetKey(KeyCode.E))
            {
                //state = 500;
                InteractableObj c = cols[0].transform.GetComponent<InteractableObj>();
                if (c != null)
                    c.interact();
            }
        }
        
        // воспроизведение анимаций
        anim.SetBool("isBlock", isBlock);
        anim.SetBool("isCrouch", isCrouch);
        anim.SetInteger("state", state);
    }

    private void OnDrawGizmos()
    {
        // радиус действия и атаки
        //Gizmos.DrawWireSphere(transform.localPosition, detectRadius);

        // точка куда побежит прихвостень
        //Gizmos.DrawWireSphere(transform.position + transform.forward * 15, 0.5f);
    }

    public void AttackOn()
    {
        attacking = true;
    }
    public void AttackOff()
    {
        attacking = false;
    }

    void AllBack()
    {
        minionsCrowd.GoBackAll();
    }
    void OneBack()
    {
        minionsCrowd.GoBackOne();
    }


    void attack()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, atkRadius, enemyLayer);

        if (cols.Length > 0)
        {
            EnemyScr c = cols[0].transform.GetComponent<EnemyScr>();
            if (c != null) c.takeDamage(20);

            GoatSheepControllerScr c2 = cols[0].transform.GetComponent<GoatSheepControllerScr>();
            if (c2 != null) c2.takeDamage(20);
        }
    }
    public void takeDamage()
    {
        anim.SetInteger("state", -1);                
        //Destroy(this.gameObject, 1);
    }
}
