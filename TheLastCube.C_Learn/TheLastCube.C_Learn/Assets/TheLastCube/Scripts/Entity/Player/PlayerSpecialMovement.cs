using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialMovement : MonoBehaviour
{
    private Vector3 moveAfterPosition;
    private GameObject bottomGround;

    private PlayerController cubeController;
    public LayerMask groundlayerMask;
    private bool isMoving = false;

    public int maxCheckDistance = 5;

    private void Awake()
    {
        cubeController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cubeController.OnSpecialMoveEvent += SpecialMove;
    }

    private void SpecialMove(Vector2 direction)
    {
        if (isMoving)
        {
            //Debug.Log("Err Moving!");
            return;
        }
        //Debug.Log(direction);
        
        CheckRoad(direction);
    }

    private bool CheckNextWall(Vector2 direction)
    {
        Vector3 dir = new Vector3(direction.x, 0, direction.y);
        Ray ray = new Ray(transform.position, dir);
        Debug.DrawRay(transform.position, dir, Color.red);

        if (Physics.Raycast(ray, 1.5f, groundlayerMask))
        {
            return true;
        }

        return false;
    }

    private bool CheckNextGround(Vector2 direction)
    {
        Ray ray = new Ray(transform.position + (new Vector3(direction.x, 0, direction.y)), Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (Physics.Raycast(ray, 0.6f, groundlayerMask))
        {
            //cubeController.playerQuadController.BlockInteract(BlockInteractionType.None);
            return true;
        }

        return false;
    }

    private void CheckRoad(Vector2 direction)
    {
        for(int i = 0; i< maxCheckDistance ; i++)
        {
            if (CheckNextGround(direction) && !CheckNextWall(direction))
            {
                transform.position += new Vector3(direction.x,0,direction.y);
            }
            else
            {
                break;
            }
        }
    }
}
