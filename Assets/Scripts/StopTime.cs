using System.Collections;
using UnityEngine;

public class StopTime : MonoBehaviour
{
    public static StopTime Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void TimeStop(float duration)
    {
        StartCoroutine(Stop_Time(duration));
    }

    IEnumerator Stop_Time(float duration)
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
    }

}
