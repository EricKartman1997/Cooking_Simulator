using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private HeroikConfig heroikConfig;
    private GameInput _gameInput;

    //private bool canMove;
    private Vector3 moveDir;
    private Rigidbody _rb;
    
    private float Speed => heroikConfig.MoveConfig.MoveSpeed;
    private float Distance => heroikConfig.MoveConfig.Distance;
    private Vector3 HalfExtence => heroikConfig.MoveConfig.HalfExtence;
    
    private void Awake()
    {
        _gameInput = GetComponent<GameInput>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        //Vector3 moveDir = new Vector3(inputVector.x,0f, inputVector.y);
        moveDir = new Vector3(inputVector.x,0f, inputVector.y);
        
        bool canMove = !Physics.BoxCast(transform.position, HalfExtence,moveDir,Quaternion.identity,Distance);
        //canMove = !Physics.BoxCast(transform.position, HalfExtence,moveDir,Quaternion.identity,Distance);
        if (canMove == false)
        {
            Vector3 moveDirX = new Vector3(moveDir.x,0f, 0f).normalized;
            canMove = !Physics.BoxCast(transform.position, HalfExtence,moveDirX,Quaternion.identity,Distance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f,0f, moveDir.z).normalized;
                canMove = !Physics.BoxCast(transform.position, HalfExtence,moveDirZ,Quaternion.identity,Distance);
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
            transform.position += moveDir * Time.deltaTime * Speed;
        }
        
        // менять направление движения тут transform.forward
        transform.forward = Vector3.Slerp(transform.forward, moveDir, heroikConfig.MoveConfig.RotateSpeed * Time.deltaTime);
    }

    // private void OnDrawGizmos()
    // {
    //     Vector3 center1 = transform.position;
    //     Vector3 halfExtents = HalfExtence * 2 ;
    //     Vector3 direction = transform.forward;
    //     float distance = Distance;
    //
    //     Gizmos.color = Color.red;
    //     //Gizmos.DrawWireCube(center, halfExtents / 2); // Начальная позиция куба
    //     Gizmos.DrawWireCube(center1 + direction * distance, halfExtents); // Конечная позиция
    //     Gizmos.DrawLine(center1, center1 + direction  * distance);
    // }
}
