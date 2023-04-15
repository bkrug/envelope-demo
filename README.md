# envelope-demo
A program that plays music on the SN76489 sound chip and TI-99/4a home computer.
Notes have envelops applied to them.
A C# program for parsing MUSICXML files into TMS9900 assembly language is also included.

The Assembly language program supports
* Allowing the user to select different songs and different envelopes to play
* If the envelope is an Attack-Decay-Sustain-Release envelope, a 5-byte area of the program can specify the rates of volume change and the sustain level
* Playing music at tempos defined according to a 60hz or 50hz "ratio"
* Upon completion, the song can either stop, repeat from the beginning, or repeat everything except a short intro
* Auto-detection of a 60hz vs. a 50hz electrical system

This repo includes a C# program that parsed musicXml files into TMS9900 assembly language.
This parsing program supports:
* outputing a different stream of data for each sound generator
* half notes that are played simultaneous with two quarter notes (or similar examples of differnt lengths)
* music that may come from more than one "part" or more than one "voice"
* repeat bars
* volta brackets
* tied notes
* compressing rests to save space
* recording grace notes as commented-out code
* recording notes outside the sound chip's range as a rest, with a comment specifying the intended note

The parser does _not_ support:
* other repeats such as codas (but the necessary plumbing is already there.)
* outputing data for the noise generator (which could otherwise be used to represent percussion)
* crescendo and other volume changes within the song
* staccato and legato
* tempo changes mid-song

## Song Header and Ratios
At the begging of each "TUNE*.asm" file, see a "Song Header".
The first three words of memory contain the addresses of the music data for each sound generator.
(Or a zero (0) if the song does not need all three generators.)
The next three words of memory contain the addresses of data telling the program which portions of a song are to be repeated.
(Again, a zero (0) will be placed here if a sound generator is unused.)
Two more words of memory specify a ratio which controls the length of notes in a 60hz environment.
The final two words of memory specify a ratio which controls the length of notes in a 50hz environment.

In a 60hz environment, a ratio of 1:1 results in a tempo of 150 quarter notes per minute.
Each quarter note has a duration of 24/60ths of a second.
A ratio of 2:1 will double the duration of each note, and split the tempo in half to 75 quarter notes per minute.
Ratios that don't divide the duration evenly are acceptable.
A ratio of 4:5 will result in some quarter notes that last 19/60ths of a second and some that last 20/60ths of a second.
This is possible because the music playing code keeps track of remainders.

In a 50hz environment, a ratio of 1:1 results in a tempo of 125 quarter notes per minute.
Each quarter note has a duration of 24/50ths of a second.
All of the other rules that apply to 60hz environments apply to 50hz environments.

The formula for determining the duration of any note is as follows: <br>
> duration = (default_duration * ratio_numerator + remainder_from_previous_note) / ratio_denominator

The note is played for the integer portion of that result.
The remainder from that result is stored for use when playing the next note in the same generator.

## Parser
The MusicXml parser is included as source code because it is assumed that my needs are different from yours.
The MusicXml parser has high unit test coverage, so another programmer should be able to add features with little fear of breaking anything.

Here is an example of calling the music parser.

`.\MusicXmlParser\MusicXmlParser\bin\Debug\netcoreapp3.1\MusicXmlParser.exe
    --input ".\Fr_Elise_SN76489.musicxml"
    --output ".\TUNEFURELISE.asm"
    --asmLabel "BEETHV"
    --ratio60Hz "2:1"
    --ratio50Hz "10:6"
    --repetitionType "RepeatFromBeginning"
    --displayRepoWarning 'true'`

`--input` -- the source file. Should be a MusicXml file<br>
`--output` -- the ouput assembly language file to be used by your own program<br>
`--asmLabel` -- the label that will correspond to the Song Header. This is the only address exposed to the rest of your assembly language program through a REF statement.<br>
`--ratio60hz` -- the ratio to shorten or lengthen note durations in a 60hz environment<br>
`--ratio50hz` -- the ratio to shorten or lengthen note durations in a 50hz environment<br>
`--repetitionType` --<br>
use "StopAtEnd" to specify that a song should be played only once<br>
use "RepeatFromBeggining" to specify that a song should be played in its entirety repeatedly<br>
use "RepeatFromFirstJump" to skip any introduction at the beginning of the song, but play the rest repeatedly<br>
`--displayRepoWarning` -- should probably be "false" or omitted for most users. When true it adds a message to the output specifying that this is auto-generated code and that it doesn't need to be included in the repo.

## To assemble the cartridge program
Have XDT99 (https://endlos99.github.io/xdt99/) available on your computer.
This, in turn, requires python.

### Finding the path to Python

In your Python interpreter, type the following commands:

`import os`
`import sys`
`os.path.dirname(sys.executable)`

'C:\\Python25'

### Running Python Scripts from Powershell (In a Windows Environment)

First, add the xas99 folder to your System PATH variables.

Then, associate *.py files with python.
Run the following commands at a shell prompt:

`assoc .py=PythonScript`
`ftype PythonScript=C:\bin\python.exe "%1" %*`

Replace C:\bin\python.exe with the path to your Python installation. This enables you to run `myscript.py` instead of `python myscript.py`

Add .py to your PATHEXT environment variable.
This makes Windows consider *.py files to be executable when searching your PATH. It enables you to run `myscript` instead of `myscript.py`
You can set it for the current cmd session:

`set PATHEXT=%PATHEXT%;.PY`

To set it permanently:

`setx PATHEXT %PATHEXT%;.PY`

## The Samples

All three sample songs where taken from the MuseScore website.
In order to produce _Turkey in the Straw_ and _Old Foks At Home_ in the cartridge program, the MuseScore application was used to take certain notes, outside the SN76489 range, and upscale them by either a perfect 5th or an octave.
The music was then exported from MuseScore as a MusicXml file.

In order to produce _Lasciate i monti_ in the cartridge program, the MuseScore file was exported unchanged to MusicXml, but then some of the Parts were deleted from an XML editor.
Only the recorder, violin 1, and cello parts were retained.
