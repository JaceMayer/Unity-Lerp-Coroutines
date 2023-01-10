using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixedWaitForSeconds
{
    public float t;

    public FixedWaitForSeconds(float t)
    {
        this.t = t;
    }
}

public class FixedCoroutine
{
    private readonly IEnumerator _enumerator;
    public bool isSleeping;
    public DateTime sleepUntil;

    public FixedCoroutine(IEnumerator Enumerator)
    {
        _enumerator = Enumerator;
    }

    public bool moveOne()
    {
        if (sleepUntil < DateTime.Now && isSleeping) return true;
        isSleeping = false;
        var moved = _enumerator.MoveNext();
        if (!moved) return false;
        if (_enumerator.Current != null && _enumerator.Current.GetType() == typeof(FixedWaitForSeconds))
        {
            var wait = (FixedWaitForSeconds)_enumerator.Current;
            sleepUntil = DateTime.Now.AddSeconds(wait.t);
            isSleeping = true;
        }

        return true;
    }
}

public class CoroutineMgr : MonoBehaviour
{
    public static CoroutineMgr _CoroutineMgr;
    private readonly List<FixedCoroutine> coroutines = new();

    public void Awake()
    {
        if (_CoroutineMgr != null) Debug.LogError("CoroutineMgr is a singleton, and should only be instantated once.");
        _CoroutineMgr = this;
        DontDestroyOnLoad(this);
    }

    public void FixedUpdate()
    {
        // Run through each active coroutine and move them by one.
        // Also removes any coroutine that has finished
        foreach (var coroutine in coroutines.ToList())
        {
            var hasMore = coroutine.moveOne();
            if (!hasMore) coroutines.Remove(coroutine);
        }
    }

    public new FixedCoroutine StartCoroutine(IEnumerator coroutine)
    {
        var routine = new FixedCoroutine(coroutine);
        coroutines.Add(routine);
        return routine;
    }

    public void StopCoroutine(FixedCoroutine coroutine)
    {
        coroutines.Remove(coroutine);
    }
}