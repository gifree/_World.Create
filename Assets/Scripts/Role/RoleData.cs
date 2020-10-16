using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责角色的信息更细
/// 并通过注册代理任务实现UI数据的更新
/// </summary>
public class RoleData : RoleObserver, IWhileChangeCharacterInfoValue
{
    // 将UI更新方法注册到代理任务中
    private Action<CharacterInfo> OnRoleDataValueChange;
    // 角色信息
    private CharacterInfo _characterInfo;

    protected override void OnInit()
    {
        base.OnInit();
        Register(this);
        Register(this as IEventReceiver);
        OnRoleDataValueChange += EmptyFunc;

        BuffMgr.Instance.RegisterAutoUpdTask(this);

        _characterInfo = new CharacterInfo();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var info = new CharacterInfo();
            UpdAtkBasic(-10);
          //  UpdCharacterInfo(info);
           // Debug.Log(_characterInfo.Atk);
        }
    }

    /// <summary>
    /// 更新角色信息
    /// </summary>
    /// <param name="infoChange">变化的数值</param>
   // public void UpdCharacterInfo(CharacterInfo infoChange) => OnRoleDataValueChange(_characterInfo += infoChange);

    /// <summary>
    /// 注册外部代理任务，更新UI
    /// </summary>
    /// <param name="task">任务</param>
    /// <param name="immediately">立即更新任务，默认true</param>
    public void RegisterTask(Action<CharacterInfo> task, bool immediately = true)
    {
        // 添加代理任务
        OnRoleDataValueChange += task;
        // 立即更新任务内容
        if (immediately)
            task(_characterInfo);
    }
    /// <summary>
    /// 反注册外部代理任务，当外部实例被销毁时调用
    /// </summary>
    /// <param name="task">任务</param>
    public void UnRegisterTask(Action<CharacterInfo> task) => OnRoleDataValueChange -= task;
    /// <summary>
    /// 空任务
    /// </summary>
    /// <param name="info"></param>
    private void EmptyFunc(CharacterInfo info) { }

    public void UpdTask() => OnRoleDataValueChange(_characterInfo);

    public override void OnEventReceiver(IEventNode @event)
    {
        
    }

    private void Start()
    {
        _characterInfo.RegisterTask(new WhileUpdCharacterInfoValue(this));

        var b1 = new BuffInfo("1", 10, true, BuffType.Atk);
        var b2 = new BuffInfo("1", .2f, false, BuffType.Atk);

        BuffMgr.Instance.AddBuff(b1);
        BuffMgr.Instance.AddBuff(b2);

        UpdAtkBasic(100);

        b1.Start = true;
        b2.Start = true;

       

        b1.Over = true;
        b2.Over = true;
        //UpdStrengthBasic(100);
        //UpdPowerBasic(100);
    }


    #region UPD
    // basic 是基本数据，非basic是依据basic计算得到的，结果为 basic+各种附加数值

    public void UpdAtkBasic(float atk) => _characterInfo.AtkBasic += atk;
    public void UpdAtk()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新ATK");
        // 获取固定数值
        var atk = _characterInfo.Strength + _characterInfo.Power + _characterInfo.AtkBasic;
        // 获取buff列表
        BuffMgr.Instance.BuffList(BuffType.Atk, out var buffList);
        // 计算倍率
        float rate = 0f;
        // 一次循环，固定数值直接加，倍率数值最后再乘
        foreach(var buff in buffList)
        {
            if (!buff.Enable) continue;
            if (buff.FixNumber)
                atk += buff.Value;
            else
                rate += buff.Value;
        }
        // 乘于倍率
        atk *= rate == 0 ? 1 : (1 + rate);
        // 赋值结果
        _characterInfo.Atk = atk;
        Debug.Log($"ATK = {_characterInfo.Atk}");
    }
    public void UpdDefBasic(float def) => _characterInfo.DefBasic += def;
    public void UpdDef()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新DEF");
    }
    public void UpdPowerBasic(float power) => _characterInfo.PowerBasic += power;
    public void UpdPower()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新POWER");
        _characterInfo.Power = _characterInfo.PowerBasic;
        Debug.Log($"Power = {_characterInfo.Power}");
    }
    public void UpdStrengthBasic(float basic) => _characterInfo.StrengthBasic += basic;
    public void UpdStrength()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新Strength");
        _characterInfo.Strength = _characterInfo.StrengthBasic;
        Debug.Log($"Strength = {_characterInfo.Strength}");
    }
    public void UpdSpiritBasic(float spirit) => _characterInfo.SpiritBasic += spirit;
    public void UpdSpirit()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新Spirit");
    }
    public void UpdIntelligenceBasic(float intelligence) => _characterInfo.IntelligenceBasic += intelligence;
    public void UpdIntelligence()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新Intelligence");
    }
    public void UpdAtkPhysicsBasic(float atkPhysics) => _characterInfo.AtkPhysicsBasic += atkPhysics;
    public void UpdAtkPhysics()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新AtkPhysics");
    }
    public void UpdAtkMagicBasic(float atkMagic) => _characterInfo.AtkPhysicsBasic += atkMagic;
    public void UpdAtkMagic()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新AtkPhysics");
    }
    public void UpdDefPhysicsBasic(float defPhysics) => _characterInfo.DefPhysicsBasic += defPhysics;
    public void UpdDefPhysics()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新DefPhysics");
    }
    public void UpdDefMagicBasic(float defMagic) => _characterInfo.DefMagicBasic += defMagic;
    public void UpdDefMagic()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新DefMagic");
    }
    public void UpdAtkNatureBasic(float atkNature) => _characterInfo.AtkNatureBasic += atkNature;
    public void UpdAtkNature()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新AtkNature");
    }
    public void UpdAtkFairyBasic(float atkFairy) => _characterInfo.AtkFairyBasic += atkFairy;
    public void UpdAtkFairy()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新AtkFairy");
    }
    public void UpdAtkElementBasic(float atkElement) => _characterInfo.AtkElementBasic += atkElement;
    public void UpdAtkElement()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新AtkElement");
    }
    public void UpdDefNatureBasic(float defNature) => _characterInfo.DefNatureBasic += defNature;
    public void UpdDefNature()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新DefNature");
    }
    public void UpdDefFairyBasic(float defFairy) => _characterInfo.DefFairyBasic += defFairy;
    public void UpdDefFairy()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新DefFairy");
    }
    public void UpdDefElementBasic(float defElement) => _characterInfo.DefElementBasic += defElement;
    public void UpdDefElement()
    {
        // 从附加数值管理器中取得附加列表，依次计算得到新的结果
        Debug.Log("更新DefElement");
    }
    #endregion
}


