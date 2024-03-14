using System;
using System.Collections;
using UnityEngine;


public class GameTimer : MonoBehaviour
{
    private TimeSpan currentTime;
    public TimeSpan CurrentTime => currentTime;


    private Coroutine timerCoroutine;


    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    public void StartTimer()
    {
        StopTimer();

        currentTime = new TimeSpan(0, 0, 0);
        timerCoroutine = StartCoroutine(DelayedTimer());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }


    private IEnumerator DelayedTimer()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);

            currentTime += new TimeSpan(0, 0, 1);
        }
    }
}
