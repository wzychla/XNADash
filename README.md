# XNADash

## Overview

Years ago, in 2011, I've ported an old DOS game, <a href="https://en.wikipedia.org/wiki/Heartlight_(video_game)">Heartlight</a>, to XNA. 

The original is playable using a DOS emulator, can be an online one. There are also other remakes, far better than mine.
<a href='https://www.google.com/search?q=heartlight+play+online'>Click here to play the original version</a>.

In 2025, the old code is recompiled in .NET8/Monogame and published here on Github.

![game](game.png)

## Assets

* the code was written in C#, completely from scratch, in 2011
* starting from 0.53, most textures are taken from the original
* all songs were written by me back in 2011, using <a href='https://openmpt.org/'>OpenMPT</a> 
  (songs are not composed by me, if by any chance you recognize their original author/title, please let me know)

## Unit tests

Since the `DashBoard` can be orchestrated without any desktop window,
unit tests are possible. Unit tests try to recreate various scenarios and
make sure the physics behaves just like the original.
	
## Status

Current version: 0.56

There are 70 levels in total. The `LEVELS.HL` file comes from the original. It's a text file that contain all levels. Creating new levels is easy, just edit the text file.

Most levels should be playable. Known issues are described below.

## Changes

### 0.56

* tunnel textures
* fixed the level 22 unit test
* first unit tests added

### 0.55

* added more textures

### 0.54

* dumped the idea of the `NullBlock`, instead changed the order of vertical processing.
* imported first few board textures from the original game. The goal is to finally
fix timing issues with correct number of frames for each texture block

### 0.53

* added the `NullBlock` that lasts 1 frame, placed on a spot where another block fall from. 
This fixes multiple timing issues, e.g. in Level 10 or Level 12 where a balloon has two blocks on it.

## Known issues 

### Level 10

Current status: playable.

Timing issues of falling balloons Fixed in 0.53

### Level 12

Current status: playable.

Timing issues of falling balloons Fixed in 0.53

### Level 13

Current status: playable.

Fixed in 0.4.

There were two issues here:

* a bomb falling on another falling bomb was still exploding. A pile of falling bombs was exploding in the very same frame.
* a boom lasted too short so that some bombs were falling prematurely

### Level 18

Current status: partially playable

If the exit is reached just before the time limit, the exit can be destroyed (and should not be).

### Level 21

Current status: unplayable

Bomb timing is still invalid. 

### Level 27

Current status: unplayable

Bomb timing is still invalid. 

### Level 40

Current status: unplayable

Bomb timing is still invalid. Bomb probably should be able to fall (and clear) a boom block.

### Level 42

Current status: unplayable

### Level 59

Current status: unplayable

Partially fixed: heart falling on a bomb should not trigger an explosion.