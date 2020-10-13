
/// <summary>
/// ROLE事件接收器管理，负责注册、反注册
/// </summary>
public interface IRoleEventReceiver
{
    void Register(RoleEventNodeType type, IEventReceiver receiver);
    void UnRegister(RoleEventNodeType type);
}

/// <summary>
/// ROLE实例对象的管理，注册、反注册、获取
/// </summary>
public interface IRoleMgr
{
    void Register<T>(T t, bool replace = false) where T : RoleObserver;
    void UnRegister<T>();
    T Mgr<T>() where T : RoleObserver;
}

/// <summary>
/// MUTIROLE实例对象的管理，注册、反注册、获取
/// </summary>
public interface IMutiRoleMgr
{
    void Register<T>(RoleNumber number, T t, bool replace = false) where T : MutiRoleObserver;
    void UnRegister<T>(RoleNumber number);
    T Mgr<T>(RoleNumber number) where T : MutiRoleObserver;
}

/// <summary>
/// MUTIROLE事件接收器管理，负责注册、反注册
/// </summary>
public interface IMutiRoleEventReceiver
{
    void Register(RoleEventNodeType type, RoleNumber number, IEventReceiver receiver);
    void UnRegister(RoleEventNodeType type, RoleNumber number);
}


