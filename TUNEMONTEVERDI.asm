       DEF  MONTEV

*
* This is auto-generated code.
* It is only included in the repo for the convenience of people who haven't cloned it.
*

*
* Lasciate i monti
* Claudio Monteverdi
* Source: http://musescore.com/user/30939776/scores/6710387
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
MONTEV DATA MONT1,MONT2,MONT3
* Data structures dealing with repeated music
       DATA REPT1,REPT2,REPT3
* Duration ratio in 60hz environment
       DATA 15,11
* Duration ratio in 50hz environment
       DATA 75,66

REPT1
       DATA MONT1A,STOP
       DATA REPEAT,STOP
REPT2
       DATA MONT2A,STOP
       DATA REPEAT,STOP
REPT3
       DATA MONT3A,STOP
       DATA REPEAT,STOP

* Generator 1
* Measure 1
MONT1
       BYTE G3,N4
       BYTE B3,N8
       BYTE C4,N8
       BYTE D4,N8
       BYTE D4,N16
       BYTE E4,N16
       BYTE D4,N8
       BYTE C4,N8
* Measure 2
       BYTE B3,N16
       BYTE A3,N16
       BYTE B3,N16
       BYTE C4,N16
       BYTE B3,N8
       BYTE C4,N8
       BYTE D4,N16
       BYTE C4,N16
       BYTE D4,N16
       BYTE E4,N16
       BYTE D4,N8
       BYTE C4,N16
       BYTE C4,N16
* Measure 3
       BYTE B3,N16
       BYTE A3,N16
       BYTE B3,N16
       BYTE C4,N16
       BYTE B3,N8
       BYTE C4,N8
       BYTE D4,N16
       BYTE C4,N16
       BYTE D4,N16
       BYTE E4,N16
       BYTE D4,N8
       BYTE C4,N16
       BYTE B3,N16
* Measure 4
       BYTE A3,N8
       BYTE G3,N8
       BYTE A3,N4
       BYTE G3,N4
       BYTE REST,N4
* Measure 5
       BYTE G3,N4
       BYTE B3,N8
       BYTE C4,N8
       BYTE D4,N8
       BYTE D4,N16
       BYTE E4,N16
       BYTE D4,N8
       BYTE C4,N8
* Measure 6
       BYTE B3,N16
       BYTE A3,N16
       BYTE B3,N16
       BYTE C4,N16
       BYTE B3,N8
       BYTE C4,N8
       BYTE D4,N16
       BYTE C4,N16
       BYTE D4,N16
       BYTE E4,N16
       BYTE D4,N8
       BYTE C4,N16
       BYTE C4,N16
* Measure 7
       BYTE B3,N16
       BYTE A3,N16
       BYTE B3,N16
       BYTE C4,N16
       BYTE B3,N8
       BYTE C4,N8
       BYTE D4,N16
       BYTE C4,N16
       BYTE D4,N16
       BYTE E4,N16
       BYTE D4,N8
       BYTE C4,N16
       BYTE B3,N16
* Measure 8
       BYTE A3,N8
       BYTE G3,N8
       BYTE A3,N4
* Measure 9
       BYTE G3,N4
       BYTE D4,N8
       BYTE Eb4,N4
       BYTE C4,N8
* Measure 10
       BYTE D4,N8
       BYTE D4,N8
       BYTE Bb3,N8
       BYTE C4,N4
       BYTE A3,N8
* Measure 11
       BYTE Bb3,N8
       BYTE Bb3,N8
       BYTE G3,N8
       BYTE C4,N4
       BYTE A3,N8
* Measure 12
       BYTE D4,N4DOT
       BYTE Eb4,N8
       BYTE C4,N4
* Measure 13
       BYTE Bb3,N4
       BYTE F4,N8
       BYTE E4,N4
       BYTE E4,N8
* Measure 14
       BYTE D4,N8
       BYTE D4,N8
       BYTE D4,N8
       BYTE C4,N4
       BYTE C4,N8
* Measure 15
       BYTE Bb3,N8
       BYTE Bb3,N8
       BYTE C4,N8
       BYTE A3,N4
       BYTE Bb3,N8
* Measure 16
       BYTE G3,N4DOT
       BYTE C4,N8
       BYTE A3,N4
* Measure 17
       BYTE G3,N4
       BYTE D4,N8
       BYTE E4,N4
       BYTE Fs4,N8
