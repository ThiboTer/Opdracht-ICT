#include <Wire.h>
#include <LiquidCrystal_I2C.h>


int Address = 0x27;
int breedte = 16;
int lijnen = 2;

LiquidCrystal_I2C lcd(Address, breedte, lijnen);

void setup() 
{
  Serial.begin(9600);
  lcd.begin(breedte, lijnen);
  lcd.backlight(); 
  lcd.setCursor(0, 0);
  lcd.print("Thibo Terryn");
  lcd.setCursor(0, 1);
  lcd.print("Opdracht ICT");
}

void loop() 
{
  if (Serial.available() > 0) 
  {
    String receivedText = Serial.readString();
    
    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print(receivedText.substring(0, breedte));

    lcd.setCursor(0, 1);
    lcd.print(receivedText.substring(breedte+1));
  }
}
