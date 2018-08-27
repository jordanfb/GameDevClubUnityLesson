using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
    [SerializeField]
    private ActionType flagAction;

    private Collider2D flagCollider;

    // Use this for initialization
    void Start () {
        flagCollider = GetComponent<PolygonCollider2D>();
    }

    private void DoAction()
    {
        switch(flagAction)
        {
            case ActionType.NextLevel:
                LevelManager.instance.NextLevel();
                break;
            case ActionType.RestartLevel:
                LevelManager.instance.ResetLevel();
                break;
            case ActionType.PreviousLevel:
                LevelManager.instance.PrevLevel();
                break;
            case ActionType.Checkpoint:
                LevelManager.instance.SetCheckpoint(transform);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DoAction();
        }
    }

    private enum ActionType
    {
        // what action the flag should do
        NextLevel, PreviousLevel, RestartLevel, Checkpoint
    }
}
