using System;
using System.Collections.Generic;
/// <summary>
/// 负责处理BUFF的外部引用，
/// 与buff生命周期内相关数值的动态管理
/// </summary>
public class BuffMgr : Singleton<BuffMgr>
{
    private Dictionary<BuffType, List<BuffInfo>> _buffDict;
    private Dictionary<BuffType, Action> _updDict;
    private IWhileChangeCharacterInfoValue _iWhileChangeValue;

    public BuffMgr()
    {
        _buffDict = new Dictionary<BuffType, List<BuffInfo>>();
        _updDict = new Dictionary<BuffType, Action>();
    }

    /// <summary>
    /// 注册外部数值更新函数，以便BUFF发生改变时触发
    /// </summary>
    /// <param name="iChange"></param>
    public void RegisterAutoUpdTask(IWhileChangeCharacterInfoValue iChange)
    {
        _iWhileChangeValue = iChange;
        _updDict.Add(BuffType.Atk, _iWhileChangeValue.UpdAtk);
    }
    /// <summary>
    /// buff发生改变时来触发
    /// </summary>
    /// <param name="buff">buff</param>
    public void StartAutoUpdTask(BuffInfo buff)
    {
        if (buff != null && _updDict.TryGetValue(buff.BuffType, out
             var action))
        {
            buff = null;
            action?.Invoke();
        }
    }

    /// <summary>
    /// buff的外部添加函数
    /// </summary>
    /// <param name="buff"></param>
    public void AddBuff(BuffInfo buff)
    {
        if (buff == null) return;
        _buffDict.TryGetValue(buff.BuffType, out var lst);
        lst = lst ?? (_buffDict[buff.BuffType] = new List<BuffInfo>());
        lst.Add(buff);
    }

    /// <summary>
    /// buff的外部移除函数
    /// </summary>
    /// <param name="buff"></param>
    public void RemoveBuff(BuffInfo buff)
    {
        if (buff == null) return;
        _buffDict.TryGetValue(buff.BuffType, out var lst);
        if (lst != null && lst.Contains(buff))
            lst.Remove(buff);
    }

    /// <summary>
    /// 对外返回当前存在的buff列表，根据类型
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="lst">buff列表</param>
    public void BuffList(BuffType type, out List<BuffInfo> lst) => _buffDict.TryGetValue(type, out lst);

}
