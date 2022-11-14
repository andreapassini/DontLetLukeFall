# Questions

## Prototype Problem
### Luke
Is not falling, check friction with Rigid body and Physic Material

### Platforms
UI Sprites does not match Platform Size, check factor size in relation with camera size and resolution

### Action UI
Not loading sprite form Action Sequence

## Platform Action

When Luke will land on a special platform, its effect will be triggered causing Luke to perform the action specified by the platform.

**How long the action will last?**

1. **De-Synch**
	The platform action will fire and affect the behavior of Luke in parallel with Luke's actions but its timer will **not** be **synched** with Luke's Actions Timer
2. **Substitute and restart**
	The platform action will substitute the current Luke's Action in the UI and its timer will reset
3. **Synch**
	The platform action will fire and affect the behavior of Luke in parallel with Luke's actions but its timer will **synched** with Luke's Actions Timer
4. Shortcut (De-Sync with "no-time related actions")

**Where it will be shown on the UI**
- On top of the Head of Luke, with a small sign (Circle with inside the action, a colored bar around the circle dictating the remaining time)
- On the Action UI substituting the current Action

Vertical actions do not affect horizontal actions
Horizontal actions do not affect vertical actions

**Platform Stopping**
What height will Luke be able to overcome?

**Crouch and Jump**
What happen if Luke is crouched and a Jump action is fired?

**Platform**
How to fuse color and outline of the platforms

<br>
## Prof Suggestions
### Luke's Action
Action duration is no more related to time, but the space traveled by Luke. 
1 Action for Each platform (considering platform of fixed length)


### Tile Size
Grid = 1 Unit
=> Player H 2, L 1
=> Platform Standard H 1, L 3
Camera Size = 10
Res Single Sprite= 256x256
# TO DO
## Game Manager
To keep track of the game state,
- Load Scene
- Open Menus

## Increase plat length to 5
cause Speed to slow
Increase platform length to 5
Increase speed of Luke to 1.25

## Spawn Action Sprite
Spawn action sprite at the feet (to the left) of Luke when a new action is triggered.

## Action UI Sprites
Action UI Sprites need to be 1x1 and not streched

## Full Player Action before starting the level

# Deadlines

