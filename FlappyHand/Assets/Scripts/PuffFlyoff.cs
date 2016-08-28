using UnityEngine;

public class PuffFlyoff : MonoBehaviour
{
  float _speed;
  float _rotation;
  float _deathTime;

  void Start()
  {
    // Here we pick some values for how this puff will behave
    
    // Random speed and rotations
    _speed = Random.Range(-0.07f, -0.1f);
    _rotation = Random.Range(-1f, 1f);

    // Die after 1 second
    _deathTime = Time.time + 1;

    // Start at a random rotation
    transform.Rotate(0, 0, Random.Range(0, 360));

    // Get a random size
    var scale = Random.Range(0.3f, 1f);
    transform.localScale = new Vector3(scale, scale);
  }

  void Update()
  {
    // Rotate it on the Z axis
    gameObject.transform.Rotate(0, 0, _rotation);

    // Translate the X
    gameObject.transform.Translate(_speed, 0, 0, Space.World);

    // Destroy us if we're over our lifetime
    if (Time.time > _deathTime)
      Destroy(gameObject);
  }
}
