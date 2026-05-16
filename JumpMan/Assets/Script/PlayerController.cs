using System.Collections.Generic;
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

    [Header("Physics")]
    public float gravityModifier = 1f;

    [Header("Particles")]
    public ParticleSystem explosionparticle;
    public ParticleSystem particleLeft;
    public ParticleSystem particleRight;

    [Header("Audio")]
    public AudioSource _audioSource;
    public List<AudioClip> _sounds;
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * (gravityModifier -1) * Physics.gravity.magnitude, ForceMode.Acceleration);
    }

    private void OnJump(InputValue value)
    {
        if(isGrounded && value.isPressed && !SpawnManager.Instance.IsGameOver)
        {
            _audioSource.PlayOneShot(_sounds[0]);
            particleRight.Stop();
            particleLeft.Stop();
          
            playerAnim.SetTrigger("Jump_trig");

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            particleLeft.Play();
            particleRight.Play();
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
            explosionparticle.Play();
            _audioSource.PlayOneShot(_sounds[1]);
            particleLeft.Stop();
            particleRight.Stop();
           
            SpawnManager.Instance.IsGameOver = true;
            Destroy(collision.gameObject);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }
}
