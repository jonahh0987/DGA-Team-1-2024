using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Movement Params
    private float _horizontal;
    private float _speed = 5f;
    private float _jumpingPower = 16f;
    private bool _isFacingRight = false;

    private bool _canDoubleJump = true;
    private float _doubleJumpPower = 10f;

    private bool _isHoldingOn = false;
    private float _gravityScale;
    private const float MAX_HOLD_TIME = 1;
    private float _holdtime = 0;
    private FloatingStatusBar _holdStatusBar;

    private bool high = true;
    private float highCool = 3f;
    private float lastHigh = 0f;
    private float highJumpPower = 30f;

    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private GameObject HoldMeter;
    [SerializeField] private SpriteRenderer PlayerSprite;
    [SerializeField] private Animator Animator;


    private void Start()
    {
        //Store the base gravity scale
        _gravityScale = RB.gravityScale;
        _holdStatusBar = HoldMeter.GetComponent<FloatingStatusBar>();

    }


    // Update is called once per frame 
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        //Update the animation transitions
        Animator.SetFloat("Horizontal Speed", Mathf.Abs(_horizontal));

        if (IsGrounded())
        {
            // Reset double jump when grounded. 
            _canDoubleJump = true;
        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                RB.velocity = new Vector2(RB.velocity.x, _jumpingPower);
            } 
            else if (_canDoubleJump)
            {
                _canDoubleJump = false;
                RB.velocity = new Vector2(RB.velocity.x, _doubleJumpPower);
            }
           
        }

        // Start jump descent early when the jump button is released
        if (Input.GetButtonUp("Jump") && RB.velocity.y > 0f)
        {
            RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.H) && high)
        {
            RB.velocity = new Vector2(RB.velocity.x, highJumpPower);
            high = false;
            lastHigh = Time.time;
        }

        if (!high && Time.time > lastHigh + highCool)
        {
            high = true;
        }

        // Climbing
        HoldMeter.SetActive(_isHoldingOn);
        if (_isHoldingOn)
        {
            _holdtime += Time.deltaTime;
            _holdStatusBar.SetCurrentValue(MAX_HOLD_TIME - _holdtime, MAX_HOLD_TIME);

            if (_holdtime > MAX_HOLD_TIME)
            {
                RB.gravityScale = _gravityScale;
                _isHoldingOn = false;
            }
        }

        Flip();
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(_horizontal * _speed, RB.velocity.y);

    }


    // Returns true if the player is on the ground.
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }

    // Flip the character sprite the face the direction of movement. 
    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f) 
        { 
            _isFacingRight = !_isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Check for any collisions with the player. 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lethal"))
        {
            GameManager.Instance.ResetScene();  
        }

        if (collision.gameObject.CompareTag("Climbable"))
        {
            RB.velocity = new Vector2(RB.velocity.x, 0);
            RB.gravityScale = 0;

            _isHoldingOn = true;
            _holdtime = 0;
            _canDoubleJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Climbable"))
        {
            RB.gravityScale = _gravityScale;
            _isHoldingOn = false;
            _canDoubleJump = true;
        }
    }
}
