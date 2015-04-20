/*
  FlightStrip.cpp - Library for WS2812 LED Strip Management.
  Version 0.7 (15.03.2015)

  This Code is published by Yannik Wertz under the "Creativ Common Attribution-NonCommercial-ShareAlike 4.0 International (CC BY-NC-SA 4.0)" License 
  For more Information see : http://creativecommons.org/licenses/by-nc-sa/4.0/
*/

#include "Arduino.h"
#include "FlightStrip.h"

FlightStrip::FlightStrip(Adafruit_NeoPixel &stripToUse, uint16_t startLed, uint16_t endLed)
{
  randomSeed(analogRead(A0));
  
  strip = &stripToUse;
  lowerBound = startLed;
  origLowerBound = startLed;
  upperBound = endLed;
  origUpperBound = endLed;
  
  lastTimeUpdated = 0;
  mode = 0;
  counter = 0;
  number = 0;
  up = true;
  reversed = false;
  disabled = false;
  
  //Effekt Stadardwerte
  constColor_color = 16711680;           //Rot
  constTripleColor_color[0] = 16711680;  //Rot
  constTripleColor_color[1] = 65280;     //Grün
  constTripleColor_color[2] = 255;       //Blau
  constTripleColor_interval = 200;       //200ms
  chaseSingleColor_color = 16711680;     //Rot
  chaseSingleColor_interval = 50;        //50ms
  chaseTripleColor_color[0] = 16711680;  //Rot
  chaseTripleColor_color[1] = 65280;     //Grün
  chaseTripleColor_color[2] = 255;       //Blau
  chaseTripleColor_interval = 50;        //50ms
  fillTripleColor_color[0] = 16711680;   //Rot
  fillTripleColor_color[1] = 65280;      //Grün
  fillTripleColor_color[2] = 255;        //Blau
  fillTripleColor_interval = 50;         //50ms
  blinkColor_color = 16777215;           //Weiß
  blinkColor_onTime = 50;                //50ms
  blinkColor_offTime = 150;              //150ms
  doubleflash_color = 16777215;          //Weiß
  doubleflash_longOff = false;
  fillAgainst_color[0] = 16711680;       //Rot
  fillAgainst_color[1] = 65280;          //Grün
  fillAgainst_interval = 50;             //50ms
  theaterChase_color = 16747520;         //Orange
  theaterChase_interval = 50;            //50ms
  knightrider_color = 16711680;          //Rot
  knightrider_background_color = 0;      //Aus
  knightrider_interval = 70;             //70ms
  knightrider_width = 3;                 //3 LEDs Breite
  thunderstorm_color = 16777215;         //Weiß
  thunderstorm_interval = 40;            //40ms
  policeRight_interval = 40;             //40ms
  policeLeft_interval = 350;             //250ms
  rainbow_interval = 5;                  //5ms
  blockswitch_color[0] = 16711680;       //Rot
  blockswitch_color[1] = 65280;          //Grün
  blockswitch_width = 5;                 //Pro Block 5 LEDs Breite
  blockswitch_interval = 150;            //150ms
  randomflash_interval = 25;             //25ms
}

void FlightStrip::learnMode(uint8_t modeNumber, uint8_t effectNumber)
{
  modes[modeNumber] = effectNumber;
}

void FlightStrip::setMode(uint8_t actMode)
{
  mode = actMode;
  
  //Originale Grenzen setzten
  lowerBound = origLowerBound;
  upperBound = origUpperBound;
  
  //Zurücksetzten aller Counter-Variablen
  if(!reversed)
    counter = 0;
  else
    counter = (upperBound - lowerBound);
  up = true;
  number = 0;
  
  
  
  disabled = false;
  
  for(int i = lowerBound ; i <= upperBound ; i++)  //Alle LEDs ausschalten
    (*strip).setPixelColor(i, 0, 0, 0);
}

