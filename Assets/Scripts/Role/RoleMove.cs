using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMove : RoleObserver
{
    protected override void OnInit()
    {
        base.OnInit();
        @event = new RoleEventNode(RoleEventNodeType.Move);
        Register(this);
        Register(this as IEventReceiver);
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        List<object> data = new List<object> { "play", "move" };
        @event.Save(RoleEventDataType.String, data);
        RoleMgr.Instance.OnEventReceiver(@event);
    }

    public override void OnEventReceiver(IEventNode @event)
    {
        var lst = (@event as RoleEventNode).Data;
        Debug.Log($"{lst[0]}  {lst[1]}");
    }

    public void PlayMove()
    {
        Debug.Log("PlayMove");
    }

    private void OnDisable()
    {
        UnRegister<RoleMove>();
        UnRegister();
    }
}
