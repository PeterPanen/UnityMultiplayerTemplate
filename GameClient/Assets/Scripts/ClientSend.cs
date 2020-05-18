using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
  private static void SendTCPData(Packet _packet)
  {
    _packet.WriteLength();
    Client.instance.tcp.SendData(_packet);
  }

  private static void SendUDPData(Packet _packet)
  {
    _packet.WriteLength();
    Client.instance.udp.SendData(_packet);
  }

  public static void WelcomeReceived()
  {
    using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
    {
      _packet.Write(Client.instance.myId);
      _packet.Write(UIManager.instance.usernameField.text);
      SendTCPData(_packet);
    }
  }

  public static void PlayerMovement(Vector3 _move, Vector3 _currentPosition)
  {
    using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
    {
      _packet.Write(_move);
      _packet.Write(_currentPosition);
      SendUDPData(_packet);
    }
  }

  public static void PlayerJump(Vector3 _currentPosition)
  {
    using (Packet _packet = new Packet((int)ClientPackets.playerJump))
    {
      _packet.Write(_currentPosition);
      SendUDPData(_packet);
    }
  }
}
