       DEF  SCHUMN

*
* This is auto-generated code.
* It is only included in the repo for the convenience of people who haven't cloned it.
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
SCHUMN DATA SCHU1,SCHU2,SCHU3
* Data structures dealing with repeated music
       DATA REPT1,REPT2,REPT3
* Duration ratio in 60hz environment
       DATA 5,4
* Duration ratio in 50hz environment
       DATA 1,1

REPT1
       DATA SCHU1A,SCHU1
       DATA REPEAT,REPT1
REPT2
       DATA SCHU2A,SCHU2
       DATA REPEAT,REPT2
REPT3
       DATA SCHU3A,SCHU3
       DATA REPEAT,REPT3

* Generator 1
* Measure 1
SCHU1
       BYTE C1,N8
* Measure 2 - 4
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE Bb2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE E2,N8
       BYTE E2,N4
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N4
* Measure 5
       BYTE REST,N8
       BYTE C2,N8
       BYTE REST,N8
       BYTE F2,N8
       BYTE REST,N8
       BYTE E2,N8
       BYTE E2,N4
* Measure 6 - 8
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE Bb2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE E2,N8
       BYTE E2,N4
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N4
* Measure 9
       BYTE REST,N8
       BYTE C2,N8
       BYTE REST,N8
       BYTE F2,N8
       BYTE REST,N8
       BYTE E2,N8
       BYTE E2,N8
       BYTE C2,N8
* Measure 10
       BYTE Bb2,N4DOT
       BYTE A2,N8
       BYTE G2,N4DOT
       BYTE C2,N8
* Measure 11
       BYTE Bb2,N8
       BYTE A2,N8
       BYTE G2,N8
       BYTE F2,N8
       BYTE G2,N4DOT
       BYTE C2,N8
* Measure 12
       BYTE F2,N4DOT
       BYTE A2,N8
       BYTE C3,N4DOT
       BYTE F2,N8
* Measure 13 - 14
       BYTE Bb2,N8
       BYTE D3,N8
       BYTE F3,N8
       BYTE D3,N8
       BYTE C3,N4DOT
       BYTE REST,N4
       BYTE E2,N8
       BYTE E2,N4
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE C2,N8
* Measure 15
       BYTE REST,N8
       BYTE D2,N8
       BYTE REST,N8
       BYTE Bb1,N8
       BYTE REST,N8
       BYTE A1,N8
       BYTE A1,N8
       BYTE C2,N8
* Measure 16
       BYTE Bb2,N4DOT
       BYTE A2,N8
       BYTE G2,N4DOT
       BYTE C2,N8
* Measure 17
       BYTE Bb2,N8
       BYTE A2,N8
       BYTE G2,N8
       BYTE F2,N8
       BYTE G2,N4DOT
       BYTE C2,N8
* Measure 18
       BYTE F2,N4DOT
       BYTE A2,N8
       BYTE C3,N4DOT
       BYTE F2,N8
* Measure 19 - 20
       BYTE Bb2,N8
       BYTE D3,N8
       BYTE F3,N8
       BYTE D3,N8
       BYTE C3,N4DOT
       BYTE REST,N4
       BYTE E2,N8
       BYTE E2,N4
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE C2,N8
* Measure 21
       BYTE REST,N8
       BYTE D2,N8
       BYTE REST,N8
       BYTE Bb1,N8
       BYTE REST,N8
       BYTE A1,N8
       BYTE A1,N8
       BYTE REST,N8
SCHU1A
*

* Generator 2
* Measure 1
SCHU2
       BYTE REST,N8
* Measure 2
       BYTE F1,N4DOT
       BYTE A1,N8
       BYTE C2,N4DOT
       BYTE F1,N8
* Measure 3
       BYTE Bb1,N8
       BYTE D2,N8
       BYTE F2,N8
       BYTE D2,N8
       BYTE C2,N4DOT
       BYTE A1,N8
* Measure 4
       BYTE Bb1,N8
       BYTE G1,N8
       BYTE C1,N8
       BYTE Bb1,N8
       BYTE A1,N8
       BYTE F1,N8
       BYTE C1,N8
       BYTE A1,N8
* Measure 5
       BYTE E1,N4
       BYTE D1,N4
       BYTE C1,N4
       BYTE REST,N8
       BYTE C1,N8
* Measure 6
       BYTE F1,N4DOT
       BYTE A1,N8
       BYTE C2,N4DOT
       BYTE F1,N8
* Measure 7
       BYTE Bb1,N8
       BYTE D2,N8
       BYTE F2,N8
       BYTE D2,N8
       BYTE C2,N4DOT
       BYTE A1,N8
* Measure 8
       BYTE Bb1,N8
       BYTE G1,N8
       BYTE C1,N8
       BYTE Bb1,N8
       BYTE A1,N8
       BYTE F1,N8
       BYTE C1,N8
       BYTE A1,N8
* Measure 9
       BYTE E1,N4
       BYTE D1,N4
       BYTE C1,N4
       BYTE REST,N8
       BYTE C1,N8
