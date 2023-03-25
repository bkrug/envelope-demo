       DEF  BEETHV

*
* This is auto-generated code.
* It is only included in the repo for the convenience of people who haven't cloned it.
*
* Für Elise in A Minor
* Ludwig van Beethoven(1770–1827)
* Source: http://musescore.com/user/19710/scores/33816
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
BEETHV DATA BEET1,BEET2,BEET3
* Data structures dealing with repeated music
       DATA REPT1,REPT2,REPT3
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA BEET1B,BEET1
       DATA BEET1A,BEET1B
       DATA BEET1E,BEET1C
       DATA BEET1D,BEET1E
       DATA BEET1F,BEET1
       DATA REPEAT,REPT1
REPT2
       DATA BEET2B,BEET2
       DATA BEET2A,BEET2B
       DATA BEET2E,BEET2C
       DATA BEET2D,BEET2E
       DATA BEET2F,BEET2
       DATA REPEAT,REPT2
REPT3
       DATA BEET3B,BEET3
       DATA BEET3A,BEET3B
       DATA BEET3E,BEET3C
       DATA BEET3D,BEET3E
       DATA BEET3F,BEET3
       DATA REPEAT,REPT3

* Generator 1
* Measure 1
BEET1
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 2
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 3
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 4
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE Gs2,N16
       BYTE B2,N16
* Measure 5
       BYTE C3,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 6
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 7
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 8
       BYTE B2,N8
       BYTE REST,N16
       BYTE D2,N16
       BYTE C3,N16
       BYTE B2,N16
* Measure 9
BEET1A
       BYTE A2,N4
* Measure 10
BEET1B
       BYTE A2,N8
       BYTE REST,N16
       BYTE B2,N16
       BYTE C3,N16
       BYTE D3,N16
* Measure 11
BEET1C
       BYTE E3,N8DOT
       BYTE G2,N16
       BYTE F3,N16
       BYTE E3,N16
* Measure 12
       BYTE D3,N8DOT
       BYTE F2,N16
       BYTE E3,N16
       BYTE D3,N16
* Measure 13
       BYTE C3,N8DOT
       BYTE E2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 14 - 15
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE REST,N8
       BYTE E3,N16
       BYTE E4,N16
       BYTE REST,N8
       BYTE Ds3,N16
* Measure 16
       BYTE E3,N8
       BYTE REST,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 17
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 18
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 19
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE Gs2,N16
       BYTE B2,N16
* Measure 20
       BYTE C3,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 21
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 22
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 23
       BYTE B2,N8
       BYTE REST,N16
       BYTE D2,N16
       BYTE C3,N16
       BYTE B2,N16
* Measure 24
BEET1D
       BYTE A2,N8
       BYTE REST,N16
       BYTE B2,N16
       BYTE C3,N16
       BYTE D3,N16
* Measure 25
BEET1E
       BYTE A2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE F2,N16
       BYTE E2,N16
* Measure 26
*       BYTE F2,0        Grace Note
*       BYTE A2,0        Grace Note
       BYTE C3,N4
       BYTE F3,N16DOT
       BYTE E3,N32
* Measure 27
       BYTE E3,N8
       BYTE D3,N8
       BYTE Bb3,N16DOT
       BYTE A3,N32
* Measure 28
       BYTE A3,N16
       BYTE G3,N16
       BYTE F3,N16
       BYTE E3,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 29
       BYTE Bb2,N8
       BYTE A2,N8
*       BYTE Bb2,0        Grace Note
       BYTE A2,N32
       BYTE G2,N32
       BYTE A2,N32
       BYTE Bb2,N32
* Measure 30
       BYTE C3,N4
       BYTE D3,N16
       BYTE Ds3,N16
* Measure 31
       BYTE E3,N8DOT
       BYTE E3,N16
       BYTE F3,N16
       BYTE A2,N16
* Measure 32
       BYTE C3,N4
       BYTE D3,N16DOT
       BYTE B2,N32
* Measure 33
       BYTE C3,N32
       BYTE G3,N32
       BYTE G2,N32
       BYTE G3,N32
       BYTE A2,N32
       BYTE G3,N32
       BYTE B2,N32
       BYTE G3,N32
       BYTE C3,N32
       BYTE G3,N32
       BYTE D3,N32
       BYTE G3,N32
