using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _World.Create
{
    /// <summary>
    /// Description:
    ///     Do player task.
    ///     
    /// Include:
    ///     Audio player. ## state: using.
    ///     Movie player. ## state: unused.
    ///     
    /// </summary>
    public sealed class Players : World
    {
        public override void Start()
        {
            base.Start();
            AudioPlayer = new AudioPlayer();
            MoviePlayer = new MoviePlayer();
        }

        public override void Update()
        {
            
        }

        public override void Shut()
        {
            base.Shut();
            AudioPlayer = null;
            MoviePlayer = null;
        }

        public AudioPlayer AudioPlayer { get; private set; }
        public MoviePlayer MoviePlayer { get; private set; }
    }

    /// <summary>
    /// Description:
    ///     Subsystem for player.
    /// 
    /// Task：
    ///     Save & get player.
    ///     Set target for palyer & execute task.
    ///     
    /// </summary>
    public sealed class AudioPlayer
    {
        public AudioPlayer()
        {
            _audios = new Dictionary<int, AudioSource>();
        }

        private readonly Dictionary<int, AudioSource> _audios;

        /// <summary>
        /// Check contains this audio by id and check audio exist.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasAudio(int id, out string msg)
        {
            bool result =  _audios.TryGetValue(id, out var audio) && audio != null;
            msg = (!result && !audio) ? "audio lost,  remove it plz." : "no this id, add it plz.";
            return result;
        }

        /// <summary>
        /// Player clip.
        /// Des:
        ///     by this way will be cause problem in scene switch,
        /// if audio gameobject lost, also audio component will be lost too, but key is exist.
        /// you need check it or update it before switch finished.
        /// or set object don't destroyed.
        /// </summary>
        /// <param name="id">player id</param>
        /// <param name="clip">clip support by external</param>
        public void Play(int id, AudioClip clip = default)
        {
            if (_audios.TryGetValue(id, out var audio))
                Play(audio, clip);
        }
        /// <summary>
        /// Play clip, player support by external.
        /// </summary>
        /// <param name="audio">player support by external</param>
        /// <param name="clip">clip support by external</param>
        public void Play(AudioSource audio, AudioClip clip = default)
        {
            // clip存在则播放，否则播放默认的
            if (audio != null)
            {
                
                if (audio.isPlaying) audio.Stop();
                if (clip != null)
                    audio.clip = clip;
                if (audio.clip != null)
                    audio.Play();

                
            }
        }
        /// <summary>
        /// Stop player.
        /// </summary>
        /// <param name="id">player id</param>
        public void Stop(int id)
        {
            if (_audios.TryGetValue(id, out var audio))
                Stop(audio);
        }
        /// <summary>
        /// Stop player.
        /// </summary>
        /// <param name="audio">player, support by external</param>
        public void Stop(AudioSource audio)
        {
            if (audio != null && !audio.isPlaying)
                audio.Stop();
        }
        /// <summary>
        /// Pause player.
        /// </summary>
        /// <param name="id">player id</param>
        public void Pause(int id)
        {
            if (_audios.TryGetValue(id, out var audio))
                Pause(audio);
        }
        /// <summary>
        /// Pause player, player support by external.
        /// </summary>
        /// <param name="audio">player, support by external</param>
        public void Pause(AudioSource audio)
        {
            if (audio != null && audio.isPlaying)
                audio.Pause();
        }
        /// <summary>
        /// Continue player.
        /// </summary>
        /// <param name="id">player id</param>
        public void UnPause(int id)
        {
            if (_audios.TryGetValue(id, out var audio))
                audio.UnPause();
        }
        /// <summary>
        /// Continue player, player support by external.
        /// </summary>
        /// <param name="audio">player, support by external</param>
        public void UnPause(AudioSource audio)
        {
            if (audio != null && !audio.isPlaying) audio.UnPause();
        }

        /// <summary>
        /// Save player.
        /// </summary>
        /// <param name="id">player id</param>
        /// <param name="audio">player support by external</param>
        public void Add(int id, AudioSource audio)
        {
            if (!_audios.TryGetValue(id, out _) && audio != null)
                _audios.Add(id, audio);
        }
        /// <summary>
        /// Remove player.
        /// </summary>
        /// <param name="id">player id</param>
        public void Remove(int id)
        {
            if (_audios.TryGetValue(id, out _))
                _audios.Remove(id);
        }

        /// <summary>
        /// Acquire player.
        /// </summary>
        /// <param name="id">player id</param>
        /// <returns></returns>
        public AudioSource GetAudio(int id) => _audios.TryGetValue(id, out var audio) ? audio : default;
        /// <summary>
        /// Add audiosource component to gameobject and return it.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public AudioSource GenAudio(GameObject obj) => obj.AddComponent<AudioSource>();

        /*
         * Extern contents.
         */

        private ListenerNode _node = default;

        /// <summary>
        /// Auto play clips by list.
        /// </summary>
        /// <param name="audio">player</param>
        /// <param name="clips">clips</param>
        /// <param name="repeat">repeat mode</param>
        public void PlayList(AudioSource audio, ClipList clips, bool repeat = false)
        {
            // pass null instance
            if (audio == null) { Debug.Log("Refuse null instace or create AudioSource by 'new'."); return; }

            // test way will be change in the feature.
            if (_node != null)
            {
                Debug.Log("Now support one handler act only. in progress has been canceled.");
                Administrators.Instance.Mgr<Listeners>().Remove(_node);
                return;
            }

            bool trail = false;
            _node = new ListenerNode((int)InternalEvents.Listener_Players_AudioPlayer_PlayList,
                new Action(
                    () =>{
                        if (clips != null && clips.Clips.Count > 0)
                        {
                            if (!audio.isPlaying)
                            {
                                if (trail)
                                {
                                    Administrators.Instance.Mgr<Listeners>().Remove(_node);
                                    Debug.Log(string.Format("Remove listener task:{0}", _node));
                                    return;
                                }

                                Play(audio, clips.Current);
                                trail = clips.Next();
                                // repeat task from first to end.
                                if (trail && repeat)
                                    trail = false;
                            }
                        }
                        else
                        {
                            Administrators.Instance.Mgr<Listeners>().Remove(_node);
                            Debug.Log(string.Format("Remove uninstance listener task:{0}", _node));
                        }
                    }
                    ));
            Administrators.Instance.Mgr<Listeners>().Add(_node);
        }

        /// <summary>
        /// Update audio saver, exclude lost instance.
        /// </summary>
        public void AutoClear()
        {
            var idLst = new List<int>();
            foreach(var key in _audios.Keys)
            {
                _audios.TryGetValue(key, out var audio);
                if (!audio)
                {
                    idLst.Add(key);
                    Debug.Log(string.Format("Aduio saver is checking, find an audio quote has been lost, and will be removed.  ID number : {0}", key));
                }
            }

            Debug.Log("Do clear lost audios...");

            for (int i = 0, j = idLst.Count; i < j; i++)
                _audios.Remove(i);

            idLst.Clear();
            Debug.Log(string.Format("<b style=color:red>Clear fisihed.<\\b>"));
        }

    }

    /// <summary>
    /// Des:
    ///     Assist audioplayer play clips;
    /// </summary>
    public class ClipList
    {
        private List<AudioClip> _clips;
        private int _index = 0;

        public ClipList(List<AudioClip> lst) => _clips = lst;

        public List<AudioClip> Clips { get => _clips; }

        public AudioClip Current { get => _clips[_index]; }

        public bool Next() => (_index = (_index + 1) % Clips.Count) == 0;
    }


    public sealed class MoviePlayer
    {

    }
}