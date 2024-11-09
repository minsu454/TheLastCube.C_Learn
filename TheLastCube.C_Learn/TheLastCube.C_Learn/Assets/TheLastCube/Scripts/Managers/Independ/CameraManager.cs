using Common.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    private Transform targetTr;                             //타겟
    private List<Vector3> pivot = new List<Vector3>();      //카메라의 4방향 정보 저장 List

    private int pivotIdx;                                   //피봇 인덱스

    /// <summary>
    /// 초기화 함수
    /// </summary>
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

    /// <summary>
    /// 카메라 시점 리셋해주는 함수
    /// </summary>
    private void ResetCameraViewPoint(Vector2 vec)
    {
        pivotIdx = 0;
        transform.position = targetTr.position + pivot[pivotIdx];
        transform.LookAt(targetTr);
    }

    /// <summary>
    /// 카메라 시점 변환해주는 함수
    /// </summary>
    private void OnChangeViewPoint(object args)
    {
        if (EventSystem.current.IsPointerOverGameObject()) //UI 반환
            return;

        pivotIdx = ++pivotIdx % pivot.Count;
        transform.position = targetTr.position + pivot[pivotIdx];
        transform.LookAt(targetTr);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEventType.ChangeViewPoint, OnChangeViewPoint);
    }
}
