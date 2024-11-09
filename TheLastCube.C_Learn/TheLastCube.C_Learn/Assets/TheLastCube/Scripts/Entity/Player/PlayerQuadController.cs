using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerQuadController : MonoBehaviour
{
    private PlayerController playerController;

    public Quad[] quads = new Quad[6];
    public int index = -1;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();    
    }

    public void BlockInteract(MapBlock mapBlock)
    {
        if(mapBlock == null) return;

        CheckBottom();

        BlockInteractionType blockInteractionType = mapBlock.Data.InteractionType;//무슨 블록인지 확인
        if (blockInteractionType == BlockInteractionType.None) return;//상호작용 내용이 없으면
        //아래 코드도 구림 자세히는 보지 말 것
        if(blockInteractionType == BlockInteractionType.Delete)
        {
            if (quads[index].playerQuadType != BlockInteractionType.None)
            {
                UseEffect();
                quads[index].ResetQuad();
            }
            
            return;
        }

        if ((int)blockInteractionType >= 100)//Lock종류에 해당하는 경우
        {
            if((int)quads[index].playerQuadType - 10 == (int)blockInteractionType - 100)//같은 색상인지 확인 -> enum을 같은 10,11,12 / 100,101,102 로 만들어서 가능
            {
                //특별 상호작용 발동
                //BlockInteraction();
                UseEffect((int)blockInteractionType - 100);
                quads[index].ResetQuad();
                GameManager.Instance.MapBlockEventAction(mapBlock);// 특정 상호작용은 다른 사람이 만들어 놓은 이벤크에 구독만 해놓아서 사용
            }//발동한 상호작용의 종류만을 전달해주고 그 값을 토대로 기능을 구현하는 것은 가른 곳
        }
        else if ((int)blockInteractionType >= 10)// 색상을 입히기만 하느 블록이면
        {
            //상호작용
            UseEffect((int)blockInteractionType-10); //lock과 비슷하게 숫자를 맞춤
            quads[index].ChangeQuadRenderer(blockInteractionType);
        }
    }

    public int CheckBottom()//밥 아저씨 출동
    {
        Vector3 DU = transform.rotation * Vector3.down;//순서도 중요하다. 회전값에 벡터를 곱할 수 있지만, 벡터에 회전 값을 곱할 순 없다.
        Vector3 RL = transform.rotation * Vector3.right;
        Vector3 FB = transform.rotation * Vector3.forward;

        //Debug.Log(Vector3.Dot(Vector3.down, DU));
        //Debug.Log(Vector3.Dot(Vector3.right, RL));
        //Debug.Log(Vector3.Dot(Vector3.forward, FB));

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

    public int CheckUP()
    {
        Vector3 DU = transform.rotation * Vector3.down;
        Vector3 RL = transform.rotation * Vector3.right;
        Vector3 FB = transform.rotation * Vector3.forward;
        
        if (Vector3.Dot(Vector3.down, DU) < -0.9f)
        {
            index = 3;
        }
        else if (Vector3.Dot(Vector3.up, DU) < -0.9f)
        {
            index = 2;
        }
        else if (Vector3.Dot(Vector3.down, RL) < -0.9f)
        {
            index = 5;
        }
        else if (Vector3.Dot(Vector3.up, RL) < -0.9f)
        {
            index = 4;
        }
        else if (Vector3.Dot(Vector3.down, FB) < -0.9f)
        {
            index = 1;
        }
        else if (Vector3.Dot(Vector3.up, FB) < -0.9f)
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

    public void UseEffect(int color = -1)
    {
        int index = CheckBottom();

        switch (index)
        {
            case 0:
                playerController.Effect.transform.localPosition = new Vector3(0,0,-0.49f); // 파티클 변경 시 gpt 형이라 이렇게 해야함 2
                break;
            case 1:
                playerController.Effect.transform.localPosition = new Vector3(0, 0, 0.49f);
                break;
            case 2:
                playerController.Effect.transform.localPosition = new Vector3(0, 0.49f, 0);
                break;
            case 3:
                playerController.Effect.transform.localPosition = new Vector3(0, -0.49f, 0);
                break;
            case 4:
                playerController.Effect.transform.localPosition = new Vector3(-0.49f, 0, 0);
                break;
            case 5:
                playerController.Effect.transform.localPosition = new Vector3(0.49f, 0, 0);
                break;
            default:
                break;
        }

        Color effectColor = new Color(); 
        switch (color)
        {
            case -1: effectColor = Color.white; break;
            case 0: effectColor = Color.red; break;
            case 1: effectColor = Color.blue; break;
            case 2: effectColor = Color.yellow; break;
            default: effectColor = Color.black; break;
        }
        var mainModuul = playerController.Effect.GetComponent<ParticleSystem>().main; // 파티클 변경 시 gpt 형이라 이렇게 해야함 1
        mainModuul.startColor = effectColor;

        playerController.Effect.SetActive(true);
    }
}

