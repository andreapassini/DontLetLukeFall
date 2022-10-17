# Game Design Document - Don't Let Luke Fall

![DontLetLukeFallConcept](https://user-images.githubusercontent.com/71270277/195996636-192c0d71-f488-4599-80b0-434929d5e3c7.png)

## Don't Let Luke Fall Team
- Andrea Passini
- Andrea Taroni
- Luca Finoia
- Carlo Ambrogi

## Design History
 - 03/10/2022 - Andrea Passini - Game Concept 
 - 10/10/2022 - Team Meeting


## Task Assignement 
- GDD => Andrea Passini
- Platform Logic => Luca
- Platform UI => Luca
- Action Logic => Andrea Taroni
- ACtion UI => Carlo


# Vision Statement

## Game Logline
Don't Let Luke Fall, place platforms in the right position and help him escape this nightmare

## Gameplay Synopsis
Don't Let Luke Fall is a 2D Platformer game within a dark environment and a tense mood.
Luke has been trapped, by the Darkness, into one of his own nightmares, the only way for him to wake up is to reach the end of the level where a light will awake him.
You will be able to forsee the next 3 moves of Luke, each will last for 5 seconds. Based on this information place one of the 3 platforms at your disposal in the right position.
After being positioned a new platform will appear in the slot with a slight dealy.


# Gameplay
## Overview


## Resources

- **Luke**
- **Actions**
- **Player Platforms**
- **Enironmental Platforms**
- **Enemies**
	- Spider 
	- Squid
	- Dog
	- "Gengar"
	- Crows
	- Undefined Mass (like **Inside**)
- **Harmful Obstacles**
	- Spines (Maybe also animated)
	- Mines/Bombs
	- Waterfall (Maybe with a unique interaction rules for stopping water)
	- Destroyable Platfroms 


## Core Mechanics

### Platform Positioning

The player will be able to drag a platform from the UI and drop it inside the game in real time.

![PlatformPositioning](https://user-images.githubusercontent.com/71270277/195996797-aa24964d-0fae-4eba-9165-95cf21fc0672.png)


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


### Character Actions

- Run Right
- Run Left
- Jump
- Crouch

#### Run Right
#### Run Left
#### **Jump**

Luke will execute a **jump** if he is standing on a plarform.

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





#### Crouch

### Platform Spawn


### Action Spawn
### Time Flow

In-Game time will slowd down, in respect to real-time, in 2 circumstances:
- Action Ending
- Action Starting

![TimeFlow](https://user-images.githubusercontent.com/71270277/196027441-b2986f34-ca2e-4f6f-ae69-4819dea4ae3b.png)






## Camera


![CameraPosition](https://user-images.githubusercontent.com/71270277/196216023-ede5f8e4-459e-43dc-bdb3-1ae66d221781.png)


## Interfaces



# Story
## Synopsis


# Media

## Artwork Refences
### Limbo


![Artwork Ref](https://user-images.githubusercontent.com/71270277/196114665-fcd48778-a045-47a1-ae97-e81dfd8d7090.png)


![ArtworkRefLimbo](https://user-images.githubusercontent.com/71270277/196114651-1f68731f-17dc-4469-9dc6-c961b4a458d4.png)


### Inside


![ArtworkRefInside](https://user-images.githubusercontent.com/71270277/196114641-08b36b65-2708-441f-9c70-c38c68d00649.png)


![ArtworkRefInsideHumanBall](https://user-images.githubusercontent.com/71270277/196114630-a2dbb93a-1981-4ff5-8885-d15c155c9051.png)





## Luke
## Enemies
## Player Platforms
## Environmental Platforms
## Harmful Obstacles
## Sound
### Background Music
### Sfx