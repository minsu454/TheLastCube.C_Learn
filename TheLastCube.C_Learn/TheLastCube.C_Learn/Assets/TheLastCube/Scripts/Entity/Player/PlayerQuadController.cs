using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuadController : MonoBehaviour
{
    public Quad[] quads = new Quad[6];
    public int index = -1;

    public void BlockInteract(BlockInteractionType blockInteractionType)
    {
        Debug.Log(CheckBottom());

        if ((int)blockInteractionType >= 10)
        {
            //특별 상호작용
            quads[index].ChangeQuadRenderer(blockInteractionType);
            Debug.Log($"interact10 {quads[index].playerQuadType}");
        }
        else if ((int)blockInteractionType >= 100)
        {
            if(quads[index].playerQuadType == blockInteractionType)//enum이 하나라도 바뀌면 바뀌어야한다.
            {
                //상호작용 발동
                Debug.Log($"interact100 {quads[index].playerQuadType}");
            }
        }
    }

    public int CheckBottom()
    {
        Vector3 DU = transform.rotation * Vector3.down;//순서도 중요하다. 회전값에 벡터를 곱할 수 있지만, 벡터에 회전 값을 곱할 순 없다.
        Vector3 RL = transform.rotation * Vector3.right;
        Vector3 FB = transform.rotation * Vector3.forward;

        Debug.Log(Vector3.Dot(Vector3.down, DU));
        Debug.Log(Vector3.Dot(Vector3.right, RL));
        Debug.Log(Vector3.Dot(Vector3.forward, FB));

        //6축 중에 어디가 아래 방향인지 확인
        if(Vector3.Dot(Vector3.down, DU) > 0.9f) 
        {
            index = 3;
        }
        else if(Vector3.Dot(Vector3.up, DU) > 0.9f)
        {
            index = 2;
        }
        else if(Vector3.Dot(Vector3.down, RL) > 0.9f) 
        {
            index = 5;
        }
        else if(Vector3.Dot(Vector3.up, RL) > 0.9f) 
        { 
            index = 4;
        }
        else if(Vector3.Dot(Vector3.down, FB) > 0.9f) 
        {
            index = 1;
        }
        else if(Vector3.Dot(Vector3.up, FB) > 0.9f) 
        {
            index = 0;
        }
        else
        {
            Debug.LogError("index error");
            return -1;
        }
        return index;
    }
}