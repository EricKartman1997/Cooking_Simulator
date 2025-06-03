using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform pointUp;
    [SerializeField] private Transform pointDown;
    [SerializeField] private HeroikConfig heroikConfig;
    private GameInput _gameInput;

    private bool isWalking;

    private void Awake()
    {
        _gameInput = GetComponent<GameInput>();
    }

    void Update()
    {

        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x,0f, inputVector.y);

        float moveDistance = heroikConfig.MoveConfig.MoveSpeed * Time.deltaTime;
        
        //bool canMove = !Physics.CapsuleCast(pointUp.position, pointDown.position, heroikConfig.MoveConfig.Radius, moveDir,_distance);
        bool canMove = !Physics.BoxCast(transform.position, new Vector3(0, 0, 0),moveDir,Quaternion.identity,heroikConfig.MoveConfig.Distance);
        if (canMove == false)
        {
            Vector3 moveDirX = new Vector3(moveDir.x,0f, 0f).normalized;
            //canMove = !Physics.CapsuleCast(pointUp.position, pointDown.position, heroikConfig.MoveConfig.Radius, moveDirX,_distance); 
            canMove = !Physics.BoxCast(transform.position, new Vector3(0, 0, 0),moveDirX,Quaternion.identity,heroikConfig.MoveConfig.Distance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f,0f, moveDir.z).normalized;
                //canMove = !Physics.CapsuleCast(pointUp.position, pointDown.position, heroikConfig.MoveConfig.Radius, moveDirZ,_distance);
                canMove = !Physics.BoxCast(transform.position, new Vector3(0, 0, 0),moveDirZ,Quaternion.identity,heroikConfig.MoveConfig.Distance);
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
        
        isWalking = moveDir != Vector3.zero;
        
        // менять направление движения тут transform.forward
        transform.forward = Vector3.Slerp(transform.forward, moveDir, heroikConfig.MoveConfig.RotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
