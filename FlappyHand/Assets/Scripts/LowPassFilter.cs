public class LowPassFilter
{
  private float _smoothingFactor;
  public float SmoothedValue;
  public LowPassFilter(float smoothingFactor)
  {
    _smoothingFactor = smoothingFactor;
  }

  public void Step(float sensorValue)
  {
    SmoothedValue = _smoothingFactor * sensorValue + (1 - _smoothingFactor) * SmoothedValue;
  }
}