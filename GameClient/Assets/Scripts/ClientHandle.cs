using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
  public static void Welcome(Packet _packet)
  {
    string _msg = _packet.ReadString();
    int _myId = _packet.ReadInt();

    Debug.Log($"Message from server: {_msg}");
    Client.instance.myId = _myId;
    ClientSend.WelcomeReceived();

    Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
  }

  public static void SpawnPlayer(Packet _packet)
  {
    int _id = _packet.ReadInt();
    string _username = _packet.ReadString();
    Vector3 _position = _packet.ReadVector3();
    Quaternion _rotation = _packet.ReadQuaternion();

    GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
  }

  public static void PlayerMove(Packet _packet)
  {
    int _id = _packet.ReadInt();
    Vector3 _move = _packet.ReadVector3();
    Vector3 _currentPosition = _packet.ReadVector3();

    if (GameManager.players.ContainsKey(_id))
    {
      GameManager.players[_id].Move(_move, _currentPosition);
    }
  }

  public static void PlayerPosition(Packet _packet)
  {
    int _id = _packet.ReadInt();
    Vector3 _position = _packet.ReadVector3();

    if (GameManager.players.ContainsKey(_id))
    {
      GameManager.players[_id].transform.position = _position;
    }
  }

  public static void PlayerRotation(Packet _packet)
  {
    int _id = _packet.ReadInt();
    Quaternion _rotation = _packet.ReadQuaternion();

    if (GameManager.players.ContainsKey(_id))
    {
      GameManager.players[_id].transform.rotation = _rotation;
    }
  }

  public static void PlayerJump(Packet _packet)
  {
    int _id = _packet.ReadInt();
    Vector3 _currentPosition = _packet.ReadVector3();

    if (GameManager.players.ContainsKey(_id))
    {
      GameManager.players[_id].Jump(_currentPosition);
    }
  }

  public static void RemovePlayer(Packet _packet)
  {
    int _id = _packet.ReadInt();
    GameManager.instance.RemovePlayer(_id);
  }
}
