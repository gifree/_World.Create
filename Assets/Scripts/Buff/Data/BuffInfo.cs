using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有buff的基类，只能处理单一数值的变化，
/// 如果有特殊变化请继承它，重写逻辑
/// 如果想设计出时间与buff的函数关系需要考虑重写During方法，并且将实例托管
/// </summary>
public class BuffInfo
{
    #region 基本属性
    private string _name;
    private float _value;
    private bool _fixNumber;
    private bool _enable;
    private bool _deBuff;
    private BuffType _buffType;
    #endregion
    public BuffInfo() { }
    public BuffInfo(string name, float value, bool fixNumber, BuffType bufferType, bool deBuff = false)
    {
        Name = name;
        Value = value;
        FixNumber = fixNumber;
        BuffType = bufferType;
        DeBuff = deBuff;
    }

    #region 属性
    public string Name { get => _name; set => _name = value; } // 名称，可有可无
    public float Value { get => _value; set => _value = value; } // 数值
    public bool FixNumber { get => _fixNumber; set => _fixNumber = value; } // 固定数值类型，还是百分比类型
    public bool Enable { get => _enable; set => _enable = value; } // 启用状态
    public BuffType BuffType { get => _buffType; set => _buffType = value; } // 影响到的类型
    public bool DeBuff { get => _deBuff; set => _deBuff = value; } // 是否是DEBUFF

    #endregion

    /// <summary>
    /// buff激活时触发
    /// </summary>
    public bool Start
    {
        set
        {
            if (value && !Enable)
            {
                Enable = value;
                BuffMgr.Instance.StartAutoUpdTask(this);
            }
        }
    }

    /// <summary>
    /// buff结束时触发
    /// </summary>
    public bool Over
    {
        set
        {
            if (value && Enable)
            {
                Enable = false;
                BuffMgr.Instance.RemoveBuff(this);
                BuffMgr.Instance.StartAutoUpdTask(this);
            }
        }
    }
}


