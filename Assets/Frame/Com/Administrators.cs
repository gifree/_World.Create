using System;
using System.Collections.Generic;
using UnityEngine;

using _World.Interface;

namespace _World.Create
{
    /// <summary>
    /// Description:
    ///     World components manager, support save & get. 
    /// </summary>
    public sealed class Administrators : Singleton<Administrators>, IWorldMgr
    {
       // private static Administrators _admin;
        public Administrators() {
            _dictMgr = new Dictionary<string, World>();
            _listComMgr = new List<IWorldMgr>();
        }
     //   public static Administrators Admin => _admin ?? (_admin = new Administrators());

        private Dictionary<string, World> _dictMgr;
        private List<IWorldMgr> _listComMgr;

        /// <summary>
        /// All components start ctrl.
        /// </summary>
        public void Start() 
        {
            // Start is the first step & the most important operators. 
            // for every one, the acts of them should be finished by themself.
        }
        /// <summary>
        /// All components update ctrl.
        /// </summary>
        public void Update() {

            for (int i = 0, j = _listComMgr.Count; i < j; i++)
                _listComMgr[i]?.Update();

        }
        /// <summary>
        /// All components shut ctrl.
        /// </summary>
        public void Shut() 
        {
            for (int i = 0, j = _listComMgr.Count; i < j; i++)
                _listComMgr[i]?.Shut();
        }

        /// <summary>
        /// Register world mgr component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">mgr type</param>
        /// <param name="t">mgr</param>
        /// <returns></returns>
        public Administrators Register<T>(string type, T t) where T : World
        {
            try
            {
                if (!_dictMgr.ContainsKey(type) && t != null)
                {
                    _dictMgr.Add(type, t);
                    _listComMgr.Add(t);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
          //  return Admin;
            return Instance;
        }
        /// <summary>
        /// UnRegister world mgr component.
        /// </summary>
        /// <param name="type">mgr type</param>
        /// <returns></returns>
        public Administrators UnRegister(string type)
        {
            try
            {
                if (_dictMgr.TryGetValue(type, out var t))
                {
                    _dictMgr.Remove(type);
                    _listComMgr.Remove(t);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            // return Admin;
            return Instance;
        }
        /// <summary>
        /// UnRegister all world mgr component.
        /// </summary>
        public Administrators UnRegister()
        {
            _dictMgr.Clear();
            //  return Admin;
            return Instance;
        }

        /// <summary>
        /// Acquire world mgr component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Mgr<T>() where T : World
        {
            _dictMgr.TryGetValue(typeof(T).Name, out var value);
            return value as T;
        }

        /*
         These are indexers that have not yet been enabled
         */

        [DevelopingTag("This is an indexer have not yet been enabled")]
        public World this[string name]
        {
            get
            {
                _dictMgr.TryGetValue(name, out var value);
                return value;
            }
        }
        [DevelopingTag("This is an indexer have not yet been enabled")]
        public World this[int id]
        {
            get
            {
                return default;
            }
        }
    }


    /// <summary>
    /// Extern ways for inherited from world instance.
    /// </summary>
    public static class AdministorExtern
    {
        /// <summary>
        /// When action finsihed do extra task.
        /// </summary>
        /// <typeparam name="World">instance type</typeparam>
        /// <param name="w">instance inherited from World</param>
        /// <param name="handler">extra task use delegate no param</param>
        public static void OnFinsihed<World>(this World w, Action handler) => handler?.Invoke();
    }
}