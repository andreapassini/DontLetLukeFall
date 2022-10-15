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

## Core Mechanics

### Platform Positioning

The player will be able to drag a platform from the UI and drop it inside the game in real time.

![PlatformPositioning](https://user-images.githubusercontent.com/71270277/195996797-aa24964d-0fae-4eba-9165-95cf21fc0672.png)


#### Overlapping
- **Character**

Player will not be able to place platform over the character outline. In case the player will drop a platform on the siluette of the character this platform will endup at the feet of Luke.


![OverlappingCharacter](https://user-images.githubusercontent.com/71270277/195995184-d583f2f0-a416-4294-8b86-85a1d1355461.png)


![OverlappingCharacter-PlatformSnapping](https://user-images.githubusercontent.com/71270277/195995428-ce0da647-1e02-4a5d-9562-06ae12c64bb7.png)

- **Player Platform**

Player will be able to place a platform over one or more player platforms causing a **Platform Combination**.


![PlatfromCombination](https://user-images.githubusercontent.com/71270277/195996132-1011af5f-effb-4ee1-84ba-ca5c7c9e15a9.png)


- **Environmental Platform**

Player will be able to place a platform over one or more Environmental Platform


![OverlappingEnv](https://user-images.githubusercontent.com/71270277/195995800-dd9b43f0-089e-4ce8-8290-724690af79a2.png)

#### Platform Combination
When the player will drop a platform (**trigger**) over a platform (or multiple platforms) he previously placed (**base**), the platforms will combine toghter resulting in a platfrom characterized by:
 - **Outline** = **merge** of the outline of **trigger** and **base** 
 -  **Color** = **trigger** 
 - **Effect** = **trigger**

![PlatfromCombination](https://user-images.githubusercontent.com/71270277/195996132-1011af5f-effb-4ee1-84ba-ca5c7c9e15a9.png)


# Story
## Synopsis