void FlightStrip::update()
{
  if(!disabled)
  {
    actTime = millis();
    
    switch(modes[mode])
    {
      case CONST_COLOR:
        if((actTime - lastTimeUpdated) > 125)
        {
          lastTimeUpdated = actTime;
          constColor();
        }
      break;
      
      case CONST_TRIPLE_COLOR:
        if((actTime - lastTimeUpdated) > constTripleColor_interval)
        {
          lastTimeUpdated = actTime;
          constTripleColor();
        }
      break;
      
      case CHASE_SINGLE_COLOR:
        if((actTime - lastTimeUpdated) > chaseSingleColor_interval)
        {
          lastTimeUpdated = actTime;
          chaseSingleColor();
        }
      break;
      
      case CHASE_TRIPLE_COLOR:
        if((actTime - lastTimeUpdated) > chaseTripleColor_interval)
        {
          lastTimeUpdated = actTime;
          chaseTripleColor();
        }
      break;
      
      case FILL_TRIPLE_COLOR:
        if((actTime - lastTimeUpdated) > fillTripleColor_interval)
        {
          lastTimeUpdated = actTime;
          fillTripleColor();
        }
      break;
      
      case BLINK_COLOR:
        if(up)  //up = true bedeutet Off Zustand!
        {
          if((actTime - lastTimeUpdated) > blinkColor_offTime)
          {
            lastTimeUpdated = actTime;
            blinkColor();
          }
        }
        else  //On Zustand
        {
          if((actTime - lastTimeUpdated) > blinkColor_onTime)
          {
            lastTimeUpdated = actTime;
            blinkColor();
          }
        }
      break;
      
      case DOUBLEFLASH:
        if(up)  //up = true bedeutet Off Zustand!
        {
          if(doubleflash_longOff)
          {
            if((actTime - lastTimeUpdated) > 500)
            {
              lastTimeUpdated = actTime;
              doubleflash_longOff = false;
              doubleflash();
            }
          }
          else
          {
            if((actTime - lastTimeUpdated) > 120)
            {
              lastTimeUpdated = actTime;
              doubleflash_longOff = true;
              doubleflash();
            }
          }
        }
        else  //On Zustand
        {
          if((actTime - lastTimeUpdated) > 60)
          {
            lastTimeUpdated = actTime;
            doubleflash();
          }
        }
      break;
      
      case FILL_AGAINST:
        if((actTime - lastTimeUpdated) > fillAgainst_interval)
        {
          lastTimeUpdated = actTime;
          fillAgainst();
        }
      break;
      
      case THEATER_CHASE:
        if((actTime - lastTimeUpdated) > theaterChase_interval)
        {
          lastTimeUpdated = actTime;
          theaterChase();
        }
      break;
      
      case KNIGHTRIDER:
        if((actTime - lastTimeUpdated) > knightrider_interval)
        {
          lastTimeUpdated = actTime;
          knightrider();
        }
      break;
      
      case THUNDERSTORM:
        if((actTime - lastTimeUpdated) > thunderstorm_interval)
        {
          lastTimeUpdated = actTime;
          thunderstorm();
        }
      break;
      
      case POLICE_RIGHT:
        if((actTime - lastTimeUpdated) > policeRight_interval)
        {
          lastTimeUpdated = actTime;
          policeRight();
        }
      break;
      
      case POLICE_LEFT:
        if((actTime - lastTimeUpdated) > policeLeft_interval)
        {
          lastTimeUpdated = actTime;
          policeLeft();
        }
      break;
      
      case RAINBOW:
        if((actTime - lastTimeUpdated) > rainbow_interval)
        {
          lastTimeUpdated = actTime;
          rainbow();
        }
      break;
      
      case BLOCKSWITCH:
        if((actTime - lastTimeUpdated) > blockswitch_interval)
        {
          lastTimeUpdated = actTime;
          blockswitch();
        }
      break;
      
      case RANDOMFLASH:
        if((actTime - lastTimeUpdated) > blockswitch_interval)
        {
          lastTimeUpdated = actTime;
          randomflash();
        }
      break;
    }//switch(mode)
  }//if(!disabled)
  
}

