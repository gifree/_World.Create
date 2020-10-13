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
    /// </summary>
    public sealed class Listeners : World
    {

        private Dictionary<int, ListenerNode> _nodes;
        private List<ListenerNode> _deletes;

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

            if (!_nodes.ContainsKey(node.Id))
                _nodes?.Add(node.Id, node);
            else
                Debug.Log("node id repeat, can't manage it by this way.");
        }

        /// <summary>
        /// Act task remove.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            if (_nodes.TryGetValue(id, out var node))
                Remove(node);
            else
                Debug.Log("node no exist.");
        }

        public void Remove(ListenerNode node) => _deletes.Add(node);

        

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


        private void Proofread()
        {
            if (_deletes.Count > 0)
            {
                foreach (var node in _deletes)
                    if (_nodes.ContainsKey(node.Id))
                        _nodes.Remove(node.Id);
                _deletes.Clear();
            }
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

        public ListenerNode(int id, Action act)
        {
            Id = id;
            Act = act;
        }

        public void Update() => Act?.Invoke();
    }
}


