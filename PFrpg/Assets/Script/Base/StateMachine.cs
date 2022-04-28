
public class StateMachine<T> where T : class
{
    // StateMachine 소유주
    private T ownerEntity;
    // 현재상태
    private State<T> currentState;

    // 초기설정
    public void SetUp(T own, State<T> entrystate)
    {
        ownerEntity = own;
        currentState = entrystate;
    }

    public void Execute()
    {
        currentState.Execute(ownerEntity);
    }
    
    // 상태를 변화시킵니다.
    public void ChangeState(State<T> nextState)
    {
        // 현재 상태의 Exit() 을 호출합니다.
        currentState.Exit(ownerEntity);

        // 상태를 변화시킵니다.
        currentState = nextState;

        // 변화시킨 상태의 Enter() 를 호출합니다.
        currentState.Enter(ownerEntity);
    }
}
