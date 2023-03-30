       DEF  FOSTER

*
* This is auto-generated code.
* It is only included in the repo for the convenience of people who haven't cloned it.
*

*
* Old Folks At Home
* Stephen Foster
* Source: http://musescore.com/user/75150/scores/3562236
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
FOSTER DATA FOST1,FOST2,FOST3
* Data structures dealing with repeated music
       DATA REPT1,REPT2,REPT3
* Duration ratio in 60hz environment
       DATA 5,4
* Duration ratio in 50hz environment
       DATA 1,1

REPT1
       DATA FOST1B,FOST1A
       DATA FOST1C,FOST1B
       DATA FOST1D,FOST1C
       DATA REPEAT,STOP
REPT2
       DATA FOST2B,FOST2A
       DATA FOST2C,FOST2B
       DATA FOST2D,FOST2C
       DATA REPEAT,STOP
REPT3
       DATA FOST3B,FOST3A
       DATA FOST3C,FOST3B
       DATA FOST3D,FOST3C
       DATA REPEAT,STOP

* Generator 1
* Measure 1
FOST1
       BYTE B2,N2
       BYTE A2,N8
       BYTE G2,N8
       BYTE B2,N8
       BYTE A2,N8
* Measure 2
       BYTE G2,N4
       BYTE G3,N4
       BYTE E3,N4
       BYTE G3,N4
* Measure 3
       BYTE D3,N2
       BYTE B2,N4DOT
       BYTE G2,N8
* Measure 4
       BYTE A2,N2DOT
       BYTE REST,N4
* Measure 5
       BYTE B2,N2
       BYTE A2,N8
       BYTE G2,N8
       BYTE B2,N8
       BYTE A2,N8
* Measure 6
       BYTE G2,N4
       BYTE G3,N4
       BYTE E3,N4
       BYTE G3,N4
* Measure 7
       BYTE D3,N4
       BYTE B2,N8
       BYTE G2,N8
       BYTE A2,N4
       BYTE A2,N4
* Measure 8
       BYTE G2,N2DOT
       BYTE REST,N4
* Measure 9
       BYTE B2,N2
       BYTE A2,N8
       BYTE G2,N8
       BYTE B2,N8
       BYTE A2,N8
* Measure 10
       BYTE G2,N4
       BYTE G3,N4
       BYTE E3,N4
       BYTE G3,N4
* Measure 11
       BYTE D3,N2
       BYTE B2,N4DOT
       BYTE G2,N8
* Measure 12
       BYTE A2,N2DOT
       BYTE REST,N4
* Measure 13
       BYTE B2,N2
       BYTE A2,N8
       BYTE G2,N8
       BYTE B2,N8
       BYTE A2,N8
* Measure 14
       BYTE G2,N4
       BYTE G3,N4
       BYTE E3,N4
       BYTE G3,N4
* Measure 15
       BYTE D3,N4
       BYTE B2,N8
       BYTE G2,N8
       BYTE A2,N4
       BYTE A2,N4
* Measure 16
       BYTE G2,N2DOT
       BYTE REST,N4
* Measure 17
       BYTE A2,N4DOT
       BYTE B2,N8
       BYTE C3,N4
       BYTE C3,N4
* Measure 18
       BYTE D3,N4DOT
       BYTE E3,N8
       BYTE D3,N4
       BYTE B2,N4
* Measure 19
       BYTE G3,N4
       BYTE E3,N4
       BYTE C3,N4
       BYTE E3,N4
* Measure 20
       BYTE B2,N2DOT
       BYTE REST,N4
* Measure 21
       BYTE B2,N2
       BYTE A2,N8
       BYTE G2,N8
       BYTE B2,N8
       BYTE A2,N8
* Measure 22
       BYTE G2,N4
       BYTE G3,N4
       BYTE E3,N4
       BYTE G3,N4
* Measure 23
       BYTE D3,N4
       BYTE B2,N8
       BYTE G2,N8
       BYTE A2,N4
       BYTE A2,N4
* Measure 24
       BYTE G2,N2DOT
       BYTE REST,N4
* Measure 25
FOST1A
       BYTE B3,N8
       BYTE E4,N16
       BYTE D4,N16
       BYTE A3,N8
       BYTE E4,N16
       BYTE D4,N16