void FlightStrip::updateParameter(uint8_t effectNumber, uint32_t color1, uint32_t color2, uint32_t color3, uint16_t interval)
{
  switch(effectNumber)
  {
    case CONST_COLOR:
      if(color1 != 0)
        constColor_color = color1;
    break;
    
    case CONST_TRIPLE_COLOR:
      if(color1 != 0)
        constTripleColor_color[0] = color1;
      if(color2 != 0)
        constTripleColor_color[1] = color2;
      if(color3 != 0)
        constTripleColor_color[2] = color3;
      if(interval != 0)
        constTripleColor_interval = interval;
    break; 
    
    case CHASE_SINGLE_COLOR:
      if(color1 != 0)
        chaseSingleColor_color = color1;
      if(interval != 0)
        chaseSingleColor_interval = interval;
    break; 
    
    case CHASE_TRIPLE_COLOR:
      if(color1 != 0)
        chaseTripleColor_color[0] = color1;
      if(color2 != 0)
        chaseTripleColor_color[1] = color2;
      if(color3 != 0)
        chaseTripleColor_color[2] = color3;
      if(interval != 0)
        chaseTripleColor_interval = interval;
    break; 
    
    case FILL_TRIPLE_COLOR:
      if(color1 != 0)
        fillTripleColor_color[0] = color1;
      if(color2 != 0)
        fillTripleColor_color[1] = color2;
      if(color3 != 0)
        fillTripleColor_color[2] = color3;
      if(interval != 0)
        fillTripleColor_interval = interval;
    break;
    
    case BLINK_COLOR:
      if(color1 != 0)
        blinkColor_color = color1;
      if(color2 != 0)
        blinkColor_onTime = color2;
      if(color3 != 0)
        blinkColor_offTime = color3;
    break;
    
    case DOUBLEFLASH:
      if(color1 != 0)
        doubleflash_color = color1;
    break;
    
    case FILL_AGAINST:
      if(color1 != 0)
        fillAgainst_color[0] = color1;
      if(color2 != 0)
        fillAgainst_color[1] = color2;
      if(interval != 0)
        fillAgainst_interval = interval;
    break; 
    
    case THEATER_CHASE:
      if(color1 != 0)
        theaterChase_color = color1;
      if(interval != 0)
        theaterChase_interval = interval;
    break; 
    
    case KNIGHTRIDER:
      if(color1 != 0)
        knightrider_color = color1;
      if(color2 != 0)
        knightrider_background_color = color2;
      if(color3 != 0)
        knightrider_width = color3;
      if(interval != 0)
        knightrider_interval = interval;
    break; 
    
    case THUNDERSTORM:
      if(color1 != 0)
        thunderstorm_color = color1;
    break; 
    
    case POLICE_RIGHT:
    break;
    
    case POLICE_LEFT:
    break;
    
    case RAINBOW:
      if(interval != 0)
        rainbow_interval = interval;
    break; 
    
    case BLOCKSWITCH:
      if(color1 != 0)
        blockswitch_color[0] = color1;
      if(color2 != 0)
        blockswitch_color[1] = color2;
      if(color3 != 0)
        blockswitch_width = color3;
      if(interval != 0)
        blockswitch_interval = interval;
    break;
    
    case RANDOMFLASH:
      if(interval != 0)
        randomflash_interval = interval;
    break;
  }
}

void FlightStrip::reverse()
{
  reversed = !reversed;
  if(!reversed)
    counter = 0;
  else
    counter = (upperBound - lowerBound);  
}

void FlightStrip::setBounds(uint16_t startLed, uint16_t endLed)
{
  lowerBound = startLed;
  upperBound = endLed;
}

void FlightStrip::disable()
{
  disabled = true;
}

void FlightStrip::constColor()
{
  for(int i = lowerBound ; i <= upperBound ; i++)
    (*strip).setPixelColor(i, constColor_color);
}

