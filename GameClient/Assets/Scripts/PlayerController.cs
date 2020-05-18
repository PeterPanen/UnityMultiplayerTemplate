using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed;
  private float prevH = 0;
  private float prevV = 0;
  private Rigidbody rb;
  private PlayerManager playerManager;

  void Start()
  {
    Camera.main.gameObject.SetActive(false);
    rb = GetComponent<Rigidbody>();
    playerManager = GetComponent<PlayerManager>();
  }

  private void FixedUpdate()
  {
    float _mH = Input.GetAxisRaw("Horizontal");
    float _mV = Input.GetAxisRaw("Vertical");
    CheckJump(Input.GetButton("Jump"));
    Vector3 _input = new Vector3(_mH, 0, _mV) * speed;
    rb.MovePosition(transform.position + _input * Time.fixedDeltaTime);

    if (_mH != prevH || _mV != prevV)
    {
      prevH = _mH;
      prevV = _mV;
      SendMovementToServer(_input, transform.position);
    }
  }

  void CheckJump(bool jump)
  {
    if (jump && playerManager.canJump && playerManager.isGrounded)
    {
      Debug.Log("Sending Jump!");
      playerManager.Jump(transform.position);
      SendJumpToServer(transform.position);
    }
  }

  void SendMovementToServer(Vector3 _move, Vector3 _currentPosition)
  {
    ClientSend.PlayerMovement(_move, _currentPosition);
  }

  void SendJumpToServer(Vector3 _currentPosition)
  {
    ClientSend.PlayerJump(_currentPosition);
  }
}
