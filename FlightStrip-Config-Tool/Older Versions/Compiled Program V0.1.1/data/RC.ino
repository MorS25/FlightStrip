/*#######################################################
#########################################################
##            RCinput for one Channel on Pin 2         ##
##            V0.2 (21.03.2015)                        ##
##                                                     ##
## This Code reads a RC Signal on Pin 2 of Arduino     ##
## using an external interrupt.                        ##
## You can get it as raw data (in µS) or as percent    ##
## value by calling getTime() or getPercent()          ##
#########################################################
#######################################################*/


/*#######################################################
##                      Variablen                      ##
#######################################################*/

boolean rc1rising = true;
uint32_t rc1High = 0;
uint16_t rc1Value = 0;
uint32_t lastTimeRC1 = 0;
uint16_t lastFrameRC1 = 0;


/*#######################################################
##                      Functions                      ##
#######################################################*/

void initializeRC()
{
  attachInterrupt(0, isr1, RISING);
}

void isr1()
{
  if(rc1rising) //Steigende Flanke -> Startzeit speichern
  {
     rc1High = micros();
     rc1rising = false;
     attachInterrupt(0, isr1, FALLING); //Interrupt auf fallende Flanke umstellen
  }
  else //Fallende Flanke
  {
     uint16_t newFrame = micros() - rc1High; //neuen Wert berechnen
     if(newFrame >= 700 && newFrame <= 2300) //Ist der Wert realistisch? Zwischen 700 und 2300µS
     {
       if(newFrame >= (lastFrameRC1 - 80) && newFrame <= lastFrameRC1 + 80) //Ist der eingelesene Wert im +-80 Intevall um den letzten Wert? (Soll einzelene Fehlwerte verhindern. Wert muss mindestens zwei mal hintereinander kommen
       {
         rc1Value = newFrame; //Ist der Wert ok, wird der aktuelle RC Wert überschrieben und die Zeit gespeichert

         lastTimeRC1 = micros();
       }
       lastFrameRC1 = newFrame; //Auch wenn der RC Wert nicht aktualisiert wurde, wird der Wert zwischengespeichert für obere Überprüfung
     }  
     rc1rising = true;
     attachInterrupt(0, isr1, RISING); //Interrupt auf steigende Flanke umstellen
  }
}

uint16_t getTime()
{
  return rc1Value;
}

int16_t getPercent()
{
  return map(rc1Value, 1000, 2000, -100, 100);
}

