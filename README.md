# Polygonal Snake

[![Build status](https://github.com/3mam/PolySnake/actions/workflows/dotnet.yml/badge.svg)](https://github.com/3mam/PolySnake/actions)
[![Live demo](https://api.netlify.com/api/v1/badges/11156706-f6b1-41f9-95b9-8fcf7743fd31/deploy-status)](https://elaborate-lily-2c0859.netlify.app)
![Snake Screenshot](Assets/output.gif)

## [live demo](https://elaborate-lily-2c0859.netlify.app)
Project goal is write from scratch 2D Snake in **.net 6**.<br><br>
This project contains port for Blazor Server and Webassembly.
I adding Blazor for proof of concept. Game on Blazor don't
works 100% same way like desktop version.
Biggest problem is behavior of floating-point numbers. Is different
between Desktop and Blazor,
and this create some problems with proper calculation.
For reduce problem I don't use **double**.


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
Blazor server.
> dotnet run --project BlazorServer

In browser go to this url.
> https://localhost:5267

Blazor webassembly.
> dotnet run --project BlazorWasm

In browser go to this url.
> https://localhost:5249

## Build
> cd Poly
 
For Windows.
> dotnet publish -c Release --runtime win-x64

For Linux.
> dotnet publish -c Release --runtime linux-x64