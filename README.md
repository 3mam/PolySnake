# Polygonal Snake

[![Build status](https://github.com/3mam/PolySnake/actions/workflows/dotnet.yml/badge.svg)](https://github.com/3mam/PolySnake/actions)

![Snake Screenshot](Assets/output.gif)

Project goal is write from scratch 2D Snake in **.Net 6**.<br>
This project contains port for Blazor Server.
I adding Blazor only for proof of concept.
Biggest problem of Blazor for now is behavior of floating-point numbers. Is different from desktop version, and
this create some problems with proper calculation.
I did hack for snake movement to behaves that same why as
desktop version.


## Used for this project

- libraries:
    - OpenTK
    - OpenGL4
- tools:
    - Rider
    - Blender

## Supported platform

- Linux
- Windows
- WebBrowser (experimental)

## Keyboard control

| Key           | Desktop      |Browser|
|---------------|--------------|---|
| Menu          | Escape       | Escape|
| Option Up     | W, ArrowUp   | ArrowUp|
| Option Down   | S, ArrowDown | ArrowDown|
| Accept Option | Space, Enter | Enter|
| Turn Left     | A, ArrowLeft| ArrowLeft|
|  Turn Right   | D, ArrowRight| ArrowRight|

## Install and run

### Download code.
> git clone https://github.com/3mam/PolySnake.git

> cd PolySnake

### Run desktop version.
> dotnet run --project Poly

### Run browser version.
> dotnet run --project Blazor

And in browser go to this url.
> http://127.0.0.1:7777

## Build
> cd Poly
 
For Windows.
> dotnet publish -c Release --runtime win-x64

For Linux.
> dotnet publish -c Release --runtime linux-x64