       DEF  OTTO

*
* This is auto-generated code.
* It is only included in the repo for the convenience of people who haven't cloned it.
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
OTTO   DATA OTTO1,OTTO2,OTTO3
* Data structures dealing with repeated music
       DATA REPT1,REPT2,REPT3
* Duration ratio in 60hz environment
       DATA 4,5
* Duration ratio in 50hz environment
       DATA 2,3

REPT1
       DATA OTTO1B,OTTO1A
       DATA OTTO1C,OTTO1B
       DATA OTTO1F,OTTO1D
       DATA OTTO1E,OTTO1F
       DATA OTTO1I,OTTO1G
       DATA OTTO1H,OTTO1I
       DATA OTTO1L,OTTO1J
       DATA OTTO1K,OTTO1L
       DATA OTTO1M,STOP
       DATA REPEAT,STOP
REPT2
       DATA OTTO2B,OTTO2A
       DATA OTTO2C,OTTO2B
       DATA OTTO2F,OTTO2D
       DATA OTTO2E,OTTO2F
       DATA OTTO2I,OTTO2G
       DATA OTTO2H,OTTO2I
       DATA OTTO2L,OTTO2J
       DATA OTTO2K,OTTO2L
       DATA OTTO2M,STOP
       DATA REPEAT,STOP
REPT3
       DATA OTTO3B,OTTO3A
       DATA OTTO3C,OTTO3B
       DATA OTTO3F,OTTO3D
       DATA OTTO3E,OTTO3F
       DATA OTTO3I,OTTO3G
       DATA OTTO3H,OTTO3I
       DATA OTTO3L,OTTO3J
       DATA OTTO3K,OTTO3L
       DATA OTTO3M,STOP
       DATA REPEAT,STOP

* Generator 1
* Measure 1
OTTO1
       BYTE E4,N8
       BYTE D4,N8
* Measure 2
       BYTE E3,N8
       BYTE E3,N4
       BYTE E3,N8
       BYTE E3,N4
       BYTE C4,N8
       BYTE B3,N8
* Measure 3
       BYTE C3,N8
       BYTE C3,N4
       BYTE C3,N8
       BYTE C3,N4
       BYTE A3,N8
       BYTE G3,N8
* Measure 4
       BYTE A2,N4
       BYTE F3,N8
       BYTE E3,N8
       BYTE F2,N4
       BYTE D3,N8
       BYTE C3,N8
* Measure 5
       BYTE D2,N4
       BYTE B2,N8
       BYTE A2,N8
       BYTE B1,N4
* Measure 6
OTTO1A
       BYTE E3,N8
       BYTE D3,N8
* Measure 7
       BYTE E2,N8
       BYTE E2,N4
       BYTE E2,N8
       BYTE E2,N4
       BYTE E2,N8
       BYTE F2,N8
* Measure 8
       BYTE C2,N8
       BYTE C2,N4
       BYTE C2,N8
       BYTE C2,N4
       BYTE C3,N8
       BYTE D3,N8
* Measure 9
       BYTE G2,N8
       BYTE G2,N4
       BYTE G2,N8
       BYTE G2,N8
       BYTE D3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 10
       BYTE G2,N8
       BYTE G2,N4
       BYTE G2,N8
       BYTE G2,N4
       BYTE E3,N8
       BYTE D3,N8
* Measure 11
       BYTE E2,N8
       BYTE E2,N4
       BYTE E2,N8
       BYTE E2,N4
       BYTE E2,N8
       BYTE F2,N8
* Measure 12
       BYTE C2,N8
       BYTE C2,N4
       BYTE C2,N8
       BYTE C2,N4
       BYTE C3,N8
       BYTE D3,N8
* Measure 13
       BYTE E3,N8
       BYTE D3,N8
       BYTE E3,N8
       BYTE F3,N8
       BYTE G3,N8
       BYTE E3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 14
       BYTE E3,N8
       BYTE C3,N8
       BYTE D3,N8
       BYTE B2,N8
       BYTE C3,N4
* Measure 15
OTTO1B
       BYTE E3,N4
* Measure 16
       BYTE C3,N8
       BYTE C3,N4
       BYTE C3,N8
       BYTE C3,N4DOT
       BYTE E3,N8
