using UnityEngine;

public class GameManager : MonoBehaviour
{
  public PlaneController PlayerPlane;
  public GameObject Camera;

  public static eState State { get; set; }

  InputController _inputController;
  float _gameSpeed = 6f;

  void Start()
  {
    State = eState.Alive;

    // This will do the network stuff
    _inputController = new InputController();
    _inputController.Begin("192.168.8.119", 26);
  }

  void Update()
  {
    if (State == eState.Dead)
      return;

    // Set the `TargetY` of the plane to the latest value that our `InputController` has received
    PlayerPlane.TargetY = _inputController.CurrentValue;

    // Move our plane and camera
    var movement = _gameSpeed * Time.deltaTime;
    PlayerPlane.transform.Translate(movement, 0, 0);
    Camera.transform.Translate(movement, 0, 0);
  }

  public void Die()
  {
    State = eState.Dead;
  }
}

public enum eState
{
  Alive,
  Dead
}
