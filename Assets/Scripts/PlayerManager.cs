using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    private PlayerInputs inputs;
    
    [SerializeField] private Transform controlled;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 moveDirection;
    private Vector3 cameraDirection;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        Initialize();
    }

    private void Initialize()
    {
        inputs = new PlayerInputs();
        inputs.Enable();
    }

    private void Update()
    {
        ReadInputs();
    }

    private void ReadInputs()
    {
        moveDirection = new Vector3(inputs.Base.Move.ReadValue<Vector2>().x, 0, inputs.Base.Move.ReadValue<Vector2>().y);
        cameraDirection = new Vector3(inputs.Base.CameraRotation.ReadValue<Vector2>().x, 0, inputs.Base.CameraRotation.ReadValue<Vector2>().y);
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (moveDirection.magnitude != 0)
        {
            controlled.transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            
            Quaternion desiredRotation = Quaternion.LookRotation(moveDirection);
            controlled.transform.rotation = Quaternion.Lerp(controlled.transform.rotation, desiredRotation,
                rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs = null;
    }

    public Transform GetControlledEntity()
    {
        return controlled;
    }

    public static Vector3 GetCameraInput()
    {
        return instance.cameraDirection;
    }

    public static PlayerInputs GetInputs()
    {
        return instance.inputs;
    }

    public static Vector3 GetMoveInput()
    {
        return instance.moveDirection;
    }

    public static Vector3 GetControlledEntityForwardPosition()
    {
        return instance.controlled.position + instance.controlled.forward;
    }
}

