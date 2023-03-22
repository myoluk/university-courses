/*  Arduino UNO : Knock Detect Lock
*/

/* Detecs patterns of knocks and triggers a motor to unlock the box
    if it the pattern is correct.
    User must define pattern.
    Pattern consists of each knock.
    For example; "happy birthday to you" song consists 6 knocks.
*/

// Used library to control servo motor
#include <Servo.h>

// Servo motor definition
Servo myServo;

// Used pins
const int piezo = A0;    // piezo sensor
const int switchPin = 2; // checks box close or not
const int yellowLed = 3; // yellow LED
const int greenLed = 4;  // green LED
const int redLed = 5;    // red LED

// Variables to hold switch and piezo values
int knockVal;  // the value detected by the piezo sensor
int switchVal; // checks box value

// Knock tresholds
const int quietKnock = 10; // minimum value detected by piezo sensor
const int loudKnock = 100; // maximum value detected by the piezo sensor

// Variable for lock state and number of knocks
boolean locked = false; // checks if system locked
int numberOfKnock = 0;  // number of knocks in a pattern

// Knocks pattern
// Knocks number of "happy birthday to you" song is 6
int knockPattern = 6;

// Waiting time
// The time required to finish the pattern
int knockEndWait = 1500;

// Maksimum number of knocks
int maxKnock = 10;

// For time calculation
unsigned long startTime = 0;
unsigned long now = 0;

// Initializing
void setup()
{
  myServo.attach(9);          // use pin 9 for servo motor
  pinMode(yellowLed, OUTPUT); // yellow led as ouput
  pinMode(greenLed, OUTPUT);  // green led as output
  pinMode(redLed, OUTPUT);    // red led as output
  pinMode(switchPin, INPUT);  // checking system as input
  Serial.begin(9600);         // start serial monitor to watch what happens

  // Unlock when initializing
  digitalWrite(greenLed, HIGH);           // turn on green led
  myServo.write(0);                       // set the servo motor angle to 0
  Serial.println("The box is unlocked!"); // give information on serial monitor
}

// System in a loop
void loop()
{

  // Checking the system if unlocked
  if (locked == false)
  {
    switchVal = digitalRead(switchPin);

    // If box is closed then switch be HIGH
    if (switchVal == HIGH)
    {

      // If box is closed set system locked
      locked = true;

      // Reset number of knock
      numberOfKnock = 0;

      // Red LED turn on, green LED turn of to show system locked
      digitalWrite(greenLed, LOW);
      digitalWrite(redLed, HIGH);
      delay(400);

      // Lock box with set the servo angle to 120
      myServo.write(120);
      Serial.println("The box is and locked!");
      delay(1000);
    }
  }

  // Checking the system if locked
  if (locked == true)
  {

    // Get value from piezo
    knockVal = analogRead(piezo);

    // Control if value that pizeo taken more than 0
    // and number of knock less than defined maximum knock
    if (numberOfKnock < maxKnock && knockVal > 0)
    {

      // Send this value to checking function for is it valid
      if (checkForKnock(knockVal) == true)
      {

        // If value is valid take this value entered time
        // to end the pattern after seconds that defined as knockEndWait
        startTime = millis();

        // And if value is valid increase the number of knocks
        numberOfKnock++;
      }

      // Delay to not get same value 2 or more times
      delay(100);
    }

    // Take now time to control for end of the pattern
    now = millis();

    // If knockEndWait secons passed and knocked and these knocs not fit pattern
    // reset number of knock and blink red LED 3 times
    if (now - startTime > knockEndWait && numberOfKnock != 0 && numberOfKnock != knockPattern)
    {
      numberOfKnock = 0;
      Serial.println("Pattern is invalid!");
      for (int i = 0; i < 3; i++)
      {
        digitalWrite(redLed, LOW);
        delay(100);
        digitalWrite(redLed, HIGH);
        delay(100);
      }
    }

    // Unlock the system
    // If knockEndWait seconds passed and number of knocked fit the pattern
    // unlock the box
    if (numberOfKnock == knockPattern && now - startTime > knockEndWait)
    {
      delay(50);

      // Unlock by making the servo motor 0 degrees
      myServo.write(0);
      delay(20);

      // Green LED turn on, red LED turn off
      digitalWrite(greenLed, HIGH);
      digitalWrite(redLed, LOW);

      // Give some informations on serial monitor
      Serial.println("Pattern is valid");
      Serial.println("The box is unlocked");
      Serial.println("Waiting to open the box...");

      // System unlocked but wait for the lid of the box to open too
      while (true)
      {
        switchVal = digitalRead(switchPin);

        // If lid of the box opened set system unlocked
        if (switchVal == LOW)
        {
          locked = false;

          // Reset number of knock
          numberOfKnock = 0;
          Serial.println("The box is opened");
          break;
        }
        delay(50);
      }
    }
  }
}

// Check validity of knock
boolean checkForKnock(int value)
{

  // If taken value between minimum and maximum range, it is valid
  if (value > quietKnock && value < loudKnock)
  {

    // Blink the yellow LED 1 time
    digitalWrite(yellowLed, HIGH);
    delay(50);
    digitalWrite(yellowLed, LOW);

    // Write the valid value on serial monitor
    Serial.print("Valid knock of value ");
    Serial.println(value);

    // Return true for icreasing number of knock
    return true;
  }

  // Otherwise, return false to do not increasing number of knock
  else
  {
    return false;
  }
}
