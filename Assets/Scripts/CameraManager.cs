using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Vector3 initialOffset;
    [SerializeField] private Vector3 offset;
    public float distance = 5f;

    private float angle;
    [Range(0,5)] public float speed = 0.7f;
    public float smoothness = 2.5f;
    
    [Header("Settings")] 
    public bool smoothed;

    private void Awake()
    {
        offset = initialOffset;
    }

    private void Update()
    {
        FollowPlayer();
    }
    
    private void FollowPlayer()
    {
        angle += PlayerManager.GetCameraInput().x * speed * Time.deltaTime;
        
        offset.x = Mathf.Sin(angle) * distance;
        offset.z = Mathf.Cos(angle) * distance;
        
        Vector3 desiredPosition = PlayerManager.instance.GetControlledEntity().position - offset;
        
        transform.position = smoothed
            ? Vector3.Lerp(transform.position, desiredPosition, smoothness * Time.deltaTime)
            : desiredPosition;
        
        Vector3 direction = PlayerManager.instance.GetControlledEntity().position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(direction);
        transform.rotation = desiredRotation;
    }
}
