using System.Runtime.CompilerServices;
using UnityEngine;

public class MapCameraManager : MonoBehaviour
{
    [SerializeField] private Camera[] cameraArr;
    private int showCameraIndex = 0;

    /// <summary>
    /// 1인칭 카메라로 리셋해주는 함수
    /// </summary>
    public void ResetCamera()
    {
        cameraArr[showCameraIndex].depth = -10;
        showCameraIndex = 0;
        cameraArr[showCameraIndex].depth = 1;
    }

    /// <summary>
    /// 다음 리스트에 들어있는 카메라 들고오는 함수
    /// </summary>
    public void NextCamera()
    {
        cameraArr[showCameraIndex].depth = -10;

        showCameraIndex++;
        showCameraIndex %= cameraArr.Length;

        cameraArr[showCameraIndex].depth = 1;
    }
}