![Scadenze](https://user-images.githubusercontent.com/71270277/198230350-f5aa6e99-eb36-4697-acf2-6da03e2db9c1.png)


# Game Design Document - Don't Let Luke Fall

## Concept

![DontLetLukeFallConcept](https://user-images.githubusercontent.com/71270277/195996636-192c0d71-f488-4599-80b0-434929d5e3c7.png)

## Team Members
- Andrea Passini
- Andrea Taroni
- Luca Finoia
- Carlo Ambrogi

### External Collaborators

#### Artists
- Luca Staffoni
- Irene Corioni

#### Sound Designer
- Matteo Bernardini

#### Artists
#### Sound Designers

## Design History
 - 03/10/2022 - Andrea Passini - Game Concept 
 - 10/10/2022 - Team Meeting


## Task Assignment 
- GDD => Andrea Passini
- Platform Logic => Luca
- Platform UI => Luca
- Action Logic => Andrea Taroni
- Action UI => Carlo

**TODO**:
- Platform UI, Reposition in UI Platform => Luca
- Platform UI, Start timer in End Drag => Luca
- Platform UI, Calculate Sprite-Platfrom ratio => Luca
- Integration
- Background Music => Carlo
- SFX integration => Carlo
- Character Controller

# Vision Statement

## Game Logline
Don't Let Luke Fall, place platforms in the right position and help him escape this nightmare

## Gameplay Synopsis
Don't Let Luke Fall is a 2D Platformer game within a dark environment and a tense mood.
Luke has been trapped, by the Darkness, into one of his own nightmares, the only way for him to wake up is to reach the end of the level where a light will awake him.
You will be able to forsee the next 3 moves of Luke, each will last for 5 seconds. Based on this information place one of the 3 platforms at your disposal in the right position.
After being positioned a new platform will appear in the slot with a slight dealy.
<br>
# Gameplay
## Overview

At the start of the level Luke will be walking right.
His goal is to reach the end of the level, where a bright light will awake him.

You will be able to place platforms in real time, preventing Luke to fall and allowing him to reach the end of the level.

You will be able to foresee the next 3 actions that Luke will perform.

<br>
## Resources

### Health of Luke

### Luke 

### Luke's Actions
Luke's Actions:
- **Instantaneous**
	- Jump
	-  Walk Left
	- Walk Right

- **Time-Limited**
	- Run Left
	- Run Right
	- Stop Moving
	- Crouch

### Player Platforms
- #### *Main Platforms*
	![[Platforms.png]]
	- Short Horizontal Platform
	- Long Horizontal Platform
	- Ramp
	- Wall
	- L - Shaped Platform
	- T - Shaped Platform
- #### *Special Platforms*
	Shaped as Main Platform but when Luke will pass over this platforms they will trigger that specific action.
	**Platform Actions**:
	![[PlatformActions.png]]
	("shortcut" [s])
	- [s]Walk Left
	- [s]Walk Right
	- Run Left
	- Run Right
	- Stop Moving
	- [s]Jump
	- Crouch
- 
### **Environmental Platforms**
- Horizontal Platforms
- Ramps
- Walls
- Destroyable Platfroms 

### **Harmful Obstacles**
- Dark Flood
- Spines
- Mines/Bombs
- Waterfalls

### Enemies
- #### Spider
- #### Squid
- #### Dog
- #### Gengar
- #### Crow
- #### Deer
- #### Undefined Mass


<br>
## Core Mechanics
### Platform Positioning

The player will be able to drag a platform from the UI and drop it inside the game in real time.

![PlatformPositioning](https://user-images.githubusercontent.com/71270277/195996797-aa24964d-0fae-4eba-9165-95cf21fc0672.png)

While moving:
- **Platform's border** will become **dotted**
- A **grid** will become visible helping the player positioning the platform

![PLatformPos](https://user-images.githubusercontent.com/71270277/196242728-a760fb8f-19e9-49c9-b884-de1c4984ea95.png)


#### **Overlapping**

##### **Character**

Player will not be able to place platform over the character outline. In case the player will drop a platform on the siluette of the character this platform will endup at the feet of Luke.


![OverlappingCharacter](https://user-images.githubusercontent.com/71270277/195995184-d583f2f0-a416-4294-8b86-85a1d1355461.png)


![OverlappingCharacter-PlatformSnapping](https://user-images.githubusercontent.com/71270277/195995428-ce0da647-1e02-4a5d-9562-06ae12c64bb7.png)


##### **Enemies**

Player will not be able to place platform over enemy outline.


##### **Player Platform**

Player will be able to place a platform over one or more player platforms causing a **Platform Combination**.


![PlatfromCombination](https://user-images.githubusercontent.com/71270277/195996132-1011af5f-effb-4ee1-84ba-ca5c7c9e15a9.png)


##### **Environmental Platform**

Player will be able to place a platform over one or more Environmental Platform


![OverlappingEnv](https://user-images.githubusercontent.com/71270277/195995800-dd9b43f0-089e-4ce8-8290-724690af79a2.png)

#### Platform Combination

When the player will drop a platform (**trigger**) over a platform (or multiple platforms) he previously placed (**base**), the platforms will combine toghter resulting in a platfrom characterized by:
 - **Outline** = **merge** of the outline of **trigger** and **base** 
 -  **Color** = **trigger** 
 - **Effect** = **trigger**

![PlatfromCombination](https://user-images.githubusercontent.com/71270277/195996132-1011af5f-effb-4ee1-84ba-ca5c7c9e15a9.png)


### Actions

Actions determine the behavior of Luke.

Actions can be divided into 2 groups, based on the duration of their effect in time:

- **Instantaneous**
	Instantaneous actions's effect is not limited by time, it will affect the behavior of Luke
	- Jump
	- Dash

- **Time-Limited**
	The effect of Time-Limited actions is limited in time.
	They will affect instantaneously the behavior of Luke, their effect will last for 5 seconds, until a new Luke's Action is fired.
	- Run Right
	- Run Left
	- Crouch
	- Stop

- **Continuous**
	The effect of these actions will keep affecting the behavior of Luke until a new Continuous Action is fired.
	- Walk Right
	- Walk Left

To prevent any misconceptions: 
**All Actions will affect instantaneously the behavior of Luke**
The subject of these division is the *effect duration* in time. 

Actions can also be divided into 2 groups, based on with axis they affect:

- **Vertical Actions**
	Vertical Actions will **affect** the **vertical axis** of Luke, without affecting the horizontal axis.

- **Horizontal Actions**
	Horizontal Actions will affect the horizontal axis of Luke, without affecting the vertical axis.
<br>
**Actions List:**

- #### Walk Right
	**Continuous Action**
	Luke will **walk right** until another action will affect Luke's Horizontal axis

- #### Walk Left
	**Continuous Action**
	Luke will **walk left** until another action will affect Luke's Horizontal axis

- #### Run Right
	**Time-Limited**
	Luke will **run right**.
	When this action is over Luke will **walk right**

- #### Run Left
	**Time-Limited**
	Luke will **run left**.
	When this action is over Luke will **walk left**

- #### **Jump**
	**Instantaneous**
	Luke will execute a **jump** if he is standing on a platform.
	
	(https://www.youtube.com/watch?v=3sWTzMsmdx8)
	
	![Jump Function](https://user-images.githubusercontent.com/71270277/196027686-55adf096-b603-4296-ba9b-6c42fd2c8502.png)
	
	##### **Cayote Time**
	
	Luke will execute jump even if he left the platfrom where he was standing few istants before.
	
	![JumpFunctionExt](https://user-images.githubusercontent.com/71270277/196027739-b21b2288-e77e-4c1f-9c37-4dddb9490cf8.png)
	
	##### **Jump Buffer**
	
	When the jump action start and Luke is still in the air, if the charter land on a platfrom few istants after the action start Luke will execute a jump anyway.
	
	##### **Edge Detection**
	
	When Luke is jumping and hitting a platform on top, if the platform is only slightly over him, he will move slightly in the opposite direction of the platform allowing him to overcome the obstacle
	
	
	![Edge Jumping](https://user-images.githubusercontent.com/71270277/196028791-5c0069b7-c5be-49d6-a8b5-b3d7327616ee.png)
	
	
	##### **Ledge Catching**
	
	When Luke is jumping and falling short to reach a platfrom for few inches, he will snap on top of the platforming.
	
	![Ledge Catch](https://user-images.githubusercontent.com/71270277/196028788-ab0361c4-d2a6-4787-81ae-114d93b28d13.png)

- #### Crouch
	**Time-Limited**
	Luke will crouch
	- **reducing** his **height** 
	- **slowing** him down by a factor of **0.5**.
	This will allow Luke to pass through narrow passages/holes
	
	When **Crouch** effect **ends** and Luke is still in a **narrow passage/hole**, Luke will keep being crouch until the end of the **passage/hole**, other actions will fire but if they are vertical, they will not affect Luke's behavior
	
	When **Crouch** is **active** and a **Jump action** is fired, Luke will:
	- Crouch-Jumping?
	- Standing and then Jumping?

- #### Stop
	**Time-Limited**
	Luke will stop moving until a new action will make him.

### Luke's Actions

Luke's Actions are actions organized in a list, that ca be observed in the UI component at the top of the screen.
They represent the fixed future behavior of Luke.

### Platform Actions

Platform actions are Actions that will trigger when Luke will pass over a special platform.

**How Luke's Actions and Platform Actions will affect each other?**

### Platform Spawn


### Action Spawn

For each level there will be a fixed sequence of actions.
At the start of the game the first action will be triggered.
Every action will last for 5 seconds, then the next action in line will be triggered.

### Time Flow

In-Game time will slowd down, in respect to real-time, in 2 circumstances:
- Action Ending
- Action Starting

![TimeFlow](https://user-images.githubusercontent.com/71270277/196027441-b2986f34-ca2e-4f6f-ae69-4819dea4ae3b.png)






<br>
## Camera

Draggin PLatfrom will stop camera movement

#### Run Right

![CameraRunRight](https://user-images.githubusercontent.com/71270277/196243609-9d573830-c69e-4cb3-aef8-c5cbf0376b64.png)

#### Run Left

![CameraRunLeft](https://user-images.githubusercontent.com/71270277/196243602-c5c9e492-7523-43ad-9561-a2fe7302313e.png)

#### Jump
#### Landing

Shake


<br>
<br>
## Game Specs

### Res
1920x1080
16:9

### Grid
grid 0.5
	Pix per Unit 64
	Slice 32

### Input

 - **PC:** 
	 - **Mouse and Keyboard**
 - **Mobile:**
	 - **Touchscreen**

### Art Free

https://opengameart.org/

# Interfaces


![UI](https://user-images.githubusercontent.com/71270277/196240380-787152ab-e3e2-49a6-a55f-5653f49a73dd.png)

![StartingMenu](https://user-images.githubusercontent.com/71270277/198277355-21028ab3-23ed-458d-8722-682783c6031b.png)
![Options](https://user-images.githubusercontent.com/71270277/198280012-097387dc-49cc-402c-aa96-9cfd48913a1c.png)

![LevelSelection](https://user-images.githubusercontent.com/71270277/198277340-3c02f3c5-4716-4092-b527-173e982be964.png)


<br>
# Characters
## Luke

**Luke is a young boy trapped in one of his nightmare.**

He will appear:
-   Scared
-   Bewildered/Disoriented
-   Helpless

He will be able to perform this actions:

- Walk Left
- Walk Right
- Run Letf
- Run Right
- Stop Moving
- Jump
- Crouch

When the actions spawner will trigger the action or when Luke will land on Special Platform.

## Enemies
#### Spider
Spiders will have 2 behaviors:
- Attack
	Spider will hang from the top of the level.
	Its body will remain static but its legs will moves trying to hit Luke.
- Slow
	Spider will hang from the top of the level.
	It will shoot a small spiderweb that will slow Luke for 2 seconds 

#### Squid

Squids will float mid-air, following a short vertical path, moving their tentacles to hit Luke.

#### Dog

Dogs will hunt Luke, following him.
Dogs cannot jump.

#### "Gengar"
#### Crow

Will fly fast, following a specific path, looking to grab Luke.

#### Undefined Mass
Mass of multiple bodies, they can be both humans or monsters.
<br>
# Story
## Synopsis

Luke is young boy how got trapped into one of his own nightmare.
Inside this scary and misterious world he will encounter his biggest fears and phobias.

<br>
# Media

## Artwork Refences

### Limbo


![Artwork Ref](https://user-images.githubusercontent.com/71270277/196114665-fcd48778-a045-47a1-ae97-e81dfd8d7090.png)


![ArtworkRefLimbo](https://user-images.githubusercontent.com/71270277/196114651-1f68731f-17dc-4469-9dc6-c961b4a458d4.png)


### Inside


![ArtworkRefInside](https://user-images.githubusercontent.com/71270277/196114641-08b36b65-2708-441f-9c70-c38c68d00649.png)


![ArtworkRefInsideHumanBall](https://user-images.githubusercontent.com/71270277/196114630-a2dbb93a-1981-4ff5-8885-d15c155c9051.png)





## Luke

![image](https://user-images.githubusercontent.com/71270277/196691871-7e19f7d8-af50-4149-ab42-6b87140d3ea9.png)


## Enemies

![abf0f62eea0a2211739ee805474359c9](https://user-images.githubusercontent.com/71270277/196252209-7bdb503d-ab9b-43ec-999b-7a732c233f22.jpg)

#### Spider


![screen-0](https://user-images.githubusercontent.com/71270277/196250343-6478a88d-5e4e-4a63-bd73-6cb3dd007732.jpg)


![shelob_1](https://user-images.githubusercontent.com/71270277/196250578-9ad1adb7-ae79-4b6f-9d0a-f716e016bd43.jpg)


#### Squid

![chulhu-death-may-die-horror-board-game-box-artwork](https://user-images.githubusercontent.com/71270277/196246840-34984842-f394-491a-98f4-9c9c9e7c4169.jpg)


![orfoia6fvsw21](https://user-images.githubusercontent.com/71270277/196246851-a6263089-0f1d-4169-9bba-109d560ec47f.jpg)

![istockphoto-1169962994-170667a](https://user-images.githubusercontent.com/71270277/196252192-0d33d81e-8f36-4db9-8a0d-be110632c9d5.jpg)

#### Dog

![Monster-in-the-dark](https://user-images.githubusercontent.com/71270277/196252203-71b09a10-3398-44e9-a671-a25c4cb75e58.jpg)

![693c03a94cfa1c4b8c2d12e01640b092](https://user-images.githubusercontent.com/71270277/196712794-1bb88868-209c-4496-afed-44b2b800847a.jpg)
![katlego-motaung-hound01](https://user-images.githubusercontent.com/71270277/196712795-8d4eee18-9ec2-48a7-a68d-9f75662370d0.jpg)
![InsidePig2 0](https://user-images.githubusercontent.com/71270277/196712809-64e05f7f-613f-43d1-b8e1-eab390619f52.jpg)

#### Gengar"

![images](https://user-images.githubusercontent.com/71270277/196252656-379e2a8e-089a-4af0-880c-4b56f4a53f8f.jpg)
![dark-monsters](https://user-images.githubusercontent.com/71270277/196252173-fc5ec369-44f8-4069-b85c-b2e3e807a41e.gif)
![HD-wallpaper-dragon-dark-creepers-dragon-artist-artwork-digital-art-deviantart](https://user-images.githubusercontent.com/71270277/196252599-bd06cfd9-818d-4e37-9222-af6648b8a3cd.jpg)
![wp3302265](https://user-images.githubusercontent.com/71270277/196252568-6d3b09af-a9e0-45d4-9ae7-03cb56483b5a.jpg)

#### Crow

![cbaba606796e7c7bec6b6c51beb748a7](https://user-images.githubusercontent.com/71270277/196706504-9ff7ab84-0f3c-4dd3-ba3d-9179fc74a4c2.jpg)
![dcm62qc-c65014bc-e98b-4f04-b806-66a57a69bd27](https://user-images.githubusercontent.com/71270277/196706680-44817fbd-77bb-4937-80be-cababf6a23cb.jpg)

#### Undefined Mass

Mass of multiple bodies, they can be both humans or monsters

![images](https://user-images.githubusercontent.com/71270277/196251313-25297a39-449e-4a36-b92d-a98d8e3e5b36.jpg)


![images](https://user-images.githubusercontent.com/71270277/196251339-25e6e462-e2a2-40c6-badf-e338a00135d8.jpg)

## Player Platforms
## Environmental Platforms
## Harmful Obstacles
## Sound
### Background Music
### Sfx
#### Platform Positioning
#### Action Triggering
#### Luke Death

### Environmental Effects
