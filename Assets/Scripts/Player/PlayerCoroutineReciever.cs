using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoroutineReciever : MonoBehaviour
{
    private Coroutine coroutine;

    public new Coroutine StartCoroutine(IEnumerator coroutine)
    {
        if (this.coroutine != null) return null;

        this.coroutine = base.StartCoroutine(coroutine);
        return this.coroutine;
    }

    public void StopCoroutine()
    {
        if (this.coroutine == null) return;

        StopCoroutine(coroutine);
        coroutine = null;
    }
}
