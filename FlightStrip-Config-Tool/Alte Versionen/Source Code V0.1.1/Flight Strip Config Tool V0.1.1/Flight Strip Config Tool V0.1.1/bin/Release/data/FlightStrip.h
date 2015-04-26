/*
  FlightStrip.h - Library for WS2812 LED Strip Management.
  Version 0.7 (15.03.2015)

  This Code is published by Yannik Wertz under the "Creativ Common Attribution-NonCommercial-ShareAlike 4.0 International (CC BY-NC-SA 4.0)" License 
  For more Information see : http://creativecommons.org/licenses/by-nc-sa/4.0/
*/

#ifndef FlightStrip_h
#define Flightstrip_h

#include "Arduino.h"
#include <Adafruit_NeoPixel.h>

//Effekt Namen - Nummer Beziehungen
#define CONST_COLOR          0
#define CONST_TRIPLE_COLOR   1
#define CHASE_SINGLE_COLOR   2
#define CHASE_TRIPLE_COLOR   3
#define FILL_TRIPLE_COLOR    4
#define BLINK_COLOR          5
#define DOUBLEFLASH          6
#define FILL_AGAINST         7
#define THEATER_CHASE        8
#define KNIGHTRIDER          9
#define THUNDERSTORM         10
#define POLICE_RIGHT         11
#define POLICE_LEFT          12
#define RAINBOW              13
#define BLOCKSWITCH          14
#define RANDOMFLASH          15


class FlightStrip
{
  //Variables
  private:
    Adafruit_NeoPixel *strip;  //Zeiger auf ein NeoPixel Objekt
    uint16_t lowerBound;       //Nummer der unteren LED
    uint16_t upperBound;       //Nummer der oberen LED
    uint16_t origLowerBound;   //Nummer der unteren LED (kann nicht geändert werden)
    uint16_t origUpperBound;   //Nummer der oberen LED (kann nicht geändert werden)
    uint8_t modes[20];         //Hier werden die Effekte zum jeweiligen Modus gespeichert
    uint8_t mode;              //Aktueller Modus
    uint32_t lastTimeUpdated;  //letztes mal aktualisiert
    uint32_t actTime;          //Aktuelle Zeit
    uint16_t counter;          //Zähler für aktuelle LED
    uint8_t number;            //Durchgang bei Triple-Funktionen
    boolean up;                //Hochzählen oder Runterzählen
    boolean reversed;          //Richtung umkehren
    boolean disabled;          //Komplett abschalten bis neuer Modus gewählt wird
    
    //Effekt Parameter
    uint32_t constColor_color;
    uint32_t constTripleColor_color[3];
    uint16_t constTripleColor_interval;
    uint32_t chaseSingleColor_color;
    uint16_t chaseSingleColor_interval;
    uint32_t chaseTripleColor_color[3];
    uint16_t chaseTripleColor_interval;
    uint32_t fillTripleColor_color[3];
    uint16_t fillTripleColor_interval;
    uint32_t blinkColor_color;
    uint16_t blinkColor_onTime;
    uint16_t blinkColor_offTime;
    uint32_t doubleflash_color;
    boolean doubleflash_longOff;
    uint32_t fillAgainst_color[2];
    uint16_t fillAgainst_interval;
    uint32_t theaterChase_color;
    uint16_t theaterChase_interval;
    uint32_t knightrider_color;
    uint32_t knightrider_background_color;
    uint16_t knightrider_interval;
    uint8_t knightrider_width;
    uint32_t thunderstorm_color;
    uint8_t thunderstorm_interval;
    uint16_t policeRight_interval;
    uint16_t policeLeft_interval;
    uint8_t rainbow_interval;
    uint32_t blockswitch_color[2];
    uint8_t blockswitch_width;
    uint16_t blockswitch_interval;
    uint16_t randomflash_interval;
    
    
    
  public:
  
  //Functions
  public:
    FlightStrip(Adafruit_NeoPixel &stripToUse, uint16_t startLed, uint16_t endLed);  //Konstruktor
    void learnMode(uint8_t modeNumber, uint8_t effectNumber);
    void setMode(uint8_t actMode);
    void update();
    void updateParameter(uint8_t effectNumber, uint32_t color1, uint32_t color2, uint32_t color3, uint16_t interval);
    void reverse();
    void setBounds(uint16_t startLed, uint16_t endLed);
    void disable();
    
  private:
    void constColor();
    void constTripleColor();
    void chaseSingleColor();
    void chaseTripleColor();
    void fillTripleColor();
    void blinkColor();
    void doubleflash();
    void fillAgainst();
    void theaterChase();
    void knightrider();
    void thunderstorm();
    void policeRight();
    void policeLeft();
    void rainbow();
    uint32_t wheel(byte wheelpos);
    void blockswitch();
    void randomflash();

};

#endif
