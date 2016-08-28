using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePropellerSpin : MonoBehaviour
{
  List<GameObject> _frames;
  int _currentFrame;

  void Start()
  {
    // Get each plane (we have 3 frames of plane
    var renderers = GetComponentsInChildren<SpriteRenderer>();

    // Get the GameObject for each one, turn them off, and add to our _frames list
    _frames = new List<GameObject>();
    for (var s = 0; s < renderers.Length; s++)
    {
      var renderer = renderers[s].gameObject;
      renderer.SetActive(false);
      _frames.Add(renderer);
    }

    StartCoroutine(Spin());
  }

  IEnumerator Spin()
  {
    while (true)
    {
      yield return new WaitForSeconds(.03f);

      // Turn off the current frame
      _frames[_currentFrame].SetActive(false);

      // Increment the frame and check we haven't gone over our max number of frames
      _currentFrame++;
      if (_currentFrame == _frames.Count)
        _currentFrame = 0;
      
      // Turn the new frame on
      _frames[_currentFrame].SetActive(true);
    }
  }
}
