using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
  class ServerHandle
  {
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
      int _clientIdCheck = _packet.ReadInt();
      string _username = _packet.ReadString();

      Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
      if (_fromClient != _clientIdCheck)
      {
        Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
      }

      Server.clients[_fromClient].SendIntoGame(_username);
    }

    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
      Console.WriteLine("Player Movement Received");
      Vector3 _move = _packet.ReadVector3();
      Vector3 _currentPosition = _packet.ReadVector3(); ;

      Server.clients[_fromClient].player.Move(_move, _currentPosition);
    }

    public static void PlayerJump(int _fromClient, Packet _packet)
    {
      Console.WriteLine("Player Jump Received");
      Vector3 _currentPosition = _packet.ReadVector3(); ;

      Server.clients[_fromClient].player.Jump(_currentPosition);
    }
  }
}
