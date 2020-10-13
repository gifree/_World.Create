using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _World.Interface;

namespace _World.Create
{
    public class World : IWorldMgr
    {

        public bool Loaded = false;

        static World()
        {
            administrators = Administrators.Instance;
        }
        /// <summary>
        /// All children can be get. the mgr of all world component.
        /// </summary>
        protected static Administrators administrators;

        public virtual void Start() { Loaded = true; }
        public virtual void Update() { }
        public virtual void Shut() { Loaded = false; }

    }

}