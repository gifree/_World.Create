using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepAndShallowCopy : MonoBehaviour
{
    /// <summary>
    /// 却别在于非值类型的处理，浅拷贝拷贝引用指针，深拷贝拷贝内容
    /// </summary>

    private void Start()
    {
        A a = new A();
        A b = (A)a.ShallowCopy();

        a.ToString();
        b.ToString();

        a.Int = 2;
        a.Str = "change";
        a.B.Str = "chenge_sub";

        a.ToString();
        b.ToString();

        Debug.Log("-------------DEEP-----------------");

        a = new A();
        b = (A)a.DeepCopy();
        a.Int = 2;
        a.Str = "change";
        a.B.Str = "chenge_sub";

        a.ToString();
        b.ToString();
    }
}
public class A
{
    public int Int = 1;
    public string Str = "org";
    public B B;

    public A()
    {
        B = new B();
    }

    public object DeepCopy()
    {
        A s = new A();
        s.Int = this.Int;
        s.Str = this.Str;
        s.B = (B)this.B.DeepCopy();
        return s;
    }
    public object ShallowCopy()
    {
        return this.MemberwiseClone();
    }

    public override string ToString()
    {
        Debug.Log($"{Int} {Str} {B.Str}");
        return default;
    }
}

public class B
{
    public string Str = "org_sub";

    public object DeepCopy()
    {
        B s = new B();
        return s;
    }
}
