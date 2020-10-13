using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ROLE的观察者基类
/// 负责派生类的统一管理
/// 负责派生类的注册、反注册、获取
/// 负责派生类事件方法的注册、反注册
/// </summary>
public class RoleObserver : MonoBehaviour, IEventReceiver, IEventDispatcher
{
    private IRoleMgr _roleMgr;
    private IRoleEventReceiver _roleEventReceiver;

    protected RoleEventNode roleEventNode;

    public RoleEventNodeType EventType = RoleEventNodeType.Default;

    /*init opertion.*/

    protected virtual void Awake() => OnInit();
    /// <summary>
    /// 初始化，派生类如果有初始化操作请重写它，此方法会在父类AWAKE中自动唤醒
    /// </summary>
    protected virtual void OnInit() 
    {
         _roleMgr = _roleMgr ?? (_roleMgr = RoleMgr.Instance);
        _roleEventReceiver = _roleEventReceiver ?? (_roleEventReceiver = RoleMgr.Instance);
    }

    /*sub class register or unregister instance entrance.*/

    /// <summary>
    /// 负责模块的注册
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    /// <param name="t">模块实例</param>
    /// <param name="replace"></param>
    protected virtual void Register<T>(T t, bool replace = false) where T : RoleObserver => _roleMgr?.Register(t, replace);
    /// <summary>
    /// 负责模块的反注册
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    protected virtual void UnRegister<T>() => _roleMgr.UnRegister<T>();
    /// <summary>
    /// 负责模块的GET
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    /// <returns></returns>
    protected virtual T Mgr<T>() where T : RoleObserver => _roleMgr.Mgr<T>();

    /*sub class register or unregister event callback entrance.*/

    /// <summary>
    /// 负责事件接收模块的注册
    /// </summary>
    /// <param name="type">模块类型</param>
    /// <param name="receiver">模块接口实例</param>
    protected virtual void Register(IEventReceiver receiver) => _roleEventReceiver?.Register(EventType, receiver);
    /// <summary>
    /// 负责事件接收模块的反注册
    /// </summary>
    /// <param name="type">模块类型</param>
    protected virtual void UnRegister() => _roleEventReceiver?.UnRegister(EventType);

    public virtual void OnEventReceiver(EventNode node) { }
    public virtual void OnEventDispatch(EventNode node) { }
}

// near feeling.

/*
 * with shadow, no same。
 * is expectation, no belong。
 */

/*
 * 身处高楼广厦，常思山泽鱼鸟
 */