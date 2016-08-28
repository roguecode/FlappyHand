using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
  public GameObject Obstacle;
  public GameObject Camera;

  float _nextTimeToSpawn;

  void Start()
  {
    UpdateNextTimeToSpawn();
  }

  void Update()
  {
    if (Time.time > _nextTimeToSpawn)
    {
      var spawned = Instantiate(Obstacle);
      spawned.transform.position = new Vector3(Camera.transform.position.x + 15, transform.position.y, 0);
      spawned.transform.localScale = new Vector3(2, Random.Range(1f, 2.7f));
      spawned.transform.SetParent(transform);
      UpdateNextTimeToSpawn();
    }
  }

  void UpdateNextTimeToSpawn()
  {
    _nextTimeToSpawn = Time.time + Random.Range(0.5f, 3f);
  }
}
