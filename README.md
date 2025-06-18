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

- Procedural Maze Generation Functionality as described below, this is required for the AI functionality to work properly with the maze system. The AI player needs to have a full view of the maze generation and its pathways. The AI scripts and Maze generation scripts should talk to one another 
- AI functionality described below - In future builds we will implement a difficulty setting so this should be considered when implementing the AI in this build
- Tagging mechanic, Tagging button, a scoring system to count the number of NPC AI Players that the Human Player has Tagged

#### Build 3.0.1 What needs to be implemented in the next build for further testing, Not the full functionalty:

- A timing system, Where the overall game time is measured, Appearing as a timer counting up from when the game starts, To when the last NPC AI Player is tagged and the game finishes
- UI system for basic game menu functionality, Pause & Unpause functionality, quit to main menu functionality - Main Menu. Start Game. Pause. Continue. Quit.
  - Pause Button in the top right hand corner of the screen, Highly transparent as to allow visibility of the viewscreen. coding for a "button press" animation
    
#### Build 4.0.1 What needs to be implemented for full game build functionality and publishing:

- Player colors. Multiple sprite animations with different colors
- iOS performance optimisations for running on device (iPhone Latest Release)
- Gamesounds, Player walking "tip-tap" 
- Full game menu with Start Menu, Credits, Settings= Difficulty settings for the NPC AI Players, Sound on/off. In game settings Pause menu, Continue, Restart, Quit to main menu. 

#### What needs to be implemented after release, For further updates:

- Multiplayer Functionality (Netcode)
  - Proximity based voice chat
  - Netcode implementation - Rollback netcode
  - Account systems - Using apple game account for account services, Allows players to account manage externally, Implement into unity game 
  - Automatically random selection sorting players into the "tagger" 1 player and the "taggys" 3 players
  - Servers - Hosted on AWS or Azure and implementing regions for optimal netplay
  - expanded animations for cosmetic items ontop of 
  - NPC AI Player & Human Player game mixing - Larger player counts
- Game Menu options for servers/auto join. 


#### AI Implementation:

Right now we are focusing on building the NPC AI Characters. This Game AI should be a technical demonstration of how game AI Pathfinding & nonvisual sensory mechanics works on an edge device like the iPhone. Before we implement the Maze generation element of the project, We need to get the AI characters working as outlined below. This AI is designed for threat detection, avoidance, continuous movement, and short-term decision making coupled with long-term maze pathfinding. The NPC characters are not passive targets; they are intelligent agents designed to actively evade the player and navigate the maze effectively. Their behavior will be governed by a set of core mechanics:

##### General Purpose

**Player Avoidance:** 
The primary directive is to avoid being tagged by the human-controlled player. The goal is for the NPC Player AI to stay away from the human player, Keep moving continuously as much as possible, and ultimately remain "untagged" for as long as possible. The human player (Tagger) have slightly faster movement speed than the NPC AI Players (Taggy) - This is to enable the "tagger" player to catch up to the "taggy" players. The NPC AI Players have variable movement speed using the animation system already implemented in the current build, Using that system to dynamically adjust the NPC AI Players speed and animation rate based on the mechanics below.

NPC AI Players structured directives: 

Continuous Movement: NPCs should generally remain in motion, exploring the maze, only stopping if their pathfinding is temporarily broken or they are in a "safe" recalculation state. The NPC AI should have a complete view of the maze so it can navigate the environment without relying on ingame senses entirly Movement should be smoothe for the AI not jittery.
Dead-End Avoidance: NPCs must be capable of recognizing and escaping dead-end paths. If stuck, they should be able to turn around and select an alternative route. This is particularly important when under threat.

##### **Maze Understanding:** The AI player will have a complete view of the generated maze and all its pathways, This enables the AI player to avoid dead ends and stucture its pathfinding based on an overall view of the maze, Their relation to the player and other AI players, And 

##### Threat Identification:

**Human Player:** The human player is the primary and highest threat.
Fellow NPCs: Other NPC characters are considered a much lower threat, just obstacles to navigate around rather than entities to actively evade unless they obstruct a critical escape path.

**Behavioral States:** The NPCs will operate using a state-based system, transitioning between behaviors based on perceived threats and environmental context.

**Default Behavior (Wandering):** When no immediate threat is perceived, NPCs will engage in random wandering. This involves selecting random reachable waypoints or directions within the maze. During wandering, they will periodically perform checks (using their sensory systems) to assess their surroundings and maintain a safe distance from potential threats.
Intensified Avoidance (Sensed)=

**Player Sensed:** If the player is detected through the non-visual "sense" (detailed below), the NPC might become more cautious, potentially altering its path to move away from the general direction of the player or increasing its scan rate for the player. When the human player is close to the  NPC' the NPC's avoidance behavior will significantly intensify. This includes:
- Increasing its run speed.
- Calculating and taking the fastest, most direct path away from the player's current position

