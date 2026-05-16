using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    private Rigidbody rb;
    public float jumpForce = 5f;
    private bool isGrounded = true;

    [Header("Animações")]
    private Animator playerAnim;
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    private void OnJump(InputValue value)
    {
        if(isGrounded && value.isPressed)
        {
            playerAnim.SetTrigger("Jump_trig");

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            
        }
    }
}
