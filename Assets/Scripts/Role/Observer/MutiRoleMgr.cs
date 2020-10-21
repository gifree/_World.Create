using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


/// <summary>
/// ROLE管理器，负责协调ROLE各个模块之间的相互索引，同时也为外部访问提供入口，降低粘性耦合
/// 通过IMutiEventReceiver接口实现外部响应
/// 通过IMutiEventDispatcher接口实现内部分派
/// 通过IMutiRoleMgr实现内部模块向管理器注册、反注册、获取对象实例
/// 通过IMutiRoleEventReceiver实现内部模块向管理器注册、反注册事件接口实例
/// </summary>
public class MutiRoleMgr : Singleton<MutiRoleMgr>, IMutiRoleMgr, IMutiEventDispatcher, IMutiEventReceiver, IMutiRoleEventReceiver
{

    private Dictionary<RoleNumber, Dictionary<string, MutiRoleObserver>> _observers;
    private Dictionary<RoleNumber, Dictionary<RoleEventNodeType, IEventReceiver>> _receivers;

    /*init opertion.*/

    public MutiRoleMgr()
    {
        _observers = new Dictionary<RoleNumber, Dictionary<string, MutiRoleObserver>>();
        _receivers = new Dictionary<RoleNumber, Dictionary<RoleEventNodeType, IEventReceiver>>();
    }
    ~MutiRoleMgr()
    {
        _observers.Clear();
        _observers = null;
        _receivers.Clear();
        _receivers = null;
    }

    /*sub class register or unregister instance entrance.*/

    /// <summary>
    /// 负责模块的注册
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    /// <param name="t">模块实例</param>
    /// <param name="replace"></param>
    public void Register<T>(RoleNumber number, T t, bool replace = false) where T : class
    {
        try
        {
            var key = typeof(T).Name;
            if (t == null) return;

            _observers.TryGetValue(number, out var roles);
            roles = roles ?? (_observers[number] = new Dictionary<string, MutiRoleObserver>());
            if (roles.ContainsKey(key) && replace)
            {
                roles[key] = t as MutiRoleObserver;
                return;
            }
            roles.Add(key, t as MutiRoleObserver);
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }
    /// <summary>
    /// 负责模块的反注册
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    public void UnRegister<T>(RoleNumber number)
    {
        try
        {
            var key = typeof(T).Name;
            if (_observers.TryGetValue(number, out var roles))
                if ((bool)roles?.ContainsKey(key))
                    roles.Remove(key);
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }
    /// <summary>
    /// 负责模块的GET
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    /// <returns></returns>
    public T Mgr<T>(RoleNumber number) where T : class
    {

        MutiRoleObserver observer = default;
        try
        {
            if (_observers.TryGetValue(number, out var roles))
                roles?.TryGetValue(typeof(T).Name, out observer);
        }
        catch (Exception e) { Debug.Log(e.Message); }
        return observer as T;
    }

    /*event dispatch and receiver entrance.*/

    /// <summary>
    /// 负责分派内容到具体事件
    /// </summary>
    /// <param name="node">事件节点</param>
    public void OnEventDispatch(RoleNumber number, IEventNode @event)
    {
        try
        {
            if (_receivers.TryGetValue(number, out var receivers))
            {
                IEventReceiver receiver = default;
                if ((bool)receivers?.TryGetValue((@event as IRoleEventNode).NodeType, out receiver))
                    receiver?.OnEventReceiver(@event);
            }
        }
        catch(Exception e) { Debug.Log(e.Message); }
    }
    /// <summary>
    /// 负责接收外部传来的事件内容
    /// </summary>
    /// <param name="node">事件节点</param>
    public void OnEventReceiver(RoleNumber number, IEventNode @event)
    {
        OnEventDispatch(number, @event);
    }

    /*sub class register or unregister event callback entrance.*/

    /// <summary>
    /// 负责事件接收模块的注册
    /// </summary>
    /// <param name="type">模块类型</param>
    /// <param name="receiver">模块接口实例</param>
    public void Register(RoleEventNodeType type, RoleNumber number, IEventReceiver receiver)
    {
        try
        {
            _receivers.TryGetValue(number, out var receivers);
            receivers = receivers ?? (_receivers[number] = new Dictionary<RoleEventNodeType, IEventReceiver>());
            if (!receivers.ContainsKey(type))
                receivers.Add(type, receiver);
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }
    /// <summary>
    /// 负责事件接收模块的反注册
    /// </summary>
    /// <param name="type">模块类型</param>
    public void UnRegister(RoleEventNodeType type, RoleNumber number)
    {
        try
        {
            if (_receivers.TryGetValue(number, out var receivers))
                if (receivers != null && receivers.ContainsKey(type))
                    receivers.Remove(type);
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }

}
