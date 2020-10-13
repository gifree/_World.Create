using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class EntryCreate : Editor
{
    private static string _entryName = "World.Entry";
    
    [MenuItem("World/Tools/_world.create()")]
    public static void Create()
    {
        var objects = FindObjectsOfType<GameObject>();

        for(int i = 0; i < objects.Length; i++)
        {
            DestroyImmediate(objects[i]);
        }
        var go = new GameObject(_entryName);
        go.AddComponent<Entry>();

        if (EditorSceneManager.SaveScene(SceneManager.GetActiveScene()))
            Debug.Log("save current scene finished...");
    }    
}
