
public interface IEventReceiver
{
    /// <summary>
    /// 接收是被动接收，被别人传参执行功能
    /// </summary>
    /// <param name="en"></param>
    void OnEventReceiver(EventNode en);
}

public interface IEventDispatcher
{
    /// <summary>
    /// 分派是主动分派，给别人传参执行功能
    /// </summary>
    /// <param name="et"></param>
    /// <param name="en"></param>
    void OnEventDispatch(EventNode en);
}


public interface IMutiEventReceiver
{
    /// <summary>
    /// 接收是被动接收，被别人传参执行功能
    /// </summary>
    /// <param name="en"></param>
    void OnEventReceiver(RoleNumber number, EventNode en);
}

public interface IMutiEventDispatcher
{
    /// <summary>
    /// 分派是主动分派，给别人传参执行功能
    /// </summary>
    /// <param name="et"></param>
    /// <param name="en"></param>
    void OnEventDispatch(RoleNumber number, EventNode en);
}

