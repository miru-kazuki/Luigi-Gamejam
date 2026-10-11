using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Item Interaction")]
    [SerializeField] private TransferableItem transferableItem;

    [Header("Status Player")]
    public bool isFacingRight = true;
    public bool hasJimat = false;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer; // Variabel penampung SpriteRenderer

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Ambil komponen SpriteRenderer
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // --- Fitur Flip Sprite & Update Arah Hadap ---
        if (moveInput.x > 0)
        {
            isFacingRight = true;
            if (spriteRenderer != null) spriteRenderer.flipX = false; // Hadap kanan
            // Alternatif jika tanpa SpriteRenderer: transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            isFacingRight = false;
            if (spriteRenderer != null) spriteRenderer.flipX = true; // Hadap kiri (flip)
            // Alternatif jika tanpa SpriteRenderer: transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Interact();
        }
    }

    protected virtual void Interact()
    {
        if (transferableItem != null)
        {
            transferableItem.TryTransferItem(transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }
}