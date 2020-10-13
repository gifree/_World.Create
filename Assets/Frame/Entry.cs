using System;
using System.Collections.Generic;
using System.Diagnostics;
using _World.Create;
using UnityEngine;

/// <summary>
/// Description:
///     Frame entry, save gen world managers and init it.
/// </summary>
public sealed class Entry : MonoBehaviour
{

    Administrators _admin;

    private void Awake() => OnInit();

    private void FixedUpdate() => _admin?.Update();

    private void OnDestroy() => _admin?.Shut();

    private void OnInit()
    {
        _admin = Administrators.Instance;

        // Action assit gen world mgr and register to administrators.
        var act_GenWorldMgr = new Action<string, World>(
           (string type, World w) =>
           {
               _admin.Register(type, w).OnFinsihed(() => w.Start());
           });
        GenWorldMgr(act_GenWorldMgr);

      //  Enter();
    }

    private void GenWorldMgr(Action<string, World> act_GenWorldMgr)
    {
        act_GenWorldMgr(typeof(Listeners).Name, new Listeners());
        act_GenWorldMgr(typeof(Helpers).Name, new Helpers());
        act_GenWorldMgr(typeof(Players).Name, new Players());
        act_GenWorldMgr(typeof(Scenes).Name, new Scenes());

        DontDestroyOnLoad(this);
    }


    private void Enter() => _admin.Mgr<Scenes>().Load(1);

}



