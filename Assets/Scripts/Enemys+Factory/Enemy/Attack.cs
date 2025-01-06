public class Attack : IState
{
    AbstractEnemy enemy;
    public Attack(AbstractEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void enter()
    {
        enemy.stop(true);
        enemy.attack(true);
    }

    public void exit()
    {
        enemy.attack(false);
        enemy.stop(false);
    }
    public void update()
    {
        enemy.attack(true);
    }
}
