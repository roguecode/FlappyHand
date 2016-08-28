public static class Extensions
{
  // From http://forum.unity3d.com/threads/re-map-a-number-from-one-range-to-another.119437/
  public static float Remap(this float value, float from1, float to1, float from2, float to2)
  {
    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
  }
}