using _World.Create;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _World.Create
{
    public class Scenes : World
    {
        public override void Start()
        {
            base.Start();
            _monoHelper = administrators.Mgr<Helpers>().MonoHelper;
        }

        public override void Shut()
        {
            base.Shut();
        }

        private MonoHelper _monoHelper;

        public int CurIndex => SceneManager.GetActiveScene().buildIndex;
        public string CurName => SceneManager.GetActiveScene().name;

        public void Load(int id, LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadScene(id, mode);
        public void Load(string name, LoadSceneMode mode = LoadSceneMode.Single) => SceneManager.LoadScene(name, mode);

        /// <summary>
        /// Load async.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="loading"></param>
        /// <param name="finished"></param>
        /// <param name="mode"></param>
        public void LoadAsync(int id, 
            Action<AsyncOperation> start, 
            Action<AsyncOperation> loading, 
            Action<AsyncOperation> finished,
            LoadSceneMode mode = LoadSceneMode.Single) 
            => _monoHelper.StartCoroutine(LoadAct(id, start, loading, finished, mode));

        /// <summary>
        /// Load async.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="loading"></param>
        /// <param name="finished"></param>
        /// <param name="mode"></param>
        public void LoadAsync(string name,
             Action<AsyncOperation> start,
             Action<AsyncOperation> loading,
             Action<AsyncOperation> finished,
             LoadSceneMode mode = LoadSceneMode.Single)
             => _monoHelper.StartCoroutine(LoadAct(name, start, loading, finished, mode));

        /// <summary>
        /// Load async act.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="loading"></param>
        /// <param name="finished"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IEnumerator LoadAct(int id,
            Action<AsyncOperation> start, 
            Action<AsyncOperation> loading, 
            Action<AsyncOperation> finished,
             LoadSceneMode mode)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(id, mode);

            operation.completed += finished;

            start?.Invoke(operation);

            while (!operation.isDone)
            {
                loading?.Invoke(operation);
                yield return new WaitForEndOfFrame();
            }

            operation.completed -= finished;

            // update saver manager.
            administrators.Mgr<Players>().AudioPlayer.AutoClear();
        }
        /// <summary>
        /// Load async act.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="loading"></param>
        /// <param name="finished"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IEnumerator LoadAct(string name,
            Action<AsyncOperation> start,
            Action<AsyncOperation> loading, 
            Action<AsyncOperation> finished,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(name, mode);

            operation.completed += finished;

            start?.Invoke(operation);

            while (!operation.isDone)
            {
                yield return new WaitForEndOfFrame();
                loading?.Invoke(operation);
            }

            operation.completed -= finished;

            // update saver manager.
            administrators.Mgr<Players>().AudioPlayer.AutoClear();
        }
    }
}
