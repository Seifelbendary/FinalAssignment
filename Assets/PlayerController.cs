/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] bool lockCursor = true;
    float cameraPitch = 0.0f;
    CharacterController controller = null;
    Vector3 velocity;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
       void Update()
       {
           UpdateMouseLook();
           UpdateMovement();



    }


    void UpdateMouseLook()
    {
        Vector2 currentMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
    void UpdateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (controller.isGrounded)
            velocity.y = 0.0f;
        velocity.y += gravity * Time.deltaTime;
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        Vector3 move = (transform.right * x) + (transform.forward * z) + (Vector3.up * velocity.y);
        controller.Move(move * Time.deltaTime * walkSpeed);
    }
   
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] bool lockCursor = true;
    float cameraPitch = 0.0f;
    CharacterController controller = null;
    Vector3 velocity;

    [SerializeField] Transform interactableObject = null;
    bool isObjectPickedUp = false;
    float objectDistance = 3f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        HandleObjectInteraction();
    }

    void UpdateMouseLook()
    {
        Vector2 currentMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (controller.isGrounded)
            velocity.y = 0.0f;
        velocity.y += gravity * Time.deltaTime;
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        Vector3 move = (transform.right * x) + (transform.forward * z) + (Vector3.up * velocity.y);
        controller.Move(move * Time.deltaTime * walkSpeed);
    }

    void HandleObjectInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isObjectPickedUp)
            {
                // Drop object
                interactableObject.parent = null;
                isObjectPickedUp = false;
            }
            else
            {
                // Check interactable object nearby
                Collider[] colliders = Physics.OverlapSphere(transform.position, objectDistance);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Interactable"))
                    {
                        // Pick up object
                        interactableObject = collider.transform;
                        interactableObject.parent = playerCamera;
                        interactableObject.localPosition = new Vector3(0f, 0f, objectDistance);
                        isObjectPickedUp = true;
                        break;
                    }
                }
            }
        }
    }
}
