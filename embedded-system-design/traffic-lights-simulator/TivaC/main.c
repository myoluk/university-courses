// Keil uVision4 : Traffic Lights Simulator

// TivaC header file
#include "tm4c123gh6pm.h"

// Prototypes func
void PortF_Init(void);
void Delay(volatile unsigned long);

unsigned long Led;

// Main
int main(void){  
  PortF_Init();  // Initialize Port F, make PF3-1 out
	
  while(1){
		// Red Light
		Led = 0x02;								// Red LED Color R--
		GPIO_PORTF_DATA_R = Led;	// Mask with Led
		Delay(10);								// 10s loop
		GPIO_PORTF_DATA_R = 0x11;	// Mask with OFF
		
		// Yellow Light
		Led = 0x0A;								// Yellow LED Color RG-
		GPIO_PORTF_DATA_R = Led;	// Mask with Led
		Delay(1);									// 1s loop
		GPIO_PORTF_DATA_R = 0x11;	// Mask with OFF
		
		// Green Light
		Led = 0x08;								// Green LED Color -G-
		GPIO_PORTF_DATA_R = Led;	// Mask with Led
		Delay(15);								// 15s loop
		GPIO_PORTF_DATA_R = 0x11;	// Mask with OFF
  }
}

void PortF_Init(void) {
	volatile unsigned long delay;
	SYSCTL_RCGC2_R |= 0x00000020;		// 1) Activate clock for Port F
	delay = SYSCTL_RCGC2_R;					// allow time for clock to start
	GPIO_PORTF_LOCK_R = 0x4C4F434B;	// 2) Unlock GPIO Port F
	GPIO_PORTF_CR_R = 0x0E;					// allow changes to PF3-1
	GPIO_PORTF_AMSEL_R = 0x00;			// 3) Disable analog for Port F
	GPIO_PORTF_PCTL_R = 0x00000000;	// 4) PCTL GPIO on PF3-1
	GPIO_PORTF_DIR_R = 0x0E;				// 5) PF3-1 out
	GPIO_PORTF_AFSEL_R = 0x00;			// 6) Disable alt func on PF7-0
	GPIO_PORTF_DEN_R = 0x0E;				// 7) Enable digital I/O on PF3-1
}

void Delay(volatile unsigned long time) {
	time *= 1454480;	// 1s * time
	while(time){
		time--;
	}
}

