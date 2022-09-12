       DEF  BEETHV

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Beethoven's WoO 59 (Fu:r Elise)
* https://musescore.com/classicman/fur-elise
*

*
* Song Header
*
BEETHV DATA BEET1,BEET2,BEET3
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

* Generator 1
BEET1
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 1
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 2
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 3
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE Gs2,N16
      BYTE B2,N16
* Measure 4
      BYTE C3,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 5
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 6
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 7
      BYTE B2,N8
      BYTE REST,N16
      BYTE D2,N16
      BYTE C3,N16
      BYTE B2,N16
* Measure 8
      BYTE A2,N4
* Measure 9
      BYTE A2,N8
      BYTE REST,N16
      BYTE B2,N16
      BYTE C3,N16
      BYTE D3,N16
* Measure 10
      BYTE E3,N8DOT
      BYTE G2,N16
      BYTE F3,N16
      BYTE E3,N16
* Measure 11
      BYTE D3,N8DOT
      BYTE F2,N16
      BYTE E3,N16
      BYTE D3,N16
* Measure 12
      BYTE C3,N8DOT
      BYTE E2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 13
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE REST,N16
* Measure 14
      BYTE REST,N16
      BYTE E3,N16
      BYTE E4,N16
      BYTE REST,N8
      BYTE Ds3,N16
* Measure 15
      BYTE E3,N8
      BYTE REST,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 16
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 17
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 18
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE Gs2,N16
      BYTE B2,N16
* Measure 19
      BYTE C3,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 20
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 21
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 22
      BYTE B2,N8
      BYTE REST,N16
      BYTE D2,N16
      BYTE C3,N16
      BYTE B2,N16
* Measure 23
      BYTE A2,N8
      BYTE REST,N16
      BYTE B2,N16
      BYTE C3,N16
      BYTE D3,N16
* Measure 24
      BYTE A2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE F2,N16
      BYTE E2,N16
* Measure 25
*      BYTE F2,0
*      BYTE A2,0
      BYTE C3,N4
      BYTE F3,N16DOT
      BYTE E3,N32
* Measure 26
      BYTE E3,N8
      BYTE D3,N8
      BYTE Bb3,N16DOT
      BYTE A3,N32
* Measure 27
      BYTE A3,N16
      BYTE G3,N16
      BYTE F3,N16
      BYTE E3,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 28
      BYTE Bb2,N8
      BYTE A2,N8
*      BYTE Bb2,0
      BYTE A2,N32
      BYTE G2,N32
      BYTE A2,N32
      BYTE Bb2,N32
* Measure 29
      BYTE C3,N4
      BYTE D3,N16
      BYTE Ds3,N16
* Measure 30
      BYTE E3,N8DOT
      BYTE E3,N16
      BYTE F3,N16
      BYTE A2,N16
* Measure 31
      BYTE C3,N4
      BYTE D3,N16DOT
      BYTE B2,N32
* Measure 32
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
* Measure 33
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
* Measure 34
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
* Measure 35
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
* Measure 36
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
* Measure 37
      BYTE E3,N8DOT
      BYTE B2,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 38
      BYTE E3,N8DOT
      BYTE B2,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 39
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 40
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 41
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 42
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE Gs2,N16
      BYTE B2,N16
* Measure 43
      BYTE C3,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 44
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 45
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 46
      BYTE B2,N8
      BYTE REST,N16
      BYTE D2,N16
      BYTE C3,N16
      BYTE B2,N16
* Measure 47
      BYTE A2,N8
      BYTE REST,N16
      BYTE B2,N16
      BYTE C3,N16
      BYTE D3,N16
* Measure 48
      BYTE E3,N8DOT
      BYTE G2,N16
      BYTE F3,N16
      BYTE E3,N16
* Measure 49
      BYTE D3,N8DOT
      BYTE F2,N16
      BYTE E3,N16
      BYTE D3,N16
* Measure 50
      BYTE C3,N8DOT
      BYTE E2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 51
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE REST,N16
* Measure 52
      BYTE REST,N16
      BYTE E3,N16
      BYTE E4,N16
      BYTE REST,N8
      BYTE Ds3,N16
