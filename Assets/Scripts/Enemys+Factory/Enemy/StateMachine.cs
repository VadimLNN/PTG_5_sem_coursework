public class StateMachine 
{
    IState currentState = null;

    public void startingState(IState state)
    {
        currentState = state;
        currentState?.enter();
    }

    public void setState(IState state)
    {
        if (currentState == state) return;

        currentState?.exit();
        currentState = state;
        currentState?.enter();
    }

    public void update()
    {
        currentState?.update();
    }
}
