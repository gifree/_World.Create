//using _World.Create;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MenuRotController : MonoBase
//{

//    // radius
//    private float _radius = 0;
//    // radian between two cell.
//    private float _radian = 0;
//    // offset distance to bg edge.
//    [SerializeField, Header("边缘距离偏差值")]
//    private float _offsetDis;
//    // start angle 
//    [SerializeField, Header("初始角度偏差值")]
//    private float _offsetAngle = 90f;
//    // move stay time
//    [SerializeField, Header("移动持续时间"), Range(0.1f, 0.6f)]
//    private float DurTime = 0.30f;

//    // target angle to rot.
//    private float _tarAngle = 0f;
//    // move speed.
//    private float _velocity = 0f;
//    // move counter.
//    private float _counter = 0f;
//    // rot dir.
//    private int _rotDir = 1;
//    // cells of child to move.
//    private List<CellBase> _cells;
//    // move ctrl signal.
//    private bool _rot = false;

//    private ListenerHandler _handlerRotDurig = null;

//    private void Start()
//    {
//        _cells = MenuLogicController.Self.Cells;
//        var rect = GetComponent<RectTransform>().rect;
//        _radius = (rect.width > rect.height ? rect.height : rect.width) / 2 - _offsetDis;
//        _radian = 360f / _cells.Count;
//        RefreshCells();
//    }

//    private void FixedUpdate()
//    {
//        if (!_rot)
//        {
//            if (Input.GetKeyDown(KeyCode.W)) RotStart(1);
//            else if (Input.GetKeyDown(KeyCode.S)) RotStart(-1);
//        }
//    }

//    public void RotStart(int dir = 1)
//    {
//        _tarAngle = 360f / _cells.Count;
//        _velocity = _tarAngle / DurTime * Time.fixedDeltaTime;
//        _rotDir = dir;
//        _rot = true;

//        Action act = new
//             Action(() => {
//                 if (_rot)
//                 {
//                     if (_counter + _velocity > _tarAngle)
//                     {
//                         _velocity = _tarAngle - _counter;
//                         _rot = false;
//                     }
//                     RotDuring();
//                     if (!_rot)
//                         RotOver();
//                 }
//             });

//        if (!(bool)Listerners?.Add(
//             _handlerRotDurig ?? (_handlerRotDurig = new ListenerHandler((int)ListenerHandlerId.HandlerMenuRotControllerWithRotDuring, act))))
//            _rot = false;
//        else
//            MenuLogicController.Self.Switch(_rotDir);


//    }

//    private void RotDuring()
//    {
//        _counter += _velocity;
//        transform.Rotate(Vector3.forward, _rotDir * _velocity);
//        foreach (var cell in _cells)
//            cell.RotDuring(-_rotDir, _velocity);
//    }

//    private void RotOver()
//    {
//        Listerners.Remove(_handlerRotDurig);
//        _velocity = 0f;
//        _rot = false;
//        _counter = 0f;
//        _tarAngle = 0f;
//    }

//    /// <summary>
//    /// cells refresh operation.
//    /// </summary>
//    public void RefreshCells()
//    {
//        var start = _offsetAngle;
//        foreach (var cell in _cells)
//        {  
//            // calculate radians.
//            float radian = 1.0f * start / Mathf.Rad2Deg;
//            // set local pos.
//            cell.RefreshPos(new Vector2(_radius * (float)Math.Cos(radian), _radius * (float)Math.Sin(radian)));
//            // add angle.
//            start += _radian;
//        }
//    }

//}
