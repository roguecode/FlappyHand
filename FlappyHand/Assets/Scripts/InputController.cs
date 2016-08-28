using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class InputController
{
  public float CurrentValue;

  // The range of inputs we expect. In this case we're saying it'll be between 0 and 30cm
  float _minInputY = 0f;
  float _maxInputY = 30;

  // The Y range that the plane can move within
  float _minFinalY = -4f;
  float _maxFinalY = 4;

  public void Begin(string ipAddress, int port)
  {
    // Give the network stuff its own special thread
    var thread = new Thread(() =>
    {
      // We'll use `LowPassFilter` to filter out some incorrect readings coming from the sensor
      var filter = new LowPassFilter(0.95f);

      // This class makes it super easy to do network stuff
      var client = new TcpClient();

      // Change this to your devices real address
      client.Connect(ipAddress, port);
      var stream = new StreamReader(client.GetStream());

      // We'll read values and buffer them up in here
      var buffer = new List<byte>();
      while (client.Connected)
      {
        // Read the next byte
        var read = stream.Read();

        // We split readings with a carriage return, so check for it 
        if (read == 13)
        {
          // Once we have a reading, convert our buffer to a string, since the values are coming as strings
          var str = Encoding.ASCII.GetString(buffer.ToArray());

          // We assume that they're floats
          var dist = float.Parse(str);

          // Ignore any value outside of our expected input range
          dist = Mathf.Clamp(dist, _minInputY, _maxInputY);

          // Use the `LowPassFilter` to smooth out values
          filter.Step(dist);

          // Remap the value from our input range to our planes movement range
          CurrentValue = filter.SmoothedValue.Remap(_minInputY, _maxInputY, _minFinalY, _maxFinalY);

          // Clear the buffer ready for another reading
          buffer.Clear();
        }
        else
          // If this wasn't the end of a reading, then just add this new byte to our buffer
          buffer.Add((byte)read);
      }
    });
    
    thread.Start();
  }
}
