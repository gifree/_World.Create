using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


/// <summary>
/// ROLE管理器，负责协调ROLE各个模块之间的相互索引，同时也为外部访问提供入口，降低粘性耦合
/// 通过IEventReceiver接口实现外部响应
/// 通过IEventDispatcher接口实现内部分派
/// 通过IRoleMgr实现内部模块向管理器注册、反注册、获取对象实例
/// 通过IRoleEventReceiver实现内部模块向管理器注册、反注册事件接口实例
/// </summary>
public class RoleMgr : Singleton<RoleMgr>, IRoleMgr, IEventDispatcher, IEventReceiver, IRoleEventReceiver
{

    private Dictionary<string, RoleObserver> _observers;
    private Dictionary<RoleEventNodeType, IEventReceiver> _receivers;

    /*init opertion.*/

    public RoleMgr()
    {
        _observers = new Dictionary<string, RoleObserver>();
        _receivers = new Dictionary<RoleEventNodeType, IEventReceiver>();
    }
    ~RoleMgr()
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
    public void Register<T>(T t, bool replace = false) where T : RoleObserver
    {
        try
        {
            var key = typeof(T).Name;
            if ((bool)_observers?.ContainsKey(key))
            {
                if (replace)
                    _observers[key] = t;
                return;
            }
            _observers?.Add(key, t);
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }
    /// <summary>
    /// 负责模块的反注册
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    public void UnRegister<T>()
    {
        try
        {
            var key = typeof(T).Name;
            if ((bool)_observers?.ContainsKey(key))
                _observers.Remove(key);
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }
    /// <summary>
    /// 负责模块的GET
    /// </summary>
    /// <typeparam name="T">模块类型</typeparam>
    /// <returns></returns>
    public T Mgr<T>() where T : RoleObserver
    {
        RoleObserver observer = default;
        try
        {
            _observers?.TryGetValue(typeof(T).Name, out observer);
        }
        catch (Exception e) { Debug.Log(e.Message); }
        return observer as T;
    }

    /*event dispatch and receiver entrance.*/

    /// <summary>
    /// 负责分派内容到具体事件
    /// </summary>
    /// <param name="node">事件节点</param>
    public void OnEventDispatch(EventNode node)
    {
        try
        {
            if (_receivers.TryGetValue((node as RoleEventNode).NodeType, out var receiver))
                receiver?.OnEventReceiver(node);
        }
        catch { Debug.Log($"{node} as {typeof(RoleEventNode).Name} equals null, check it type!"); }
    }
    /// <summary>
    /// 负责接收外部传来的事件内容
    /// </summary>
    /// <param name="node">事件节点</param>
    public void OnEventReceiver(EventNode node)
    {
        OnEventDispatch(node);
    }

    /*sub class register or unregister event callback entrance.*/

    /// <summary>
    /// 负责事件接收模块的注册
    /// </summary>
    /// <param name="type">模块类型</param>
    /// <param name="receiver">模块接口实例</param>
    public void Register(RoleEventNodeType type, IEventReceiver receiver)
    {
        try
        {
            if (_receivers != null && !_receivers.ContainsKey(type) && receiver != null)
                _receivers.Add(type, receiver);
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }
    /// <summary>
    /// 负责事件接收模块的反注册
    /// </summary>
    /// <param name="type">模块类型</param>
    public void UnRegister(RoleEventNodeType type)
    {
        try
        {
            if (_receivers != null && _receivers.ContainsKey(type))
                _receivers.Remove(type);
        }
        catch (Exception e) { Debug.Log(e.Message); }
    }
}