* Measure 34
       BYTE E3,N32
       BYTE G3,N32
       BYTE C4,N32
       BYTE B3,N32
       BYTE A3,N32
       BYTE G3,N32
       BYTE F3,N32
       BYTE E3,N32
       BYTE D3,N32
       BYTE G3,N32
       BYTE F3,N32
       BYTE D3,N32
* Measure 35
       BYTE C3,N32
       BYTE G3,N32
       BYTE G2,N32
       BYTE G3,N32
       BYTE A2,N32
       BYTE G3,N32
       BYTE B2,N32
       BYTE G3,N32
       BYTE C3,N32
       BYTE G3,N32
       BYTE D3,N32
       BYTE G3,N32
* Measure 36
       BYTE E3,N32
       BYTE G3,N32
       BYTE C4,N32
       BYTE B3,N32
       BYTE A3,N32
       BYTE G3,N32
       BYTE F3,N32
       BYTE E3,N32
       BYTE D3,N32
       BYTE G3,N32
       BYTE F3,N32
       BYTE D3,N32
* Measure 37
       BYTE E3,N32
       BYTE F3,N32
       BYTE E3,N32
       BYTE Ds3,N32
       BYTE E3,N32
       BYTE B2,N32
       BYTE E3,N32
       BYTE Ds3,N32
       BYTE E3,N32
       BYTE B2,N32
       BYTE E3,N32
       BYTE Ds3,N32
* Measure 38
       BYTE E3,N8DOT
       BYTE B2,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 39
       BYTE E3,N8DOT
       BYTE B2,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 40
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 41
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 42
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 43
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE Gs2,N16
       BYTE B2,N16
* Measure 44
       BYTE C3,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 45
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 46
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 47
       BYTE B2,N8
       BYTE REST,N16
       BYTE D2,N16
       BYTE C3,N16
       BYTE B2,N16
* Measure 48
       BYTE A2,N8
       BYTE REST,N16
       BYTE B2,N16
       BYTE C3,N16
       BYTE D3,N16
* Measure 49
       BYTE E3,N8DOT
       BYTE G2,N16
       BYTE F3,N16
       BYTE E3,N16
* Measure 50
       BYTE D3,N8DOT
       BYTE F2,N16
       BYTE E3,N16
       BYTE D3,N16
* Measure 51
       BYTE C3,N8DOT
       BYTE E2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 52 - 53
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE REST,N8
       BYTE E3,N16
       BYTE E4,N16
       BYTE REST,N8
       BYTE Ds3,N16
* Measure 54
       BYTE E3,N8
       BYTE REST,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 55
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 56
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 57
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE Gs2,N16
       BYTE B2,N16
* Measure 58
       BYTE C3,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 59
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 60
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 61
       BYTE B2,N8
       BYTE REST,N16
       BYTE D2,N16
       BYTE C3,N16
       BYTE B2,N16
* Measure 62
       BYTE A2,N8
       BYTE REST,N4
* Measure 63
       BYTE E2,N4DOT
* Measure 64
       BYTE F2,N4
       BYTE Cs3,N16
       BYTE D3,N16
* Measure 65
       BYTE Gs2,N4
       BYTE Gs2,N8
* Measure 66
       BYTE A2,N4DOT
* Measure 67
       BYTE F2,N4
       BYTE E2,N16
       BYTE D2,N16
* Measure 68
       BYTE C2,N4
       BYTE C2,N8
* Measure 69
       BYTE C2,N8
       BYTE E2,N8
       BYTE D2,N8
* Measure 70
       BYTE C2,N4DOT
* Measure 71
       BYTE E2,N4DOT
* Measure 72
       BYTE F2,N4
       BYTE Cs3,N16
       BYTE D3,N16
* Measure 73
       BYTE D3,N4
       BYTE D3,N8
* Measure 74
       BYTE D3,N4DOT
* Measure 75
       BYTE G2,N4
       BYTE F2,N16
       BYTE Eb2,N16
* Measure 76
       BYTE D2,N4
       BYTE D2,N8
* Measure 77
       BYTE D2,N4
       BYTE D2,N8
* Measure 78
       BYTE C2,N4
       BYTE REST,N8
* Measure 79
       BYTE E2,N8
       BYTE REST,N4
