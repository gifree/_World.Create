using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIEventCallBackTest : MonoBehaviour 
{

    private void Start()
    {
        // RoleMgr.Instance.Mgr<RoleData>().RegisterTask(SS);
        SkillSystem skillSystem = new SkillSystem();
        SkillEventDispatcher ds = new SkillEventDispatcher(new SkillEventReceiver());
       // ds.Register("ss", new Action(()=> { }));
        ds.Register<SkillInfo>("ss", SS);

        SkillInstance sk1 = new SkillInstance(1);
        sk1.Name = "11";
        sk1.Triggers.Add(new TriggerFaceToTarget());
        skillSystem.AddToPool(sk1);

        var s1 = skillSystem.GetSkillInstance(1);
        s1.Begin(Time.time);
        SkillInfo s11 = new SkillInfo();
        s11.From = 100;
        s1.Update(Time.time + 1f, s11);

        //    ds.Trigger("ss", new SkillInfo());
        //ds.UnRegister<SkillInfo>("ss", SS);
        //ds.Trigger("ss", new SkillInfo());
    }


    private void SS(SkillInfo skil)
    {
        print(skil.From);
    }


    private void SS(CharacterInfo info)
    {
        Debug.Log("更新UI");
    }

}


public class TriggerFaceToTarget : SkillTrigger
{
    public override ISkillTrigger Clone() => (ISkillTrigger)this.MemberwiseClone();

    public override void Init(string args)
    {
     
    }

    public override void Update(SkillInfo info)
    {
        Debug.Log(info.From);
        executing = true;
    }
}