* Measure 10 - 13
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE REST,N4
       BYTE Bb1,N8
       BYTE Bb1,N8
       BYTE REST,N4
       BYTE C2,N8
       BYTE C2,N8
       BYTE B1,N8
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE REST,N4
       BYTE A1,N8
       BYTE A1,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE REST,N8
       BYTE F2,N8
       BYTE REST,N8
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N8
* Measure 14
       BYTE Bb1,N8
       BYTE G1,N8
       BYTE C1,N8
       BYTE Bb1,N8
       BYTE A1,N8
       BYTE F1,N8
       BYTE C1,N8
       BYTE A1,N8
* Measure 15
       BYTE G1,N4
       BYTE E1,N4
       BYTE F1,N4
       BYTE REST,N8
       BYTE C1,N8
* Measure 16 - 19
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE REST,N4
       BYTE Bb1,N8
       BYTE Bb1,N8
       BYTE REST,N4
       BYTE C2,N8
       BYTE C2,N8
       BYTE B1,N8
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE REST,N4
       BYTE A1,N8
       BYTE A1,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE REST,N8
       BYTE F2,N8
       BYTE REST,N8
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N8
* Measure 20
       BYTE Bb1,N8
       BYTE G1,N8
       BYTE C1,N8
       BYTE Bb1,N8
       BYTE A1,N8
       BYTE F1,N8
       BYTE C1,N8
       BYTE A1,N8
* Measure 21
       BYTE G1,N4
       BYTE E1,N4
       BYTE F1,N4
       BYTE REST,N4
SCHU2A
*

* Generator 3
* Measure 1 - 4
SCHU3
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE A2,N8
       BYTE A2,N8
       BYTE REST,N4
       BYTE Bb2,N8
       BYTE D3,N8
       BYTE REST,N4
       BYTE A2,N8
       BYTE A2,N8
       BYTE REST,N4
       BYTE Bb2,N8
       BYTE Bb2,N4
       BYTE REST,N8
       BYTE F2,N8
       BYTE F2,N4
* Measure 5
       BYTE REST,N8
       BYTE G2,N8
       BYTE REST,N8
       BYTE G2,N8
       BYTE REST,N8
       BYTE G2,N8
       BYTE G2,N4
* Measure 6 - 8
       BYTE REST,N8
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE A2,N8
       BYTE A2,N8
       BYTE REST,N4
       BYTE Bb2,N8
       BYTE D3,N8
       BYTE REST,N4
       BYTE A2,N8
       BYTE A2,N8
       BYTE REST,N4
       BYTE Bb2,N8
       BYTE Bb2,N4
       BYTE REST,N8
       BYTE F2,N8
       BYTE F2,N4
* Measure 9
       BYTE REST,N8
       BYTE G2,N8
       BYTE REST,N8
       BYTE G2,N8
       BYTE REST,N8
       BYTE G2,N8
       BYTE G2,N8
       BYTE REST,N8
* Measure 10
       BYTE G1,N4DOT
       BYTE F1,N8
       BYTE E1,N4DOT
       BYTE C1,N8
* Measure 11
       BYTE G1,N8
       BYTE F1,N8
       BYTE E1,N8
       BYTE D1,N8
       BYTE E1,N4DOT
       BYTE C1,N8
* Measure 12
       BYTE F1,N4DOT
       BYTE A1,N8
       BYTE C2,N4DOT
       BYTE F1,N8
* Measure 13
       BYTE Bb1,N8
       BYTE D2,N8
       BYTE F2,N8
       BYTE D2,N8
       BYTE C2,N4DOT
       BYTE A1,N8
* Measure 14
       BYTE REST,N8
       BYTE Bb2,N8
       BYTE Bb2,N4
       BYTE REST,N8
       BYTE F2,N8
       BYTE F2,N8
       BYTE F2,N8
* Measure 15
       BYTE Bb0,N4
       BYTE C1,N4
       BYTE F1,N4
       BYTE REST,N4
* Measure 16
       BYTE G1,N4DOT
       BYTE F1,N8
       BYTE E1,N4DOT
       BYTE C1,N8
* Measure 17
       BYTE G1,N8
       BYTE F1,N8
       BYTE E1,N8
       BYTE D1,N8
       BYTE E1,N4DOT
       BYTE C1,N8
* Measure 18
       BYTE F1,N4DOT
       BYTE A1,N8
       BYTE C2,N4DOT
       BYTE F1,N8
* Measure 19
       BYTE Bb1,N8
       BYTE D2,N8
       BYTE F2,N8
       BYTE D2,N8
       BYTE C2,N4DOT
       BYTE A1,N8
* Measure 20
       BYTE REST,N8
       BYTE Bb2,N8
       BYTE Bb2,N4
       BYTE REST,N8
       BYTE F2,N8
       BYTE F2,N8
       BYTE F2,N8
* Measure 21
       BYTE Bb0,N4
       BYTE C1,N4
       BYTE REST,N2
SCHU3A
*

