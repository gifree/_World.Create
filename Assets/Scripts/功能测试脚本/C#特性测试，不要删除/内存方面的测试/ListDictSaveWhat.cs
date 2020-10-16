using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ListDictSaveWhat
{
    public class ListDictSaveWhat : MonoBehaviour
    {
        // 保存的是地址指针的拷贝，不是保存的引用，所以保存后修改对象不会影响已存结果
        private void Start()
        {
            Main();

            not1[0].f = 100;

            print(not1[0].f);
            print(not1[1].f);

        }


        // 数组和LIST不一样，
        // 数组创建时对于对象的处理 :
        //      如果是值类型则将数据分别拷贝一份存到堆内存里，
        //      如果是引用类型，如不赋值则采用NULL，赋值的话则存储对象指向的地址值
        //      
        // LIST需要手动添加内容
        //      但考虑到list实现使用了数组，所以在底层机制上还是一样的
        //
        //  看着呢吧！
        // 下一步呢？
        //
        public note[] not1 = new note[5];
        public A[] not2 = new A[5];

        List<note> not3 = new List<note>(5);
        List<A> not4 = new List<A>(5);

        private void Main()
        {
            List<string> lst1 = new List<string>();
            List<string> lst2 = new List<string>();

            List<A1> lst3 = new List<A1>();
            List<A1> lst4 = new List<A1>();

            Dictionary<int, string> d1 = new Dictionary<int, string>();
            Dictionary<int, string> d2 = new Dictionary<int, string>();
            Dictionary<int, A1> d3 = new Dictionary<int, A1>();
            Dictionary<int, A1> d4 = new Dictionary<int, A1>();

            string a = "ss";

            lst1.Add(a);
            d1.Add(0, a);
            lst2.Add(a);
            d2.Add(0, a);

            a = "dd";

            print(lst1[0]);
            print(d1[0]);
            print(lst1[0]);
            print(d2[0]);

            print("--------------------------");

            A1 a1 = new A1();

            lst3.Add(a1);
            d3.Add(0, a1);
            lst4.Add(a1);
            d4.Add(0, a1);

            a1 = new A1();
            a1.S = "A2";
            print(lst3[0].S);
            print(d3[0].S);
            print(lst4[0].S);
            print(d4[0].S);
        }
    }

    public class A1
    {
        public string S = "A1";
    }



    


    public class A
    {
        public int a;
    }

    public struct note
    {
        public int x;  //横坐标
        public int y;  //纵坐标
        public int f;  //上一步在队列中的编号
        public int s;  //步数
    }
}
