#include <Wire.h>
#include <LiquidCrystal_I2C.h>

// Adres van het LCD-scherm
int lcdAddress = 0x27;

// Aantal kolommen en rijen van het LCD-scherm
int lcdColumns = 16;
int lcdRows = 2;

LiquidCrystal_I2C lcd(lcdAddress, lcdColumns, lcdRows);

void setup() {
  Serial.begin(9600);
  lcd.begin(lcdColumns, lcdRows);
  lcd.backlight(); 
  lcd.setCursor(0, 0);
  lcd.print("Thibo Terryn");
  lcd.setCursor(0, 1);
  lcd.print("Opdracht ICT");
}

void loop() {
  if (Serial.available() > 0) {
    String receivedText = Serial.readString();
    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print(receivedText);
  }
}
