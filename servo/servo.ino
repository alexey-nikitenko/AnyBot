#include <Wire.h>
#include <Adafruit_PWMServoDriver.h>

// called this way, it uses the default address 0x40
Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver();
// you can also call it with a different address you want
//Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver(0x41);
// you can also call it with a different address and I2C interface
//Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver(0x40, Wire);

#define SERVOMIN  150 // This is the 'minimum' pulse length count (out of 4096)
#define SERVOMAX  600 // This is the 'maximum' pulse length count (out of 4096)
#define USMIN  600 // This is the rounded 'minimum' microsecond length based on the minimum pulse of 150
#define USMAX  2400 // This is the rounded 'maximum' microsecond length based on the maximum pulse of 600
#define SERVO_FREQ 50 // Analog servos run at ~50 Hz updates

// our servo # counter
uint8_t servonum = 0;

String servoMotorIndex;
String readString;
char myData[10];
byte counter = 0;
int frase[6];

void setup() {
  Serial.begin(115200);
  pwm.begin();
  pwm.setOscillatorFrequency(27000000);
  pwm.setPWMFreq(SERVO_FREQ);  // Analog servos run at ~50 Hz updates
}

void loop() {
  recvWithEndMarker();
}

void recvWithEndMarker() {
  while (!Serial.available());
  // read the incoming byte:
  do {
    byte m = Serial.readBytesUntil(' ', myData, 6);
    myData[m] = '\0';  //nul charcater
    frase[counter] = atoi(myData);
    counter++;
  }
  while (counter != 7);
  counter = 0;

  if (frase[0] == 1)
  {
    doRotation(frase[1], frase[2], frase[3], frase[4]);
  }

  if (frase[0] == 2)
  {
    doRotationSync(frase[1], frase[2], frase[3], frase[4], frase[5]);
  }

  if (frase[0] == 3)
  {
    doRotationMouseSync(frase[1], frase[2], frase[3], frase[4], frase[5], frase[6]);
  }
  if (frase[0] == 4)
  {
    doForwardAndBack(frase[1], frase[2], frase[3], frase[4]);
  }
}

void doForwardAndBack(int pin, int initial, int goal, int delayValue)
{
  if (initial < goal)
  {
    for (int pulselen = initial; pulselen < goal; pulselen++)
    {
      pwm.setPWM(pin, 0, pulselen);
      delay(delayValue);  //  <-------- Increasing this delay will slow down the servo movement
    }
    for (int pulselen = goal; pulselen > initial; pulselen--)
    {
      pwm.setPWM(pin, 0, pulselen);
      delay(delayValue);  //  <-------- Increasing this delay will slow down the servo movement
    }
  }

  if (initial > goal)
  {
    for (int pulselen = initial; pulselen > goal; pulselen--)
    {
      pwm.setPWM(pin, 0, pulselen);
      delay(delayValue);  //  <-------- Increasing this delay will slow down the servo movement
    }
    for (int pulselen = goal; pulselen < initial; pulselen++)
    {
      pwm.setPWM(pin, 0, pulselen);
      delay(delayValue);  //  <-------- Increasing this delay will slow down the servo movement
    }
  }
}

void doRotation(int pin, int initial, int goal, int delayValue)
{
  if (initial < goal)
  {
    for (int pulselen = initial; pulselen < goal; pulselen++)
    {
      pwm.setPWM(pin, 0, pulselen);
      delay(delayValue);  //  <-------- Increasing this delay will slow down the servo movement
    }
  }

  if (initial > goal)
  {
    for (int pulselen = initial; pulselen > goal; pulselen--)
    {
      pwm.setPWM(pin, 0, pulselen);
      delay(delayValue);  //  <-------- Increasing this delay will slow down the servo movement
    }
  }
}

void doRotationSync(int pin1, int pin2, int initial, int goal, int delayValue)
{
  if (initial < goal)
  {
    for (int pulselen = initial; pulselen < goal; pulselen++)
    {
      pwm.setPWM(pin1, 0, pulselen);
      pwm.setPWM(pin2, 0, pulselen);
      delay(delayValue);
    }
  }

  if (initial > goal)
  {
    for (int pulselen = initial; pulselen > goal; pulselen--)
    {
      pwm.setPWM(pin1, 0, pulselen);
      pwm.setPWM(pin2, 0, pulselen);
      delay(delayValue);
    }
  }
}

void doRotationMouseSync(int pin1, int pin2, int pin3, int initial, int goal, int delayValue)
{
  if (initial < goal)
  {
    for (int pulselen = initial, pulselen2 = goal; pulselen < goal; pulselen++, pulselen2--)
    {
      pwm.setPWM(pin1, 0, pulselen);
      pwm.setPWM(pin2, 0, pulselen);
      pwm.setPWM(pin3, 0, pulselen2);
      delay(delayValue);
    }
  }

  if (initial > goal)
  {
    for (int pulselen = initial, pulselen2 = goal; pulselen > goal; pulselen--, pulselen2++)
    {
      pwm.setPWM(pin1, 0, pulselen);
      pwm.setPWM(pin2, 0, pulselen);
      pwm.setPWM(pin3, 0, pulselen2);
      delay(delayValue);
    }
  }
}
