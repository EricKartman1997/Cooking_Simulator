using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private HeroikConfig heroikConfig;

    private bool isWalking;
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x,0f, inputVector.y);

        float moveDistance = heroikConfig.MoveConfig.MoveSpeed * Time.deltaTime;
        
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * heroikConfig.MoveConfig.Height, heroikConfig.MoveConfig.Radius, moveDir,moveDistance);    
        
        if (canMove == false)
        {
            Vector3 moveDirX = new Vector3(moveDir.x,0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * heroikConfig.MoveConfig.Height, heroikConfig.MoveConfig.Radius, moveDirX,moveDistance); 
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f,0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * heroikConfig.MoveConfig.Height, heroikConfig.MoveConfig.Radius, moveDirZ,moveDistance);
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
        
        transform.right = Vector3.Slerp(transform.right, moveDir, heroikConfig.MoveConfig.RotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
