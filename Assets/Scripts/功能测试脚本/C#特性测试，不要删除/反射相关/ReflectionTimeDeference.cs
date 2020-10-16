using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

public class ReflectionTimeDeference : MonoBehaviour
{

    private void Awake()
    {
        
    }

    private void Start()
    {
        print(System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion());

        int count = 1000000;

        OrderInfo testObj = new OrderInfo();
        PropertyInfo propInfo = typeof(OrderInfo).GetProperty("OrderID");

        print("直接访问花费时间：       ");
        Stopwatch watch1 = Stopwatch.StartNew();

        for (int i = 0; i < count; i++)
            testObj.OrderID = 123;

        watch1.Stop();
        print(watch1.Elapsed.ToString());


        SetValueDelegate setter2 = DynamicMethodFactory.CreatePropertySetter(propInfo);
        print("EmitSet花费时间：        ");
        Stopwatch watch2 = Stopwatch.StartNew();

        for (int i = 0; i < count; i++)
            setter2(testObj, 123);

        watch2.Stop();
        print(watch2.Elapsed.ToString());


        print("纯反射花费时间：　       ");
        Stopwatch watch3 = Stopwatch.StartNew();

        for (int i = 0; i < count; i++)
            propInfo.SetValue(testObj, 123, null);

        watch3.Stop();
        print(watch3.Elapsed.ToString());


        print("-------------------");
        print(string.Format("{0} / {1} = {2}",
            watch3.Elapsed.ToString(),
            watch1.Elapsed.ToString(),
            watch3.Elapsed.TotalMilliseconds / watch1.Elapsed.TotalMilliseconds));

        print(string.Format("{0} / {1} = {2}",
            watch3.Elapsed.ToString(),
            watch2.Elapsed.ToString(),
            watch3.Elapsed.TotalMilliseconds / watch2.Elapsed.TotalMilliseconds));

        print(string.Format("{0} / {1} = {2}",
            watch2.Elapsed.ToString(),
            watch1.Elapsed.ToString(),
            watch2.Elapsed.TotalMilliseconds / watch1.Elapsed.TotalMilliseconds));
    }



}

public class OrderInfo
{
    public int OrderID { get; set; }
}


public delegate void SetValueDelegate(object target, object arg);

public static class DynamicMethodFactory
{
    public static SetValueDelegate CreatePropertySetter(PropertyInfo property)
    {
        if (property == null)
            throw new ArgumentNullException("property");

        if (!property.CanWrite)
            return null;

        MethodInfo setMethod = property.GetSetMethod(true);

        DynamicMethod dm = new DynamicMethod("PropertySetter", null,
            new Type[] { typeof(object), typeof(object) },
            property.DeclaringType, true);

        ILGenerator il = dm.GetILGenerator();

        if (!setMethod.IsStatic)
        {
            il.Emit(OpCodes.Ldarg_0);
        }
        il.Emit(OpCodes.Ldarg_1);

        EmitCastToReference(il, property.PropertyType);
        if (!setMethod.IsStatic && !property.DeclaringType.IsValueType)
        {
            il.EmitCall(OpCodes.Callvirt, setMethod, null);
        }
        else
            il.EmitCall(OpCodes.Call, setMethod, null);

        il.Emit(OpCodes.Ret);

        return (SetValueDelegate)dm.CreateDelegate(typeof(SetValueDelegate));
    }

    private static void EmitCastToReference(ILGenerator il, Type type)
    {
        if (type.IsValueType)
            il.Emit(OpCodes.Unbox_Any, type);
        else
            il.Emit(OpCodes.Castclass, type);
    }
}