* Measure 18
       BYTE G4,N8DOT
       BYTE A4,N16
       BYTE G4,N8
       BYTE A4,N16
       BYTE G4,N16
       BYTE F4,N8
       BYTE D4,N8
* Measure 19
       BYTE G4,N8DOT
       BYTE F4,N16
       BYTE E4,N16
       BYTE D4,N16
       BYTE C4,N8DOT
       BYTE B3,N16
       BYTE A3,N8
* Measure 20
       BYTE D4,N8
       BYTE C4,N16
       BYTE B3,N16
       BYTE A3,N8
       BYTE B3,N4
       BYTE Cs4,N8
* Measure 21
       BYTE D4,N8
       BYTE E4,N16
       BYTE Fs4,N16
       BYTE G4,N8
       BYTE E4,N8
       BYTE A4,N8
       BYTE Fs4,N8
* Measure 22
       BYTE G4,N8DOT
       BYTE F4,N16
       BYTE D4,N8
       BYTE E4,N16
       BYTE D4,N16
       BYTE E4,N16
       BYTE F4,N16
       BYTE G4,N16
       BYTE A4,N16
* Measure 23
       BYTE Fs4,N8DOT
       BYTE D4,N16
       BYTE E4,N16
       BYTE Fs4,N16
       BYTE G4,N8
       BYTE Fs4,N4
* Measure 24
       BYTE G4,N4
       BYTE D4,N8
       BYTE E4,N4
       BYTE Fs4,N8
* Measure 25
       BYTE G4,N8DOT
       BYTE A4,N16
       BYTE G4,N8
       BYTE A4,N16
       BYTE G4,N16
       BYTE F4,N8
       BYTE D4,N8
* Measure 26
       BYTE G4,N8DOT
       BYTE F4,N16
       BYTE E4,N16
       BYTE D4,N16
       BYTE C4,N8DOT
       BYTE B3,N16
       BYTE A3,N8
* Measure 27
       BYTE D4,N8
       BYTE C4,N16
       BYTE B3,N16
       BYTE A3,N8
       BYTE B3,N4
       BYTE Cs4,N8
* Measure 28
       BYTE D4,N8
       BYTE E4,N16
       BYTE Fs4,N16
       BYTE G4,N8
       BYTE E4,N8
       BYTE A4,N8
       BYTE Fs4,N8
* Measure 29
       BYTE G4,N8DOT
       BYTE F4,N16
       BYTE D4,N8
       BYTE E4,N16
       BYTE D4,N16
       BYTE E4,N16
       BYTE F4,N16
       BYTE G4,N16
       BYTE A4,N16
* Measure 30
       BYTE Fs4,N8DOT
       BYTE D4,N16
       BYTE E4,N16
       BYTE Fs4,N16
       BYTE G4,N8
       BYTE Fs4,N4
* Measure 31
       BYTE G4,N2DOT
MONT1A
*

* Generator 2
* Measure 1
MONT2
       BYTE REST,N2
       BYTE G2,N4
       BYTE B2,N8
       BYTE C3,N8
* Measure 2
       BYTE D3,N8
       BYTE D3,N16
       BYTE E3,N16
       BYTE D3,N8
       BYTE C3,N8
       BYTE B2,N16
       BYTE A2,N16
       BYTE B2,N16
       BYTE C3,N16
       BYTE B2,N8
       BYTE C3,N8
* Measure 3
       BYTE D3,N16
       BYTE C3,N16
       BYTE D3,N16
       BYTE E3,N16
       BYTE D3,N8
       BYTE C3,N16
       BYTE C3,N16
       BYTE B2,N16
       BYTE A2,N16
       BYTE B2,N16
       BYTE C3,N16
       BYTE B2,N8
       BYTE A2,N16
       BYTE G2,N16
* Measure 4 - 5
       BYTE Fs2,N8
       BYTE G2,N8
       BYTE G2,N8
       BYTE Fs2,N8
       BYTE G2,N4
       BYTE REST,N2DOT
       BYTE G2,N4
       BYTE B2,N8
       BYTE C3,N8
* Measure 6
       BYTE D3,N8
       BYTE D3,N16
       BYTE E3,N16
       BYTE D3,N8
       BYTE C3,N8
       BYTE B2,N16
       BYTE A2,N16
       BYTE B2,N16
       BYTE C3,N16
       BYTE B2,N8
       BYTE C3,N8
