using System;
using UnityEngine;
public class PlaneController : MonoBehaviour
{
  public float TargetY;

  float _velocity;

  void Update()
  {
    // Get the current position
    var position = transform.position;
    switch (GameManager.State)
    {
      case eState.Alive:
        // Move towards our `TargetY` value smoothly
        position.y = Mathf.SmoothDamp(position.y, TargetY, ref _velocity, 0.1f);
        break;
      case eState.Dead:
        // Spin around if we're dead
        transform.Rotate(0, 0, 10f);
        // And head downwards
        position.y = transform.position.y - 0.07f;
        break;
    }

    transform.position = position;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    // If we hit something, find our `GameManager` and die
    FindObjectOfType<GameManager>().Die();
  }
}


