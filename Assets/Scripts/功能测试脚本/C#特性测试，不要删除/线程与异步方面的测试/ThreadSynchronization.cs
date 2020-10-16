using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadSynchronization : MonoBehaviour
{
    private void Start()
    {
        ThreadSynManualResetEventSlimWay.Main();
    }
}

/*
  对象有两种信号量状态True和False。构造函数设置初始状态。简单来说，
     ◆ 如果构造函数由true创建，则第一次WaitOne()不会阻止线程的执行，而是等待Reset后的第二次WaitOne()才阻止线程执行。
     ◆ 如果构造函数有false创建，则WaitOne()必须等待Set()才能往下执行。
　　一句话总结就是：是否忽略第一次阻塞。
　　方法如下：
       ◆ WaitOne：该方法用于阻塞线程，默认是无限期的阻塞，支持设置等待时间，如果超时就放弃阻塞，不等了，继续往下执行;
       ◆ Set:手动修改信号量为True，也就是恢复线程执行;
       ◆ ReSet:重置状态; 重置后，又能WaitOne()啦
 */
public class ThreadSynManualResetEventWay
{
    //一开始设置为false才会等待收到信号才执行  
    static ManualResetEvent mr = new ManualResetEvent(false);
    // 等待子线程通知主线程
    public static void SubCallMain()
    {
        Thread t = new Thread(Run1);
        //启动辅助线程
        t.Start();
        //等待辅助线程执行完毕之后，主线程才继续执行
       Debug.Log("主线程一边做自己的事，一边等辅助线程执行!" + DateTime.Now.ToString("mm:ss"));
        mr.WaitOne();
        Debug.Log("收到信号，主线程继续执行" + DateTime.Now.ToString("mm:ss"));
    }

    static void Run1()
    {
        //模拟长时间任务
        Thread.Sleep(3000);
        Debug.Log("辅助线程长时间任务完成！" + DateTime.Now.ToString("mm:ss"));
        mr.Set();
    }

    //等待主线程通知子线程
    public static void MainCallSub()
    {
        Thread t = new Thread(Run2);
        Debug.Log("开始" + DateTime.Now.ToString("mm:ss"));
        t.Start();
        mr.WaitOne();
        Debug.Log("第一次等待完成!" + DateTime.Now.ToString("mm:ss"));
        mr.Reset();     //重置后，又能WaitOne()啦
        mr.WaitOne(1000);
        Debug.Log("第二次等待完成!" + DateTime.Now.ToString("mm:ss"));
        Console.ReadKey();
    }

    static void Run2()
    {
        mr.Set();
        Thread.Sleep(2000);
        Debug.Log("第二次set");
        mr.Set();
    }

}


/*
 * AutoResetEvent与ManualResetEvent的区别在于AutoResetEvent 的WaitOne会改变信号量的值为false，让其等待阻塞。
 * 比如说初始信号量为True，如果WaitOne超时信号量将自动变为False，而ManualResetEvent则不会。
 * 第二个区别：
 *      ◆ ManualResetEvent：每次可以唤醒一个或多个线程
 *      ◆ AutoResetEvent：每次只能唤醒一个线程*/
public class ThreadSynAutoResetEventWay
{
    static AutoResetEvent ar = new AutoResetEvent(true);
    public static void Main()
    {
        Thread t = new Thread(Run);
        t.Start();

        bool state = ar.WaitOne(1000);
        Debug.Log($"当前的信号量状态:{state}");

        state = ar.WaitOne(1000);
        Debug.Log($"再次WaitOne后现在的状态是:{state}");
        Debug.Log("当前时间" + DateTime.Now.ToString("mm:ss"));

        state = ar.WaitOne(1000);
        Debug.Log($"再次WaitOne后现在的状态是:{state}");
        Debug.Log("当前时间" + DateTime.Now.ToString("mm:ss"));
    }

    static void Run()
    {
        Debug.Log("当前时间" + DateTime.Now.ToString("mm:ss"));
    }




    static AutoResetEvent BuyBookEvent = new AutoResetEvent(false);
    static AutoResetEvent PayMoneyEvent = new AutoResetEvent(false);
    static AutoResetEvent GetBookEvent = new AutoResetEvent(false);
    static int number = 10;

