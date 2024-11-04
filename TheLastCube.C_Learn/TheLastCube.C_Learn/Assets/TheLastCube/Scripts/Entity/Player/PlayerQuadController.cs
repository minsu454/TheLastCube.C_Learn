using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerQuadType
{
    None = 0,

    Red = 10,
    Blue
}


public class PlayerQuadController : MonoBehaviour
{
    public Quad[] quads = new Quad[6];
    public int index = -1;

    private float lastCheckTime = 0f;
    private float lastCheckDistance = 5f;

    private void Update()
    {
        if(Time.time - lastCheckTime < lastCheckDistance)
        {
            return;
        }
        lastCheckTime = Time.time;

        Vector3 DU = transform.rotation * Vector3.down;//순서도 중요하다. 회전값에 벡터를 곱할 수 있지만, 벡터에 회전 값을 곱할 순 없다.
        Vector3 RL = transform.rotation * Vector3.right;
        Vector3 FB = transform.rotation * Vector3.forward;

        Debug.Log(Vector3.Dot(Vector3.down, DU));// 1이면 index 3, -1이면 index 2
        Debug.Log(Vector3.Dot(Vector3.down, RL));// 1이면 index 5, -1이면 index 4
        Debug.Log(Vector3.Dot(Vector3.down, FB));// 1이면 index 1, -1이면 index 0
    }

    public void BlockInteract(BlockInteractionType blockInteractionType)
    {
        if ((int)blockInteractionType >= 100)
        {
            if((int)quads[index].playerQuadType - 10 == (int)blockInteractionType - 100)//enum이 하나라도 바뀌면 바뀌어야한다.
            {
                //상호작용 발동
                Debug.Log($"interact {quads[index].playerQuadType}");
            }
        }
        if ((int)blockInteractionType >= 10)
        {
            //quads[index].playerQuadType = (BlockInteractionType)((int)blockInteractionType - 10);
        }
    }

    public int CheckBottom()
    {
        Vector3 DU = transform.rotation * Vector3.down;//순서도 중요하다. 회전값에 벡터를 곱할 수 있지만, 벡터에 회전 값을 곱할 순 없다.
        Vector3 RL = transform.rotation * Vector3.right;
        Vector3 FB = transform.rotation * Vector3.forward;

        if(Vector3.Dot(Vector3.down, DU) > 0.99f) 
        {
            index = 3;
        }
        if(Vector3.Dot(Vector3.up, DU) > 0.99f)
        {
            index = 2;
        }
        if(Vector3.Dot(Vector3.right, RL) > 0.99f) 
        {
            index = 5;
        }
        if(Vector3.Dot(Vector3.left, RL) > 0.99f) 
        { 
            index = 4;
        }
        if(Vector3.Dot(Vector3.forward, FB) > 0.99f) 
        {
            index = 1;
        }
        if(Vector3.Dot(Vector3.back, FB) > 0.99f) 
        {
            index = 0;
        }

        return index;
    }
}