* Measure 17
       BYTE C3,N8
       BYTE C3,N4
       BYTE C3,N8
       BYTE C3,N4
       BYTE C3,N4
* Measure 18
       BYTE C3,N8
       BYTE C3,N4
       BYTE C3,N8
       BYTE C3,N4DOT
       BYTE C3,N8
* Measure 19
       BYTE A2,N8
       BYTE C3,N4
       BYTE C3,N8
       BYTE C3,N4
       BYTE G2,N4
* Measure 20
       BYTE E3,N4
       BYTE E3,N4
       BYTE B2,N4
       BYTE B2,N4
* Measure 21
       BYTE G2,N4
       BYTE G2,N4
       BYTE G2,N4
       BYTE C3,N8
       BYTE D3,N8
* Measure 22
       BYTE E3,N8
       BYTE D3,N8
       BYTE E3,N8
       BYTE F3,N8
       BYTE G3,N8
       BYTE E3,N8
       BYTE C3,N8
       BYTE D3,N8
* Measure 23
       BYTE E3,N8
       BYTE C3,N8
       BYTE D3,N8
       BYTE B2,N8
       BYTE C3,N4
* Measure 24
OTTO1C
       BYTE C2,N4
* Measure 25
OTTO1D
       BYTE C2,N4
       BYTE C2,N8
       BYTE F2,N8
       BYTE D2,N4
       BYTE D2,N8
       BYTE F2,N8
* Measure 26
       BYTE C2,N4
       BYTE C2,N8
       BYTE F2,N8
       BYTE D2,N4
       BYTE D2,N8
       BYTE F2,N8
* Measure 27
       BYTE C2,N4
       BYTE C2,N8
       BYTE D2,N8
       BYTE F2,N4
       BYTE F2,N8
       BYTE G2,N8
* Measure 28
       BYTE A2,N4
       BYTE A2,N8
       BYTE Bb2,N8
       BYTE C3,N8
       BYTE Bb2,N8
       BYTE G2,N8
       BYTE E2,N8
* Measure 29
       BYTE C2,N4
       BYTE C2,N8
       BYTE F2,N8
       BYTE D2,N4
       BYTE D2,N8
       BYTE F2,N8
* Measure 30
       BYTE C2,N4
       BYTE C2,N8
       BYTE F2,N8
       BYTE D2,N4
       BYTE D2,N8
       BYTE F2,N8
* Measure 31
       BYTE C2,N4
       BYTE C2,N8
       BYTE D2,N8
       BYTE F2,N4
       BYTE F2,N8
       BYTE G2,N8
* Measure 32
OTTO1E
       BYTE A2,N8
       BYTE F2,N8
       BYTE G2,N8
       BYTE E2,N8
       BYTE F2,N8
       BYTE E2,N8
       BYTE D2,N8
       BYTE C2,N8
* Measure 33
OTTO1F
       BYTE A2,N8
       BYTE F2,N8
       BYTE G2,N8
       BYTE E2,N8
       BYTE F2,N4
       BYTE A2,N4
* Measure 34
OTTO1G
       BYTE C3,N4
       BYTE C3,N8
       BYTE F3,N8
       BYTE D3,N4
       BYTE D3,N8
       BYTE F3,N8
* Measure 35
       BYTE C3,N4
       BYTE C3,N8
       BYTE F3,N8
       BYTE D3,N4
       BYTE D3,N8
       BYTE F3,N8
* Measure 36
       BYTE C3,N4
       BYTE F3,N8
       BYTE E3,N8
       BYTE D3,N4
       BYTE G3,N8
       BYTE F3,N8
* Measure 37
       BYTE E3,N4
       BYTE G5,N32
       BYTE F5,N32
       BYTE E5,N32
       BYTE D5,N32
       BYTE C5,N32
       BYTE B4,N32
       BYTE A4,N32
       BYTE G4,N32
       BYTE F4,N32
       BYTE E4,N32
       BYTE D4,N32
       BYTE C4,N32
       BYTE B3,N32
       BYTE A3,N32
       BYTE G3,N32
       BYTE F3,N32
       BYTE E3,N32
       BYTE D3,N32
       BYTE C3,N32
       BYTE B2,N32
       BYTE A2,N32
       BYTE G2,N32
       BYTE F2,N32
       BYTE E2,N32