* Measure 7
       BYTE D3,N16
       BYTE C3,N16
       BYTE D3,N16
       BYTE E3,N16
       BYTE D3,N8
       BYTE C3,N16
       BYTE C3,N16
       BYTE B2,N16
       BYTE A2,N16
       BYTE B2,N16
       BYTE C3,N16
       BYTE B2,N8
       BYTE A2,N16
       BYTE G2,N16
* Measure 8
       BYTE Fs2,N8
       BYTE G2,N8
       BYTE G2,N8
       BYTE Fs2,N8
* Measure 9
       BYTE G2,N4
       BYTE A2,N8
       BYTE C3,N4
       BYTE G2,N8
* Measure 10
       BYTE Bb2,N8
       BYTE Bb2,N8
       BYTE F2,N8
       BYTE A2,N4
       BYTE E2,N8
* Measure 11
       BYTE G2,N8
       BYTE G2,N8
       BYTE Bb2,N8
       BYTE A2,N4
       BYTE C3,N8
* Measure 12
       BYTE Bb2,N4DOT
       BYTE Bb2,N4
       BYTE A2,N8
* Measure 13
       BYTE Bb2,N4
       BYTE D3,N8
       BYTE C3,N4
       BYTE C3,N8
* Measure 14
       BYTE Bb2,N8
       BYTE Bb2,N8
       BYTE Bb2,N8
       BYTE A2,N4
       BYTE A2,N8
* Measure 15
       BYTE G2,N8
       BYTE G2,N8
       BYTE G2,N8
       BYTE F2,N4
       BYTE F2,N8
* Measure 16
       BYTE Bb2,N4
       BYTE G2,N8
       BYTE G2,N4
       BYTE Fs2,N8
* Measure 17
       BYTE G2,N4
       BYTE B2,N8
       BYTE E3,N8
       BYTE Cs3,N8
       BYTE D3,N8
* Measure 18
       BYTE D3,N4
       BYTE E3,N8
       BYTE D3,N4
       BYTE B2,N8
* Measure 19
       BYTE E3,N8DOT
       BYTE D3,N16
       BYTE C3,N16
       BYTE B2,N16
       BYTE A2,N8DOT
       BYTE B2,N16
       BYTE C3,N8
* Measure 20
       BYTE B2,N8
       BYTE A2,N16
       BYTE G2,N16
       BYTE A2,N8
       BYTE G2,N8
       BYTE Gs2,N8
       BYTE A2,N8
* Measure 21
       BYTE A2,N4
       BYTE D3,N8
       BYTE C3,N4
       BYTE A2,N8
* Measure 22
       BYTE B2,N8DOT
       BYTE C3,N16
       BYTE D3,N8
       BYTE C3,N4
       BYTE B2,N8
* Measure 23
       BYTE D3,N4
       BYTE G2,N4
       BYTE D3,N4
* Measure 24
       BYTE B2,N4
       BYTE B2,N8
       BYTE E3,N8
       BYTE Cs3,N8
       BYTE D3,N8
* Measure 25
       BYTE D3,N4
       BYTE E3,N8
       BYTE D3,N4
       BYTE B2,N8
* Measure 26
       BYTE E3,N8DOT
       BYTE D3,N16
       BYTE C3,N16
       BYTE B2,N16
       BYTE A2,N8DOT
       BYTE B2,N16
       BYTE C3,N8
* Measure 27
       BYTE B2,N8
       BYTE A2,N16
       BYTE G2,N16
       BYTE A2,N8
       BYTE G2,N8
       BYTE Gs2,N8
       BYTE A2,N8
* Measure 28
       BYTE A2,N4
       BYTE D3,N8
       BYTE C3,N4
       BYTE A2,N8
* Measure 29
       BYTE B2,N8DOT
       BYTE C3,N16
       BYTE D3,N8
       BYTE C3,N4
       BYTE B2,N8
* Measure 30
       BYTE D3,N4
       BYTE G2,N4
       BYTE D3,N4
* Measure 31
       BYTE B2,N2DOT
MONT2A
*

* Generator 3
* Measure 1
MONT3
       BYTE G1,N4
       BYTE REST,N8
       BYTE A1,N8
       BYTE G1,N4
       BYTE REST,N8
       BYTE C1,N8
