
public class StateMachine<T> where T : class
{
    // StateMachine ������
    private T ownerEntity;
    // �������
    private State<T> currentState;

    // �ʱ⼳��
    public void SetUp(T own, State<T> entrystate)
    {
        ownerEntity = own;
        currentState = entrystate;
    }

    public void Execute()
    {
        currentState.Execute(ownerEntity);
    }
    
    // ���¸� ��ȭ��ŵ�ϴ�.
    public void ChangeState(State<T> nextState)
    {
        // ���� ������ Exit() �� ȣ���մϴ�.
        currentState.Exit(ownerEntity);

        // ���¸� ��ȭ��ŵ�ϴ�.
        currentState = nextState;

        // ��ȭ��Ų ������ Enter() �� ȣ���մϴ�.
        currentState.Enter(ownerEntity);
    }
}
