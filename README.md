# Roguelike Visibility (Unity / C#)

![Roguelike visibility](/doc/roguelike_visibility.gif)

## What is it?
A Unity project I created to learn how to create a typical roguelike visibility system. In this case, I haven't yet tried to implement Bresenham's line algorithm, so I used a custom solution I cooked up. 

This specific solution is using Unity's Bounds struct - it contains really nice IntersectRay method, that can be used for pretty performant ray-bounds intersection test to see which cells fall into visibility lines path.

I also did a shadow casting, each ray stops to closest wall - this wouldn't actually be a roguelike visibility system unless there is occlusion!
 
# Classes

## VisibilityTest.cs
Test class that shows how to use visibility checker class.

## VisibilityChecker.cs
The actual class that does line of sight and visibility circle calculations.

# About
I created this visibility calculation system for myself as a learning experience.

# Copyright 
Created by Sami S. use of any kind without a written permission from the author is not allowed. But feel free to take a look.
