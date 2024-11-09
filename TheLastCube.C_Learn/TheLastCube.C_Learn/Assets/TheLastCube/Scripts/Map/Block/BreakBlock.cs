using Common.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MapBlock
{
    [SerializeField] private GameObject animatorGo;
    [SerializeField] private BoxCollider myCollider;

    private Coroutine coroutine;
    
    void Start()
    {
        GroundRenderer.material = Managers.Material.Return(data.MoveType);
    }

    /// <summary>
    /// 블록 파괴
    /// </summary>
    public void Broken()
    {
        animatorGo.SetActive(true);
        GroundRenderer.enabled = false;
        MoveRenderer.enabled = false;
        myCollider.enabled = false;

        coroutine = StartCoroutine(CoTimer.Start(1f, () => gameObject.SetActive(false)));
    }

    private void OnDisable()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        GroundRenderer.enabled = true;
        myCollider.enabled = true;
        MoveRenderer.enabled = true;
        animatorGo.SetActive(false);
    }
}
