using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAnim : RoleObserver
{

    protected override void OnInit()
    {
        base.OnInit();
        //roleEventNode = new RoleEventNode(RoleEventNodeType.Anim);
        Register(this);
        Register(this as IEventReceiver);
    }


    private void Start()
    {
        List<object> data = new List<object> { "play", "anim" };
        @event.Save(RoleEventDataType.String, data);
        RoleMgr.Instance.OnEventReceiver(@event);
    }

    public override void OnEventReceiver(IEventNode @event)
    {
        var lst = (@event as RoleEventNode).Data;

        Debug.Log($"{lst[0]}  {lst[1]}");
    }

    public void PlayAnim()
    {
        Debug.Log("PlayAnim");
    }
}


