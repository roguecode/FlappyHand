using UnityEngine;
using System.Collections;

public class PlanePuffGenerator : MonoBehaviour
{
  public GameObject[] Puffs;

  void Start()
  {
    StartCoroutine(GeneratePuffs());
  }

  IEnumerator GeneratePuffs()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.03f);
      // Make a new puff instance, picked randomly from our array of puffs
      var puff = Instantiate(Puffs[Random.Range(0, Puffs.Length - 1)]);
      // Position it here
      puff.transform.position = transform.position;
    }
  }
}
