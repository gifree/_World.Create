
public interface IEventReceiver
{
    /// <summary>
    /// 接收是被动接收，被别人传参执行功能
    /// </summary>
    /// <param name="en"></param>
    void OnEventReceiver(IEventNode eventNode);
}

public interface IEventDispatcher
{
    /// <summary>
    /// 分派是主动分派，给别人传参执行功能
    /// </summary>
    /// <param name="et"></param>
    /// <param name="en"></param>
    void OnEventDispatch(IEventNode eventNode);
}


public interface IMutiEventReceiver
{
    /// <summary>
    /// 接收是被动接收，被别人传参执行功能
    /// </summary>
    /// <param name="en"></param>
    void OnEventReceiver(RoleNumber number, IEventNode eventNode);
}

public interface IMutiEventDispatcher
{
    /// <summary>
    /// 分派是主动分派，给别人传参执行功能
    /// </summary>
    /// <param name="et"></param>
    /// <param name="en"></param>
    void OnEventDispatch(RoleNumber number, IEventNode eventNode);
}



