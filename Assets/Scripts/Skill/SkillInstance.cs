using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _World.Tools;
using System;

public class SkillInstance
{
    public int Id = -1; // id
    public bool Using = false; // 使用状态，应用到对象池
    public string Name = "Default skill";
    private float _startTime = 0f; // 开始时间
    private int _counter = 0;// 触发器执行数量

    /// <summary>
    /// 触发器列表，来源是克隆和配置文件解析注入
    /// </summary>
    public readonly List<ISkillTrigger> Triggers = new List<ISkillTrigger>();

    /// <summary>
    /// 解析构造
    /// </summary>
    /// <param name="id"></param>
    public SkillInstance(int id) => Id = id;
    /// <summary>
    /// 克隆构造
    /// </summary>
    /// <param name="template"></param>
    public SkillInstance(SkillInstance @template)
    {
        Id = @template.Id;
        foreach (ISkillTrigger trigger in @template.Triggers)
            Triggers.Add(trigger.Clone());
    }

    /// <summary>
    /// 更新实例内的触发器列表
    /// </summary>
    /// <param name="time"></param>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool Update(float time, SkillInfo info)
    {
        var count = Triggers.Count;
        var elapsed = time - _startTime;
        for (int i = 0; i < count; i++)
        {
            var trigger = Triggers[i];
            if (elapsed >= trigger.StartTime && !trigger.Executing)
            {
                Triggers[i].Update(info);
                _counter++;
            }
        }
        return CheckEnd();
    }

    /// <summary>
    /// 表示这个触发器被重置，用于当技能实例使用完毕
    /// </summary>
    public void Reset()
    {
        foreach (var trigger in Triggers)
            trigger.Reset();
    }
    /// <summary>
    /// 获取这个类型的触发器数量
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public int GetTriggerCount(string typeName)
    {
        var count = 0;
        foreach (var trigger in Triggers)
            count += trigger.TypeName
                    == typeName ? 0 : 1;
        return count;
    }
    /// <summary>
    /// 释放开始
    /// </summary>
    /// <param name="time"></param>
    public void Begin(float time)
    {
        _startTime = time;
        Using = true;
        _counter = 0;
        Reset();
    }
    /// <summary>
    /// 释放完毕
    /// </summary>
    public void End()
    {
        Using = false;
        // 返回到对象池 TODO
    }
    /// <summary>
    /// 检查是否释放完毕，判别标准是触发器是否使用完毕
    /// </summary>
    /// <returns></returns>
    public bool CheckEnd()
    {
        var result = _counter == Triggers.Count;
        if(result) End();
        return result;
    }
}


/// <summary>
/// Des:
///     负责skillinstance的管理
/// </summary>
public class SkillSystem
{
    private Dictionary<int, Pools<SkillInstance>> _pools;
    private int _maxCout = 3;

    public SkillSystem() => _pools = new Dictionary<int, Pools<SkillInstance>>();

    public SkillInstance GetSkillInstance(int id)
    {
        _pools.TryGetValue(id, out var pool);
        if (pool == null) return default;
        var skill = pool.Pop();
        if(skill == null)
            skill = new SkillInstance(id);
        return skill;
    }

    public void RecycleToPool(SkillInstance skill)
    {
        AddToPool(skill);
    }

    public void AddToPool(SkillInstance skill)
    {
        try
        {
            if (skill != null)
            {
                _pools.TryGetValue(skill.Id, out var pool);
                pool = pool ?? (_pools[skill.Id] = PoolsMgr.GenPool<SkillInstance>(skill.Id, skill.Name, _maxCout));
                pool.Push(skill);
            }
        }
        catch(Exception e) { Debug.Log(e.Message); }
    }

    public void Reset()
    {
        _pools.Clear();
    }
}



