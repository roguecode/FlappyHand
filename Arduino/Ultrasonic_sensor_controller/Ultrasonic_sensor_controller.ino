#include <ESP8266WiFi.h>
#include <WiFiClient.h>

const char* ssid = "PUT YOUR NETWORK NAME HERE";
const char* password = "PUT YOUR PASSWORD HERE";

WiFiServer server(26);
WiFiClient client;

#define echoPin D1 // Echo Pin connected to sensor
#define trigPin D2 // Trigger Pin connected to sensor
long duration, distance; // Used to calculate distance

void setup() {
  Serial.begin(115200);

  // Set our pins
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  
  WiFi.begin(ssid,password);
  Serial.println("Connecting");

  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.print("Connected to "); 
  Serial.println(ssid);
  
  Serial.print("IP Address: "); 
  Serial.println(WiFi.localIP());
 
  // Start the TCP server
  server.begin();
}

void loop() {
  // Listen for connecting clients
  client = server.available();
  if (client){
    Serial.println("Client connected");
    while (client.connected()){

        // Calculate the distance of the ultrasonic sensor
        // Roughly http://playground.arduino.cc/Main/UltrasonicSensor
        digitalWrite(trigPin, LOW);
        delayMicroseconds(2);
        digitalWrite(trigPin, HIGH);
        delayMicroseconds(10);
        digitalWrite(trigPin, LOW);
        duration = pulseIn(echoPin, HIGH);
        //Calculate the distance (in cm) based on the speed of sound.
        distance = duration/58.2;

        // Send the distance to the client, along with a break to separate our messages
        client.print(distance);
        client.print('\r');

        // Delay before the next reading
        delay(10);
    }
  }
 } 
