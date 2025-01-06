public class Stunned : IState
{
    AbstractEnemy enemy;
    public Stunned(AbstractEnemy enemy) => this.enemy = enemy;
    public void enter() => enemy.stop(true);
    public void exit() => enemy.stop(false);
    public void update() { }
    
}
