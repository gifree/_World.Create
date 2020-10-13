using _World.Create;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _World.Tools
{
    public class Timers
    {
        private static List<TimerHandler> _handlers = new List<TimerHandler>();
        private static List<TimerHandler> _handlersAdd = new List<TimerHandler>();

        public static void Register(TimerHandler handler) => _handlersAdd.Add(handler);

        public static void Update()
        {
            if(_handlersAdd.Count != 0)
            {
                foreach (var handler in _handlersAdd)
                {
                    _handlers.Add(handler);
                    _handlersAdd.Clear();
                }
            }

            foreach (var handler in _handlers)
                handler?.Update();

            _handlers.RemoveAll(handler => handler.Done);
        }
    }
}

public class TimerHandler
{
    public int Id;
    private Action _complete;
    private Action _update;

    public bool Done;
    public bool Pause;
    public bool Loop;

    private float delay;
    private float dur;
    private float start;
    private float last;

    public TimerHandler(int id, Action complete, Action update, float delay, float dur)
    {
        start = TimeNow;
        last = start;
    }

    private float TimeNow => Time.time;
    private float TimeDelat => Time.time - last;
    private float TimeEnd => start + dur;


    public void Update()
    {
        if (Done) return;

        if(Pause)
        {
            start += TimeDelat;
            last = TimeNow;
            return;
        }

        last = TimeNow;

        _update?.Invoke();

        if(TimeEnd <= TimeNow)
        {
            _complete?.Invoke();

            if (Loop) start = TimeNow;
            else
                Done = true;
        }

    }
}