* Measure 80
       BYTE A1,N16TRP
       BYTE C2,N16TRP
       BYTE E2,N16TRP
       BYTE A2,N16TRP
       BYTE C3,N16TRP
       BYTE E3,N16TRP
       BYTE D3,N16TRP
       BYTE C3,N16TRP
       BYTE B2,N16TRP
* Measure 81
       BYTE A2,N16TRP
       BYTE C3,N16TRP
       BYTE E3,N16TRP
       BYTE A3,N16TRP
       BYTE C4,N16TRP
       BYTE E4,N16TRP
       BYTE D4,N16TRP
       BYTE C4,N16TRP
       BYTE B3,N16TRP
* Measure 82
       BYTE A3,N16TRP
       BYTE C4,N16TRP
       BYTE E4,N16TRP
       BYTE A4,N16TRP
       BYTE C5,N16TRP
       BYTE E5,N16TRP
       BYTE D5,N16TRP
       BYTE C5,N16TRP
       BYTE B4,N16TRP
* Measure 83
       BYTE Bb4,N16TRP
       BYTE A4,N16TRP
       BYTE Gs4,N16TRP
       BYTE G4,N16TRP
       BYTE Fs4,N16TRP
       BYTE F4,N16TRP
       BYTE E4,N16TRP
       BYTE Ds4,N16TRP
       BYTE D4,N16TRP
* Measure 84
       BYTE Cs4,N16TRP
       BYTE C4,N16TRP
       BYTE B3,N16TRP
       BYTE Bb3,N16TRP
       BYTE A3,N16TRP
       BYTE Gs3,N16TRP
       BYTE G3,N16TRP
       BYTE Fs3,N16TRP
       BYTE F3,N16TRP
* Measure 85
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 86
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 87
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE Gs2,N16
       BYTE B2,N16
* Measure 88
       BYTE C3,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 89
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 90
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 91
       BYTE B2,N8
       BYTE REST,N16
       BYTE D2,N16
       BYTE C3,N16
       BYTE B2,N16
* Measure 92
       BYTE A2,N8
       BYTE REST,N16
       BYTE B2,N16
       BYTE C3,N16
       BYTE D3,N16
* Measure 93
       BYTE E3,N8DOT
       BYTE G2,N16
       BYTE F3,N16
       BYTE E3,N16
* Measure 94
       BYTE D3,N8DOT
       BYTE F2,N16
       BYTE E3,N16
       BYTE D3,N16
* Measure 95
       BYTE C3,N8DOT
       BYTE E2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 96 - 97
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE REST,N8
       BYTE E3,N16
       BYTE E4,N16
       BYTE REST,N8
       BYTE Ds3,N16
* Measure 98
       BYTE E3,N8
       BYTE REST,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 99
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 100
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 101
       BYTE B2,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE Gs2,N16
       BYTE B2,N16
* Measure 102
       BYTE C3,N8
       BYTE REST,N16
       BYTE E2,N16
       BYTE E3,N16
       BYTE Ds3,N16
* Measure 103
       BYTE E3,N16
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE B2,N16
       BYTE D3,N16
       BYTE C3,N16
* Measure 104
       BYTE A2,N8
       BYTE REST,N16
       BYTE C2,N16
       BYTE E2,N16
       BYTE A2,N16
* Measure 105
       BYTE B2,N8
       BYTE REST,N16
       BYTE D2,N16
       BYTE C3,N16
       BYTE B2,N16
* Measure 106
       BYTE A2,N8
       BYTE REST,N4
BEET1F
*

* Generator 2
* Measure 1 - 2
BEET2
       BYTE REST,N2
* Measure 3
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 4
       BYTE REST,N16      * Invalid: E0
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 5 - 6
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,54
* Measure 7
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 8
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 9
BEET2A
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N16
* Measure 10
BEET2B
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 11
BEET2C
       BYTE C1,N16
       BYTE G1,N16
       BYTE C2,N16
       BYTE REST,N8DOT
* Measure 12
       BYTE REST,N16      * Invalid: G0
       BYTE G1,N16
       BYTE B1,N16
       BYTE REST,N8DOT
* Measure 13
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 14
       BYTE B0,N16
       BYTE E1,N16
       BYTE E2,N16
       BYTE REST,N8
       BYTE E2,N16
* Measure 15 - 17
       BYTE E3,N16
       BYTE REST,N8
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE REST,N8
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE REST,54
* Measure 18
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 19
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 20 - 21
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,54
* Measure 22
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 23
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 24
BEET2D
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 25
BEET2E
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE Bb1,N16
       BYTE A1,N16
       BYTE G1,N16