* Measure 26
       BYTE G3,N8
       BYTE G4,N16
       BYTE Fs4,N16
       BYTE G4,N8
       BYTE E4,N8
* Measure 27
       BYTE D4,N8
       BYTE B3,N8
       BYTE B3,N16
       BYTE A3,N16
       BYTE G3,N8
* Measure 28
       BYTE A3,N8
       BYTE D4,N16
       BYTE Cs4,N16
       BYTE D4,N8
       BYTE A3,N8
* Measure 29
       BYTE B3,N8
       BYTE E4,N16
       BYTE D4,N16
       BYTE A3,N8
       BYTE E4,N16
       BYTE D4,N16
* Measure 30
       BYTE G3,N8
       BYTE G4,N16
       BYTE Fs4,N16
       BYTE G4,N8
       BYTE E4,N8
* Measure 31
       BYTE E4,N16
       BYTE D4,N16
       BYTE B3,N8
       BYTE D4,N16
       BYTE C4,N16
       BYTE A3,N8
* Measure 32
       BYTE G3,N8
       BYTE D3,N8
       BYTE B2,N8
       BYTE G4,N8
* Measure 33
       BYTE Fs4,N16
       BYTE A4,N16
       BYTE D4,N8
       BYTE Fs4,N16
       BYTE A4,N16
       BYTE D4,N8
* Measure 34
       BYTE G4,N16
       BYTE B4,N16
       BYTE D4,N8
       BYTE G4,N16
       BYTE B4,N16
       BYTE D4,N8
* Measure 35
       BYTE G4,N16
       BYTE C5,N16
       BYTE E4,N8
       BYTE G4,N16
       BYTE C5,N16
       BYTE E4,N8
* Measure 36
       BYTE D4,N8
       BYTE G4,N8
       BYTE C4,N8
       BYTE Fs4,N8
* Measure 37
       BYTE B3,N8
       BYTE D4,N16
       BYTE Cs4,N16
       BYTE D4,N8
       BYTE B3,N8
* Measure 38
       BYTE C4,N8
       BYTE E4,N16
       BYTE Ds4,N16
       BYTE E4,N8
       BYTE C4,N8
* Measure 39
       BYTE G4,N16
       BYTE F4,N16
       BYTE D4,N8
       BYTE D4,N16
       BYTE C4,N16
       BYTE A3,N8
* Measure 40
       BYTE G3,N8
       BYTE D3,N8
       BYTE B2,N4
* Measure 41
FOST1B
       BYTE E3,N4DOT
       BYTE D3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 42
       BYTE C3,N4
       BYTE C4,N8
       BYTE A3,N4
       BYTE C4,N8
* Measure 43
       BYTE G3,N8
       BYTE F3,N8
       BYTE E3,N8
       BYTE E3,N8
       BYTE D3,N8
       BYTE C3,N8
* Measure 44
       BYTE D3,N2DOT
* Measure 45
       BYTE E3,N4DOT
       BYTE D3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 46
       BYTE C3,N4
       BYTE C4,N8
       BYTE A3,N4
       BYTE C4,N8
* Measure 47
       BYTE G3,N8
       BYTE F3,N8
       BYTE E3,N8
       BYTE D3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 48
       BYTE C3,N4DOT
       BYTE C3,N4
       BYTE C4,N8
* Measure 49
       BYTE B3,N8
       BYTE C4,N8
       BYTE D4,N8
       BYTE G3,N8
       BYTE A3,N8
       BYTE B3,N8
* Measure 50
       BYTE C4,N8
       BYTE E4,N8
       BYTE D4,N8
       BYTE C4,N8
       BYTE B3,N8
       BYTE A3,N8
* Measure 51
       BYTE Gs3,N8
       BYTE A3,N8
       BYTE B3,N8
       BYTE D4,N8
       BYTE C4,N8
       BYTE A3,N8
* Measure 52
       BYTE G3,N8
       BYTE C4,N8
       BYTE B3,N8
       BYTE A3,N8
       BYTE G3,N8
       BYTE F3,N8
* Measure 53
       BYTE E3,N4DOT
       BYTE D3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 54
       BYTE C3,N4
       BYTE C4,N8
       BYTE A3,N4
       BYTE C4,N8