* Measure 53
      BYTE E3,N8
      BYTE REST,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 54
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 55
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 56
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE Gs2,N16
      BYTE B2,N16
* Measure 57
      BYTE C3,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 58
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 59
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 60
      BYTE B2,N8
      BYTE REST,N16
      BYTE D2,N16
      BYTE C3,N16
      BYTE B2,N16
* Measure 61
      BYTE A2,N8
      BYTE REST,N4
* Measure 62
      BYTE E2,N4DOT
* Measure 63
      BYTE F2,N4
      BYTE Cs3,N16
      BYTE D3,N16
* Measure 64
      BYTE Gs2,N4
      BYTE Gs2,N8
* Measure 65
      BYTE A2,N4DOT
* Measure 66
      BYTE F2,N4
      BYTE E2,N16
      BYTE D2,N16
* Measure 67
      BYTE C2,N4
      BYTE C2,N8
* Measure 68
      BYTE C2,N8
      BYTE E2,N8
      BYTE D2,N8
* Measure 69
      BYTE C2,N4DOT
* Measure 70
      BYTE E2,N4DOT
* Measure 71
      BYTE F2,N4
      BYTE Cs3,N16
      BYTE D3,N16
* Measure 72
      BYTE D3,N4
      BYTE D3,N8
* Measure 73
      BYTE D3,N4DOT
* Measure 74
      BYTE G2,N4
      BYTE F2,N16
      BYTE Eb2,N16
* Measure 75
      BYTE D2,N4
      BYTE D2,N8
* Measure 76
      BYTE D2,N4
      BYTE D2,N8
* Measure 77
      BYTE C2,N4
      BYTE REST,N8
* Measure 78
      BYTE E2,N8
      BYTE REST,N4
* Measure 79
      BYTE A1,N16TRP
      BYTE C2,N16TRP
      BYTE E2,N16TRP
      BYTE A2,N16TRP
      BYTE C3,N16TRP
      BYTE E3,N16TRP
      BYTE D3,N16TRP
      BYTE C3,N16TRP
      BYTE B2,N16TRP
* Measure 80
      BYTE A2,N16TRP
      BYTE C3,N16TRP
      BYTE E3,N16TRP
      BYTE A3,N16TRP
      BYTE C4,N16TRP
      BYTE E4,N16TRP
      BYTE D4,N16TRP
      BYTE C4,N16TRP
      BYTE B3,N16TRP
* Measure 81
      BYTE A3,N16TRP
      BYTE C4,N16TRP
      BYTE E4,N16TRP
      BYTE A4,N16TRP
      BYTE C5,N16TRP
      BYTE E5,N16TRP
      BYTE D5,N16TRP
      BYTE C5,N16TRP
      BYTE B4,N16TRP
* Measure 82
      BYTE Bb4,N16TRP
      BYTE A4,N16TRP
      BYTE Gs4,N16TRP
      BYTE G4,N16TRP
      BYTE Fs4,N16TRP
      BYTE F4,N16TRP
      BYTE E4,N16TRP
      BYTE Ds4,N16TRP
      BYTE D4,N16TRP
* Measure 83
      BYTE Cs4,N16TRP
      BYTE C4,N16TRP
      BYTE B3,N16TRP
      BYTE Bb3,N16TRP
      BYTE A3,N16TRP
      BYTE Gs3,N16TRP
      BYTE G3,N16TRP
      BYTE Fs3,N16TRP
      BYTE F3,N16TRP
* Measure 84
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 85
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 86
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE Gs2,N16
      BYTE B2,N16
* Measure 87
      BYTE C3,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 88
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 89
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 90
      BYTE B2,N8
      BYTE REST,N16
      BYTE D2,N16
      BYTE C3,N16
      BYTE B2,N16
* Measure 91
      BYTE A2,N8
      BYTE REST,N16
      BYTE B2,N16
      BYTE C3,N16
      BYTE D3,N16
