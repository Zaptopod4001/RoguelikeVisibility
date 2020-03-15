# Roguelike Visiblity (Unity / C#)

![Roguelike visibility](/doc/roguelike_visibility.gif)

## What is it?
A test project to see how to create a typical roguelike visibility system. In this case, I haven't yet tried to implement brensenham's line algorithm, so I used a custom solution I cooked up. 

This specific solution is using Unity's bounds, class - it contains IntersectRay method, that can be used for pretty performant ray-bounds intersection test to see which cells visibility lines cross. 

I also did a shadow casting, rays stop to closest walls - well this wouldn't be roguelike visibility unless there is occlusion.
 

# Classes

## VisibilityTest.cs
Test class that shows how to use visibility checker class.

## VisibilityChecker.cs
The actual class that does line of sight and visibility circle calculations.


# About
I created this visibility calculation system for myself, as a learning experience.

# Copyright 
Created by Sami S. use of any kind without a written permission from the author is not allowed. But feel free to take a look.
