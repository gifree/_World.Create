using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _World.Create;
using System;

public class SceneTest : MonoBehaviour
{
    _World.Create.Scenes _scenes;
    _World.Create.AudioPlayer _audioPlayer;

    public List<AudioClip> Clips;

    private void Start()
    {
        _scenes = Administrators.Instance.Mgr<Scenes>();
        _audioPlayer = Administrators.Instance.Mgr<Players>().AudioPlayer;

        var audioSource = gameObject.AddComponent<AudioSource>();

        ClipList list = new ClipList(Clips);

        _audioPlayer.PlayList(audioSource, list, false);

        
    }
}