* Measure 92
      BYTE E3,N8DOT
      BYTE G2,N16
      BYTE F3,N16
      BYTE E3,N16
* Measure 93
      BYTE D3,N8DOT
      BYTE F2,N16
      BYTE E3,N16
      BYTE D3,N16
* Measure 94
      BYTE C3,N8DOT
      BYTE E2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 95
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE REST,N16
* Measure 96
      BYTE REST,N16
      BYTE E3,N16
      BYTE E4,N16
      BYTE REST,N8
      BYTE Ds3,N16
* Measure 97
      BYTE E3,N8
      BYTE REST,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 98
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 99
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 100
      BYTE B2,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE Gs2,N16
      BYTE B2,N16
* Measure 101
      BYTE C3,N8
      BYTE REST,N16
      BYTE E2,N16
      BYTE E3,N16
      BYTE Ds3,N16
* Measure 102
      BYTE E3,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE B2,N16
      BYTE D3,N16
      BYTE C3,N16
* Measure 103
      BYTE A2,N8
      BYTE REST,N16
      BYTE C2,N16
      BYTE E2,N16
      BYTE A2,N16
* Measure 104
      BYTE B2,N8
      BYTE REST,N16
      BYTE D2,N16
      BYTE C3,N16
      BYTE B2,N16
* Measure 105
      BYTE A2,N8
      BYTE REST,N4
* Measure 106
       DATA REPEAT,BEET1

* Generator 2
BEET2
      BYTE REST,N8
* Measure 1
      BYTE REST,N4DOT
* Measure 2
      BYTE REST,N4DOT
* Measure 3
      BYTE REST,N4DOT
* Measure 4
      BYTE REST,N4DOT
* Measure 5
      BYTE REST,N4DOT
* Measure 6
      BYTE REST,N4DOT
* Measure 7
      BYTE REST,N4DOT
* Measure 8
      BYTE REST,N4
* Measure 9
      BYTE REST,N4DOT
* Measure 10
      BYTE REST,N4DOT
* Measure 11
      BYTE REST,N4DOT
* Measure 12
      BYTE REST,N4DOT
* Measure 13
      BYTE REST,N4DOT
* Measure 14
      BYTE REST,N4DOT
* Measure 15
      BYTE REST,N4DOT
* Measure 16
      BYTE REST,N4DOT
* Measure 17
      BYTE REST,N4DOT
* Measure 18
      BYTE REST,N4DOT
* Measure 19
      BYTE REST,N4DOT
* Measure 20
      BYTE REST,N4DOT
* Measure 21
      BYTE REST,N4DOT
* Measure 22
      BYTE REST,N4DOT
* Measure 23
      BYTE REST,N4DOT
* Measure 24
      BYTE REST,N8DOT
      BYTE C3,N16
      BYTE C3,N16
      BYTE G2,N16
* Measure 25
      BYTE REST,N4DOT
* Measure 26
      BYTE REST,N4DOT
* Measure 27
      BYTE REST,N8
      BYTE G1,N16
      BYTE REST,N16
      BYTE G1,N16
      BYTE REST,N16
* Measure 28
      BYTE REST,N4DOT
* Measure 29
      BYTE REST,N4DOT
* Measure 30
      BYTE REST,N4
      BYTE D2,N16
      BYTE REST,N16
* Measure 31
      BYTE REST,N4DOT
* Measure 32
      BYTE E2,N8
      BYTE REST,N16
      BYTE G2,N16
      BYTE G2,N16
      BYTE F2,N16
* Measure 33
      BYTE E2,N8
      BYTE A1,N8
      BYTE B1,N8
* Measure 34
      BYTE REST,N8DOT
      BYTE G2,N16
      BYTE G2,N16
      BYTE F2,N16
* Measure 35
      BYTE E2,N8
      BYTE A1,N8
      BYTE B1,N8
* Measure 36
      BYTE B1,N8
      BYTE REST,N4
* Measure 37
      BYTE REST,N4DOT
* Measure 38
      BYTE REST,N4DOT
