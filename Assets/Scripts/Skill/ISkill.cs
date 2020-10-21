using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public int From;
    public int To;
}

public interface ISkillTrigger
{
    void Init(string args);
    void Start();
    void Update(SkillInfo info);
    void Reset();
    
    ISkillTrigger Clone();

    bool Enable { get; }
    bool Executing { get; }
    float StartTime { get; }
    string TypeName { get; }
}

public abstract class SkillTrigger : ISkillTrigger
{
    private bool _enable = false;
    private float _start = 0;
    private string _typeName;
    protected bool executing = false;
    public float StartTime => _start;
    public string TypeName => _typeName;

    public abstract void Init(string args);
    public virtual void Start() => _enable = true;
    public abstract void Update(SkillInfo info);
    public virtual void Reset() => _enable = false;
    public abstract ISkillTrigger Clone();

    public bool Enable => _enable;
    public bool Executing => executing;
}




public interface ISkillEventReceiver
{
    void Register(string key, Action act);
    void Register<Tpr1>(string key, Action<Tpr1> act);
    void Register<Tpr1, Tpr2>(string key, Action<Tpr1, Tpr2> act);
    void Register<Tpr1, Tpr2, Tpr3>(string key, Action<Tpr1, Tpr2, Tpr3> act);
    void Register<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Action<Tpr1, Tpr2, Tpr3, Tpr4> act);
    void UnRegister(string key, Action act);
    void UnRegister<Tpr1>(string key, Action<Tpr1> act);
    void UnRegister<Tpr1, Tpr2>(string key, Action<Tpr1, Tpr2> act);
    void UnRegister<Tpr1, Tpr2, Tpr3>(string key, Action<Tpr1, Tpr2, Tpr3> act);
    void UnRegister<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Action<Tpr1, Tpr2, Tpr3, Tpr4> act);
    void Trigger(string key);
    void Trigger<Tpr1>(string key, Tpr1 tpr1);
    void Trigger<Tpr1, Tpr2>(string key, Tpr1 tpr1, Tpr2 tpr2);
    void Trigger<Tpr1, Tpr2, Tpr3>(string key, Tpr1 tpr1, Tpr2 tpr2, Tpr3 tpr3);
    void Trigger<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Tpr1 tpr1, Tpr2 tpr2, Tpr3 tpr3, Tpr4 tpr4);
    void Clear();
}

public class SkillEventDispatcher : ISkillEventReceiver
{
    private ISkillEventReceiver _receiver;
    public SkillEventDispatcher(ISkillEventReceiver receiver) => _receiver = receiver;

    public void Register(string key, Action act) => _receiver.Register(key, act);
    public void Register<Tpr1>(string key, Action<Tpr1> act) => _receiver.Register(key, act);
    public void Register<Tpr1, Tpr2>(string key, Action<Tpr1, Tpr2> act) => _receiver.Register(key, act);
    public void Register<Tpr1, Tpr2, Tpr3>(string key, Action<Tpr1, Tpr2, Tpr3> act) => _receiver.Register(key, act);
    public void Register<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Action<Tpr1, Tpr2, Tpr3, Tpr4> act) => _receiver.Register(key, act);
    public void Trigger(string key) => _receiver.Trigger(key);
    public void Trigger<Tpr1>(string key, Tpr1 tpr1) => _receiver.Trigger(key, tpr1);
    public void Trigger<Tpr1, Tpr2>(string key, Tpr1 tpr1, Tpr2 tpr2) => _receiver.Trigger(key, tpr1, tpr2);
    public void Trigger<Tpr1, Tpr2, Tpr3>(string key, Tpr1 tpr1, Tpr2 tpr2, Tpr3 tpr3) => _receiver.Trigger(key, tpr1, tpr2, tpr3);
    public void Trigger<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Tpr1 tpr1, Tpr2 tpr2, Tpr3 tpr3, Tpr4 tpr4) => _receiver.Trigger(key, tpr1, tpr2, tpr3, tpr4);
    public void UnRegister(string key, Action act) => _receiver.UnRegister(key, act);
    public void UnRegister<Tpr1>(string key, Action<Tpr1> act) => _receiver.UnRegister(key, act);
    public void UnRegister<Tpr1, Tpr2>(string key, Action<Tpr1, Tpr2> act) => _receiver.UnRegister(key, act);
    public void UnRegister<Tpr1, Tpr2, Tpr3>(string key, Action<Tpr1, Tpr2, Tpr3> act) => _receiver.UnRegister(key, act);
    public void UnRegister<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Action<Tpr1, Tpr2, Tpr3, Tpr4> act) => _receiver.UnRegister(key, act);

    public void Clear() => _receiver.Clear();
}

public class SkillEventReceiver : ISkillEventReceiver
{
    /// <summary>
    /// 代理任务集合
    /// </summary>
    private Dictionary<string, Delegate> _delegates;
    public SkillEventReceiver() => _delegates = new Dictionary<string, Delegate>();