* Measure 38
       BYTE C2,N4
       BYTE C2,N8
       BYTE F2,N8
       BYTE D2,N4
       BYTE D2,N8
       BYTE F2,N8
* Measure 39
       BYTE C2,N4
       BYTE C2,N8
       BYTE F2,N8
       BYTE D2,N4
       BYTE D2,N8
       BYTE F2,N8
* Measure 40
       BYTE C2,N4
       BYTE C2,N8
       BYTE D2,N8
       BYTE F2,N4
       BYTE F2,N8
       BYTE G2,N8
* Measure 41
OTTO1H
       BYTE A2,N8
       BYTE F2,N8
       BYTE G2,N8
       BYTE E2,N8
       BYTE F2,N4
       BYTE A2,N4
* Measure 42
OTTO1I
       BYTE A2,N8
       BYTE F2,N8
       BYTE G2,N8
       BYTE E2,N8
       BYTE F2,N4
       BYTE C3,N4
* Measure 43
OTTO1J
       BYTE C3,N8
       BYTE C3,N4
       BYTE C3,N8
       BYTE C3,N4DOT
       BYTE C3,N8
* Measure 44
       BYTE A2,N8
       BYTE C3,N4
       BYTE C3,N8
       BYTE C3,N4
       BYTE C3,N4
* Measure 45
       BYTE D3,N8
       BYTE D3,N4
       BYTE D3,N8
       BYTE D3,N4DOT
       BYTE D3,N8
* Measure 46
       BYTE D3,N8
       BYTE D3,N4
       BYTE D3,N8
       BYTE D3,N4
       BYTE C3,N4
* Measure 47
       BYTE F3,N4
       BYTE F3,N4
       BYTE C3,N4
       BYTE C3,N4
* Measure 48
       BYTE A2,N4
       BYTE A2,N4
       BYTE G2,N4
       BYTE F3,N8
       BYTE G3,N8
* Measure 49
       BYTE A3,N8
       BYTE G3,N8
       BYTE A3,N8
       BYTE Bb3,N8
       BYTE C4,N8
       BYTE A3,N8
       BYTE F3,N8
       BYTE G3,N8
* Measure 50
OTTO1K
       BYTE A3,N8
       BYTE F3,N8
       BYTE G3,N8
       BYTE E3,N8
       BYTE F3,N4
       BYTE C3,N4
* Measure 51
OTTO1L
       BYTE A3,N8
       BYTE F3,N8
       BYTE G3,N8
       BYTE E3,N8
       BYTE F3,N4
* Measure 52
       BYTE C4,N8TRP
       BYTE D4,N8TRP
       BYTE E4,N8TRP
* Measure 53
       BYTE A3,N4
       BYTE A3,N8TRP
       BYTE Bb3,N8TRP
       BYTE B3,N8TRP
       BYTE F3,N4
       BYTE F3,N8TRP
       BYTE G3,N8TRP
       BYTE Gs3,N8TRP
* Measure 54
       BYTE C3,N4
       BYTE C3,N8TRP
       BYTE D3,N8TRP
       BYTE E3,N8TRP
       BYTE A2,N4
       BYTE A2,N8TRP
       BYTE Bb2,N8TRP
       BYTE B2,N8TRP
* Measure 55
       BYTE C2,N4
       BYTE Cs2,N4
       BYTE D2,N4
       BYTE E2,N4
* Measure 56
       BYTE F2,N4
       BYTE REST,N4
       BYTE F3,N4
OTTO1M
*

* Generator 2
* Measure 1
OTTO2
       BYTE REST,N4
* Measure 2
       BYTE E1,N8
       BYTE E1,N4
       BYTE E1,N8
       BYTE E1,N4
       BYTE REST,N4
* Measure 3
       BYTE F1,N8
       BYTE F1,N4
       BYTE F1,N8
       BYTE F1,N4
       BYTE REST,N4
* Measure 4
       BYTE F1,N4
       BYTE REST,N4
       BYTE D1,N4
       BYTE REST,N4
* Measure 5
       BYTE D1,N4
       BYTE REST,N4
       BYTE B0,N4
* Measure 6
OTTO2A
       BYTE REST,N4
* Measure 7
       BYTE C1,N4
       BYTE E1,N4
       BYTE B0,N4
       BYTE E1,N4
