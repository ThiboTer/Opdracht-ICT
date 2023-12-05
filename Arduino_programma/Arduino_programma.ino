#include <Wire.h>
#include <LiquidCrystal_I2C.h>


int Address = 0x27;

LiquidCrystal_I2C lcd(Address, 16, 2);

void setup() {
  Serial.begin(9600);
  lcd.begin(16, 2);
  lcd.backlight(); 
  lcd.setCursor(0, 0);
  lcd.print("Thibo Terryn");
  lcd.setCursor(0, 1);
  lcd.print("Opdracht ICT");
}

void loop() {
  if (Serial.available() > 0) {
    String receivedText = Serial.readString();
    // laatste karakter van string weg doen anders krijg je raar teken...
    receivedText = receivedText.substring(0, receivedText.length() - 1);
    
    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print(receivedText);
  }
}