* Measure 2
       BYTE G1,N4
       BYTE REST,N8
       BYTE A1,N8
       BYTE G1,N4
       BYTE REST,N8
       BYTE C1,N8
* Measure 3
       BYTE REST,N4DOT      * Invalid: G0
       BYTE A0,N16
       BYTE A0,N16
       BYTE B0,N4DOT
       BYTE C1,N8
* Measure 4
       BYTE D1,N8
       BYTE E1,N8
       BYTE C1,N8
       BYTE D1,N8
       BYTE REST,N4      * Invalid: G0
       BYTE REST,N4
* Measure 5
       BYTE G1,N4
       BYTE REST,N8
       BYTE A1,N8
       BYTE G1,N4
       BYTE REST,N8
       BYTE C1,N8
* Measure 6
       BYTE G1,N4
       BYTE REST,N8
       BYTE A1,N8
       BYTE G1,N4
       BYTE REST,N8
       BYTE C1,N8
* Measure 7
       BYTE REST,N4DOT      * Invalid: G0
       BYTE A0,N16
       BYTE A0,N16
       BYTE B0,N4DOT
       BYTE C1,N8
* Measure 8
       BYTE D1,N8
       BYTE E1,N8
       BYTE C1,N8
       BYTE D1,N8
* Measure 9 - 13
       BYTE REST,N4      * Invalid: G0
       BYTE REST,N4DOT
       BYTE REST,252
       BYTE Bb1,N8
       BYTE C2,N4
       BYTE A1,N8
* Measure 14
       BYTE Bb1,N8
       BYTE Bb1,N8
       BYTE G1,N8
       BYTE A1,N4
       BYTE F1,N8
* Measure 15
       BYTE G1,N8
       BYTE G1,N8
       BYTE E1,N8
       BYTE F1,N4
       BYTE D1,N8
* Measure 16
       BYTE Eb1,N4DOT
       BYTE C1,N8
       BYTE D1,N4
* Measure 17
       BYTE REST,N4      * Invalid: G0
       BYTE B1,N8
       BYTE C2,N8
       BYTE A1,N8
       BYTE D2,N8
* Measure 18
       BYTE G1,N4
       BYTE E1,N8
       BYTE F1,N8
       BYTE D1,N8
       BYTE G1,N8
* Measure 19
       BYTE C1,N8DOT
       BYTE D1,N16
       BYTE E1,N8
       BYTE F1,N8DOT
       BYTE G1,N16
       BYTE A1,N8
* Measure 20
       BYTE G1,N4
       BYTE Fs1,N8
       BYTE G1,N8
       BYTE E1,N8
       BYTE A1,N8
* Measure 21
       BYTE D1,N4
       BYTE B0,N8
       BYTE C1,N8
       BYTE A0,N8
       BYTE D1,N8
* Measure 22
       BYTE REST,N8DOT      * Invalid: G0
       BYTE A0,N16
       BYTE B0,N8
       BYTE C1,N8DOT
       BYTE D1,N16
       BYTE E1,N8
* Measure 23
       BYTE D1,N4
       BYTE C1,N4
       BYTE D1,N4
* Measure 24
       BYTE REST,N4      * Invalid: G0
       BYTE B1,N8
       BYTE C2,N8
       BYTE A1,N8
       BYTE D2,N8
* Measure 25
       BYTE G1,N4
       BYTE E1,N8
       BYTE F1,N8
       BYTE D1,N8
       BYTE G1,N8
* Measure 26
       BYTE C1,N8DOT
       BYTE D1,N16
       BYTE E1,N8
       BYTE F1,N8DOT
       BYTE G1,N16
       BYTE A1,N8
* Measure 27
       BYTE G1,N4
       BYTE Fs1,N8
       BYTE G1,N8
       BYTE E1,N8
       BYTE A1,N8
* Measure 28
       BYTE D1,N4
       BYTE B0,N8
       BYTE C1,N8
       BYTE A0,N8
       BYTE D1,N8
* Measure 29
       BYTE REST,N8DOT      * Invalid: G0
       BYTE A0,N16
       BYTE B0,N8
       BYTE C1,N8DOT
       BYTE D1,N16
       BYTE E1,N8
* Measure 30
       BYTE D1,N4
       BYTE C1,N4
       BYTE D1,N4
* Measure 31
       BYTE REST,N2DOT      * Invalid: G0
MONT3A
*

