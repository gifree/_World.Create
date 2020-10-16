
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IEnumerableClass : MonoBehaviour
{
    private void Start()
    {

        P.Main();
    }
}

class Person
{
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public Person(string name)
    {
        this.name = name;
    }
}
class P
{
    public static void Main()
    {
        string a = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });
        string b = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });
        Debug.Log(a == b);
        Debug.Log(a.Equals(b));
        object g = a;
        object h = b;
        Debug.Log(g == h);
        Debug.Log(g.Equals(h));
        Person p1 = new Person("jia");
        Person p2 = new Person("jia");
        Debug.Log(p1 == p2);
        Debug.Log(p1.Equals(p2));
        Person p3 = new Person("jia");
        Person p4 = p3;
        Debug.Log(p3 == p4);
        Debug.Log(p3.Equals(p4));
        Console.ReadLine();
    }
}


public class Fruit
{
    public string fruitName;
    public string fruitPrice;
    public Fruit(string fruitName, string fruitPrice)
    {
        this.fruitName = fruitName;
        this.fruitPrice = fruitPrice;
    }
}


class FruitShop : IEnumerable
{
    Fruit[] fruits = new Fruit[10];
    int current = 0;
    public void Add(Fruit fruit)
    {
        fruits[current] = fruit;
        current++;
    }

    public IEnumerator GetEnumerator()
    {
        return new FruitEnumerator(fruits);
    }
}

public class FruitEnumerator : IEnumerator
{
    Fruit[] fruits;
    int current = -1;
    public FruitEnumerator(Fruit[] fruits)
    {
        this.fruits = fruits;
    }

    //这里需要做一个判断，因为有可能此时current<0或超出数组长度
    public object Current
    {
        get { return CurrentFruit(); }
    }
    object CurrentFruit()
    {
        if (current < 0 || current > fruits.Length)
            return null;
        else
            return fruits[current];
    }

    public bool MoveNext()
    {
        current++;
        if (current < fruits.Length && fruits[current] != null)
            return true;
        return false;
    }

    public void Reset()
    {
        current = 0;
    }
}


class Program
{
    public static void Main()
    {
        FruitShop fruitShop = new FruitShop();
        Fruit fruitApple = new Fruit("Apple", "10");
        Fruit fruitPear = new Fruit("Pear", "12");
        Fruit fruitGrape = new Fruit("Grape", "15");
        fruitShop.Add(fruitApple);
        fruitShop.Add(fruitPear);
        fruitShop.Add(fruitGrape);

        foreach (Fruit f in fruitShop)
        {
            Debug.Log(f.fruitName + ": " + f.fruitPrice);
        }
    }
}



public class MyCollectioin : ICollection
{
    private string[] list;
    private object root;

    public MyCollectioin() => list = new string[3] { "1", "3", "4" };

    #region ICollection Members
    public bool IsSynchronized => true;

    public int Count => list.Length;

    public void CopyTo(Array array, int index) => list.CopyTo(array, index);

    public object SyncRoot => root;
    #endregion

    #region IEnumerable Members
    public IEnumerator GetEnumerator()
    {
        return list.GetEnumerator();
    }

    #endregion
}



class Apple : IComparable
{
    public string Name;
    public int Price;
    public int CompareTo(object obj)
    {
        //实现接口方法一：
        if (obj == null) return 1;
        Apple otherFruit = obj as Apple;
        if (Price > otherFruit.Price) { return 1; }
        else
        {
            if (Price == otherFruit.Price) { return 0; }
            else { return -1; }
        }
    }
}

public class AppleSort
{
   public static void Main()
    {
        List<Apple> fruit = new List<Apple>();
        fruit.Add(new Apple { Name = "grape", Price = 30 });
        fruit.Add(new Apple { Name = "apple", Price = 10 });
        fruit.Add(new Apple { Name = "banana", Price = 15 });
        fruit.Add(new Apple { Name = "orage", Price = 12 });

        Debug.Log("未排序：");
        foreach (var f in fruit) Debug.Log($"{f.Name} ");

        fruit.Sort();

        var fruitSort = fruit.OrderBy(x => x.Price);

        Debug.Log("排序后：");
        foreach (var f in fruit) Debug.Log($"{f.Name} ");
    }
}


class Node<T>
{
    public T Value;
    public Node<T> NextNode;

    public Node() : this(default(T)) { }
    public Node(T value)
    {
        Value = value;
        NextNode = null;
    }
}