using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour {
    [SerializeField]
    private Door[] doorsOpenWhenPressed;
    [SerializeField]
    private Door[] doorsClosedWhenPressed;
    [SerializeField]
    private Door[] doorsToggleWhenPressed;

    [Space]
    [SerializeField]
    private Sprite regularSprite;
    [SerializeField]
    private Sprite pressedSprite;

    [SerializeField]
    private bool pressedByPlayer;
    [SerializeField]
    private bool pressedByCoin;
    [SerializeField]
    private bool pressedByBlock;
    [SerializeField]
    private bool launchCollider; // if the collider has a gameobject with a rigidbody this launches it vertically upwards
    [SerializeField]
    private float launchForce = 500f;

    private int coinLayer; // these are for collision checking
    private int playerLayer;
    private int blockLayer;
    private Collider2D buttonCollider;
    private SpriteRenderer spriteRenderer;
    private int collidingObjects = 0;
    public bool Pressed
    {
        get
        {
            return collidingObjects > 0;
        }
    }

    // Use this for initialization
    void Start () {
        buttonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        coinLayer = LayerMask.NameToLayer("Coin");
        playerLayer = LayerMask.NameToLayer("Player");
        blockLayer = LayerMask.NameToLayer("Block");
    }

    private void UponPress()
    {
        // this gets called when it gets pressed. This updates all the items it's connected to.
        foreach (Door door in doorsOpenWhenPressed)
        {
            door.Unlock();
        }
        foreach (Door door in doorsClosedWhenPressed)
        {
            door.Lock();
        }
        foreach (Door door in doorsToggleWhenPressed)
        {
            door.ToggleLock();
        }
    }

    private void UponRelease()
    {
        // same deal as uponpress just upon release
        foreach (Door door in doorsOpenWhenPressed)
        {
            door.Lock();
        }
        foreach (Door door in doorsClosedWhenPressed)
        {
            door.Unlock();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pressedByCoin && collision.gameObject.layer == coinLayer)
        {
            collidingObjects++;
        }
        if (pressedByPlayer && collision.gameObject.layer == playerLayer)
        {
            collidingObjects++;
        }
        if (pressedByBlock && collision.gameObject.layer == blockLayer)
        {
            collidingObjects++;
        }
        if (Pressed)
        {
            spriteRenderer.sprite = pressedSprite;
            UponPress();
            if (launchCollider)
            {
                if (collision.attachedRigidbody)
                {
                    collision.attachedRigidbody.AddForce(transform.up * launchForce);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pressedByCoin && collision.gameObject.layer == coinLayer)
        {
            collidingObjects--;
        }
        if (pressedByPlayer && collision.gameObject.layer == playerLayer)
        {
            collidingObjects--;
        }
        if (pressedByBlock && collision.gameObject.layer == blockLayer)
        {
            collidingObjects--;
        }
        if (!Pressed)
        {
            spriteRenderer.sprite = regularSprite;
            UponRelease();
        }
    }
}