* Measure 39
      BYTE REST,N4DOT
* Measure 40
      BYTE REST,N4DOT
* Measure 41
      BYTE REST,N4DOT
* Measure 42
      BYTE REST,N4DOT
* Measure 43
      BYTE REST,N4DOT
* Measure 44
      BYTE REST,N4DOT
* Measure 45
      BYTE REST,N4DOT
* Measure 46
      BYTE REST,N4DOT
* Measure 47
      BYTE REST,N4DOT
* Measure 48
      BYTE REST,N4DOT
* Measure 49
      BYTE REST,N4DOT
* Measure 50
      BYTE REST,N4DOT
* Measure 51
      BYTE REST,N4DOT
* Measure 52
      BYTE REST,N4DOT
* Measure 53
      BYTE REST,N4DOT
* Measure 54
      BYTE REST,N4DOT
* Measure 55
      BYTE REST,N4DOT
* Measure 56
      BYTE REST,N4DOT
* Measure 57
      BYTE REST,N4DOT
* Measure 58
      BYTE REST,N4DOT
* Measure 59
      BYTE REST,N4DOT
* Measure 60
      BYTE REST,N4DOT
* Measure 61
      BYTE REST,N4DOT
* Measure 62
      BYTE G2,N4DOT
* Measure 63
      BYTE A2,N4
      BYTE E3,N16
      BYTE F3,N16
* Measure 64
      BYTE D3,N4
      BYTE D3,N8
* Measure 65
      BYTE C3,N4DOT
* Measure 66
      BYTE D3,N4
      BYTE C3,N16
      BYTE B2,N16
* Measure 67
      BYTE Fs2,N4
      BYTE A2,N8
* Measure 68
      BYTE A2,N8
      BYTE C3,N8
      BYTE B2,N8
* Measure 69
      BYTE A2,N4DOT
* Measure 70
      BYTE G2,N4DOT
* Measure 71
      BYTE A2,N4
      BYTE E3,N16
      BYTE F3,N16
* Measure 72
      BYTE F3,N4
      BYTE F3,N8
* Measure 73
      BYTE F3,N4DOT
* Measure 74
      BYTE Eb3,N4
      BYTE D3,N16
      BYTE C3,N16
* Measure 75
      BYTE F2,N4
      BYTE F2,N8
* Measure 76
      BYTE F2,N4
      BYTE F2,N8
* Measure 77
      BYTE E2,N4
      BYTE REST,N8
* Measure 78
      BYTE B2,N8
      BYTE REST,N4
* Measure 79
      BYTE REST,N4
      BYTE C2,N8
* Measure 80
      BYTE C2,N8
      BYTE REST,N8
      BYTE C2,N8
* Measure 81
      BYTE C2,N8
      BYTE REST,N8
      BYTE C2,N8
* Measure 82
      BYTE C2,N8
      BYTE REST,N4
* Measure 83
      BYTE REST,N4DOT
* Measure 84
      BYTE REST,N4DOT
* Measure 85
      BYTE REST,N4DOT
* Measure 86
      BYTE REST,N4DOT
* Measure 87
      BYTE REST,N4DOT
* Measure 88
      BYTE REST,N4DOT
* Measure 89
      BYTE REST,N4DOT
* Measure 90
      BYTE REST,N4DOT
* Measure 91
      BYTE REST,N4DOT
* Measure 92
      BYTE REST,N4DOT
* Measure 93
      BYTE REST,N4DOT
* Measure 94
      BYTE REST,N4DOT
* Measure 95
      BYTE REST,N4DOT
* Measure 96
      BYTE REST,N4DOT
* Measure 97
      BYTE REST,N4DOT
* Measure 98
      BYTE REST,N4DOT
* Measure 99
      BYTE REST,N4DOT
* Measure 100
      BYTE REST,N4DOT
* Measure 101
      BYTE REST,N4DOT
* Measure 102
      BYTE REST,N4DOT
* Measure 103
      BYTE REST,N4DOT
* Measure 104
      BYTE REST,N4DOT
