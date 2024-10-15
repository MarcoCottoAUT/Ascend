using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    private Rigidbody2D RB;

    float MoveDirection;
    bool IsJumping = false;

    bool AwaitingLand = false;
    bool InJump = false;
    public string Direction = "Right";

    public GameObject Jumpbar;
    public Camera GameCamera;

    public GameObject BarPosition;
    public Animator JumpbarAnimator;
    private Animator PlayerAnimator;

    private Slider JumpbarSlider;
    private CapsuleCollider2D Collider;

    public SpriteRenderer SpriteRenderer;
    public PhysicsMaterial2D SlideMaterial;

    public PlayerGrounded PlayerGrounded;
    public AudioSource FallSound;

    private void Start()
    {
        Jumpbar.SetActive(false);
        JumpbarSlider = Jumpbar.GetComponent<Slider>();

        RB = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CapsuleCollider2D>();
        PlayerAnimator = GetComponent<Animator>();
    }

    private int GetGroundLayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.75f);

        if (hit)
        {
            return hit.transform.gameObject.layer;
        }

        return 0;
    }

    // Check if the player is on the floor
    private bool IsGrounded()
    {
        return RB.velocity.y == 0;
    }

    /*
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.75f);

        if (hit)
        {
            float YVelocity = Mathf.Clamp(RB.velocity.y, 0f, 1f);

            if (hit.transform.CompareTag("Floor") && YVelocity == 0)
            {
                return true;
            }
        }

        return false;
    }*/

    // Handle Movements Inputs
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
        if (IsGrounded())
        {
            MoveDirection = Input.GetAxis("Horizontal");
        }
        else
        {
            MoveDirection = 0;
        }

        // Move Jumpbar to be always on top of the player
        Vector2 viewportPosition = Camera.main.WorldToScreenPoint(BarPosition.transform.position);
        Jumpbar.transform.position = viewportPosition;

        // Determine move direction
        //if (!IsJumping)
        //{
            if (MoveDirection > 0)
            {
                Direction = "Right";
                SpriteRenderer.flipX = false;
            }
            else if (MoveDirection < 0)
            {
                Direction = "Left";
                SpriteRenderer.flipX = true;
            }
        //}

        // Handle Jumping Inputs
        if (IsGrounded())
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0)) && !IsJumping)
            {
                IsJumping = true;
                Jumpbar.SetActive(true);

                PlayerAnimator.SetBool("JumpCharge", true);
                JumpbarAnimator.SetBool("Jump", true);
                RB.velocity = Vector2.zero;
            }

            if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetMouseButtonUp(0)) && IsJumping)
            {
                JumpbarAnimator.SetBool("Jump", false);
                PlayerAnimator.SetBool("JumpCharge", false);

                Jumpbar.SetActive(false);
                PlayerAnimator.SetBool("InJump", true);

                InJump = true;
                Collider.sharedMaterial = SlideMaterial;
            }
        }

        // Check if the jump failed
        if (!IsGrounded() && Jumpbar.activeSelf)
        {
            JumpbarAnimator.SetBool("Jump", false);
            PlayerAnimator.SetBool("JumpCharge", false);

            Jumpbar.SetActive(false);
            PlayerAnimator.SetBool("InJump", true);

            InJump = true;
            Collider.sharedMaterial = SlideMaterial;
        }
    }

    // Handle Movement Physics
    private void FixedUpdate()
    {
        // Move camera based on player Y position
        /*
        float CameraDistance = transform.position.y - GameCamera.transform.position.y;

        if (CameraDistance >= 5) // Top of screen
        {
            GameCamera.transform.position = new Vector3(GameCamera.transform.position.x, GameCamera.transform.position.y + 10, -10);
        } else if (CameraDistance <= -5) // Bottom of screen
        {
            GameCamera.transform.position = new Vector3(GameCamera.transform.position.x, GameCamera.transform.position.y - 10, -10);
        }*/


        if (!IsJumping)
        {
            // Handle movement physics
            if (IsGrounded())
            {
                RB.velocity = new Vector2(MoveDirection * Time.fixedDeltaTime * MoveSpeed, RB.velocity.y);
            }
        } else
        {
            // Handle jump physics
            if (InJump)
            {
                float JumpPower = Mathf.Clamp(JumpbarSlider.value, 0.05f, 1) * 300;
                float ForwardForce = 0f;

                if (JumpbarSlider.value < 0.7f)
                {
                    ForwardForce = JumpPower * 1.1f + JumpbarSlider.value;
                } else
                {
                    ForwardForce = JumpPower;
                }

                if (Direction == "Left")
                {
                    RB.AddForce(new Vector2(-ForwardForce, JumpPower * 1.8f));
                }
                else
                {
                    RB.AddForce(new Vector2(ForwardForce, JumpPower * 1.8f));
                }

                InJump = false;
            }
        }

        if (RB.velocity.x != 0 && IsGrounded())
        {
            if (!PlayerAnimator.GetBool("Walking"))
            {
                PlayerAnimator.SetBool("Walking", true);
            }
        } else
        {
            if (PlayerAnimator.GetBool("Walking")) 
            {
                PlayerAnimator.SetBool("Walking", false);
            }
        }

        // Wall bounce physics

        if (!IsGrounded())
        {
            if (!PlayerAnimator.GetBool("InJump")) {
                PlayerAnimator.SetBool("InJump", true);
            }

            RaycastHit2D LeftDetection = Physics2D.Raycast(transform.position, -Vector2.right, 0.75f);
            RaycastHit2D RightDetection = Physics2D.Raycast(transform.position, Vector2.right, 0.75f);

            if (LeftDetection)
            {
                if (LeftDetection.transform.CompareTag("CanBounce"))
                {
                    Direction = "Right";
                    SpriteRenderer.flipX = false;
                    RB.velocity = new Vector2(-RB.velocity.x, RB.velocity.y);
                }
            } else if (RightDetection)
            {
                if (RightDetection.transform.CompareTag("CanBounce"))
                {
                    Direction = "Left";
                    SpriteRenderer.flipX = true;
                    RB.velocity = new Vector2(-RB.velocity.x, RB.velocity.y);
                }
            }
        } else
        {
            if (PlayerAnimator.GetBool("InJump") && !IsJumping)
            {
                PlayerAnimator.SetBool("InJump", false);
            }
        }

        // Check if the player is in freefall
        if (!IsGrounded())
        {
            if (!AwaitingLand && IsJumping)
            {
                AwaitingLand = true;
            }
        } else
        {
            int LayerLanded = GetGroundLayer();

            if (LayerLanded == 7 && !IsJumping)
            {
                IsJumping = true;
                InJump = true;

                RB.velocity = Vector2.zero;
                PlayerAnimator.SetBool("InJump", true);
            }
        }

        if (IsGrounded() && !InJump && IsJumping && AwaitingLand)
        {
            IsJumping = false;
            AwaitingLand = false;

            //RB.velocity = Vector2.zero;
            Collider.sharedMaterial = null;
            FallSound.time = 0.07f;
            FallSound.Play();

            int LayerLanded = GetGroundLayer();

            if (LayerLanded != 7)
            {
                PlayerAnimator.SetBool("InJump", false);
            }
        }

    }
}
