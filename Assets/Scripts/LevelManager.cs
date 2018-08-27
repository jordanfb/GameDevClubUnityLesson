using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    // this is the component which handles reseting the level, loading the level before this, and the level after this.
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private string prevSceneName;

    public static LevelManager instance;

    private Vector3 checkpointPosition;
    private Quaternion checkpointRotation;
    private Vector3 levelSpawn;
    private Quaternion levelSpawnRotation;
    private GameObject playerGameObject;


    public void Awake()
    {
        instance = this; // this is so that everything in the scene can find it easily
    }

    public void OnDestroy()
    {
        // just so we don't reference a destroyed gameobject
        instance = null;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BackToCheckpoint()
    {
        playerGameObject.transform.position = checkpointPosition;
        playerGameObject.transform.rotation = checkpointRotation;
        playerGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void PrevLevel()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void SetCheckpoint(Vector3 pos, Quaternion rot)
    {
        checkpointPosition = pos;
        checkpointRotation = rot;
    }

    public void SetLevelSpawn(Vector3 pos, Quaternion rot)
    {
        levelSpawn = pos;
        levelSpawnRotation = rot;
        SetCheckpoint(pos, rot);
    }

    public void SetPlayerGameobject(GameObject player)
    {
        playerGameObject = player;
    }
}
