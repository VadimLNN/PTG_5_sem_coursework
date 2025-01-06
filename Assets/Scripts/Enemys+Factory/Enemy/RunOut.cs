using UnityEngine;

public class RunOut : IState
{
    AbstractEnemy enemy;

    public RunOut(AbstractEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void enter()
    {
        enemy.stop(false);
        enemy.moveTo(enemy.transform.position - enemy.Target.position);
    }

    public void exit()
    {
        enemy.stop(true);
    }

    public void update()
    {
        enemy.moveTo(enemy.transform.position - enemy.Target.position);
    }
}