void FlightStrip::constTripleColor()
{
  for(int i = lowerBound ; i <= upperBound ; i++)
    (*strip).setPixelColor(i, constTripleColor_color[number]);
  if(number < 2)
    number++;
  else
    number = 0; 
}

void FlightStrip::chaseSingleColor()
{
  if(up)
    (*strip).setPixelColor((counter + lowerBound), chaseSingleColor_color);
  else
    (*strip).setPixelColor((counter + lowerBound), 0, 0, 0);
  
  if(!reversed)
  {
    if((counter + lowerBound) < upperBound && up)
      counter++;
    else if((counter + lowerBound) == upperBound && up)
      up = false;
    else if(counter > 0 && !up)
      counter--;
    else if(counter == 0 && !up)
      up = true;
  }
  else  //reversed
  {
    if(counter > 0 && up)
      counter--;
    else if(counter == 0 && up)
      up = false;
    else if((counter + lowerBound) < upperBound && !up)
      counter++;
    else if((counter + lowerBound) == upperBound && !up)
      up = true;
  }
}

void FlightStrip::chaseTripleColor()
{
  if(up)
    (*strip).setPixelColor((counter + lowerBound), chaseTripleColor_color[number]);
  else
    (*strip).setPixelColor((counter + lowerBound), 0, 0, 0);
  
  if(!reversed)
  {
    if((counter + lowerBound) < upperBound && up)
      counter++;
    else if((counter + lowerBound) == upperBound && up)
      up = false;
    else if(counter > 0 && !up)
      counter--;
    else if(counter == 0 && !up)
    {
      up = true;
      if(number < 2)
        number++;
      else
        number = 0;      
    }
  }
  else  //reversed
  {
    if(counter > 0 && up)
      counter--;
    else if(counter == 0 && up)
      up = false;
    else if((counter + lowerBound) < upperBound && !up)
      counter++;
    else if((counter + lowerBound) == upperBound && !up)
    {
      up = true;
      if(number < 2)
        number++;
      else
        number = 0;      
    }
  }
}

void FlightStrip::fillTripleColor()
{
  (*strip).setPixelColor((counter + lowerBound), fillTripleColor_color[number]);
  
  if(!reversed)
  {
    if((counter + lowerBound) == upperBound)
    {
      counter = 0;
      if(number < 2)
        number++;
      else
        number = 0;
    }
    else
      counter++;
  }
  else  //reversed
  {
    if(counter == 0)
    {
      counter = (upperBound - lowerBound);
      if(number < 2)
        number++;
      else
        number = 0;
    }
    else
      counter--;
  }
}

void FlightStrip::blinkColor()
{
  if(up) //Aktuell aus -> Einschalten
  {
    for(int i = lowerBound ; i <= upperBound ; i++)
      (*strip).setPixelColor(i, blinkColor_color); 
    up = false;
  }
  else  //Aktuell an -> Ausschalten
  {
    for(int i = lowerBound ; i <= upperBound ; i++)
      (*strip).setPixelColor(i, 0, 0, 0);
    up = true;
  }  
}

void FlightStrip::doubleflash()
{
  if(up) //Aktuell aus -> Einschalten
  {
    for(int i = lowerBound ; i <= upperBound ; i++)
      (*strip).setPixelColor(i, doubleflash_color); 
    up = false;
  }
  else  //Aktuell an -> Ausschalten
  {
    for(int i = lowerBound ; i <= upperBound ; i++)
      (*strip).setPixelColor(i, 0, 0, 0);
    up = true;
  }  
}

void FlightStrip::fillAgainst()
{
  if(up)
    (*strip).setPixelColor((counter + lowerBound), fillAgainst_color[0]);
  else
    (*strip).setPixelColor((counter + lowerBound), fillAgainst_color[1]);
  
  if((counter + lowerBound) < upperBound && up)
    counter++;
  else if((counter + lowerBound) == upperBound && up)
    up = false;
  else if(counter > 0 && !up)
    counter--;
  else if(counter == 0 && !up)
    up = true;
}

