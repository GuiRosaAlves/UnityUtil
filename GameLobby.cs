using System.Collections;
using System.Collections.Generic;
using Malimbe.PropertySerializationAttribute;
using UnityEngine;
using UnityEngine.Serialization;

public class GameLobby : MonoBehaviour
{
    [SerializeField] private PlayerController _playerPrefab;
    [Serialized] public int _maxPlayers { get; private set; }
    [Serialized] public List<PlayerController> PlayerList { get; private set; }

    public void AddPlayer()
    {
        var newPlayer = Instantiate(_playerPrefab);
        newPlayer.PlayerID = PlayerList.Count + 1;
        PlayerList.Add(newPlayer);
    }

    public void SendInfo()
    {
        if (_App.PlaySession)
            _App.PlaySession.ActivePlayers = PlayerList;
    }
}