* Measure 55
       BYTE G3,N8
       BYTE F3,N8
       BYTE E3,N8
       BYTE D3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 56
       BYTE C3,N2DOT
* Measure 57
*       BYTE B3,0        Grace Note
       BYTE A3,N4DOT
       BYTE A3,N8
       BYTE Gs3,N8
       BYTE A3,N8
* Measure 58
       BYTE E4,N4DOT
       BYTE C4,N4DOT
* Measure 59
       BYTE F4,N4DOT
       BYTE D4,N4
       BYTE Ds4,N8
* Measure 60
       BYTE E4,N4DOT
       BYTE E4,N4
       BYTE REST,N8
* Measure 61
       BYTE D4,N4DOT
       BYTE B3,N4
       BYTE D4,N8
* Measure 62
       BYTE C4,N4DOT
       BYTE A3,N4DOT
* Measure 63
       BYTE G3,N4DOT
       BYTE E3,N4
       BYTE C3,N8
* Measure 64
       BYTE D3,N2DOT
* Measure 65
       BYTE E3,N4DOT
       BYTE D3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 66
       BYTE C3,N4
       BYTE C4,N8
       BYTE A3,N4
       BYTE C4,N8
* Measure 67
       BYTE G3,N8
       BYTE F3,N8
       BYTE E3,N8
       BYTE D3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 68
       BYTE C3,N2DOT
* Measure 69
FOST1C
       BYTE B3,N8DOT
       BYTE D4,N16
       BYTE C4,N8DOT
       BYTE B3,N16
       BYTE C4,N8DOT
       BYTE E4,N16
       BYTE D4,N8DOT
       BYTE C4,N16
* Measure 70
       BYTE G3,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE G4,N16
       BYTE A4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE E4,N16
* Measure 71
       BYTE D4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE E4,N16
       BYTE D4,N8DOT
       BYTE C4,N16
       BYTE B3,N8DOT
       BYTE C4,N16
* Measure 72
       BYTE A3,N8DOT
       BYTE D4,N16
       BYTE Cs4,N8DOT
       BYTE D4,N16
       BYTE E4,N8DOT
       BYTE D4,N16
       BYTE C4,N8DOT
       BYTE A3,N16
* Measure 73
       BYTE B3,N8DOT
       BYTE D4,N16
       BYTE C4,N8DOT
       BYTE B3,N16
       BYTE C4,N8DOT
       BYTE E4,N16
       BYTE D4,N8DOT
       BYTE C4,N16
* Measure 74
       BYTE G3,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE G4,N16
       BYTE A4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE E4,N16
* Measure 75
       BYTE D4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE E4,N16
       BYTE D4,N8DOT
       BYTE C4,N16
       BYTE B3,N8DOT
       BYTE C4,N16
* Measure 76
       BYTE G3,N8DOT
       BYTE G3,N16
       BYTE B3,N8DOT
       BYTE D4,N16
       BYTE G4,N4
       BYTE REST,N8
       BYTE G4,N8
* Measure 77
       BYTE Fs4,N8DOT
       BYTE G4,N16
       BYTE A4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE D4,N16
       BYTE E4,N8DOT
       BYTE Fs4,N16
* Measure 78
       BYTE G4,N8DOT
       BYTE A4,N16
       BYTE B4,N8DOT
       BYTE A4,N16
       BYTE G4,N8DOT
       BYTE Fs4,N16
       BYTE E4,N8DOT
       BYTE Ds4,N16
* Measure 79
       BYTE E4,N8DOT
       BYTE Fs4,N16
       BYTE G4,N8DOT
       BYTE Fs4,N16
       BYTE A4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE E4,N16
* Measure 80
       BYTE D4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE E4,N16
       BYTE D4,N8DOT
       BYTE C4,N16
       BYTE B3,N8DOT
       BYTE A3,N16
* Measure 81
       BYTE B3,N8DOT
       BYTE D4,N16
       BYTE C4,N8DOT
       BYTE B3,N16
       BYTE C4,N8DOT
       BYTE E4,N16
       BYTE D4,N8DOT
       BYTE C4,N16
