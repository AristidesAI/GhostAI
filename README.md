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
- A game timer that counts up from 0 when the software runs
- Debugmanager that displays information about the players movements 

#### Build 2.0.1 What needs to be implemented in this build for further testing, Not the full functionalty:


- Pause Button in the top right hand corner of the screen, Highly transparent as to allow visibility of the viewscreen while the game is running. (Described further Below)

- Tagging mechanic, Tagging button, game finishes when all AI players are tagged (Described further Below)

- more robust camera system that followers the human players "taggers" movement but zooms in and out dynamically to frame in the AI players so they are viewable on screen at all times - When an AI Player "taggy" is tagged and eliminated the camera should smoothly zoom in and out dynamically to accomodate the new number of AI players on screen and thus the new zoom range (Described further Below)

The timer mechanic has already been implemented and the script is found here My project/Assets/_Scripts/GameTimer.cs

**Pause Button:**

Pause Button Feature: The pause button is a square semi transparent floating clickable element that appears in the right notch display area  
When the game is paused, The gametimer should stop at its current time Pause the gamestate, when the pause button is pressed again the timer continues where it stopped and continues counting up. When the pause buttons is pressed again the gamestate unpauses and the game continues

The timer mechanic has already been implemented and the script is found here My project/Assets/_Scripts/GameTimer.cs - Use the existing script to assist in building the Pause Button Mechanic

For testing the pause button mechanic should have lots of unity [SerializeField] 

**Tagging Mechanic:**

The player "tagger" tags/catches the other AI Players "taggys" by running within a close enough range & pressing the tag button at which point they disappear in a cloud of smoke. Use the killing mechanic that the "imposter" character in "among us" has as a reference. When implementing the tagging mechanic, Look at how the kill mechanic works for imposter players in Among Us. The tagging mechanic is inspired by the kill mechanic in the game "Among Us" - When the player "tagger" gets in a certain range the tag button appears more visible and less transparent indicating a "tag" can take place when the player clicks on/presses the tag button on screen the AI player in range is "tagged" and eliminated. A puff of smoke frame by frame animation takes place ontop of the AI player and they disappear, That AI player "taggy" has been eliminated from the round. 

The Tag mechanic is implemented with an onscreen button - this button appears highly transparent and non-interactable while there are no AI Players in range of the Human Player. When the human player is in a certain circular radius of an AI Player, the Tag button becomes less transparent and interactable. When the tag button is pressed by the player - AI player that was in range of the human player is eliminated from the round and will disapear. 

In the event the "tagger" player is in range of 2 "taggy" AI players, the closer "taggy" player is selected and tagged and eliminated. When the tag animation occours, The camera should dynamically zoom to the new range while the animation is playing and not linger on the eliminated AI player. When you are implementing this feature - make sure you follow the guide of making it function like for like with the among us killing mechanic. 

For testing the tagging mechanic should have lots of unity [SerializeField]-plural
the current AI script exists here My project/Assets/_Scripts/AIController.cs in the github repo - ensure any code implementing the tag mechanic is using the existing AI controllers logic to divise the method. If you need to make changes to the AI Controller script, Document them with comments and make sure any changes includes lots of unity [SerializeField]-plural 

**Camera System:**

Camera system that smoothly followers the human players "taggers" movement but zooms in and out dynamically to frame in the AI players so they are visible on screen the edge of the screen at all times, the zoom should be pushed into the viewscreen to a certain range so that the audience can see the AI players and a small area of them to better see the AI players and their animations, to accomodate the tagging purpose of the game & to accomodate the later maze funtionality. The zoom should be smooth, clean, and adjustable in the unity editor - When an AI Player "taggy" is tagged and eliminated the camera should smoothly zoom in and out dynamically to accomodate the new number of AI players on screen and thus the new zoom range. The camera should follow the human player and change the zoom range and zoom speed very slowly so as not to confuse the players sense of space in the game, this is also to make chasing the AI players easier as zooming the camera in and out too broadly and too fast will make tracking both the players own movements and the AI players movements difficult

The existing main camera uses this script in this repo in My project/Assets/_Scripts/CameraController.cs - Change this camera system to implement these new features. 


For testing the camera system should have lots of unity [SerializeField]-plural

