using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class AwitAsyc : MonoBehaviour
{
    private void Start()
    {
        AwitAsyc3.Main();
    }

   
}


public class AwitAsyc1
{
    public static void Main()
    {
        _ = Method1();
        Method2();
    }

    public static async Task Method1()
    {
        await Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(" Method 1");
            }
        });
    }


    public static void Method2()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(" Method 2");
        }
    }
}

/*
 * 在本例中，Method 1将总长度作为整数值返回，我们在Method 3中以长度的形式传递一个参数，它来自Method 1。
 * 在这里，在传递Method 3中的参数之前，我们必须使用AWAIT关键字，为此，我们必须使用调用方法中的async 关键字。
 */
public class AwitAsyc2
{
     public static void Main()
    {
        callMethod();
    }

    public static async void callMethod()
    {
        Task<int> task = Method1();
        Method2();
        int count = await task;
        Method3(count);
    }

    public static async Task<int> Method1()
    {
        int count = 0;

        await Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(" Method 1");
                count += 1;
            }
        });
        return count;
    }

    public static void Method2()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(" Method 2");
        }
    }

    public static void Method3(int count)
    {
        Debug.Log("Total count is " + count);
    }
}

/*
 .NET Framework4.5中有一些支持API，Windows运行时包含支持异步编程的方法。
包含异步方法的API有HttpClient, SyndicationClient, StorageFile, StreamWriter, StreamReader, XmlReader, MediaCapture, BitmapEncoder, BitmapDecoder 等。
本例中，异步读取大型文本文件中的所有字符，并获取所有字符的总长度。
 */
public class AwitAsyc3
{
    public static void Main()
    {
        Task task = new Task(CallMethod);
        task.Start();
        task.Wait();
    }

    static async void CallMethod()
    {
        string filePath = "D:/ProjectCJB/18.3.1/_World/Assets/Resources/test.txt";


        Task<int> task = ReadFile(filePath);
        
        Debug.Log(" Other Work 1");
        Debug.Log(" Other Work 2");
        Debug.Log(" Other Work 3");

        int length = await task;
        Debug.Log(" Total length: " + length);

        Debug.Log(" After work 1");
        Debug.Log(" After work 2");
    }

    static async Task<int> ReadFile(string file)
    {
        int length = 0;

        Debug.Log(" File reading is stating");
        using (StreamReader reader = new StreamReader(file))
        {
            // Reads all characters from the current position to the end of the stream asynchronously   
            // and returns them as one string.   
            string s = await reader.ReadToEndAsync();

            length = s.Length;
        }
        Debug.Log(" File reading is completed");
        return length;
    }
}