* Measure 105
      BYTE A0,N8
      BYTE REST,N4
* Measure 106
       DATA REPEAT,BEET2

* Generator 3
BEET3
      BYTE REST,N8
* Measure 1
      BYTE REST,N4DOT
* Measure 2
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 3
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 4
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 5
      BYTE REST,N4DOT
* Measure 6
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 7
      BYTE B0,N16
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 8
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N16
* Measure 9
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 10
      BYTE C1,N16
      BYTE G1,N16
      BYTE C2,N16
      BYTE REST,N8DOT
* Measure 11
      BYTE REST,N16      * Invalid: G0
      BYTE G1,N16
      BYTE B1,N16
      BYTE REST,N8DOT
* Measure 12
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 13
      BYTE B0,N16
      BYTE E1,N16
      BYTE E2,N16
      BYTE REST,N8
      BYTE E2,N16
* Measure 14
      BYTE E3,N16
      BYTE REST,N8
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE REST,N16
* Measure 15
      BYTE REST,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE REST,N8DOT
* Measure 16
      BYTE REST,N4DOT
* Measure 17
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 18
      BYTE B0,N16
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 19
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 20
      BYTE REST,N4DOT
* Measure 21
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 22
      BYTE B0,N16
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 23
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 24
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE Bb1,N16
      BYTE A1,N16
      BYTE G1,N16
* Measure 25
      BYTE F1,N16
      BYTE A1,N16
      BYTE C2,N16
      BYTE A1,N16
      BYTE C2,N16
      BYTE A1,N16
* Measure 26
      BYTE F1,N16
      BYTE Bb1,N16
      BYTE D2,N16
      BYTE Bb1,N16
      BYTE D2,N16
      BYTE Bb1,N16
* Measure 27
      BYTE F1,N16
      BYTE E2,N16
      BYTE F1,N16
      BYTE E2,N16
      BYTE F1,N16
      BYTE E2,N16
* Measure 28
      BYTE F1,N16
      BYTE A1,N16
      BYTE C2,N16
      BYTE A1,N16
      BYTE C2,N16
      BYTE A1,N16
* Measure 29
      BYTE F1,N16
      BYTE A1,N16
      BYTE C2,N16
      BYTE A1,N16
      BYTE C2,N16
      BYTE A1,N16
* Measure 30
      BYTE E1,N16
      BYTE A1,N16
      BYTE C2,N16
      BYTE A1,N16
      BYTE D1,N16
      BYTE F1,N16
* Measure 31
      BYTE G1,N16
      BYTE E2,N16
      BYTE G1,N16
      BYTE F2,N16
      BYTE G1,N16
      BYTE F2,N16
* Measure 32
      BYTE C2,N8
      BYTE REST,N16
      BYTE F2,N16
      BYTE E2,N16
      BYTE D2,N16
* Measure 33
      BYTE C2,N8
      BYTE F1,N8
      BYTE G1,N8
* Measure 34
      BYTE C2,N8
      BYTE REST,N16
      BYTE F2,N16
      BYTE E2,N16
      BYTE D2,N16
* Measure 35
      BYTE C2,N8
      BYTE F1,N8
      BYTE G1,N8
* Measure 36
      BYTE Gs1,N8
      BYTE REST,N4
* Measure 37
      BYTE REST,N4DOT
* Measure 38
      BYTE REST,N4DOT
* Measure 39
      BYTE REST,N4DOT
* Measure 40
      BYTE REST,N4DOT
* Measure 41
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 42
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 43
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 44
      BYTE REST,N4DOT
* Measure 45
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 46
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 47
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 48
      BYTE C1,N16
      BYTE G1,N16
      BYTE C2,N16
      BYTE REST,N8DOT
* Measure 49
      BYTE REST,N16      * Invalid: G0
      BYTE G1,N16
      BYTE B1,N16
      BYTE REST,N8DOT
* Measure 50
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 51
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE E2,N16
      BYTE REST,N8
      BYTE E2,N16
* Measure 52
      BYTE E3,N16
      BYTE REST,N8
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE REST,N16
* Measure 53
      BYTE REST,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE REST,N8DOT
