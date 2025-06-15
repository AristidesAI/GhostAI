## Ghost AI


#### Project Goal:
To develop a technical prototype for a Unity-based mobile game for iOS. This prototype will serve as the foundational framework for a future multiplayer game. The core of this project is to successfully replicate the movement, 2.5D perspective, and unique vision/lighting mechanics of the game "Among Us," but adapt them for a vertical screen orientation.
1. High-Level Concept
The game is a top-down 2.5D action game where the player controls a ghost character on a single, contained map. The primary user experience is built around exploration and navigation within a world where visibility is limited. The game must feel intuitive and immersive on an iPhone, with a user interface that supports single-handed play.
Inspiration: The primary inspiration is "Among Us." We are specifically focused on emulating its:
Movement & Animation Style: Simple, fluid 8-directional movement with distinct idle and running animations.
Vision & Lighting System: A dynamic "cone of vision" that emanates from the player. This light is realistically blocked by walls and other environmental objects, creating shadows and hiding areas from view.
2.5D Camera Perspective: A top-down view that still provides a sense of depth.
Key Adaptation: The most significant change from the inspiration is the vertical viewport. All gameplay, UI, and camera work must be optimized for a portrait orientation on an iPhone.
2. Core Gameplay Features & Mechanics
#### Player Character & Control:
The player controls a single ghost character.
Movement is handled by a virtual joystick located at the bottom-center of the screen. This joystick must be responsive and visually unobtrusive.
Movement is 8-directional (including diagonals) and should feel smooth.
Keyboard controls (WASD/Arrows) must also be implemented for easy testing within the Unity Editor.
#### The World & Environment:
The game takes place on a single, square map enclosed by walls.
The map is populated with various objects (e.g., crates, pillars, walls) that players must navigate around.
These objects are critical as they will have collision and, more importantly, will block the player's line of sight.
#### "Fog of War" Lighting System:
This is a critical feature. A visual representation of light should emanate from the player in a 360-degree circle.
This light must be dynamically interrupted by any object on the "Obstacle" layer. When light is blocked, it should create a "shadow" or "darkness" effect.
Any part of the map (and any other characters) within this darkness should be hidden from the player's view. This effect is crucial for the intended gameplay loop of discovery and suspense.
#### Camera System:
The camera will be a perspective camera, angled to give a 2.5D view.
It will smoothly follow the player.
The framing must prioritize the vertical view, showing more of the map ahead of and behind the player than to the sides.
A key feature is that taller objects should be able to obscure the player if the player walks behind them, enhancing the sense of depth.
#### Characters & Animation:
The player character will have two basic animation states: "Idle" and "Running." The appropriate animation should play based on whether the character is moving.
To simulate a multiplayer environment for this prototype, there will be three placeholder AI characters. These AI will wander the map randomly, also featuring idle/run animations and interacting with the collision and lighting systems just as a player would.
3. User Interface (UI) - The Virtual Joystick
The UI must be minimal and highly functional. The virtual joystick is the centerpiece.
Unobtrusive Design: It should have adjustable transparency, becoming more opaque when in use and fading when idle.
Ergonomic Placement: Positioned at the bottom-center for comfortable thumb access during single-handed play.
Configurable: The size, dead zone, and transparency levels (active vs. inactive) must be adjustable from the Unity Inspector to allow for fine-tuning the feel of the controls.
#### How to Use This Document
Use this project brief as the guiding context for the entire development process. When you are asked to generate a specific C# script (like PlayerController.cs or FieldOfView.cs) using the prompts from the other document, this brief provides the "why" behind the request. It explains how each individual script fits into the larger game system and the overall player experience we are trying to achieve.


### Artwork Concepts
![Idle](PrototypeAssets/GhostiOSIcons/Ghost.AI-iOS-ClearDark-1024x1024@2x.png)