    /// <summary>
    /// 注册前的安全校验
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    private void BeforeRegister(string key, Delegate value)
    {
        _delegates.TryGetValue(key, out var @delegate);
        @delegate = @delegate ?? (_delegates[key] = null);

        if (@delegate != null && @delegate.GetType() != value.GetType())
            throw new Exception($"Try to add not correct event {key}. Current key is {@delegate.GetType().Name}, adding key is {value.GetType().Name}.");
    }
    /// <summary>
    /// 反注册前的安全校验
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private bool BeforeUnRegister(string key, Delegate value)
    {
        bool result;
        if (!_delegates.ContainsKey(key))
            result = false;
        else
        {
            var @delegate = _delegates[key];
            if (@delegate != null && @delegate.GetType() != value.GetType())
                throw new Exception($"Remove listener {key} failed, Current key is { @delegate.GetType() }, adding key is { value.GetType() }.");
            result = true;

        }

        return result;
    }
    /// <summary>
    /// 执行注册函数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="delegate"></param>
    private void Add(string key, Delegate @delegate)
    {
        BeforeRegister(key, @delegate);
        _delegates[key] = Delegate.Combine(_delegates[key], @delegate);
    }
    /// <summary>
    /// 执行反注册函数
    /// </summary>
    /// <param name="key"></param>
    /// <param name="delegate"></param>
    private void Remove(string key, Delegate @delegate)
    {
        BeforeUnRegister(key, @delegate);
        _delegates[key] = Delegate.Remove(_delegates[key], @delegate);
    }

    public void Register(string key, Action act) => Add(key, act);
    public void Register<Tpr1>(string key, Action<Tpr1> act) => Add(key, act);
    public void Register<Tpr1, Tpr2>(string key, Action<Tpr1, Tpr2> act) => Add(key, act);
    public void Register<Tpr1, Tpr2, Tpr3>(string key, Action<Tpr1, Tpr2, Tpr3> act) => Add(key, act);
    public void Register<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Action<Tpr1, Tpr2, Tpr3, Tpr4> act) => Add(key, act);
    public void UnRegister(string key, Action act) => Remove(key, act);
    public void UnRegister<Tpr1>(string key, Action<Tpr1> act) => Remove(key, act);
    public void UnRegister<Tpr1, Tpr2>(string key, Action<Tpr1, Tpr2> act) => Remove(key, act);
    public void UnRegister<Tpr1, Tpr2, Tpr3>(string key, Action<Tpr1, Tpr2, Tpr3> act) => Remove(key, act);
    public void UnRegister<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Action<Tpr1, Tpr2, Tpr3, Tpr4> act) => Remove(key, act);

    public void Trigger(string key)
    {
        if(_delegates.TryGetValue(key, out var @delegate))
        {
            if (@delegate == null)
            {
                Debug.Log($"TriggerEvent {key} error: delegate null.");
                return;
            }
            var invokes = @delegate.GetInvocationList();

            for (var i = 0; i < invokes.Length; i++)
            {
                var action = invokes[i] as Action;
                if (action == null)
                {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", key));
                    continue;
                }
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex, null);
                }
            }
        }
    }
    public void Trigger<Tpr1>(string key, Tpr1 tpr1)
    {
        if (_delegates.TryGetValue(key, out var @delegate))
        {
            if (@delegate == null)
            {
                Debug.Log($"TriggerEvent {key} error: delegate null.");
                return;
            }
            var invokes = @delegate?.GetInvocationList();
            for (var i = 0; i < invokes.Length; i++)
            {
                var action = invokes[i] as Action<Tpr1>;
                if (action == null)
                {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", key));
                    continue;
                }
                try
                {
                    action(tpr1);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex, null);
                }
            }
        }
    }
    public void Trigger<Tpr1, Tpr2>(string key, Tpr1 tpr1, Tpr2 tpr2)
    {
        if (_delegates.TryGetValue(key, out var @delegate))
        {
            if (@delegate == null)
            {
                Debug.Log($"TriggerEvent {key} error: delegate null.");
                return;
            }
            var invokes = @delegate?.GetInvocationList();
            for (var i = 0; i < invokes.Length; i++)
            {
                var action = invokes[i] as Action<Tpr1, Tpr2>;
                if (action == null)
                {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", key));
                    continue;
                }
                try
                {
                    action(tpr1, tpr2);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex, null);
                }
            }
        }
    }
    public void Trigger<Tpr1, Tpr2, Tpr3>(string key, Tpr1 tpr1, Tpr2 tpr2, Tpr3 tpr3)
    {
        if (_delegates.TryGetValue(key, out var @delegate))
        {
            if (@delegate == null)
            {
                Debug.Log($"TriggerEvent {key} error: delegate null.");
                return;
            }
            var invokes = @delegate?.GetInvocationList();
            for (var i = 0; i < invokes.Length; i++)
            {
                var action = invokes[i] as Action<Tpr1, Tpr2, Tpr3>;
                if (action == null)
                {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", key));
                    continue;
                }
                try
                {
                    action(tpr1, tpr2, tpr3);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex, null);
                }
            }
        }
    }
    public void Trigger<Tpr1, Tpr2, Tpr3, Tpr4>(string key, Tpr1 tpr1, Tpr2 tpr2, Tpr3 tpr3, Tpr4 tpr4)
    {
        if (_delegates.TryGetValue(key, out var @delegate))
        {
            if (@delegate == null)
            {
                Debug.Log($"TriggerEvent {key} error: delegate null.");
                return;
            }
            var invokes = @delegate?.GetInvocationList();
            for (var i = 0; i < invokes.Length; i++)
            {
                var action = invokes[i] as Action<Tpr1, Tpr2, Tpr3, Tpr4>;
                if (action == null)
                {
                    Debug.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.", key));
                    continue;
                }
                try
                {
                    action(tpr1, tpr2, tpr3, tpr4);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex, null);
                }
            }
        }
    }

    public void Clear() => _delegates.Clear();
}



/*
 以花为貌,以鸟为声,以月为神,以柳为态,以玉为骨,以冰雪为肤,以秋水为姿,以诗词为心。吾无间然矣。《幽梦影》张潮。
 花样，鸟声，月神，柳态，玉骨，冰肤，秋姿，诗心。
 */
