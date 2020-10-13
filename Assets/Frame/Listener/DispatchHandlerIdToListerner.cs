using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatchHandlerIdToListerner
{
    private static Dictionary<string, int> _idDicts;

    public static void ReadConfig()
    {
        var path = Application.dataPath + "/Resources/HandlerIdTab.txt";
        _idDicts = new Dictionary<string, int>();
        var contents = ReadStart(path);
        ConfigStart(contents);
    }

    public static int GetIdFromConfigTab(string key) => _idDicts[key];


    private static void ConfigStart(string contents)
    {
        string[] kvStr = contents.Replace('\n', ' ').Trim().Split(',');
        foreach (var kvs in kvStr)
        {
            string[] kv = kvs.Trim().Split(':');
            if(kv.Length == 2)
            {
                if (!_idDicts.ContainsKey(kv[0])) _idDicts.Add(kv[0], int.Parse(kv[1]));
            }
        }
    }

    /// <summary>
    /// 执行读文件
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns></returns>
    private static string ReadStart(string path)
    {
        try
        {
            // 开始读文件
            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                var contents = file.ReadToEnd();
                file.Dispose();
                return contents;
            }
            //var reader = new StreamReader(_path);
            //contents = reader.ReadToEnd();
            //reader.Dispose();
        }
        catch (Exception e)
        {
            Debug.Log(string.Format("Read file failed, exception info = {0}：", e));
        }

        return default;
    }

}


public enum InternalEvents
{
    Listener_Players_AudioPlayer_PlayList = 1,
}
