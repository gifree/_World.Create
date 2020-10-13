using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有buff的基类，只能处理单一数值的变化，
/// 如果有特殊变化请继承它，重新设计逻辑
/// 如果想设计出时间与buff的函数关系需要另外设计，暂不考虑
/// </summary>
public class BuffBase
{
    #region 基本属性
    private string _name;
    private float _value;
    private bool _fixNumber;
    private BuffType _buffType;
    private List<BuffBase> _belongs; 
    #endregion
    public BuffBase() { }
    public BuffBase(string name, float value, bool fixNumber, BuffType bufferType)
    {
        Name = name;
        Value = value;
        FixNumber = fixNumber;
        BuffType = bufferType;
    }

    #region 属性
    public string Name { get => _name; set => _name = value; } // 名称，可有可无
    public float Value { get => _value; set => _value = value; } // 数值
    public bool FixNumber { get => _fixNumber; set => _fixNumber = value; } // 固定数值类型，还是百分比类型
    public BuffType BuffType { get => _buffType; set => _buffType = value; } // 影响到的类型
    public List<BuffBase> Belongs { get => _belongs; set => _belongs = value; }// 记录自己所属的队列，用来脱离组织
    #endregion

    /// <summary>
    /// buff激活时触发
    /// </summary>
    public virtual void LifeSatrt()
    {
       // BuffMgr.Instance.StartAutoUpdTask(this);
    }
    /// <summary>
    /// buff持续时触发
    /// </summary>
    public virtual void LifeDuring()
    {

    }
    /// <summary>
    /// buff结束时触发
    /// </summary>
    public virtual void LifeOver()
    {
        Belongs?.Remove(this);
        BuffMgr.Instance.StartAutoUpdTask(this);
    }
}