* Measure 26
       BYTE F1,N16
       BYTE A1,N16
       BYTE C2,N16
       BYTE A1,N16
       BYTE C2,N16
       BYTE A1,N16
* Measure 27
       BYTE F1,N16
       BYTE Bb1,N16
       BYTE D2,N16
       BYTE Bb1,N16
       BYTE D2,N16
       BYTE Bb1,N16
* Measure 28
       BYTE F1,N16
       BYTE E2,N16
       BYTE F1,N16
       BYTE E2,N16
       BYTE F1,N16
       BYTE E2,N16
* Measure 29
       BYTE F1,N16
       BYTE A1,N16
       BYTE C2,N16
       BYTE A1,N16
       BYTE C2,N16
       BYTE A1,N16
* Measure 30
       BYTE F1,N16
       BYTE A1,N16
       BYTE C2,N16
       BYTE A1,N16
       BYTE C2,N16
       BYTE A1,N16
* Measure 31
       BYTE E1,N16
       BYTE A1,N16
       BYTE C2,N16
       BYTE A1,N16
       BYTE D1,N16
       BYTE F1,N16
* Measure 32
       BYTE G1,N16
       BYTE E2,N16
       BYTE G1,N16
       BYTE F2,N16
       BYTE G1,N16
       BYTE F2,N16
* Measure 33
       BYTE C2,N8
       BYTE REST,N16
       BYTE F2,N16
       BYTE E2,N16
       BYTE D2,N16
* Measure 34
       BYTE C2,N8
       BYTE F1,N8
       BYTE G1,N8
* Measure 35
       BYTE C2,N8
       BYTE REST,N16
       BYTE F2,N16
       BYTE E2,N16
       BYTE D2,N16
* Measure 36
       BYTE C2,N8
       BYTE F1,N8
       BYTE G1,N8
* Measure 37 - 41
       BYTE Gs1,N8
       BYTE REST,168
* Measure 42
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 43
       BYTE REST,N16      * Invalid: E0
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 44 - 45
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,54
* Measure 46
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 47
       BYTE REST,N16      * Invalid: E0
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 48
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 49
       BYTE C1,N16
       BYTE G1,N16
       BYTE C2,N16
       BYTE REST,N8DOT
* Measure 50
       BYTE REST,N16      * Invalid: G0
       BYTE G1,N16
       BYTE B1,N16
       BYTE REST,N8DOT
* Measure 51
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 52
       BYTE REST,N16      * Invalid: E0
       BYTE E1,N16
       BYTE E2,N16
       BYTE REST,N8
       BYTE E2,N16
* Measure 53 - 55
       BYTE E3,N16
       BYTE REST,N8
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE REST,N8
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE REST,54
* Measure 56
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 57
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 58 - 59
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,54
* Measure 60
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 61
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 62
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 63
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 64
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 65
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 66
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 67
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 68
       BYTE As0,N16
       BYTE As0,N16
       BYTE As0,N16
       BYTE As0,N16
       BYTE As0,N16
       BYTE As0,N16
* Measure 69
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE D1,N16
       BYTE D1,N16
* Measure 70
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 71
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 72
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 73
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
       BYTE A0,N16
* Measure 74
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
* Measure 75
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
* Measure 76
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
       BYTE Bb0,N16
* Measure 77
       BYTE B0,N16
       BYTE B0,N16
       BYTE B0,N16
       BYTE B0,N16
       BYTE B0,N16
       BYTE B0,N16
* Measure 78
       BYTE C1,N4
       BYTE REST,N8
* Measure 79
       BYTE E1,N8
       BYTE REST,N4
* Measure 80
       BYTE A0,N8
       BYTE REST,N8
       BYTE A1,N8
* Measure 81
       BYTE A1,N8
       BYTE REST,N8
       BYTE A1,N8
* Measure 82
       BYTE A1,N8
       BYTE REST,N8
       BYTE A1,N8
* Measure 83 - 85
       BYTE A1,N8
       BYTE REST,N1
* Measure 86
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 87
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 88 - 89
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,54
* Measure 90
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 91
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 92
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 93
       BYTE C1,N16
       BYTE G1,N16
       BYTE C2,N16
       BYTE REST,N8DOT
