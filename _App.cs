using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _App : MonoBehaviour
{
    public static _App instance { get; private set; }
    public static _SceneSwapManager SceneManager { get; private set; }
    public static _PlaySessionManager PlaySession { get; private set; }

    private void Awake()
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
    }

    private void Start()
    {
        SceneManager = GetComponent<_SceneSwapManager>();
        PlaySession = GetComponent<_PlaySessionManager>();
    }
}