void FlightStrip::theaterChase()
{
  for(int i = lowerBound ; i <= upperBound ; i++)
    (*strip).setPixelColor(i, 0, 0, 0); 
  
  for(int i = (lowerBound + number) ; i <= upperBound ; i = (i + 3))
    (*strip).setPixelColor(i, theaterChase_color);  
    
  if(number < 2)
    number++;
  else
    number = 0;
}

void FlightStrip::knightrider()
{
  for(int i = lowerBound ; i <= upperBound ; i++)
      (*strip).setPixelColor(i, knightrider_background_color);
      
  if(!reversed) //normal
  {  
    for(int i = 0 ; i < knightrider_width ; i++)
      (*strip).setPixelColor((lowerBound + counter + i), knightrider_color); 
    
    if((counter + lowerBound) < (upperBound - knightrider_width + 1) && up)
      counter++;
    else if((counter + lowerBound) == (upperBound - knightrider_width + 1) && up)
    {
      up = false;
      counter--;
    }
    else if(counter > 0 && !up)
      counter--;
    else if(counter == 0 && !up)
    {
      up = true;
      counter++;
    }
  }
  else
  {
    for(int i = 0 ; i < knightrider_width ; i++)
      (*strip).setPixelColor((lowerBound + counter - i), knightrider_color); 
      
    if((counter + lowerBound) < upperBound && up)
      counter++;
    else if((counter + lowerBound) == upperBound && up)
    {
      up = false;
      counter--;
    }
    else if(counter > (knightrider_width - 1) && !up)
      counter--;
    else if(counter == (knightrider_width - 1) && !up)
    {
      up = true;
      counter++;
    }
  }
}

void FlightStrip::thunderstorm()
{
  if(!up)
  {
    for(int i = lowerBound ; i <= upperBound ; i++)
      (*strip).setPixelColor(i, thunderstorm_color);
    up = true;
    thunderstorm_interval = random(30, 50);
  }
  else
  {
    for(int i = lowerBound ; i <= upperBound ; i++)
      (*strip).setPixelColor(i, 0, 0, 0);
    up = false;
    thunderstorm_interval = random(50, 120);
  } 
}

void FlightStrip::policeRight()
{
  if(!up)  //Momentan aus
  {
    switch(number)
    {
      case 0:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 255, 0 , 0);
      break;
      
      case 1:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 255, 0 , 0);
      break;
      
      case 2:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 0, 0 , 255);
      break;
    }  //switch
    up = true;
    policeRight_interval = 60;
  }  //if
  else  //up -> Momentan an
  {
    switch(number)
    {
      case 0:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 0, 0 , 0);
        policeRight_interval = 60;
      break;
      
      case 1:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 0, 0 , 0);
        policeRight_interval = 60;
      break;
      
      case 2:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 0, 0 , 0);
        policeRight_interval = 400;
      break;
    }//switch
    up = false;
    if(number < 2)
      number++;
    else
      number = 0;
  }
}

void FlightStrip::policeLeft()
{
  if(!up)  //Momentan aus
  {
    switch(number)
    {
      case 0:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 255, 0 , 0);
      break;
      
      case 1:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 255, 0 , 0);
      break;
      
      case 2:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 0, 0 , 255);
      break;
    }  //switch
    up = true;
    policeLeft_interval = 60;
  }  //if
  else  //up -> Momentan an
  {
    switch(number)
    {
      case 0:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 0, 0 , 0);
        policeLeft_interval = 60;
      break;
      
      case 1:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 0, 0 , 0);
        policeLeft_interval = 60;
      break;
      
      case 2:
        for(int i = lowerBound ; i <= upperBound ; i++)
          (*strip).setPixelColor(i, 0, 0 , 0);
        policeLeft_interval = 400;
      break;
    }//switch
    up = false;
    if(number < 2)
      number++;
    else
      number = 0;
  }
}

