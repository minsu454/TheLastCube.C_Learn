using Common.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MapBlock
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject animatorGo;
    [SerializeField] private BoxCollider myCollider;

    private Coroutine coroutine;
    
    void Start()
    {
        GroundRenderer.material = Managers.Material.Return(data.MoveType);
    }

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
        StopCoroutine(coroutine);

        GroundRenderer.enabled = true;
        myCollider.enabled = true;
        MoveRenderer.enabled = true;
        animatorGo.SetActive(false);
    }
}
