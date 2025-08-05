using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HeroikConfig heroikConfig;
    private GameInput _gameInput;
    //private bool isWalking;
    
    private float Speed => heroikConfig.MoveConfig.MoveSpeed;
    private float Distance => heroikConfig.MoveConfig.Distance;
    
    private void Awake()
    {
        _gameInput = GetComponent<GameInput>();
    }

    void Update()
    {

        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x,0f, inputVector.y);

        float moveDistance = Speed * Time.deltaTime;
        
        bool canMove = !Physics.BoxCast(transform.position, new Vector3(0, 0, 0),moveDir,Quaternion.identity,Distance);
        if (canMove == false)
        {
            Vector3 moveDirX = new Vector3(moveDir.x,0f, 0f).normalized;
            canMove = !Physics.BoxCast(transform.position, new Vector3(0, 0, 0),moveDirX,Quaternion.identity,Distance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f,0f, moveDir.z).normalized;
                canMove = !Physics.BoxCast(transform.position, new Vector3(0, 0, 0),moveDirZ,Quaternion.identity,Distance);
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
        
        //isWalking = moveDir != Vector3.zero;
        
        // менять направление движения тут transform.forward
        transform.forward = Vector3.Slerp(transform.forward, moveDir, heroikConfig.MoveConfig.RotateSpeed * Time.deltaTime);
    }

}
