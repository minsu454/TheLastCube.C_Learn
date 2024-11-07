using Common.Timer;
using UnityEngine;

public class ActiveAutoFalse : MonoBehaviour
{
    public float lifeTime = 1;          //지속시간
    Coroutine coroutine;

    private void OnEnable()
    {
        coroutine = StartCoroutine(CoTimer.Start(lifeTime, () => gameObject.SetActive(false)));
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
        coroutine = null;
    }
}