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
public class MutiRoleObserver : MonoBehaviour, IEventReceiver, IEventDispatcher
{
    private IMutiRoleMgr _mutiRoleMgr;
    private IMutiRoleEventReceiver _mutiRoleEventReceiver;

    public RoleNumber RoleNumber = RoleNumber.One;

    /*init opertion.*/

    protected virtual void Awake() => OnInit();
    /// <summary>
    /// 初始化，派生类如果有初始化操作请重写它，此方法会在父类AWAKE中自动唤醒
    /// </summary>
    protected virtual void OnInit()
    {
        _mutiRoleMgr = _mutiRoleMgr ?? (_mutiRoleMgr = MutiRoleMgr.Instance);
        _mutiRoleEventReceiver = _mutiRoleEventReceiver ?? (_mutiRoleEventReceiver = MutiRoleMgr.Instance);
    }

    /*sub class register or unregister instance entrance.*/

    /// <summary>
    /// 负责模块的注册
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    /// <param name="t">模块实例</param>
    /// <param name="replace"></param>
    protected virtual void Register<T>(T t, bool replace = false) where T : MutiRoleObserver => _mutiRoleMgr?.Register(RoleNumber, t, replace);
    /// <summary>
    /// 负责模块的反注册
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    protected virtual void UnRegister<T>() => _mutiRoleMgr.UnRegister<T>(RoleNumber);
    /// <summary>
    /// 负责模块的GET
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    /// <returns></returns>
    protected virtual T Mgr<T>() where T : MutiRoleObserver => _mutiRoleMgr.Mgr<T>(RoleNumber);

    /*sub class register or unregister event callback entrance.*/

    /// <summary>
    /// 负责事件接收模块的注册
    /// </summary>
    /// <param name="type">模块类型</param>
    /// <param name="receiver">模块接口实例</param>
    protected virtual void Register(RoleEventNodeType type, IEventReceiver receiver) => _mutiRoleEventReceiver?.Register(type, RoleNumber, receiver);
    /// <summary>
    /// 负责事件接收模块的反注册
    /// </summary>
    /// <param name="type">模块类型</param>
    protected virtual void UnRegister(RoleEventNodeType type) => _mutiRoleEventReceiver?.UnRegister(type, RoleNumber);

    public virtual void OnEventReceiver(IEventNode @event) { }
    public virtual void OnEventDispatch(IEventNode @event) { }

}

// near feeling.

/*
 * with shadow, no same。
 * is expectation, no belong。
 */

/*
 * 身处高楼广厦，常思山泽鱼鸟
 */
