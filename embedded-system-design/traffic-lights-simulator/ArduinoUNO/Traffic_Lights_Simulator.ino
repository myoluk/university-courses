//Arduino UNO : Traffic Lights Simulator

//Assign port's numbers to variables
int red = 4;    //Red LED on port 4
int yellow = 3; //Yellow LED on port 3
int green = 2;  //Green LED on port 2
void setup()
{                            //Initialize ports
    pinMode(red, OUTPUT);    //Initialize red (P4) as output
    pinMode(yellow, OUTPUT); //Initialize yellow (P3) as output
    pinMode(green, OUTPUT);  //Initialize green (P2) as output
}
void loop()
{
    //Turn on red LED for 10sec and after that turn off
    digitalWrite(red, HIGH); //turn on red
    delay(10000);            //10sec delay
    digitalWrite(red, LOW);  //turn off red
    //Turn on yellow LED for 1sec and after that turn off
    digitalWrite(yellow, HIGH); //turn on yellow
    delay(1000);                //1sec delay
    digitalWrite(yellow, LOW);  //turn off yellow
    //Turn on green LED for 15sec and after that turn off
    digitalWrite(green, HIGH); //turn on green
    delay(15000);              //15sec delay
    digitalWrite(green, LOW);  //turn off green
}