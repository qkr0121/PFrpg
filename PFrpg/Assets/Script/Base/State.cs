
public abstract class State<T> where T : class
{
    /// <summary>
    /// 해당 상태 시작, 업데이트, 종료 시 호출합니다.
    /// </summary>

    public abstract void Enter(T entity);

    public abstract void Execute(T entity);

    public abstract void Exit(T entity);

}
