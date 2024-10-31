using Common.Yield;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController cubeController;
    public LayerMask groundlayerMask;

    [SerializeField]private float rotateSpeed;
    public float rotateRate;

    private void Awake()
    {
        cubeController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        cubeController.OnMoveEvent += Move;
    }


    private void Move(Vector2 direction)
    {
        Debug.Log(direction);
        Vector3 ancher = transform.position + (new Vector3(direction.x, -1, direction.y)) * 0.5f;//회전시킬 위치
        Vector3 axis = Vector3.Cross(Vector3.up, new Vector3(direction.x, 0, direction.y));//두선과 수직인 회전시킬 축을 찾기
        Debug.Log($"axis :  {axis}");

        StartCoroutine(Roll(ancher, axis));
    }

    private bool CheckNextGround(Vector2 direction)
    {
        Ray ray = new Ray(transform.position + (new Vector3(direction.x, -0.5f, direction.y)), Vector3.down);
        //Debug.DrawRay(transform.position - (new Vector3(0,0.5f,0)), Vector3.down, Color.red);

        if (Physics.Raycast(ray, 0.1f, groundlayerMask))
        {
            return true;
        }
        return false;
    }

    IEnumerator Roll(Vector3 ancher, Vector3 axis)
    {
        cubeController.isMoving = true;
        rotateRate = 90 / rotateSpeed;

        for (int i = 0; i < rotateRate; i++)
        {
            transform.RotateAround(ancher, axis, rotateSpeed);//지정한 점을 통과하는 벡터를 중심으로 회전 
            yield return YieldCache.WaitForSeconds(0.01f);
        }

        cubeController.isMoving = false;
    }

    void CubeDown()
    {
        transform.position += Vector3.down;
    }
}