* Measure 82
       BYTE G3,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE G4,N16
       BYTE A4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE E4,N16
* Measure 83
       BYTE D4,N8DOT
       BYTE G4,N16
       BYTE Fs4,N8DOT
       BYTE E4,N16
       BYTE D4,N8DOT
       BYTE C4,N16
       BYTE B3,N8DOT
       BYTE C4,N16
* Measure 84
       BYTE G3,N8DOT
       BYTE G3,N16
       BYTE B3,N8DOT
       BYTE D4,N16
       BYTE G4,N4
       BYTE REST,N4
* Measure 85
FOST1D
       BYTE B2,N2
       BYTE A2,N8
       BYTE G2,N8
       BYTE B2,N8
       BYTE A2,N8
* Measure 86
       BYTE G2,N4
       BYTE REST,N4
       BYTE G2,N4
       BYTE E2,N4
* Measure 87
       BYTE D2,N4
       BYTE REST,N4
       BYTE A2,N4
       BYTE REST,N4
* Measure 88
       BYTE B2,N2DOT
       BYTE REST,N4
*

* Generator 2
* Measure 1
FOST2
       BYTE G1,N1
* Measure 2
       BYTE G1,N1
* Measure 3
       BYTE G1,N1
* Measure 4
       BYTE Fs1,N1
* Measure 5
       BYTE G1,N1
* Measure 6
       BYTE C1,N1
* Measure 7
       BYTE D1,N2
       BYTE D1,N2
* Measure 8
       BYTE G1,N2DOT
       BYTE REST,N4
* Measure 9
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
* Measure 10
       BYTE G1,N8
       BYTE E2,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G1,N8
       BYTE E2,N8
       BYTE C2,N8
       BYTE E2,N8
* Measure 11
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
* Measure 12
       BYTE D1,N8
       BYTE D2,N8
       BYTE C2,N8
       BYTE D2,N8
       BYTE Fs1,N8
       BYTE D2,N8
       BYTE C2,N8
       BYTE D2,N8
* Measure 13
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
* Measure 14
       BYTE G1,N8
       BYTE E2,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G1,N8
       BYTE E2,N8
       BYTE C2,N8
       BYTE E2,N8
* Measure 15
       BYTE D1,N8
       BYTE B1,N8
       BYTE G1,N8
       BYTE B1,N8
       BYTE D1,N8
       BYTE C2,N8
       BYTE A1,N8
       BYTE C2,N8
* Measure 16
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
       BYTE G1,N4
       BYTE REST,N4
* Measure 17
       BYTE A0,N4
       BYTE A1,N4
       BYTE A1,N4
       BYTE A1,N4
* Measure 18
       BYTE B2,N2
       BYTE REST,N2
* Measure 19
       BYTE C3,N2
       BYTE REST,N2
* Measure 20
       BYTE D1,N4
       BYTE G1,N4
       BYTE G1,N4
       BYTE REST,N4
* Measure 21
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
* Measure 22
       BYTE G1,N8
       BYTE E2,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G1,N8
       BYTE E2,N8
       BYTE C2,N8
       BYTE E2,N8
* Measure 23
       BYTE D1,N8
       BYTE B1,N8
       BYTE G1,N8
       BYTE B1,N8
       BYTE D1,N8
       BYTE C2,N8
       BYTE A1,N8
       BYTE C2,N8
* Measure 24
       BYTE G1,N8
       BYTE D2,N8
       BYTE B1,N8
       BYTE D2,N8
       BYTE G1,N4
       BYTE REST,N4
* Measure 25
FOST2A
       BYTE G1,N8
       BYTE B1,N8
       BYTE Fs1,N8
       BYTE C2,N8
* Measure 26
       BYTE G1,N8
       BYTE B1,N8
       BYTE G1,N8
       BYTE C2,N8
* Measure 27
       BYTE G1,N8
       BYTE B1,N8
       BYTE G1,N8
       BYTE B1,N8
* Measure 28
       BYTE D1,N8
       BYTE Fs1,N8
       BYTE Fs1,N8
       BYTE Fs1,N8
* Measure 29
       BYTE G1,N8
       BYTE B1,N8
       BYTE Fs1,N8
       BYTE C2,N8