* Measure 8
       BYTE C1,N4
       BYTE E1,N4
       BYTE B0,N4
       BYTE E1,N4
* Measure 9
       BYTE C1,N4
       BYTE E1,N4
       BYTE E1,N4
       BYTE G1,N4
* Measure 10
       BYTE B0,N4
       BYTE D1,N4
       BYTE B0,N4
       BYTE D1,N4
* Measure 11
       BYTE C1,N4
       BYTE E1,N4
       BYTE B0,N4
       BYTE E1,N4
* Measure 12
       BYTE C1,N4
       BYTE E1,N4
       BYTE B0,N4
       BYTE E1,N4
* Measure 13
       BYTE C1,N4
       BYTE E1,N4
       BYTE B0,N4
       BYTE E1,N4
* Measure 14
       BYTE E1,N4
       BYTE D1,N4
       BYTE E1,N4
* Measure 15
OTTO2B
       BYTE G3,N4
* Measure 16
       BYTE C1,N4
       BYTE E1,N4
       BYTE B0,N4
       BYTE E1,N4
* Measure 17
       BYTE C1,N4
       BYTE E1,N4
       BYTE E1,N4
       BYTE G1,N4
* Measure 18
       BYTE F1,N4
       BYTE A1,N4
       BYTE C1,N4
       BYTE A1,N4
* Measure 19
       BYTE A0,N4
       BYTE F1,N4
       BYTE F1,N4
       BYTE D1,N4
* Measure 20
       BYTE C1,N4
       BYTE E1,N4
       BYTE B0,N4
       BYTE D1,N4
* Measure 21
       BYTE C1,N4
       BYTE E1,N4
       BYTE D1,N4
       BYTE REST,N4
* Measure 22
       BYTE C1,N4
       BYTE E1,N4
       BYTE E1,N4
       BYTE E1,N4
* Measure 23
       BYTE E1,N4
       BYTE D1,N4
       BYTE E1,N4
* Measure 24
OTTO2C
       BYTE REST,N4
* Measure 25
OTTO2D
       BYTE A0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE F1,N4
* Measure 26
       BYTE A0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE F1,N4
* Measure 27
       BYTE A0,N4
       BYTE F1,N4
       BYTE A0,N4
       BYTE F1,N4
* Measure 28
       BYTE C1,N4
       BYTE F1,N4
       BYTE C1,N2
* Measure 29
       BYTE A0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE F1,N4
* Measure 30
       BYTE A0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE F1,N4
* Measure 31
       BYTE A0,N4
       BYTE F1,N4
       BYTE A0,N4
       BYTE F1,N4
* Measure 32
OTTO2E
       BYTE F1,N4
       BYTE C1,N4
       BYTE F1,N4
       BYTE REST,N4
* Measure 33
OTTO2F
       BYTE F1,N4
       BYTE C1,N4
       BYTE F1,N4
       BYTE REST,N4
* Measure 34
OTTO2G
       BYTE A0,N4
       BYTE F1,N4
       BYTE D1,N4
       BYTE F1,N4
* Measure 35
       BYTE A0,N4
       BYTE F1,N4
       BYTE D1,N4
       BYTE F1,N4
* Measure 36
       BYTE A0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE G1,N4
* Measure 37
       BYTE E1,N4
       BYTE REST,N2DOT
* Measure 38
       BYTE A0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE F1,N4
* Measure 39
       BYTE A0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE F1,N4
* Measure 40
       BYTE A0,N4
       BYTE F1,N4
       BYTE A0,N4
       BYTE F1,N4
* Measure 41
OTTO2H
       BYTE F1,N4
       BYTE C1,N4
       BYTE F1,N4
       BYTE REST,N4
* Measure 42
OTTO2I
       BYTE F1,N4
       BYTE C1,N4
       BYTE F1,N4
       BYTE REST,N4
* Measure 43
OTTO2J
       BYTE A0,N4
       BYTE F1,N4
       BYTE A0,N4
       BYTE F1,N4
* Measure 44
       BYTE A0,N4
       BYTE F1,N4
       BYTE A0,N4
       BYTE F1,N4
* Measure 45
       BYTE A0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE F1,N4
* Measure 46
       BYTE Bb0,N4
       BYTE F1,N4
       BYTE Bb0,N4
       BYTE C1,N4