/// <summary>
/// 这个接口实现，由数值改变时属性器内部触发，在这里实现映射关系调用，低级影响高级数据，因为附加数值由附加管理器管理
/// 所以这里只需要处理映射即可，具体数值改变交给别的管理器来管理
/// </summary>
public class WhileUpdCharacterInfoValue : IWhileUpdCharacterInfoValue
{
    private readonly RoleData _roleDat;

    public WhileUpdCharacterInfoValue(RoleData data) => _roleDat = data;

    public void UpdAtk(CharacterInfo info)
    {

    }
    public void UpdAtkBasic(CharacterInfo info)
    { 
        _roleDat.UpdAtk();
    }

    public void UpdAtkElement(CharacterInfo info)
    {

    }
    public void UpdAtkElementBasic(CharacterInfo info)
    {
        UpdAtkElement(info);
    }

    public void UpdAtkFairy(CharacterInfo info)
    {

    }
    public void UpdAtkFairyBasic(CharacterInfo info)
    {
        UpdAtkFairy(info);
    }

    public void UpdAtkMagic(CharacterInfo info)
    {

    }
    public void UpdAtkMagicBasic(CharacterInfo info) { }

    public void UpdAtkNature(CharacterInfo info)
    {

    }
    public void UpdAtkNatureBasic(CharacterInfo info) { }

    public void UpdAtkPhysics(CharacterInfo info)
    {

    }
    public void UpdAtkPhysicsBasic(CharacterInfo info) { }

    public void UpdDef(CharacterInfo info) 
    {
    
    }
    public void UpdDefBasic(CharacterInfo info) { }

    public void UpdDefElement(CharacterInfo info)
    {

    }
    public void UpdDefElementBasic(CharacterInfo info) { }

    public void UpdDefFairy(CharacterInfo info)
    {

    }
    public void UpdDefFairyBasic(CharacterInfo info) { }

    public void UpdDefMagic(CharacterInfo info)
    {

    }
    public void UpdDefMagicBasic(CharacterInfo info) { }

    public void UpdDefNature(CharacterInfo info)
    {

    }
    public void UpdDefNatureBasic(CharacterInfo info) { }

    public void UpdDefPhysics(CharacterInfo info) 
    {
    }
    public void UpdDefPhysicsBasic(CharacterInfo info)
    {

    }

    public void UpdIntelligence(CharacterInfo info)
    {

    }
    public void UpdIntelligenceBasic(CharacterInfo info) { }

    public void UpdPower(CharacterInfo info)
    {
    }
    public void UpdPowerBasic(CharacterInfo info)
    {
        _roleDat.UpdPower();
        _roleDat.UpdAtk();
    }

    public void UpdSpirit(CharacterInfo info)
    {

    }
    public void UpdSpiritBasic(CharacterInfo info) { }

    public void UpdStrength(CharacterInfo info)
    {

    }
    public void UpdStrengthBasic(CharacterInfo info)
    {
        _roleDat.UpdStrength();
        _roleDat.UpdAtk();
    }
}
