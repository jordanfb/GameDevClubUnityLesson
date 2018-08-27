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
    private Transform checkpointTransform;
    private Transform levelSpawn;

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

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void PrevLevel()
    {
        SceneManager.LoadScene(prevSceneName);
    }

    public void SetCheckpoint(Transform pos)
    {
        checkpointTransform = pos;
    }

    public void SetLevelSpawn(Transform pos)
    {
        levelSpawn = pos;
    }
}
