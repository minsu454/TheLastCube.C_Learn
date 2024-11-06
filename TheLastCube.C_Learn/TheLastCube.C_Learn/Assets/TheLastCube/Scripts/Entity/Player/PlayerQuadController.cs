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

        Debug.Log(CheckBottom());

        BlockInteractionType blockInteractionType = mapBlock.Data.InteractionType;
        if (blockInteractionType == BlockInteractionType.None) return;//상호작용 내용이 없으면

        if(blockInteractionType == BlockInteractionType.Delete)
        {
            if (quads[index].playerQuadType != BlockInteractionType.None)
            {
                UseEffect();
                quads[index].ResetQuad();
            }
            
            return;
        }

        if ((int)blockInteractionType >= 100)
        {
            if((int)quads[index].playerQuadType - 10 == (int)blockInteractionType - 100)//enum이 하나라도 바뀌면 바뀌어야한다.
            {
                //특별 상호작용 발동
                //BlockInteraction();
                UseEffect((int)blockInteractionType - 100);
                quads[index].ResetQuad();
                GameManager.Instance.MapBlockEventAction(mapBlock);
            }
        }
        else if ((int)blockInteractionType >= 10)
        {
            //상호작용
            UseEffect((int)blockInteractionType-10);
            quads[index].ChangeQuadRenderer(blockInteractionType);
        }
    }

    public int CheckBottom()
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
                playerController.Effect.transform.localPosition = new Vector3(0,0,-0.49f);
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
        var mainModuul = playerController.Effect.GetComponent<ParticleSystem>().main;
        mainModuul.startColor = effectColor;

        playerController.Effect.SetActive(true);
    }
}