* Measure 30
       BYTE G1,N8
       BYTE B1,N8
       BYTE C1,N8
       BYTE G1,N8
* Measure 31
       BYTE D1,N8
       BYTE Fs1,N8
       BYTE D1,N8
       BYTE Fs1,N8
* Measure 32
       BYTE G1,N8
       BYTE B1,N8
       BYTE G1,N8
       BYTE REST,N8
* Measure 33
       BYTE D1,N8
       BYTE Fs1,N8
       BYTE D1,N8
       BYTE Fs1,N8
* Measure 34
       BYTE G1,N8
       BYTE B1,N8
       BYTE G1,N8
       BYTE B1,N8
* Measure 35
       BYTE C1,N8
       BYTE G1,N8
       BYTE C1,N8
       BYTE G1,N8
* Measure 36
       BYTE D1,N8
       BYTE G1,N8
       BYTE D1,N8
       BYTE A1,N8
* Measure 37
       BYTE G1,N8
       BYTE B1,N8
       BYTE G1,N8
       BYTE G1,N8
* Measure 38
       BYTE C1,N8
       BYTE G1,N8
       BYTE G1,N8
       BYTE G1,N8
* Measure 39
       BYTE D1,N8
       BYTE G1,N8
       BYTE D1,N8
       BYTE Fs1,N8
* Measure 40
       BYTE G1,N8
       BYTE B1,N8
       BYTE G1,N4
* Measure 41
FOST2B
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE B1,N8
       BYTE F2,N8
       BYTE G2,N8
* Measure 42
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE C2,N8
       BYTE F2,N8
       BYTE A2,N8
* Measure 43
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
* Measure 44
       BYTE B1,N8
       BYTE F2,N8
       BYTE G2,N8
       BYTE A1,N8
       BYTE F2,N8
       BYTE G2,N8
* Measure 45
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE B1,N8
       BYTE F2,N8
       BYTE G2,N8
* Measure 46
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE F1,N8
       BYTE C2,N8
       BYTE F2,N8
* Measure 47
       BYTE G1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G1,N8
       BYTE B1,N8
       BYTE F2,N8
* Measure 48
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE C2,N4
       BYTE REST,N8
* Measure 49
       BYTE G1,N8
       BYTE F2,N8
       BYTE G2,N8
       BYTE B1,N8
       BYTE F2,N8
       BYTE G2,N8
* Measure 50
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
* Measure 51
       BYTE F1,N8
       BYTE C2,N8
       BYTE F2,N8
       BYTE F1,N8
       BYTE C2,N8
       BYTE F2,N8
* Measure 52
       BYTE G1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G1,N8
       BYTE C2,N8
       BYTE E2,N8
* Measure 53
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE B1,N8
       BYTE F2,N8
       BYTE G2,N8
* Measure 54
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE F1,N8
       BYTE C2,N8
       BYTE F2,N8
* Measure 55
       BYTE G1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G1,N8
       BYTE B1,N8
       BYTE F2,N8
* Measure 56
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE C2,N4
       BYTE REST,N8
* Measure 57
       BYTE A1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE A1,N8
       BYTE C2,N8
       BYTE E2,N8
* Measure 58
       BYTE A1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE A1,N8
       BYTE C2,N8
       BYTE E2,N8
* Measure 59
       BYTE A1,N8
       BYTE D2,N8
       BYTE F2,N8
       BYTE A1,N8
       BYTE D2,N8
       BYTE F2,N8
* Measure 60
       BYTE A1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE A1,N8
       BYTE C2,N8
       BYTE E2,N8
* Measure 61
       BYTE Gs1,N8
       BYTE D2,N8
       BYTE E2,N8
       BYTE Gs1,N8
       BYTE D2,N8
       BYTE E2,N8
* Measure 62
       BYTE A1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE F1,N8
       BYTE A1,N8
       BYTE D2,N8
* Measure 63
       BYTE G1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G1,N8
       BYTE C2,N8
       BYTE E2,N8
* Measure 64
       BYTE G1,N8
       BYTE B1,N8
       BYTE D2,N8
       BYTE G1,N4DOT
* Measure 65
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE B1,N8
       BYTE F2,N8
       BYTE G2,N8
* Measure 66
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE F1,N8
       BYTE C2,N8
       BYTE F2,N8
