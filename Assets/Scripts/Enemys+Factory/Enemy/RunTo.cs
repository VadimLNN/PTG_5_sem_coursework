public class RunTo : IState
{
    AbstractEnemy enemy;

    public RunTo(AbstractEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void enter()
    {
        enemy.stop(false);
        enemy.moveTo(enemy.Target.position);
    }

    public void exit()
    {
        enemy.stop(true);
    }

    public void update()
    {
        enemy.moveTo(enemy.Target.position);
    }
}
