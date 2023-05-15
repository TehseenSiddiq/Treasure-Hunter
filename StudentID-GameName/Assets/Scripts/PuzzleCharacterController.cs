using UnityEngine;

public class PuzzleCharacterController : MonoBehaviour
{
    public float speed = 5.0f;      // The speed at which the player moves.
    public float jumpForce = 10.0f; // The force with which the player jumps.
    public float groundDistance = 0.2f; // The distance from the player's center to the ground.
    public LayerMask groundMask;    // The layer(s) that represent the ground.

    public float sensitivity = 2.0f; // The sensitivity of the mouse rotation.
    public float minY = -60.0f;     // The minimum y rotation angle.
    public float maxY = 60.0f;      // The maximum y rotation angle.

    private float rotationY = 0.0f; // The current y rotation angle.

    private Vector3 movement;       // The movement vector.
    private bool isGrounded;        // Whether the player is grounded.
    public Transform groundCheck;
    private Rigidbody rb;           // The player's rigidbody component.
    public Transform startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the horizontal and vertical input axes.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Construct the movement vector based on the input axes.
        movement = new Vector3(horizontal, 0.0f, vertical);

        // Normalize the movement vector if its magnitude is greater than 1.
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
        Debug.Log("Grounded " + isGrounded);
        // Jump if the player is grounded and the jump key is pressed.
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
             rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           // transform.Translate(new Vector3(0, jumpForce, 0) * Time.deltaTime);

            isGrounded = false;
        }
        transform.Translate(movement * speed * Time.deltaTime);

        // Rotate the player based on mouse input.
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0.0f);
    }

    void FixedUpdate()
    {
        // Move the player based on the movement vector.
      //  rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);

        // Check if the player is grounded.
        isGrounded = Physics.Raycast(groundCheck.position, -Vector3.up, groundDistance, groundMask);
        Debug.DrawRay(groundCheck.position, -Vector3.up, Color.green);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("Restart");
            transform.position = startPos.position;
        }
    }
}