* Measure 67
       BYTE G1,N8
       BYTE C2,N8
       BYTE E2,N8
       BYTE G1,N8
       BYTE B1,N8
       BYTE F2,N8
* Measure 68
       BYTE C2,N8
       BYTE E2,N8
       BYTE G2,N8
       BYTE C2,N4DOT
* Measure 69
FOST2C
       BYTE G1,N4
       BYTE B1,N4
       BYTE Fs1,N4
       BYTE A1,N4
* Measure 70
       BYTE G1,N4
       BYTE B1,N4
       BYTE G1,N4
       BYTE C2,N4
* Measure 71
       BYTE G1,N4
       BYTE B1,N4
       BYTE G1,N4
       BYTE B1,N4
* Measure 72
       BYTE Fs1,N4
       BYTE A1,N4
       BYTE D1,N4
       BYTE A1,N4
* Measure 73
       BYTE G1,N4
       BYTE B1,N4
       BYTE Fs1,N4
       BYTE A1,N4
* Measure 74
       BYTE G1,N4
       BYTE B1,N4
       BYTE C1,N4
       BYTE G1,N4
* Measure 75
       BYTE D1,N4
       BYTE G1,N4
       BYTE D1,N4
       BYTE Fs1,N4
* Measure 76
       BYTE G1,N4
       BYTE B1,N4
       BYTE G1,N4
       BYTE REST,N4
* Measure 77
       BYTE D1,N2DOT
       BYTE REST,N4
* Measure 78
       BYTE G1,N2DOT
       BYTE REST,N4
* Measure 79
       BYTE C1,N2DOT
       BYTE REST,N4
* Measure 80
       BYTE G1,N4
       BYTE B1,N4
       BYTE G1,N4
       BYTE REST,N4
* Measure 81
       BYTE G1,N4
       BYTE B1,N4
       BYTE Fs1,N4
       BYTE A1,N4
* Measure 82
       BYTE G1,N4
       BYTE B1,N4
       BYTE C1,N4
       BYTE G1,N4
* Measure 83
       BYTE D1,N4
       BYTE G1,N4
       BYTE D1,N4
       BYTE Fs1,N4
* Measure 84
       BYTE G1,N4
       BYTE B1,N4
       BYTE G1,N4
       BYTE REST,N4
* Measure 85
FOST2D
       BYTE G1,N4
       BYTE B1,N4
       BYTE Fs1,N4
       BYTE B1,N4
* Measure 86
       BYTE E1,N4
       BYTE B1,N4
       BYTE C1,N4
       BYTE G1,N4
* Measure 87
       BYTE D1,N4
       BYTE REST,N4
       BYTE A0,N4
       BYTE REST,N4
* Measure 88
       BYTE B0,N2DOT
       BYTE REST,N4
*

* Generator 3
* Measure 1
FOST3
       BYTE REST,N4
       BYTE B1,N4
       BYTE B1,N4
       BYTE B1,N4
* Measure 2
       BYTE REST,N4
       BYTE C2,N4
       BYTE C2,N4
       BYTE C2,N4
* Measure 3
       BYTE REST,N4
       BYTE B1,N4
       BYTE B1,N4
       BYTE B1,N4
* Measure 4
       BYTE REST,N4
       BYTE A1,N4
       BYTE A1,N4
       BYTE A1,N4
* Measure 5
       BYTE REST,N4
       BYTE B1,N4
       BYTE B1,N4
       BYTE B1,N4
* Measure 6
       BYTE REST,N4
       BYTE G1,N4
       BYTE G1,N4
       BYTE G1,N4
* Measure 7
       BYTE REST,N4
       BYTE B1,N4
       BYTE REST,N4
       BYTE Fs1,N4
* Measure 8 - 16
       BYTE REST,N4
       BYTE B1,N4
       BYTE B1,N4
       BYTE REST,N4DOT
       BYTE REST,252
       BYTE REST,252
       BYTE REST,252
* Measure 17
       BYTE Fs3,N4DOT
       BYTE G3,N8
       BYTE A3,N4
       BYTE D3,N4
* Measure 18
       BYTE D1,N4
       BYTE B1,N4
       BYTE B1,N4
       BYTE B1,N4
