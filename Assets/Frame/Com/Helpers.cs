using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _World.Create
{
    public class Helpers : World
    {
        public override void Start()
        {
            base.Shut();
            var helper = (new GameObject("MonoHelper"));
            if (helper)
            {
                helper.transform.SetParent(GameObject.Find("World.Entry").transform);
                MonoHelper = helper.AddComponent<MonoHelper>();
            }
        }

        public override void Shut()
        {
            base.Shut();
        }

        public MonoHelper MonoHelper { get; private set; }
    }
}

