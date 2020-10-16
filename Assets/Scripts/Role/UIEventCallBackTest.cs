using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventCallBackTest : MonoBehaviour 
{

    private void Start()
    {
        // RoleMgr.Instance.Mgr<RoleData>().RegisterTask(SS);

    }



    private void SS(CharacterInfo info)
    {
        Debug.Log("更新UI");
    }

}