* Measure 47
       BYTE F1,N4
       BYTE F1,N4
       BYTE E1,N4
       BYTE E1,N4
* Measure 48
       BYTE A0,N4
       BYTE A0,N4
       BYTE C1,N4
       BYTE REST,N4
* Measure 49
       BYTE F1,N4
       BYTE C1,N4
       BYTE F1,N4
       BYTE A0,N4
* Measure 50
OTTO2K
       BYTE C1,N4
       BYTE C1,N4
       BYTE F1,N4
       BYTE REST,N4
* Measure 51
OTTO2L
       BYTE C1,N4
       BYTE C1,N4
       BYTE F1,N4
* Measure 52
       BYTE REST,N4
* Measure 53
       BYTE F1,N4
       BYTE REST,N4
       BYTE C1,N4
       BYTE REST,N4
* Measure 54
       BYTE A0,N4
       BYTE REST,N4
       BYTE A0,N4
       BYTE REST,N4
* Measure 55
       BYTE C1,N4
       BYTE Bb0,N4
       BYTE A0,N4
       BYTE G1,N4
* Measure 56
       BYTE A0,N4
       BYTE REST,N4
       BYTE A0,N4
OTTO2M
*

* Generator 3
* Measure 1
OTTO3
       BYTE REST,N4
* Measure 2
       BYTE G3,N8
       BYTE G3,N4
       BYTE G3,N8
       BYTE G3,N4
       BYTE REST,N4
* Measure 3
       BYTE F3,N8
       BYTE F3,N4
       BYTE F3,N8
       BYTE F3,N4
       BYTE REST,N4
* Measure 4
       BYTE D3,N4
       BYTE REST,N4
       BYTE A2,N4
       BYTE REST,N4
* Measure 5
       BYTE G2,N4
       BYTE REST,N4
       BYTE D2,N4
* Measure 6
OTTO3A
       BYTE REST,N4
* Measure 7
       BYTE G2,N8
       BYTE G2,N4
       BYTE G2,N8
       BYTE G2,N4
       BYTE REST,N4
* Measure 8
       BYTE E2,N8
       BYTE E2,N4
       BYTE E2,N8
       BYTE E2,N4
       BYTE REST,N4
* Measure 9
       BYTE C3,N8
       BYTE C3,N4
       BYTE C3,N8
       BYTE C3,N8
       BYTE REST,N4DOT
* Measure 10
       BYTE B2,N8
       BYTE B2,N4
       BYTE Bb2,N8
       BYTE B2,N4
       BYTE REST,N4
* Measure 11
       BYTE G2,N8
       BYTE G2,N4
       BYTE G2,N8
       BYTE G2,N4
       BYTE REST,N4
* Measure 12 - 13
       BYTE E2,N8
       BYTE E2,N4
       BYTE E2,N8
       BYTE E2,N4
       BYTE REST,N2
       BYTE G1,N4
       BYTE REST,N4
       BYTE G1,N4
* Measure 14
       BYTE G1,N4
       BYTE G1,N4
       BYTE G1,N4
* Measure 15
OTTO3B
       BYTE REST,N4
* Measure 16
       BYTE E3,N8
       BYTE E3,N4
       BYTE E3,N8
       BYTE E3,N4DOT
       BYTE G3,N8
* Measure 17
       BYTE E3,N8
       BYTE E3,N4
       BYTE E3,N8
       BYTE E3,N4
       BYTE E3,N4
* Measure 18
       BYTE F3,N8
       BYTE F3,N4
       BYTE F3,N8
       BYTE F3,N4DOT
       BYTE F3,N8
* Measure 19
       BYTE C3,N8
       BYTE F3,N4
       BYTE F3,N8
       BYTE F3,N4
       BYTE G3,N4
* Measure 20
       BYTE G3,N4
       BYTE G3,N4
       BYTE D3,N4
       BYTE D3,N4
* Measure 21 - 22
       BYTE C3,N4
       BYTE C3,N4
       BYTE B2,N4
       BYTE REST,N2
       BYTE G1,N4
       BYTE G1,N4
       BYTE G1,N4
* Measure 23
       BYTE G1,N4
       BYTE G1,N4
       BYTE G1,N4
* Measure 24
OTTO3C
       BYTE REST,N4
