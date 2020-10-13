using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _World.Tools
{
    /// <summary>
    /// Pools
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pools<T> : object
    {

        public Pools(int id, string name, int maxCount)
        {
            _ts = new Queue<T>();
            Id = id;
            Name = name;
            MaxCount = maxCount;
        }

        ~Pools()
        {
            _ts = null;
            Id = default;
            Name = null;
            MaxCount = default;
        }

        #region Infos
        private Queue<T> _ts;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int MaxCount { get; private set; }

        public int Count => _ts == null ? -1 : _ts.Count;

        #endregion

        /// <summary>
        /// Pop T object.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T t = default;
            if (Count > 0)
                t = _ts.Dequeue();
            else
                Debug.Log(string.Format("Pool has empty pool infos: id = {0}, name = {1}, type = {2}", Id, Name, typeof(T)));
            return t;
        }

        /// <summary>
        /// Pop list objects
        /// </summary>
        /// <param name="count">pop counts</param>
        /// <returns></returns>
        public List<T> Pop(int count)
        {
            List<T> lst = new List<T>();
            // Sure if need pop.
            if (count > 0)
            {
                for(var i = 0; i < count; i++)
                {
                    if (Count > 0)
                    {
                        T t = _ts.Dequeue();
                        if (t != null) lst.Add(t);
                        else
                        {
                            // Cancel it.
                            Debug.Log("Exist null instance and has been skipped!");
                            i--;
                        }
                    }
                    else
                        break;
                }

                if (Count < count || Count <= 0)
                    Debug.Log(string.Format("Pool has empty pool infos: id = {0}, name = {1}, type = {2}", Id, Name, typeof(T)));
            }

            return lst;
        }

        /// <summary>
        /// Pus a new object.
        /// </summary>
        /// <param name="t"></param>
        public void Push(T t)
        {
            if (_ts != null && t != null && Count < MaxCount)
                _ts.Enqueue(t);
            else
                Debug.Log(string.Format("push failed error infos :\n saver null : {0}, \n instance null : {1}, \n beyound max : {2}", 
                    _ts == null, t == null, Count >= MaxCount));
        }

        /// <summary>
        /// Push list objects.
        /// </summary>
        /// <param name="lst">object list</param>
        public void Push(List<T> lst)
        {
            if(lst != null && lst.Count > 0)
            {
                for (int i = 0, j = lst.Count; i < j; i++)
                {
                    if (Count < MaxCount)
                    {
                        T t = lst[i];
                        if (t != null)
                            _ts.Enqueue(t);
                        else
                            Debug.Log("null instance and skip it.");
                    }
                    else
                    {
                        Debug.Log(string.Format("Beyond max count pool infos:\n id = {0},\n name = {1},\n type = {2}", Id, Name, typeof(T)));
                    }
                } 
            }
            else
                Debug.Log(string.Format("Beyond max count pool infos:\n id = {0},\n name = {1},\n type = {2}", Id, Name, typeof(T)));
        }

        /// <summary>
        /// Clear pools.
        /// </summary>
        public void Clear() => _ts.Clear();
    }

    public class PoolsMgr
    {
        private static Dictionary<int, object> _poolsDict;

        private PoolsMgr() {
            _poolsDict = new Dictionary<int, object>();
        }

        /// <summary>
        /// Generate a new pool.
        /// </summary>
        /// <typeparam name="T">pool type</typeparam>
        /// <param name="id">pool id</param>
        /// <param name="name">pool name</param>
        /// <param name="max">max count</param>
        public static Pools<T> GenPool<T>(int id, string name, int max)
        {
            if(_poolsDict.ContainsKey(id))
            {
                Debug.Log("Pool with this id already exist.");
                return default;
            }
            var pool = new Pools<T>(id, name, max);
            if (pool != null)
                _poolsDict.Add(id, pool);

            return pool;
        }

        /// <summary>
        /// Destroy an exist pool.
        /// </summary>
        /// <typeparam name="T">pool type</typeparam>
        /// <param name="id">pool id</param>
        public static void DestroyPool<T>(int id)
        {
            if (_poolsDict.TryGetValue(id, out var pool))
            {
                if (pool != null)
                {
                    (pool as Pools<T>).Clear();
                    _poolsDict.Remove(id);
                }
            }
        }

        /// <summary>
        /// Push object to pool.
        /// </summary>
        /// <typeparam name="T">object type.</typeparam>
        /// <param name="id">pool id</param>
        /// <param name="t">object</param>
        public static void Push<T>(int id, T t)
        {
            if (_poolsDict.TryGetValue(id, out var pool))
                if (pool != null)
                    (pool as Pools<T>).Push(t);
        }
        /// <summary>
        /// Push objects to pool.
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="id">pool id</param>
        /// <param name="lst">objcets ist</param>
        public static void Push<T>(int id, List<T> lst)
        {
            if (_poolsDict.TryGetValue(id, out var pool))
                if (pool != null) 
                    (pool as Pools<T>).Push(lst);
        }
        /// <summary>
        /// Pop object from pool.
        /// </summary>
        /// <typeparam name="T">return object type</typeparam>
        /// <param name="id">pool id</param>
        /// <returns></returns>
        public static T Pop<T>(int id)
        {
            T t = default;
            if (_poolsDict.TryGetValue(id, out var pool))
                if (pool != null)
                    t = (pool as Pools<T>).Pop();
            return t;
        }
        /// <summary>
        /// Pop objects from pool.
        /// </summary>
        /// <typeparam name="T">return object type</typeparam>
        /// <param name="id">pool id</param>
        /// <param name="count">pop count</param>
        /// <returns></returns>
        public static List<T> Pop<T>(int id, int count)
        {
            List<T> lst = default;
            if (_poolsDict.TryGetValue(id, out var pool))
                if (pool != null)
                    lst = (pool as Pools<T>).Pop(count);
            return lst;
        }
        /// <summary>
        /// Get a pool.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetPool<T>(int id)
        {
            _poolsDict.TryGetValue(id, out var pool);
            return (T)pool;
        }

    }
}
