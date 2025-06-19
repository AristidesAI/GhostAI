# Ghost AI

## Project Goal

To develop a technical prototype for a Unity-based mobile game for iOS. This prototype will serve as the foundational framework for a future NPC AI & multiplayer game features. The purpose of this game is to be published on the IOS application store as a technical demonstration of Game AI. The game must feel intuitive and immersive on an iPhone, with a user interface that supports single-handed play.

## High-Level Concept

The game is a top-down 2.5D action game where the player controls a ghost character on a procedurally generated maze map. The primary user experience is built around exploration and navigation of this maze, While attempting to "catch" or "tag" the NPC AI Players and eventually other humans with multiplayer functionality. The game involves one human-controlled character whose objective is to "tag" three NPC AI characters within a procedurally generated maze. The human player tags the other NPC AI Players by running into them & pressing the tag button at which point they disappear in a cloud of smoke. The human player (Tagger) have slightly faster movement speed than the NPC AI Players (Taggy) - This is to enable the "tagger" player to catch up to the "taggy" players. 

###### Definition: In this scenario the human player refers to the human operator of the game, In future implementations multiplayer will be a feature so keep in mind "the human player" refers to the "tagging" player with the ability to tag and eliminate "taggys players"

###### Definition: In this scenarion the NPC AI Player refers to the autonumous game AI that will be implemented in this build, In future implementations the "NPC AI Player" will not tie directly to the "taggys players". In this build of the game it will.

Inspiration: The primary inspiration is "Among Us." We are specifically focused on emulating its:
- Movement & Animation Style: Simple, fluid 8-directional movement with distinct idle and running animations.
- 2.5D Camera Perspective: A top-down view that still provides a sense of depth.
- The killing gameplay mechanic, where a player is close enough to another player a Kill or in this case Tag option appears and the player can execute a tag action, eliminating the NPC AI Player
###### a significant change from the inspiration is the vertical viewport. All gameplay, UI, and camera work must be optimized for a portrait orientation on an iPhone.

## Core Gameplay Features & Mechanics
#### Player Character & Control:
- The player controls a single ghost character.
- Movement is handled by a virtual joystick located at the bottom-center of the screen. This joystick must be responsive and visually unobtrusive.
- Movement is 8-directional (including diagonals) and should feel smooth.
- Tagging NPC AI Players is handled by a "tag" button that appears situationally in the middle of the screen to the right above and to the right of the virtual joystick. 
- Keyboard controls (WASD/Arrows/E to "tag") must also be implemented for easy testing within the Unity Editor.

#### Build 1.0.1 In the current build of the project, The working features are:

The player character & AI Player has two basic animation states: "Idle" and "Running." The appropriate animation should play based on whether the character is moving. There are three placeholder AI characters. These AI will wander the map randomly, also featuring idle/run animations and interacting with the collision

- A player character with advanced movement (acceleration and deceleration).
- A responsive virtual joystick and keyboard controls.
- Player animations with dynamic speed and correct sprite flipping.
- Basic AI characters that wander the map.
- Basic CapsuleCollider2D colliders for the player & AI player
- Basic collision mechanics on the walls
- Game layers: Background|Midground|Player|Obstacles|Foreground 
- AI animations with dynamic speed and correct sprite flipping.
- A smooth camera that follows the player.

#### Build 2.0.1 What needs to be implemented in this build for further testing, Not the full functionalty:

- A timing system, Where the overall game time is measured, Appearing as a timer counting up from when the game starts, To when the last AI Player is tagged and the game finishes (Described further Below)
- UI system for basic game menu functionality, Pause & Unpause functionality, quit to main menu functionality - Main Menu. Color. Start Game. Pause. Continue. Quit. (Described further Below)
  - Pause Button in the top right hand corner of the screen, Highly transparent as to allow visibility of the viewscreen. coding for a "button press" animation (Described further Below)
- Tagging mechanic, Tagging button, a scoring system to count the number of AI Players that the Human Player has Tagged (Described further Below)
- more robust camera system that followers the human players "taggers" movement but zooms in and out dynamically to frame in the AI players so they are viewable on screen at all times - When an AI Player "taggy" is tagged and eliminated the camera should smoothly zoom in and out dynamically to accomodate the new number of AI players on screen and thus the new zoom range (Described further Below)