    public static void BuyBook()
    {
        Thread buyBookThread = new Thread(new ThreadStart(BuyBookProc));
        buyBookThread.Name = "买书线程";
        Thread payMoneyThread = new Thread(new ThreadStart(PayMoneyProc));
        payMoneyThread.Name = "付钱线程";
        Thread getBookThread = new Thread(new ThreadStart(GetBookProc));
        getBookThread.Name = "取书线程";

        buyBookThread.Start();
        payMoneyThread.Start();
        getBookThread.Start();


        // 当NewThread调用Join方法的时候，MainThread就被停止执行，直到NewThread线程执行完毕。
        buyBookThread.Join();
        //payMoneyThread.Join();
        //getBookThread.Join();

        Debug.Log(1111);

    }

    static void BuyBookProc()
    {
        while (number > 0)
        {
            Debug.Log(string.Format("{0}：数量{1}", Thread.CurrentThread.Name, number));
            PayMoneyEvent.Set();
            BuyBookEvent.WaitOne();
            Debug.Log("------------------------------------------");
            number--;
        }
    }

    static void PayMoneyProc()
    {
        while (number > 0)
        {
            PayMoneyEvent.WaitOne();
            Debug.Log(string.Format("{0}：数量{1}", Thread.CurrentThread.Name, number));
            GetBookEvent.Set();
        }
    }

    static void GetBookProc()
    {
        while (number > 0)
        {
            GetBookEvent.WaitOne();
            Debug.Log(string.Format("{0}：数量{1}", Thread.CurrentThread.Name, number));
            BuyBookEvent.Set();
        }
    }
}



/*
 ManualResetEventSlim是ManualResetEvent的混合版本,一直保持大门敞开直到手工调用Reset方法，
　　Set() 相当于打开了大门从而允许准备好的线程接收信号并继续工作
　　Reset() 相当于关闭了大门 此时已经准备好执行的信号量 则只能等到下次大门开启时才能够执行
 */
public class ThreadSynManualResetEventSlimWay
{
    public static void Main()
    {
        var t1 = new Thread(() => TravelThroughGates("Thread 1", 5));
        var t2 = new Thread(() => TravelThroughGates("Thread 2", 9));
        var t3 = new Thread(() => TravelThroughGates("Thread 3", 12));
        t1.Start();
        t2.Start();
        t3.Start();
        Debug.Log("时间：" + DateTime.Now.ToString("mm:ss"));
        Thread.Sleep(TimeSpan.FromSeconds(6));
        Debug.Log("时间：" + DateTime.Now.ToString("mm:ss"));
        Debug.Log("The gates are now open!");
        _mainEvent.Set();
        Debug.Log("时间：" + DateTime.Now.ToString("mm:ss"));
        Thread.Sleep(TimeSpan.FromSeconds(2));
        _mainEvent.Reset();
        Debug.Log("时间：" + DateTime.Now.ToString("mm:ss"));
        Debug.Log("The gates have been closed!");
        Debug.Log("时间：" + DateTime.Now.ToString("mm:ss"));
        Thread.Sleep(TimeSpan.FromSeconds(10));
        Debug.Log("The gates are now open for the second time!");
        _mainEvent.Set();
        Debug.Log("时间：" + DateTime.Now.ToString("mm:ss"));
        Thread.Sleep(TimeSpan.FromSeconds(2));
        Debug.Log("The gates have been closed!");
        _mainEvent.Reset();
    }

    static void TravelThroughGates(string threadName, int seconds)
    {
        Debug.Log(string.Format("线程{0}  内部开始时间：{1}", threadName, DateTime.Now.ToString("mm:ss")));
        Debug.Log($"{threadName} falls to sleep {seconds}");
        
        Thread.Sleep(TimeSpan.FromSeconds(seconds));
        Debug.Log(string.Format("线程{0}  内部结束等待时间：{1}", threadName, DateTime.Now.ToString("mm:ss")));
        _mainEvent.Wait();
        Debug.Log($"{threadName} enters the gates!");
    }
    /// <summary>
    /// ManualResetEventSlim是ManualResetEvent的混合版本,一直保持大门敞开直到手工调用Reset方法，
    /// _mainEvent.Set 相当于打开了大门从而允许准备好的线程接收信号并继续工作
    /// _mainEvent.Reset 相当于关闭了大门 此时已经准备好执行的信号量 则只能等到下次大门开启时才能够执行
    /// </summary>
    static ManualResetEventSlim _mainEvent = new ManualResetEventSlim(false);
}
