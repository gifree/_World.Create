using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


// 无论哪种锁，都会出现阻塞
// 定义的锁对象应该是 私有的，静态的，只读的，引用类型的对象，这样可以防止外部改变锁对象
public class ThreadLock : MonoBehaviour
{
    private void Start()
    {
        MutexWay.Main();
    }
}


class LockWay
{
   public  static void Main()
    {

        Thread t1 = new Thread(ThreadFunc1);
        Thread t2 = new Thread(ThreadFunc2);
        t1.Start();
        t2.Start();

    }
    private static object obj = new object();

    public static Dictionary<int, string> StrDict = new Dictionary<int, string>();

    static void ThreadFunc1()
    {
        lock (obj)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (obj)
                {
                    Func(i, "ThreadFunc1");
                    Debug.Log("over1!");
                    Thread.Sleep(100);
                }
            }
        }
    }

    static void ThreadFunc2()
    {
        lock (obj)
        {
            for (int i = 10; i < 20; i++)
            {
                lock (obj)
                {
                    Func(i, "ThreadFunc2");
                    Debug.Log("over2!");
                    Thread.Sleep(100);
                }
            }

        }
    }
    static void Func(int id, string str)
    {
        Debug.Log(string.Format("{0} {1}", str, System.DateTime.Now.Millisecond.ToString()));
        StrDict.Add(id, str);
        
    }
}

// Mutex本身是可以系统级别的，所以是可以跨越进程的
class MutexWay
{
    public static void Main()
    {

        Thread t1 = new Thread(ThreadFunc1);
        Thread t2 = new Thread(ThreadFunc2);
        t1.Start();
        t2.Start();

    }
    public static Dictionary<int, string> StrDict = new Dictionary<int, string>();

    private static Mutex m = new Mutex();
    static void ThreadFunc1()
    {
        m.WaitOne();
        for (int i = 0; i < 10; i++)
        {
            m.WaitOne();
            Func(i, "ThreadFunc1");
            m.ReleaseMutex();
        }
        m.ReleaseMutex();
    }

    static void ThreadFunc2()
    {
        m.WaitOne();
        for (int i = 10; i < 20; i++)
        {
            m.WaitOne();
            Func(i, "ThreadFunc2");
            m.ReleaseMutex();
        }
        m.ReleaseMutex();
    }
    static void Func(int id, string str)
    {
        Debug.Log(string.Format("{0} {1}", str, System.DateTime.Now.Millisecond.ToString()));
        
        StrDict.Add(id, str);
        Thread.Sleep(100);
    }
}

// Monitor有TryEnter的功能，可以防止出现死锁的问题，lock没有
class MonitorWay
{
    public static void Main()
    {

        Thread t1 = new Thread(ThreadFunc1);
        Thread t2 = new Thread(ThreadFunc2);
        t1.Start();
        t2.Start();

    }
    private static object obj = new object();

    public static Dictionary<int, string> StrDict = new Dictionary<int, string>();

    static void ThreadFunc1()
    {
        Monitor.Enter(obj);
        for (int i = 0; i < 10; i++)
        {
            Monitor.TryEnter(obj);
            Func(i, "ThreadFunc1");
            Monitor.Exit(obj);
        }
        Monitor.Exit(obj);
    }

    static void ThreadFunc2()
    {
        Monitor.Enter(obj);
        for (int i = 0; i < 10; i++)
        {
            Monitor.Enter(obj);
            Func(i, "ThreadFunc2");
            Monitor.Exit(obj);
        }
        Monitor.Exit(obj);
    }
    static void Func(int id,  string str)
    {
        Debug.Log(string.Format("{0} {1}", str, System.DateTime.Now.Millisecond.ToString()));

        StrDict.Add(id, str);
        Thread.Sleep(500);
    }
}

// 当同一个资源被多个线程读，少个线程写的时候，使用读写锁
class ReaderWriterLockSlimWay
{
    /*
      问题： 既然读读不互斥，为何还要加读锁
            如果只是读，是不需要加锁的，加锁本身就有性能上的损耗
            如果读可以不是最新数据，也不需要加锁
            如果读必须是最新数据，必须加读写锁
            读写锁相较于互斥锁的优点仅仅是允许读读的并发，除此之外并无其他。

      结论： 读写锁能够保证读取数据的 严格实时性，如果不需要这种 严格实时性，那么不需要加读写锁。
     
     */
}