* Measure 25
OTTO3D
       BYTE REST,N4
       BYTE A1,N4
       BYTE REST,N4
       BYTE Bb1,N4
* Measure 26
       BYTE REST,N4
       BYTE A1,N4
       BYTE REST,N4
       BYTE Bb1,N4
* Measure 27
       BYTE REST,N4
       BYTE A1,N4
       BYTE REST,N4
       BYTE A1,N4
* Measure 28
       BYTE REST,N4
       BYTE A1,N4
       BYTE E1,N2
* Measure 29
       BYTE REST,N4
       BYTE A1,N4
       BYTE REST,N4
       BYTE Bb1,N4
* Measure 30
       BYTE REST,N4
       BYTE A1,N4
       BYTE REST,N4
       BYTE Bb1,N4
* Measure 31
       BYTE REST,N4
       BYTE A1,N4
       BYTE REST,N4
       BYTE A1,N4
* Measure 32
OTTO3E
       BYTE A1,N4
       BYTE E1,N4
       BYTE A1,N4
       BYTE REST,N4
* Measure 33
OTTO3F
       BYTE A1,N4
       BYTE E1,N4
       BYTE A1,N4
       BYTE REST,N4
* Measure 34
OTTO3G
       BYTE C4,N4
       BYTE C4,N8
       BYTE A3,N8
       BYTE D4,N4
       BYTE D4,N8
       BYTE A3,N8
* Measure 35
       BYTE C4,N4
       BYTE C4,N8
       BYTE A3,N8
       BYTE D4,N4
       BYTE D4,N8
       BYTE A3,N8
* Measure 36
       BYTE C4,N4
       BYTE F4,N8
       BYTE E4,N8
       BYTE D4,N4
       BYTE G4,N8
       BYTE F4,N8
* Measure 37 - 38
       BYTE E4,N4
       BYTE REST,N1
       BYTE A1,N4
       BYTE REST,N4
       BYTE Bb1,N4
* Measure 39
       BYTE REST,N4
       BYTE A1,N4
       BYTE REST,N4
       BYTE Bb1,N4
* Measure 40
       BYTE REST,N4
       BYTE A1,N4
       BYTE REST,N4
       BYTE A1,N4
* Measure 41
OTTO3H
       BYTE A1,N4
       BYTE E1,N4
       BYTE A1,N4
       BYTE REST,N4
* Measure 42
OTTO3I
       BYTE REST,N2DOT
       BYTE A3,N4
* Measure 43
OTTO3J
       BYTE F3,N8
       BYTE F3,N4
       BYTE F3,N8
       BYTE F3,N4DOT
       BYTE F3,N8
* Measure 44
       BYTE C3,N8
       BYTE F3,N4
       BYTE F3,N8
       BYTE F3,N4
       BYTE F3,N4
* Measure 45
       BYTE F3,N8
       BYTE F3,N4
       BYTE F3,N8
       BYTE F3,N4DOT
       BYTE F3,N8
* Measure 46
       BYTE F3,N8
       BYTE F3,N4
       BYTE F3,N8
       BYTE F3,N4
       BYTE C4,N4
* Measure 47
       BYTE A3,N4
       BYTE A3,N4
       BYTE E3,N4
       BYTE E3,N4
* Measure 48
       BYTE C3,N4
       BYTE C3,N4
       BYTE C3,N4
       BYTE REST,N4
* Measure 49
       BYTE A1,N4
       BYTE E1,N4
       BYTE A1,N4
       BYTE F1,N4
* Measure 50
OTTO3K
       BYTE REST,N2DOT
       BYTE C4,N4
* Measure 51
OTTO3L
       BYTE REST,N4
       BYTE E1,N4
       BYTE A1,N4
* Measure 52
       BYTE REST,N4
* Measure 53
       BYTE C4,N4
       BYTE REST,N4
       BYTE A3,N4
       BYTE REST,N4
* Measure 54
       BYTE F3,N4
       BYTE REST,N4
       BYTE C3,N4
       BYTE REST,N4
* Measure 55
       BYTE C3,N4
       BYTE Cs3,N4
       BYTE D3,N4
       BYTE E3,N4
* Measure 56
       BYTE F3,N4
       BYTE REST,N4
       BYTE A3,N4
OTTO3M
*

