using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
    [SerializeField]
    private float maxSpeed = 2f;
    [SerializeField]
    private float acceleration = 10000f;
    [SerializeField]
    private float jumpForce = 5000f;
    [SerializeField]
    private float rotationSpeed = 1000f;
    [SerializeField]
    private float maxRotationSpeed = 2f;

    [SerializeField]
    private Sprite regularSprite;
    [SerializeField]
    private Sprite jumpingSprite;


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float timeHoldingReset = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        LevelManager.instance.SetLevelSpawn(transform.position, transform.rotation);
        LevelManager.instance.SetPlayerGameobject(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        bool onGround = false;
        LayerMask onFloorMask = LayerMask.GetMask("Terrain", "Coin", "Block");
        if (Physics2D.Raycast(transform.position, -1 * transform.up, .1f, onFloorMask) || Physics2D.Raycast(transform.position + transform.right * -.4f, -1 * transform.up, .1f, onFloorMask) || Physics2D.Raycast(transform.position + transform.right * .4f, -1 * transform.up, .1f, onFloorMask))
        {
            onGround = true;
        }
        float dx = Input.GetAxisRaw("Horizontal");
        if (onGround)
        {
            float sidewaysVelocity = Vector3.Dot(rb.velocity, transform.right);
            if (Mathf.Abs(sidewaysVelocity) < maxSpeed || ((dx < 0) == (sidewaysVelocity > 0)))
            {
                // if you're slower than the max speed or working against your current speed then you can apply force in that direction
                rb.AddForce(dx * transform.right * Time.deltaTime * acceleration);
            }
        } else
        {
            if (Mathf.Abs(rb.angularVelocity) < maxRotationSpeed || ((dx > 0) == (rb.angularVelocity > 0)))
            {
                // if you're rotating slower than the max speed or working against your current speed then you can apply torque in that direction
                rb.AddTorque(-dx * rotationSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                // if the player is less than .1f off of the ground, then jump
                // (it's written .6f because the raycast starts from the center of the sprite, which is .5f off the ground)
                rb.AddForce(transform.up * jumpForce);
            }
            spriteRenderer.sprite = jumpingSprite;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            spriteRenderer.sprite = regularSprite;
        }
        
        if (dx > 0)
        {
            transform.localScale = Vector3.one;
        } else if (dx < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        // Reset position or reset level if you hold R long enough
        if (Input.GetKey(KeyCode.R))
        {
            timeHoldingReset += Time.deltaTime;
            if (timeHoldingReset > 1)
            {
                timeHoldingReset = 0;
                LevelManager.instance.ResetLevel();
            }
        }
        else
        {
            if (timeHoldingReset > 0)
            {
                timeHoldingReset = 0;
                LevelManager.instance.BackToCheckpoint();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LevelManager.instance.NextLevel();
        }
	}
}
