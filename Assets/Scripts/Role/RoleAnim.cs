using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAnim : RoleObserver
{

    protected override void OnInit()
    {
        base.OnInit();
        roleEventNode = new RoleEventNode(RoleEventNodeType.Anim);
        Register(this);
        Register(this as IEventReceiver);
    }


    private void Start()
    {
        List<object> data = new List<object> { "play", "anim" };
        roleEventNode.Save(RoleEventDataType.String, data);
        RoleMgr.Instance.OnEventReceiver(roleEventNode);
    }

    public override void OnEventReceiver(EventNode node)
    {
        var lst = (node as RoleEventNode).Data;

        Debug.Log($"{lst[0]}  {lst[1]}");
    }

    public void PlayAnim()
    {
        Debug.Log("PlayAnim");
    }
}


