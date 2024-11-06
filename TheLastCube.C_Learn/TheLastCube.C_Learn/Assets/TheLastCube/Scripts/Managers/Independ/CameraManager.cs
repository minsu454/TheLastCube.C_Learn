using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform targetTr;
    private Vector3 pivot = new Vector3(0, 10, -10);

    public void Init(GameObject target)
    {
        targetTr = target.transform;
    }

    private void LateUpdate()
    {
        transform.position = targetTr.position + pivot;
    }
}
