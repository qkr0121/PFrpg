
public abstract class State<T> where T : class
{
    /// <summary>
    /// �ش� ���� ����, ������Ʈ, ���� �� ȣ���մϴ�.
    /// </summary>

    public abstract void Enter(T entity);

    public abstract void Execute(T entity);

    public abstract void Exit(T entity);

}
