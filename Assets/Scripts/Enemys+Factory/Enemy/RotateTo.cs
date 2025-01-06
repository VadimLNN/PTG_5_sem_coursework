public class RotateTo : IState
{
    AbstractEnemy enemy;
    float updatesPerSecond;
    public RotateTo(AbstractEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void enter()
    {
        enemy.stop(true);
        updatesPerSecond = enemy.updatesPerSecond;
        enemy.updatesPerSecond = 60;
    }

    public void exit()
    {
        enemy.stop(false);
        enemy.updatesPerSecond = updatesPerSecond;
    }
    public void update()
    {
        enemy.rotateTo(enemy.Target.position);
    }
}