* Measure 19
       BYTE C1,N4
       BYTE G1,N4
       BYTE G1,N4
       BYTE G1,N4
* Measure 20 - 24
       BYTE D3,N2DOT
       BYTE REST,156
       BYTE REST,252
* Measure 25
FOST3A
       BYTE REST,N8
       BYTE D2,N8
       BYTE REST,N8
       BYTE D2,N8
* Measure 26
       BYTE REST,N8
       BYTE D2,N8
       BYTE REST,N8
       BYTE E2,N8
* Measure 27
       BYTE REST,N8
       BYTE D2,N8
       BYTE REST,N8
       BYTE D2,N8
* Measure 28
       BYTE REST,N8
       BYTE C2,N8
       BYTE C2,N8
       BYTE C2,N8
* Measure 29
       BYTE REST,N8
       BYTE D2,N8
       BYTE REST,N8
       BYTE D2,N8
* Measure 30
       BYTE REST,N8
       BYTE D2,N8
       BYTE REST,N8
       BYTE C2,N8
* Measure 31
       BYTE REST,N8
       BYTE B1,N8
       BYTE REST,N8
       BYTE C2,N8
* Measure 32 - 33
       BYTE REST,N8
       BYTE B3,N8
       BYTE G3,N8
       BYTE REST,N4
       BYTE C2,N8
       BYTE REST,N8
       BYTE C2,N8
* Measure 34
       BYTE REST,N8
       BYTE D2,N8
       BYTE REST,N8
       BYTE D2,N8
* Measure 35
       BYTE REST,N8
       BYTE C2,N8
       BYTE REST,N8
       BYTE C2,N8
* Measure 36
       BYTE B4,N8
       BYTE REST,N8
       BYTE A4,N8
       BYTE REST,N8
* Measure 37
       BYTE G4,N8
       BYTE REST,N4DOT
* Measure 38 - 39
       BYTE G4,N8
       BYTE REST,N2
       BYTE B1,N8
       BYTE REST,N8
       BYTE C2,N8
* Measure 40
       BYTE REST,N8
       BYTE B3,N8
       BYTE G3,N4
* Measure 41 - 47
FOST3B
       BYTE REST,252
       BYTE REST,252
* Measure 48 - 54
       BYTE REST,252
       BYTE REST,252
* Measure 55 - 61
       BYTE REST,252
       BYTE REST,252
* Measure 62 - 68
       BYTE REST,252
       BYTE REST,252
* Measure 69
FOST3C
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N4
       BYTE C2,N4
* Measure 70
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N4
       BYTE E2,N4
* Measure 71
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N4
       BYTE D2,N4
* Measure 72
       BYTE REST,N4
       BYTE C2,N4
       BYTE REST,N4
       BYTE C2,N4
* Measure 73
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N4
       BYTE C2,N4
* Measure 74
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N4
       BYTE C2,N4
* Measure 75
       BYTE REST,N4
       BYTE B1,N4
       BYTE REST,N4
       BYTE C2,N4
* Measure 76 - 77
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N2DOT
       BYTE Fs1,N4
       BYTE Fs1,N4
       BYTE Fs1,N4
* Measure 78
       BYTE REST,N4
       BYTE B1,N4
       BYTE B1,N4
       BYTE B1,N4
* Measure 79
       BYTE REST,N4
       BYTE G1,N4
       BYTE G1,N4
       BYTE G1,N4
* Measure 80 - 81
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N2DOT
       BYTE D2,N4
       BYTE REST,N4
       BYTE C2,N4
* Measure 82
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N4
       BYTE C2,N4
* Measure 83
       BYTE REST,N4
       BYTE B1,N4
       BYTE REST,N4
       BYTE C2,N4
* Measure 84
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N2
* Measure 85
FOST3D
       BYTE REST,N4
       BYTE D2,N4
       BYTE REST,N4
       BYTE Ds2,N4
* Measure 86
       BYTE REST,N2
       BYTE G3,N4
       BYTE E3,N4
* Measure 87
       BYTE D3,N4
       BYTE REST,N4
       BYTE A3,N4
       BYTE REST,N4
* Measure 88
       BYTE G3,N2DOT
       BYTE REST,N4
*

