using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Timer : IDisposable
    {
        #region STATIC

        public static Timer SecondsTimer { get; private set; }
        
        private static HashSet<Timer> timers = new();
        private static TimerCoroutineObject coroutineObject;

        public static Timer CreateTimer(float delay, float rate, bool loop)
        {
            if(coroutineObject == null)
                InstantiateTimerCoroutineObject();

            var timer = new Timer(delay, rate, loop);
            timers.Add(timer);
            
            return timer;
        }

        private static IEnumerator TimersTick()
        {
            while (Application.isPlaying)
            {
                foreach (var timer in timers)
                {
                    timer.Tick(Time.deltaTime);
                }
                yield return null;
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void InstantiateTimerCoroutineObject()
        {
            coroutineObject = new GameObject(nameof(TimerCoroutineObject)).AddComponent<TimerCoroutineObject>();
            coroutineObject.StartCoroutine(TimersTick());
            Object.DontDestroyOnLoad(coroutineObject.gameObject);
            
            CreateDefaultTimers();
        }

        private static void CreateDefaultTimers()
        {
            SecondsTimer = CreateTimer(1, 1, true);
        }
        
        #endregion

        public event Action<Timer> OnTimeout;

        private float timer;
        private float rate;
        public float Delay { get; private set; }
        public bool Loop { get; private set; }
        
        private Timer(float delay, float rate, bool loop)
        {
            timer = 0;
            this.rate = rate;
            Delay = delay;
            Loop = loop;
        }

        public void SetRate(float rate)
        {
            this.rate = rate;
        }

        private void Tick(float deltaTime)
        {
            timer += deltaTime * rate;
            
            if (!(timer > Delay))
                return;
            
            Notify();

            if (!Loop)
                Dispose();

            timer = 0;
        }

        private void Notify()
        {
            OnTimeout?.Invoke(this);
        }

        public void Dispose()
        {
            timers.Remove(this);
            OnTimeout = null;
        }
    }