using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class VRCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3.0f;
    public float gravity = -9.81f;

    [Header("Rotation Settings")]
    [Tooltip("Speed for continuous turning (degrees per second)")]
    public float turnSpeed = 60f;
    
    [Header("References")]
    public Transform cameraTransform;
    
    private CharacterController _characterController;
    private float _verticalVelocity = 0;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
        // Auto-find camera if missing
        if (cameraTransform == null)
        {
            var rig = GetComponentInChildren<OVRCameraRig>();
            if (rig != null) cameraTransform = rig.centerEyeAnchor;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // 1. Get Input (Left Controller)
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        // 2. Move relative to Camera looking direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        
        // Flatten Y so looking up doesn't make us fly
        forward.y = 0; 
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * input.y + right * input.x).normalized;

        // 3. Gravity Logic
        if (_characterController.isGrounded)
        {
            _verticalVelocity = -1f; // Apply slight downforce to stick to floor
        }
        else
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }

        // 4. Apply
        Vector3 finalMove = (moveDirection * moveSpeed) + (Vector3.up * _verticalVelocity);
        _characterController.Move(finalMove * Time.deltaTime);
    }

    void HandleRotation()
    {
        // 1. Get Input (Right Controller)
        Vector2 turnInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

        // Continuous Turn Logic
        // We use a small deadzone (0.1f) to prevent drift if the stick is slightly loose
        if (Mathf.Abs(turnInput.x) > 0.1f)
        {
            transform.Rotate(0, turnInput.x * turnSpeed * Time.deltaTime, 0);
        }
    }

    // Keeps the physics capsule centered on the player's real head position
    void LateUpdate()
    {
        if (cameraTransform == null) return;
        
        Vector3 capsuleCenter = transform.InverseTransformPoint(cameraTransform.position);
        _characterController.center = new Vector3(capsuleCenter.x, _characterController.center.y, capsuleCenter.z);
    }
}