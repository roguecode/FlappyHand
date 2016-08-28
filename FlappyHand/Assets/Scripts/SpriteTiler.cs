using UnityEngine;

public class SpriteTiler : MonoBehaviour
{
  public GameObject Camera;
  public GameObject Piece;

  float _okUntilX;

  void Update()
  {
    // Check if the cameras position (plus a buffer of 30 to the right) is greater than what we calculated as `_okUntilX`
    if (Camera.transform.position.x + 30 > _okUntilX)
    {
      // Instantiate a new piece
      var piece = Instantiate(Piece);

      // Parent it to this
      piece.transform.SetParent(transform);

      // Move it here
      piece.transform.position = new Vector3(_okUntilX, transform.position.y, transform.position.z);

      // Work out when we'll next need a new piece by taking where we spawned this piece, and adding the width of the piece
      _okUntilX += piece.GetComponent<SpriteRenderer>().bounds.size.x;
    }
  }
}