**Path Obstruction and Recalculation:** If the player obstructs an NPC's current navigational path or is too close to its immediate waypoint destination, the NPC must be able to adapt. This is especially critical when the threat detection is highest. The NPC will turn around or choose an alternative path to continue its evasion.
All these states should be adjustable/customizable in the unity inspector by adding variables at the beginning of the code - This should be a big priority when writing the NPC AI code.
NPCs will perceive their environment and the player through a primary non visual sensory systems:

##### Player "Sense" (Non-Visual):

**Mechanism:** This is a unique, non-visual detection system. An invisible circular "aura" or "scent" emanates from the player character every frame. This aura can be conceptualized as a Gaussian blur effect in terms of its diminishing strength with distance.

**Wall Dampening:** Maze walls will "slightly dampen" this sense. This implies that the "aura" will still penetrate walls to some extent, but its strength or propagation distance will be reduced when passing through solid obstacles.
Dissipation: The sense phenomenon generated by the player dissipates over time or distance, meaning its effect is localized and not persistent indefinitely.

**Implementation Strategy:**
This requires a custom solution beyond simple raycasting. An influence map or a grid-based propagation algorithm appears most suitable:
Overlay a fine grid onto the maze structure.
Each frame, the player "emits" a value at their grid cell(s).
This value propagates (diffuses/blurs) to neighboring grid cells over a few iterations or frames.
Grid cells corresponding to walls will have a dampening factor (e.g., reduce propagated value by x amount, make this element customizable in the unity inspector).
NPCs sample the value of this "sense grid" at their current location to determine the strength of the player's presence. This approach allows for the "blur" and "dampening" effects. The "dissipation" can be managed by reducing all grid values slightly each frame or by limiting the propagation steps. Raycasts could be used to determine the number of walls between the player and NPC to apply a cumulative dampening factor, offering a more precise but potentially more computationally expensive alternative to grid cell-based dampening.

#### The World & Environment:
The game takes place in a procedurally generated maze structure that. procedural maze generation algorithm must create environments with specific structural characteristics to facilitate the intended gameplay dynamics:
Central Open Areas: The maze must include one or more relatively open areas in or near its center. These areas serve as hubs for player and NPC interaction, allowing for more dynamic chase and evasion scenarios.
Pathways to Central Area: Pathways should consistently lead back to these central open areas from various points in the maze. This ensures that players and NPCs are frequently routed through common zones, increasing the likelihood of encounters.
Dead-End Paths: To increase complexity and provide strategic opportunities (e.g., for trapping NPCs or for players to get cornered), the maze must incorporate dead-end paths.
Edge Paths and Loops: The maze design should include pathways that loop around its periphery and connect back to the central area. These edge loops offer alternative routes and can be used by both player and NPCs for strategic navigation.

##### Unity Implementation Strategy for Maze Generation
Translating the chosen algorithmic approach into a functional Unity system requires careful consideration of data structures, the generation pipeline, and refinement techniques.
Data Structures for Maze Representation
Effective maze generation and manipulation hinge on appropriate data structures:
##### 1. Primary Grid Data:
The core of the maze will be represented by a 30x30 grid. For optimal performance, especially with Unity's Job System and Burst compiler, a NativeArray<CellData> is highly recommended. CellData could be a struct containing flags or an enum for cell type (e.g., Wall, Passage, CentralArea_Passage, EdgeLoop_Passage). This approach is demonstrated for MazeFlags in a performant Unity maze generation tutorial.7 Simpler alternatives include CellType[,] or int[,] 2, but these may offer less direct Burst compatibility.
##### 2. Cell Object Representation (Optional Enhancement):
While a NativeArray of simple structs is good for raw generation, a more detailed MazeCell class or struct could be used for higher-level logic or easier debugging if needed, though this is generally less performant for the core generation loop. Such a structure might hold:
UnityEngine.Vector2Int coordinates
CellType type (enum: Wall, Passage, CentralOpen, EdgeLoop)
Boolean flags for wall presence on each side (hasNorthWall, etc.) or, conversely, passage openings, as used with MazeFlags.7
Flags for generation algorithm state (e.g., isVisited).
Region identifiers (e.g., isInCentralArea, edgeLoopID).
##### 3. Unity Tilemaps (for Visualization and Collision):
For visual representation and handling 2D collisions within Unity, the Tilemap system is a strong candidate.22
Separate Tilemap layers can be used for walls and floors.
Distinct Tile assets can visually differentiate standard passages, central open areas, edge loop paths, and dead ends, aiding both development and player understanding.
Tilemap colliders can be automatically generated based on the tile data, providing the physical boundaries for player and NPC movement.
The choice of NativeArray for the primary generation logic is driven by performance needs, particularly for runtime generation on iOS. Direct array manipulation is generally faster than continuous interaction with Unity GameObjects or complex Tilemap APIs during the computationally intensive phases of maze creation.7
Step-by-Step Generation Process (Hybrid Growing Tree & Post-Processing)
The recommended generation pipeline involves several sequential steps:

