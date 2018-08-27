using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private float jumpForce = 100f;

    [SerializeField]
    private Sprite regularSprite;
    [SerializeField]
    private Sprite jumpingSprite;


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        float dx = Input.GetAxisRaw("Horizontal");
        rb.AddForce(dx * Vector2.right * Time.deltaTime * movementSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics2D.Raycast(transform.position, -1 * transform.up, .1f, LayerMask.GetMask("Terrain", "Coin")) || Physics2D.Raycast(transform.position + transform.right * -.5f, -1 * transform.up, .1f, LayerMask.GetMask("Terrain", "Coin")) || Physics2D.Raycast(transform.position + transform.right * .5f, -1 * transform.up, .1f, LayerMask.GetMask("Terrain", "Coin")))
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


        // Reset level
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelManager.instance.ResetLevel();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LevelManager.instance.NextLevel();
        }
	}
}
