using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class WanderScr : MonoBehaviour
{
    // ссылки на navMesh поверхность и агента  
    public NavMeshSurface surface;
    public NavMeshAgent agent;

    // таймер
    float timer;

    // точка назначени€ 
    Vector3 destination;

    // состо€ние блуждани€
    public bool isWander = true;

    void Start()
    {
        agent.destination = SetRandomDest(surface.navMeshData.sourceBounds);
        timer = 0;
    }

    void Update()
    {
        if (isWander)
        {
            // отсчЄт таймера до установки новой точки назначени€ обнулени€ таймера 
            timer += Time.deltaTime;
            if (timer > 5)
            {
                agent.destination = SetRandomDest(surface.navMeshData.sourceBounds);
                timer = 0;
            }
        }
    }

    Vector3 SetRandomDest(Bounds bounds)
    {
        // генераци€ случайной точки в пределах 7 метров 
        var x = Random.Range(transform.position.x - 7, transform.position.x + 7);
        var z = Random.Range(transform.position.z - 7, transform.position.z + 7);

        destination = new Vector3(x, transform.position.y, z);
        return destination;
    }
}