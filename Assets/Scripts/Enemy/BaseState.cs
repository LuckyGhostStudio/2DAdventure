/// <summary>
/// ״̬����
/// </summary>
public abstract class BaseState
{
    protected Enemy currentEnemy;

    /// <summary>
    /// ����״̬
    /// </summary>
    /// <param name="enemy"></param>
    public abstract void OnEnter(Enemy enemy);

    /// <summary>
    /// ����
    /// </summary>
    public abstract void LogicUpdate();

    /// <summary>
    /// �������
    /// </summary>
    public abstract void PhysicsUpdate();

    /// <summary>
    /// �˳�״̬
    /// </summary>
    public abstract void OnExit();
}
