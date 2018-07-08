﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance { get; private set; }

    private AsyncOperation _resourceUnloadTask;
    private AsyncOperation _sceneLoadTask;
    private enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };
    private SceneState _sceneState;
    private delegate void UpdateDelegate();
    private UpdateDelegate[] _updateDelegates;
    public delegate void SceneLoadHandler(Scene scene, LoadSceneMode lsm);

    public int PreviousScene { get; private set; }
    public int CurrScene { get; private set; }
    public int NextScene { get; private set; }

#if UNITY_EDITOR
    private string inputBuffer;
#endif

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Object.DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _updateDelegates = new UpdateDelegate[(int)SceneState.Count];
        _updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
        _updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
        _updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
        _updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
        _updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
        _updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
        _updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

        CurrScene = 1;
        NextScene = CurrScene;
        _sceneState = SceneState.Reset;
    }

    protected void Update()
    {
        if (_updateDelegates[(int)_sceneState] != null)
            _updateDelegates[(int)_sceneState]();
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.N))
        {
            GoFoward();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.B))
        {
            GoBack();
        }
        if (Input.anyKeyDown)
        {
            inputBuffer += Input.inputString;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            var aux = -1;
            int.TryParse(inputBuffer, out aux);
            inputBuffer = "";
            aux = Mathf.Clamp(aux, 0, SceneManager.sceneCountInBuildSettings - 1);
            ChangeScene(aux);
        }
#endif
    }

    public bool ChangeScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex <= (SceneManager.sceneCountInBuildSettings - 1))
        {
            PreviousScene = NextScene;
            NextScene = sceneIndex;
            return true;
        }
        else
        {
            Debug.Log("Scene: " + sceneIndex + " doesn't exist! Check Build Settings!");
            return false;
        }
    }
    public bool GoBack()
    {
        if (NextScene - 1 == -1)
        {
            Debug.Log("Scene: " + (NextScene - 1) + " doesn't exist!");
            return false;
        }
        else
        {
            ChangeScene(NextScene - 1);
            return true;
        }
    }
    public bool GoFoward()
    {
        if (NextScene + 1 > (SceneManager.sceneCountInBuildSettings - 1))
        {
            Debug.Log("Scene: " + (NextScene + 1) + " doesn't exist!");
            return false;
        }
        else
        {
            ChangeScene(NextScene + 1);
            return true;
        }
    }
    public bool GoPrevious()
    {
        if (PreviousScene == -1)
        {
            Debug.Log("There is no previous scene!");
            return false;
        }
        else
        {
            var aux = PreviousScene;
            PreviousScene = NextScene;
            ChangeScene(aux);
            return true;
        }
    }
    public void ReloadScene()
    {
        _sceneState = SceneState.Reset;
    }
    public void OnSceneLoad()
    {

    }
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public string GetSceneName(int sceneIndex)
    {
        return SceneManager.GetSceneByBuildIndex(sceneIndex).name;
    }

    //State functions
    private void UpdateSceneReset()
    {
        System.GC.Collect();
        _sceneState = SceneState.Preload;
    }
    private void UpdateScenePreload()
    {
        _sceneLoadTask = SceneManager.LoadSceneAsync(NextScene);
        _sceneState = SceneState.Load;
    }
    private void UpdateSceneLoad()
    {
        if (_sceneLoadTask.isDone == true)
        {
            _sceneState = SceneState.Unload;
        }
        else
        {
            // update scene loading
        }
    }
    private void UpdateSceneUnload()
    {
        if (_resourceUnloadTask == null)
        {
            _resourceUnloadTask = Resources.UnloadUnusedAssets();
        }
        else
        {
            if (_resourceUnloadTask.isDone == true)
            {
                _resourceUnloadTask = null;
                _sceneState = SceneState.Postload;
            }
        }
    }
    private void UpdateScenePostload()
    {
        CurrScene = NextScene;
        _sceneState = SceneState.Ready;
    }
    private void UpdateSceneReady()
    {
        System.GC.Collect();
        _sceneState = SceneState.Run;
    }
    private void UpdateSceneRun()
    {
        if (NextScene != CurrScene)
        {
            _sceneState = SceneState.Reset;
        }
    }
}