**UI Components:**

The Game has a starting menu [ with 2 buttons, the game title "Ghost.AI" at the top middle centered 22% from the top of the screen - behind the "Ghost.AI" title and the buttons is a white background ] 

The Virtual Joystick element and the Pause Button element are hidden and fade into view when the game session starts, the white background fades out and the buttons and title disapear 

The 2 buttons appear in 1 column 30% from the bottom of the game title, the buttons are seperated by 5% from the bottom to the top of the next in order

Start
Color

first option “Start” (Starts the game session) - makes the starting menu
second option “Color” lets (Players select their players color) and it changes the color of the onscreen idle character on the menu.

The Color button changes the players sprite set with a corresponding identical set with different solid colors the sprites will be set up as assets>sprites>%colornamefolder%> (sprites of the running and idle animations but in different colors). When the sprites are not in the required folders use the default sprite model 

For testing the UI components like the menu, color option and the restart option should have lots of unity [SerializeField] 

**Player Timer & Pause Button:**

Timing System Feature: A timing system, Where the overall game time is measured, Appearing as a timer counting up from when the game starts, To when the last AI Player is tagged and the game finishes. 
At the end of the game the time is displayed where the game title appears, the the game resets (timer disapears reset game logic to the beginning) and fades back to the title screen 
timer is a semi transparent floating counter that appears in the left notch display area 

Pause Button Feature: The pause button is a square semi transparent floating clickable element that appears in the right notch display area  
When the game is paused there are 2 button clickable elements "continue" which unpauses the game and returns to gameplay "restart" which resets the game fading back to the title screen 
On the top of the iOS display it should look like this: {Timer} left side of apple notch {apple notch element} right side of apple notch {Pause button} 

For testing the timer mechanic should have lots of unity [SerializeField] 
For testing the pause button mechanic should have lots of unity [SerializeField] 

**Tagging Mechanic:**

The player "tagger" tags/catches the other AI Players "taggys" by running within a close enough range & pressing the tag button at which point they disappear in a cloud of smoke. Use the killing mechanic that the "imposter" character in "among us" has as a reference. The tagging mechanic is inspired by the kill mechanic in the game "Among Us" - When the player "tagger" gets in a certain range the tag button appears more visible and less transparent indicating a "tag" can take place when the player clicks on/presses the tag button on screen the AI player in range is "tagged" and eliminated. A puff of smoke frame by frame animation takes place ontop of the AI player and they disappear, That AI player "taggy" has been eliminated from the round. 
In the event the "tagger" player is in range of 2 "taggy" AI players, the closer "taggy" player is selected and tagged and eliminated. When the tag animation occours, The camera should dynamically zoom to the new range while the animation is playing and not linger on the eliminated AI player. When you are implementing this feature - make sure you follow the guide of making it function like for like with the among us killing mechanic. 

For testing the tagging mechanic should have lots of unity [SerializeField] 

**Camera System:**

Camera system that smoothly followers the human players "taggers" movement but zooms in and out dynamically to frame in the AI players so they are visible on screen the edge of the screen at all times, the zoom should be pushed into the viewscreen to a certain range so that the audience can see the AI players and a small area of them to better see the AI players and their animations, to accomodate the tagging purpose of the game & to accomodate the later maze funtionality. The zoom should be smooth, clean, and adjustable in the unity editor - When an AI Player "taggy" is tagged and eliminated the camera should smoothly zoom in and out dynamically to accomodate the new number of AI players on screen and thus the new zoom range. The camera should follow the human player and change the zoom range and zoom speed very slowly so as not to confuse the players sense of space in the game, this is also to make chasing the AI players easier as zooming the camera in and out too broadly and too fast will make tracking both the players own movements and the AI players movements difficult

For testing the camera system should have lots of unity [SerializeField]





## Artwork Concepts
![Idle](PrototypeAssets/GhostiOSIcons/Ghost.AI-iOS-ClearDark-1024x1024@2x.png)
