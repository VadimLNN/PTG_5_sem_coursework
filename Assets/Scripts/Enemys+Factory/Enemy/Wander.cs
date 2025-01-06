using System.Collections;
using UnityEngine;

public class Wander : IState
{
    AbstractEnemy enemy;
    private Coroutine wanderCoroutine;
    public Wander(AbstractEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void enter()
    {
        enemy.stop(false);
        wanderCoroutine = enemy.StartCoroutine(WanderRoutine());
    }

    public void exit()
    {
        if (wanderCoroutine != null)
            enemy.StopCoroutine(wanderCoroutine);

        enemy.stop(true);
    }

    public void update()
    {
        enemy.moveTo(SetRandomDest());
    }

    Vector3 SetRandomDest()
    {
        // генераци€ случайной точки в пределах 7 метров 
        var x = Random.Range(enemy.transform.position.x - 7, enemy.transform.position.x + 7);
        var z = Random.Range(enemy.transform.position.z - 7, enemy.transform.position.z + 7);

        var destination = new Vector3(x, enemy.transform.position.y, z);
        return destination;
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            Vector3 destination = SetRandomDest();
            enemy.moveTo(destination);

            // ∆дем, пока моб не достигнет текущей цели
            while (!HasReachedDestination(destination))
            {
                yield return null;
            }

            // ƒобавл€ем задержку между сменой точек
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }
    }

    private bool HasReachedDestination(Vector3 destination)
    {
        float distance = Vector3.Distance(enemy.transform.position, destination);
        return distance <= enemy.stoppingDistance;
    }
}
