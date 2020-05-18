using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
  class Player
  {
    public int id;
    public string username;

    public Vector3 move;
    public Vector3 position;
    public Quaternion rotation;

    public Player(int _id, string _username, Vector3 _spawnPosition)
    {
      id = _id;
      username = _username;
      position = _spawnPosition;
      rotation = Quaternion.Identity;
    }

    public void Move(Vector3 _move, Vector3 _currentPosition)
    {
      move = _move;
      position = _currentPosition;

      ServerSend.PlayerMove(this);
    }

    public void Jump(Vector3 _currentPosition)
    {
      position = _currentPosition;

      ServerSend.PlayerJump(this);
    }
  }
}