void FlightStrip::rainbow()  //Basierend auf RainbowCycle der Neopixel Library
{ 
  if(!reversed)
  { 
    for(int i = 0 ; i <= (upperBound - lowerBound); i++)
    {
      (*strip).setPixelColor(i, wheel(((i * 256 / (upperBound - lowerBound + 1)) + counter) & 255));
    }
    if(counter < 255)
      counter++;
    else
      counter = 0;
  }
  else
  { 
    for(int i = 0 ; i <= (upperBound - lowerBound); i++)
    {
      (*strip).setPixelColor(i, wheel(((i * 256 / (upperBound - lowerBound + 1)) + counter) & 255));
    }
    if(counter > 0)
      counter--;
    else
      counter = 255;
  }
  
}

uint32_t FlightStrip::wheel(byte wheelPos)  //Basireend auf Wheel der Neopixel Library
{
  wheelPos = 255 - wheelPos;
  if(wheelPos < 85) 
    return (*strip).Color(255 - wheelPos * 3, 0, wheelPos * 3);
  else if(wheelPos < 170) 
  {
    wheelPos -= 85;
    return (*strip).Color(0, wheelPos * 3, 255 - wheelPos * 3);
  } 
  else 
  {
    wheelPos -= 170;
    return (*strip).Color(wheelPos * 3, 255 - wheelPos * 3, 0);
  }
}

void FlightStrip::blockswitch()
{
  if(!reversed)
  {
    if(up) //aktuell leuchtet zweiter -> auf ersten Schalten
    {
      for(int i = lowerBound ; i <= upperBound ; i++)
        (*strip).setPixelColor(i, 0);
      for(int a = 0; a <= (upperBound - lowerBound) ; a++)
      {
        if(a % (blockswitch_width * 2) == 0)
        {
          for(int i = 0; i < blockswitch_width; i++)
            (*strip).setPixelColor(constrain((a + i + lowerBound), lowerBound, upperBound), blockswitch_color[0]);  
        }  
      }
      up = !up;
    } 
    else //aktuell leuchtet zweiter -> auf ersten Schalten
    {
      for(int i = lowerBound ; i <= upperBound ; i++)
        (*strip).setPixelColor(i, 0);
      for(int a = 0; a <= (upperBound - lowerBound) ; a++)
      {
        if((a + blockswitch_width) % (blockswitch_width * 2) == 0)
        {
          for(int i = 0; i < blockswitch_width; i++)
            (*strip).setPixelColor(constrain((a + i + lowerBound), lowerBound, upperBound), blockswitch_color[1]);  
        }  
      }
      up = !up;
    } 
  }
  else
  {
    if(!up)
    {
      for(int i = lowerBound ; i <= upperBound ; i++)
        (*strip).setPixelColor(i, 0);
      for(int a = 0; a <= (upperBound - lowerBound) ; a++)
      {
        if(a % (blockswitch_width * 2) == 0)
        {
          for(int i = 0; i < blockswitch_width; i++)
            (*strip).setPixelColor(constrain((a + i + lowerBound), lowerBound, upperBound), blockswitch_color[0]);  
        }  
      }
      up = !up;
    } 
    else //aktuell leuchtet zweiter -> auf ersten Schalten
    {
      for(int i = lowerBound ; i <= upperBound ; i++)
        (*strip).setPixelColor(i, 0);
      for(int a = 0; a <= (upperBound - lowerBound) ; a++)
      {
        if((a + blockswitch_width) % (blockswitch_width * 2) == 0)
        {
          for(int i = 0; i < blockswitch_width; i++)
            (*strip).setPixelColor(constrain((a + i + lowerBound), lowerBound, upperBound), blockswitch_color[1]);  
        }  
      }
      up = !up;
    } 
  }
}

void FlightStrip::randomflash()
{
  uint8_t value[] = {0,100,255};
  
  for(int i = lowerBound ; i <= upperBound ; i++)
    (*strip).setPixelColor(i, (*strip).Color(value[random(0,3)], value[random(0,3)], value[random(0,3)])); 
}
