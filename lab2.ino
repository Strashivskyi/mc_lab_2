const int firstButtonPin = A11; 
const int secondButtonPin = A13; 
const int timer = 600;
const int pinCount = 8;
const int ledPins[pinCount] = {
  37, 36, 35, 34, 33, 32, 31, 30
};
bool first_button_called = false;
bool second_button_called = false;

void setup() {
  DDRC = B11111111;
  PORTC = B00000000;
  pinMode(firstButtonPin, INPUT_PULLUP);
  pinMode(secondButtonPin, INPUT_PULLUP);
  Serial.begin(9600);
}

void loop() {
  if (Serial.available()) {
    int signal = Serial.read();
    if (signal == '1') {
      first_button_called = true;
    }
    else if (signal == '2')
    {
      second_button_called = true;
    }
  

  }
      if(!digitalRead(firstButtonPin )){
      Serial.write("3");
    }
    else if(!digitalRead(secondButtonPin)){
      Serial.write("4");
    }
    if (first_button_called) {
    
    first_button_called = false;
    for (int thisPin = 0; thisPin < pinCount; thisPin = thisPin + 1 ) {
    digitalWrite(ledPins[thisPin], HIGH);
    delay(timer);
    digitalWrite(ledPins[thisPin], LOW);
  }
  }
  if (second_button_called) {
    second_button_called = false;
    for (int thisPin = 7; thisPin >= 0; thisPin = thisPin - 2 ) {
    digitalWrite(ledPins[thisPin], HIGH);
    delay(timer);
    digitalWrite(ledPins[thisPin], LOW);
    if(thisPin == 1){
      thisPin=8;
    }
  }
  }

}
