using System.Collections.Generic;
/// <summary>
/// ROLE事件节点，存储ROLE当前要响应的事件内容
/// 暂时不确定需求，一般针对通信用
/// </summary>
public class RoleEventNode : IRoleEventNode
{
    public RoleEventNodeType NodeType { get;  set; }
    public List<object> Data { get;  set; }
    public RoleEventDataType DataType { get;  set; }

    public RoleEventNode(RoleEventNodeType type) => NodeType = type;

    public void Save(RoleEventDataType type, List<object> data)
    {
        DataType = type;
        Data = data;
    }
}

/// <summary>
/// ROLE事件节点，存储ROLE当前要响应的事件内容
/// 需要继承这个接口来实现具体内容，好处是接口可以扩充
/// 暂时不确定需求，一般针对通信用
/// </summary>
public interface IRoleEventNode : IEventNode
{
    RoleEventNodeType NodeType { get; set; }
    List<object> Data { get; set; }
    RoleEventDataType DataType { get; set; }
    void Save(RoleEventDataType type, List<object> data);
}



