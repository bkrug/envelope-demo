       DEF  SETHRZ
*
       REF  HERTZ

*
* Addresses
*
       COPY 'CPUADR.asm'

*
* Public Method:
* Set HERTZ
*    0 = 60hz
*    -1 = 50hz
*
SETHRZ
       DECT R10
       MOV  R11,*R10
*
       BL   @INTTIM
* Turn on VDP interrupts
       LIMI 2
* Skip first VDP interrupt; it's too late to measure the full time.
       MOVB @VINTTM,R0
FRSTLP CB   @VINTTM,R0
       JEQ  FRSTLP
* Let R9 = recorded time at begging of interrupt
       BL   @GETTIM
       MOV  R2,R9
VDPLP
* Let R0 = most recently read VDP time
       MOVB @VINTTM,R0
* Wait for VDP interrupt
WAITLP CB   @VINTTM,R0
       JEQ  WAITLP
* Let R2 = newly recorded time
       BL   @GETTIM
* Let R9 = quantity of time between interrupts
       S    R2,R9
* Turn off interrupts so we can write to VDP
       LIMI 0
*
* In a 50hz environment R3 should contain about 938.
* We'll accept 888 - 988 in case an emulator is not accurate.
* Any other value implies 60hz or an emulator that doesn't implement the CRU timer.
*
       CLR  R4
       CI   R9,988
       JH   HRZ1
       CI   R9,888
       JL   HRZ1
       DEC  R4
HRZ1   MOVB R4,@HERTZ
*
       MOV  *R10+,R11
       RT

*
* Private Method:
* Initialize Timer
*
INTTIM 
       CLR  R12         CRU base of the TMS9901 
       SBO  0           Enter timer mode 
       LI   R1,>3FFF    Maximum value
       INCT R12         Address of bit 1 
       LDCR R1,14       Load value 
       DECT R12         There is a faster way (see http://www.nouspikel.com/ti99/titechpages.htm) 
       SBZ  0           Exit clock mode, start decrementer 
       RT

*
* Private Method:
* Get Time from CRU
* Output: R2
*
GETTIM CLR  R12 
       SBO  0           Enter timer mode 
       STCR R2,15       Read current value (plus mode bit)
       SBZ  0
       SRL  R2,1        Get rid of mode bit
       ANDI R2,>3FFF
       RT