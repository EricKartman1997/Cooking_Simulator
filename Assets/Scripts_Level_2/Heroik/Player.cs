using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    void Update()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x,0f, inputVector.y);

        float moveDistance = speed * Time.deltaTime;
        float playerRadius = 1.4f;
        float playerHeight = 0.3f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir,moveDistance);    
        
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x,0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX,moveDistance); 
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f,0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ,moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    Debug.Log("не может двигаться");
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        //transform.position += moveDir * Time.deltaTime * speed;
        
        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.right = Vector3.Slerp(transform.right, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
