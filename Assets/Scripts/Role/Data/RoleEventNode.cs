using System.Collections.Generic;
/// <summary>
/// ROLE事件节点，存储ROLE当前要响应的事件内容
/// 暂时不确定需求，一般针对通信用
/// </summary>
public class RoleEventNode : EventNode
{
    public RoleEventNodeType NodeType { get; private set; }
    public List<object> Data { get; private set; }
    public RoleEventDataType DataType { get; private set; }

    public RoleEventNode(RoleEventNodeType type) => NodeType = type;

    public void Save(RoleEventDataType type, List<object> data)
    {
        DataType = type;
        Data = data;
    }
}



