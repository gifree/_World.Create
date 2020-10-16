using _World.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _World.Create
{
    /// <summary>
    /// Description:
    ///     Listerners for actions.
    ///     
    /// Task:
    ///     Do listener actions save & remove & execute by fixedupdate.
    ///     current aims to world mgr components.
    ///     
    /// Attention:
    ///     This class used to world manager. 
    ///     if you need deal with muti item instance please create a new mgr make it.
    /// </summary>
    public sealed class Listeners : World
    {

        private Dictionary<int, ListenerNode> _nodes;
        private List<ListenerNode> _deletes;
        private int _dashCount = 0;

        public override void Start()
        {
            base.Start();
            _nodes = new Dictionary<int, ListenerNode>();
            _deletes = new List<ListenerNode>();
        }

        public override void Update()
        {
            Proofread();
            foreach (var node in _nodes.Values)
                node.Update();
        }

        public override void Shut()
        {
            base.Shut();
        }

        /// <summary>
        /// Act task add.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="action"></param>
        public void Add(ListenerNode node)
        {

            if (node != null && !_nodes.ContainsKey(node.Id))
            {
                _nodes?.Add(node.Id, node);
                node.Start(this);
            }
            else
                Debug.Log("node id repeat, can't manage it by this way.");
        }

        /// <summary>
        /// Act task remove by id.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            if (_nodes.TryGetValue(id, out var node))
                Remove(node);
            else
                Debug.Log("node no exist.");
        }
        /// <summary>
        /// Acr task remove
        /// </summary>
        /// <param name="node"></param>
        public void Remove(ListenerNode node)
        {
            // 空实例不添加
            if (node != null)
            {
                _dashCount++;
                _deletes.Add(node);
            }
        }

        /// <summary>
        /// Action get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ListenerNode Node(int id) => this[id];
        /// <summary>
        /// Action indexer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ListenerNode this[int id]
        {
            get
            {
                ListenerNode node = default;
                if (!(bool)_nodes?.TryGetValue(id, out node))
                    Debug.Log($"listener nodes no exist this instance id = {id}.");
                return node;
            }
        }

        /// <summary>
        /// Before update instance node, remove dash node instance.
        /// this is an proofread function.
        /// </summary>
        private void Proofread()
        {
            if (_dashCount <= 0) return;
            foreach (var node in _deletes)
                if (_nodes.ContainsKey(node.Id))
                    _nodes.Remove(node.Id);
            _deletes.Clear();
            _dashCount = 0;
        }
    }

    /// <summary>
    /// Des:
    ///     Listener node.
    /// </summary>
    public class ListenerNode
    {
        public int Id { private set; get; }
        public Action Act { private set; get; }
        private Listeners _listener;
        private bool _enable = false;

        public ListenerNode(int id, Action act)
        {
            Id = id;
            Act = act;
        }

        /// <summary>
        /// while register to listener finished, then start it.
        /// </summary>
        public void Start(Listeners listener)
        {
            if (_enable)
            {
                Debug.Log("listener node already start enabled.");
                return;
            }
            _listener = listener;
            _enable = true;
        }
        /// <summary>
        /// auto update by listeners trigger.
        /// </summary>
        public void Update()
        {
            if (_enable)
                Act?.Invoke();
        }
        /// <summary>
        /// Auto shut enable and remove self.
        /// be triggered by self logic execute finished.
        /// </summary>
        public void Shut()
        {
            _enable = false;
            _listener.Remove(this);
        }
    }
}



