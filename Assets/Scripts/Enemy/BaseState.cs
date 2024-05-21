/// <summary>
/// 状态基类
/// </summary>
public abstract class BaseState
{
    protected Enemy currentEnemy;

    /// <summary>
    /// 进入状态
    /// </summary>
    /// <param name="enemy"></param>
    public abstract void OnEnter(Enemy enemy);

    /// <summary>
    /// 更新
    /// </summary>
    public abstract void LogicUpdate();

    /// <summary>
    /// 物理更新
    /// </summary>
    public abstract void PhysicsUpdate();

    /// <summary>
    /// 退出状态
    /// </summary>
    public abstract void OnExit();
}