* Measure 54
      BYTE REST,N4DOT
* Measure 55
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 56
      BYTE B0,N16
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 57
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 58
      BYTE REST,N4DOT
* Measure 59
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 60
      BYTE B0,N16
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 61
      BYTE A0,N16
      BYTE A0,N16
      BYTE A0,N16
      BYTE A0,N16
      BYTE A0,N16
      BYTE A0,N16
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
      BYTE REST,N16      * Invalid: D0
      BYTE REST,N16      * Invalid: D0
      BYTE REST,N16      * Invalid: D0
      BYTE REST,N16      * Invalid: D0
      BYTE REST,N16      * Invalid: D0
      BYTE REST,N16      * Invalid: D0
* Measure 67
      BYTE REST,N16      * Invalid: Ds0
      BYTE REST,N16      * Invalid: Ds0
      BYTE REST,N16      * Invalid: Ds0
      BYTE REST,N16      * Invalid: Ds0
      BYTE REST,N16      * Invalid: Ds0
      BYTE REST,N16      * Invalid: Ds0
* Measure 68
      BYTE REST,N16      * Invalid: E0
      BYTE REST,N16      * Invalid: E0
      BYTE REST,N16      * Invalid: E0
      BYTE REST,N16      * Invalid: E0
      BYTE REST,N16      * Invalid: E0
      BYTE REST,N16      * Invalid: E0
* Measure 69
      BYTE REST,N16      * Invalid: A-1
      BYTE A0,N16
      BYTE A0,N16
      BYTE A0,N16
      BYTE A0,N16
      BYTE A0,N16
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
      BYTE Bb0,N16
      BYTE Bb0,N16
      BYTE Bb0,N16
      BYTE Bb0,N16
      BYTE Bb0,N16
      BYTE Bb0,N16
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
      BYTE B0,N16
      BYTE B0,N16
      BYTE B0,N16
      BYTE B0,N16
      BYTE B0,N16
      BYTE B0,N16
* Measure 77
      BYTE C1,N4
      BYTE REST,N8
* Measure 78
      BYTE E1,N8
      BYTE REST,N4
* Measure 79
      BYTE REST,N8      * Invalid: A-1
      BYTE REST,N8
      BYTE A1,N8
* Measure 80
      BYTE A1,N8
      BYTE REST,N8
      BYTE A1,N8
* Measure 81
      BYTE A1,N8
      BYTE REST,N8
      BYTE A1,N8
* Measure 82
      BYTE A1,N8
      BYTE REST,N4
* Measure 83
      BYTE REST,N4DOT
* Measure 84
      BYTE REST,N4DOT
* Measure 85
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 86
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 87
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 88
      BYTE REST,N4DOT
* Measure 89
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 90
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 91
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 92
      BYTE C1,N16
      BYTE G1,N16
      BYTE C2,N16
      BYTE REST,N8DOT
* Measure 93
      BYTE REST,N16      * Invalid: G0
      BYTE G1,N16
      BYTE B1,N16
      BYTE REST,N8DOT
* Measure 94
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 95
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE E2,N16
      BYTE REST,N8
      BYTE E2,N16
* Measure 96
      BYTE E3,N16
      BYTE REST,N8
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE REST,N16
* Measure 97
      BYTE REST,N16
      BYTE Ds3,N16
      BYTE E3,N16
      BYTE REST,N8DOT
* Measure 98
      BYTE REST,N4DOT
* Measure 99
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 100
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 101
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 102
      BYTE REST,N4DOT
* Measure 103
      BYTE A0,N16
      BYTE E1,N16
      BYTE A1,N16
      BYTE REST,N8DOT
* Measure 104
      BYTE REST,N16      * Invalid: E0
      BYTE E1,N16
      BYTE Gs1,N16
      BYTE REST,N8DOT
* Measure 105
      BYTE REST,N8      * Invalid: A-1
      BYTE REST,N4
* Measure 106
       DATA REPEAT,BEET3


       END