##### 1. Initialization:

Create the 30x30 NativeArray<CellData>, initializing all cells to an "unvisited" state with all potential passages closed (i.e., all walls intact between cells).

##### 2. Base Maze Generation (Growing Tree Algorithm):
Implement the core maze carving using the Growing Tree algorithm, structured as a Unity IJob and compiled with Burst for maximum efficiency.
Inputs: A random seed for reproducibility, and a pickLastProbability float (0.0 to 1.0) to control the cell selection strategy (high values lean towards DFS-like long corridors; lower values towards Prim's-like shorter branches).
Process:
Maintain a NativeList<int> (or equivalent NativeArray acting as a stack/list) for activeIndices representing cells currently being processed.
Select a random starting cell, mark it as a passage, and add its index to activeIndices.
Loop while activeIndices is not empty:
Choose an active cell from the list based on pickLastProbability (e.g., if random.NextFloat() < pickLastProbability, pick the last index, else pick a random index).
Identify its unvisited neighbors.
If no unvisited neighbors exist, remove the current cell from activeIndices.
If unvisited neighbors are found:
Select one randomly.
"Carve" a passage between the current cell and the chosen neighbor (e.g., update CellData flags for both cells to indicate an open passage between them).
Mark the neighbor as visited and add it to activeIndices.
Output: The NativeArray<CellData> now represents a basic maze structure with passages and walls.
##### 3. Carve Central Open Area:
Define the coordinates and dimensions of the central open area (e.g., a 7x7 square region at the maze's center).
Iterate through the cells within this defined central region in the NativeArray<CellData>.
Forcibly change their type to CellType.CentralArea_Passage.
Ensure all passages between adjacent cells within this central area are marked as open. This creates a large, contiguous open space.2
##### 4. Ensure Pathways to Central Hub (BFS Connectivity):
This step guarantees that all parts of the maze are connected to the central hub, as per 1 requirements.
Initialize a queue for BFS and a NativeHashSet<int> (or boolean NativeArray) for reachableCells (indexed by cell index).
Add all cell indices belonging to the CentralArea_Passage to the queue and mark them in reachableCells.
While the queue is not empty:
Dequeue a currentCellIndex.
For each valid neighbor of currentCellIndex:
If the neighbor is a passage type (any passage, including potential edge loops later) and is not yet in reachableCells:
Mark neighbor in reachableCells and enqueue it.
After the BFS completes:
Iterate through the entire grid. If a cell is a passage type but not marked in reachableCells, it belongs to an isolated section.
Correction Strategy: For each such isolated region, identify the cell in that region closest to any cell in reachableCells. Then, carve a path (change intervening CellType.Wall to CellType.Passage) to connect them. This ensures full connectivity to the central network, preferable to simply filling isolated areas with walls.2
##### 5. Incorporate Dead-Ends:
The Growing Tree algorithm, especially with a high pickLastProbability (making it behave like DFS), inherently produces dead ends.4 These should largely satisfy the requirement. The number of dead ends can be tuned by adjusting pickLastProbability.
##### 6. Create Looping Edge Paths with Central Connectivity:
a. Identify Edge Regions: Define a band of cells near the maze perimeter (e.g., cells in rows/columns 0-2 and 27-29).
b. Introduce Loops (Controlled Imperfection):
This step modifies the maze to be non-perfect by adding cycles in the edge regions.
Iterate through potential walls within the edge regions that separate two existing passage cells.
With a certain probability (addEdgeLoopProbability), convert such a wall cell into a passage cell (CellType.EdgeLoop_Passage), effectively creating a loop. The number of loops can be controlled by this probability or by a target loop count. This is a simplified adaptation of ideas from adding edges to a spanning tree or targeted passage opening.
c. Ensure Loop Connectivity to Central Area: This is a critical enforcement step.
For each distinct edge loop created:
Perform a BFS or A* search from an arbitrary cell within that loop, searching for any cell already marked in reachableCells (i.e., connected to the central hub).
If no path is found, the loop is isolated. Identify the cell in the loop closest to any cell in reachableCells and carve a new connecting path. This path should ideally be short and integrate naturally.
##### 7. Finalization and Visualization:
The NativeArray<CellData> now holds the complete logical maze structure.
Translate this data into visual GameObjects or Tilemap tiles.
Iterate through the grid:
If CellData indicates a wall between cells, instantiate a wall prefab or set a wall tile.
If CellData indicates a passage, instantiate a floor prefab or set a floor tile (potentially different tiles for CentralArea_Passage, EdgeLoop_Passage, etc.).
This step effectively "draws" the maze in the Unity scene.19
The order of these operations is paramount. For instance, carving the central room before ensuring general connectivity might simplify some logic, but doing it after base maze generation (as suggested) necessitates a robust connectivity pass. Edge loops should be formed after the central hub and its main arteries are established to ensure they can meaningfully connect to this core structure.
Techniques for Post-Generation Refinement to Guarantee All Specifications
To ensure all specifications are met robustly:
**Connectivity Analysis (BFS/DFS): ** As detailed, these are fundamental for verifying that the central area is the nexus of the maze and that edge loops are not isolated.
Rule-Based Checks: After each significant generation or modification step, programmatic checks should verify key constraints. For example: "Is the central area of the minimum specified openness?", "Does each identified edge loop possess at least one path leading to the reachableCells network?".
**Targeted Path Carving:** If a required connection is found to be missing (e.g., an edge loop is isolated), a specific path-carving routine can be invoked. This routine would use A* or a simple line-drawing algorithm to identify the cells between the two points to be connected and change their type from Wall to Passage.
Iterative Refinement Loop: For particularly complex interactions between features, it might be necessary to allow the generator to iterate. If an initial pass at creating edge loops results in poor connectivity, the system could discard those loops and try generating them with slightly different parameters or starting points, or apply a more aggressive connection-forcing step.
The principle here is that "guaranteed" features, as stipulated in 1, require more than just running an algorithm that tends to produce them. They necessitate explicit verification and, if needed, deterministic correction logic. This makes the overall generation process more robust and reliable in delivering mazes that meet all design criteria.
Hybrid Growing Tree Algorithm with Targeted Post-Processing emerges as the most promising approach.
**Base Maze Generation (Growing Tree Algorithm):**
The Growing Tree algorithm, provides a flexible foundation. Its key advantage lies in the ability to influence maze texture by altering the cell selection strategy from its active list (e.g., controlled by a pickLastProbability parameter). This can produce a suitable initial maze structure with a network of corridors and a natural occurrence of dead ends. Furthermore, this algorithm has been demonstrated to be implementable in Unity using NativeArray and Burst-compiled jobs, which is crucial for performance.
##### Targeted Post-Processing Steps (executed in sequence):
**Carve Central Open Area(s):** Following the base maze generation, a predefined central region (e.g., a 5x5 or 7x7 cell area within the 30x30 grid) will be explicitly cleared. All cells within this designated zone will be converted to open passages, and any internal walls will be removed. This directly addresses the requirement for central open areas 1 and is conceptually similar to methods described for carving out rooms.2
Ensure Connectivity to Central Area (BFS Flood Fill): A Breadth-First Search (BFS) will originate from all cells comprising the newly formed central open area. All cells reachable from this central hub will be marked. Any passage cells generated in the base maze that are not reached by this BFS are considered disconnected from the central network. These isolated passages will then be integrated, either by converting them to walls. ensuring a fully explorable maze, by carving a new path from an isolated section to the nearest cell already connected to the central hub. This guarantees that all pathways ultimately lead back to, or are part of, the network connected to the central area.
Manage Dead-End Paths: The Growing Tree algorithm, especially when configured to behave like a Depth-First Search (high pickLastProbability), naturally generates a good number of dead ends. The impact of the central carving and connectivity assurance steps on dead-end density should be assessed. If further dead ends are desired for complexity, a pass could identify locations where adding short dead-end paths is feasible, though the base algorithm is expected to be largely sufficient.
##### Create Edge Looping Paths with Central Connectivity: This is a multi-faceted step:
**Identify Edge Regions: ** Define a perimeter band of cells (e.g., cells within 2-3 units of the maze's outer boundary).
Introduce Loops: Within these edge regions, introduce loops. This can be achieved by adapting concepts from a modified Kruskal's algorithm (using a "cycle bias" to remove additional walls within this zone) or by targeted random wall removal (similar to the openDeadEndProbability or openArbitraryProbability parameters from the Growing Tree variations, but specifically applied to the edge regions).
Ensure Central Connectivity for Loops: This is critical. For each loop formed in the edge region, a pathfinding check (e.g., A* or BFS) must be performed from a cell within that loop back to any cell already established as part of the central area's connected network. If no such path exists, a new connecting path must be explicitly carved. This ensures the "connecting back to the central area" stipulation is met.




## Artwork Concepts
![Idle](PrototypeAssets/GhostiOSIcons/Ghost.AI-iOS-ClearDark-1024x1024@2x.png)
