using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public int id;
  public string username;
  public bool isLocal = false;
  public bool isGrounded = false;
  public bool canJump = true;
  private Vector3 move = Vector3.zero;
  private Rigidbody rb;
  private Collider col;

  public void Start()
  {
    rb = GetComponent<Rigidbody>();
    col = GetComponent<Collider>();
  }

  public void Move(Vector3 _move, Vector3 _currentPosition)
  {
    move = _move;
    transform.position = _currentPosition;
  }

  public void Jump(Vector3 _currentPosition)
  {
    canJump = false;
    transform.position = _currentPosition;
    rb.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
    Invoke("ResetJump", 1f);
  }

  public void ResetJump()
  {
    canJump = true;
  }

  private void FixedUpdate()
  {
    float _distanceToGround = col.bounds.extents.y;
    isGrounded = Physics.Raycast(transform.position, Vector3.down, _distanceToGround);

    if (!isLocal)
    {
      rb.MovePosition(transform.position + move * Time.fixedDeltaTime);
    }
  }
}