* Measure 94
       BYTE D1,N16
       BYTE G1,N16
       BYTE B1,N16
       BYTE REST,N8DOT
* Measure 95
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 96
       BYTE B0,N16
       BYTE E1,N16
       BYTE E2,N16
       BYTE REST,N8
       BYTE E2,N16
* Measure 97 - 99
       BYTE E3,N16
       BYTE REST,N8
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE REST,N8
       BYTE Ds3,N16
       BYTE E3,N16
       BYTE REST,54
* Measure 100
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 101
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 102 - 103
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,54
* Measure 104
       BYTE A0,N16
       BYTE E1,N16
       BYTE A1,N16
       BYTE REST,N8DOT
* Measure 105
       BYTE B0,N16
       BYTE E1,N16
       BYTE Gs1,N16
       BYTE REST,N8DOT
* Measure 106
       BYTE REST,N8      * Invalid: A-1
       BYTE REST,N4
BEET2F
*

* Generator 3
* Measure 1
BEET3
       BYTE REST,N8
* Measure 2 - 8
       BYTE REST,252
* Measure 9
BEET3A
       BYTE REST,N4
* Measure 10
BEET3B
       BYTE REST,N4DOT
* Measure 11 - 16
BEET3C
       BYTE REST,216
* Measure 17 - 23
       BYTE REST,252
* Measure 24
BEET3D
       BYTE REST,N4DOT
* Measure 25
BEET3E
       BYTE REST,N8DOT
       BYTE C3,N16
       BYTE C3,N16
       BYTE G2,N16
* Measure 26 - 32
       BYTE REST,84
       BYTE G1,N16
       BYTE REST,N16
       BYTE G1,N16
       BYTE REST,102
       BYTE D2,N16
       BYTE REST,42
* Measure 33
       BYTE E2,N8
       BYTE REST,N16
       BYTE G2,N16
       BYTE G2,N16
       BYTE F2,N16
* Measure 34
       BYTE E2,N8
       BYTE A1,N8
       BYTE B1,N8
* Measure 35
       BYTE REST,N8DOT
       BYTE G2,N16
       BYTE G2,N16
       BYTE F2,N16
* Measure 36
       BYTE E2,N8
       BYTE A1,N8
       BYTE B1,N8
* Measure 37 - 41
       BYTE B1,N8
       BYTE REST,168
* Measure 42 - 48
       BYTE REST,252
* Measure 49 - 55
       BYTE REST,252
* Measure 56 - 62
       BYTE REST,252
* Measure 63
       BYTE G2,N4DOT
* Measure 64
       BYTE A2,N4
       BYTE E3,N16
       BYTE F3,N16
* Measure 65
       BYTE D3,N4
       BYTE D3,N8
* Measure 66
       BYTE C3,N4DOT
* Measure 67
       BYTE D3,N4
       BYTE C3,N16
       BYTE B2,N16
* Measure 68
       BYTE Fs2,N4
       BYTE A2,N8
* Measure 69
       BYTE A2,N8
       BYTE C3,N8
       BYTE B2,N8
* Measure 70
       BYTE A2,N4DOT
* Measure 71
       BYTE G2,N4DOT
* Measure 72
       BYTE A2,N4
       BYTE E3,N16
       BYTE F3,N16
* Measure 73
       BYTE F3,N4
       BYTE F3,N8
* Measure 74
       BYTE F3,N4DOT
* Measure 75
       BYTE Eb3,N4
       BYTE D3,N16
       BYTE C3,N16
* Measure 76
       BYTE F2,N4
       BYTE F2,N8
* Measure 77
       BYTE F2,N4
       BYTE F2,N8
* Measure 78
       BYTE E2,N4
       BYTE REST,N8
* Measure 79 - 80
       BYTE B2,N8
       BYTE REST,N2
       BYTE C2,N8
* Measure 81
       BYTE C2,N8
       BYTE REST,N8
       BYTE C2,N8
* Measure 82
       BYTE C2,N8
       BYTE REST,N8
       BYTE C2,N8
* Measure 83 - 84
       BYTE C2,N8
       BYTE REST,60
* Measure 85 - 91
       BYTE REST,252
* Measure 92 - 98
       BYTE REST,252
* Measure 99 - 105
       BYTE REST,252
* Measure 106
       BYTE A0,N8
       BYTE REST,N4
BEET3F
*

