using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField]
    private Sprite unlockedSprite;
    [SerializeField]
    private Sprite lockedSprite;
    [SerializeField]
    private bool locked = false; // this is for setting it at the start

    private Collider2D wallCollider;
    SpriteRenderer spriteRenderer;

    public void Unlock()
    {
        locked = false;
        spriteRenderer.sprite = unlockedSprite;
        wallCollider.enabled = false;
    }

    public void Lock()
    {
        locked = true;
        spriteRenderer.sprite = lockedSprite;
        wallCollider.enabled = true;
    }

    public void ToggleLock()
    {
        if (locked)
        {
            Unlock();
        } else
        {
            Lock();
        }
    }

    // Use this for initialization
    void Start () {
        wallCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // then ensure the graphics and colliders are in sync with the variables
        if (locked)
        {
            Lock();
        } else
        {
            Unlock();
        }
	}
}
