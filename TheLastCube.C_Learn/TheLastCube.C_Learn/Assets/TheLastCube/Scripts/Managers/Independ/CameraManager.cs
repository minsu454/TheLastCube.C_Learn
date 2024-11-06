using Common.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform targetTr;
    private List<Vector3> pivot = new List<Vector3>();

    private int pivotIdx;

    public void Init(GameObject target)
    {
        targetTr = target.transform;

        PlayerController controller = target.GetComponent<PlayerController>();

        controller.OnMoveEvent += ResetCameraViewPoint;

        pivot.Add(new Vector3(0, 10, -10));
        pivot.Add(new Vector3(-10, 10, 0));
        pivot.Add(new Vector3(0, 10, 10));
        pivot.Add(new Vector3(10, 10, 0));

        EventManager.Subscribe(GameEventType.ChangeViewPoint, OnChangeViewPoint);
    }

    private void LateUpdate()
    {
        transform.position = targetTr.position + pivot[pivotIdx];
    }

    private void ResetCameraViewPoint(Vector2 vec)
    {
        pivotIdx = 0;
        transform.position = targetTr.position + pivot[pivotIdx];
        transform.LookAt(targetTr);
    }

    private void OnChangeViewPoint(object args)
    {
        pivotIdx = ++pivotIdx % pivot.Count;
        transform.position = targetTr.position + pivot[pivotIdx];
        transform.LookAt(targetTr);
    }
}
