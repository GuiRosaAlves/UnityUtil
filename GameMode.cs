using System.Collections;
using System.Collections.Generic;
using Malimbe.PropertySerializationAttribute;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public abstract class GameMode : MonoBehaviour
{
    [Serialized] public Transform[] SpawnPoints { get; private set; }
    [Serialized][field: Tooltip("In seconds!")][field: Range(0f, 3600f)] private float TimeLimit { get; set; }
    [Serialized] public int PlayerLimit { get; private set; }
    [Serialized] public List<PlayerController> PlayerList { get; set; }

    public abstract void GameStarted();
    public abstract bool GameOverCondition();
    public abstract void GameOver();
    public abstract void GamePaused();
}