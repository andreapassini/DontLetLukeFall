# Game Design Document - Don't Let Luke Fall

## Don't Let Luke Fall Team
- Andrea Passini
- Andrea Taroni
- Luca Finoia
- Carlo Ambrogi

## Design History
03/10/2022 - Andrea Passini - Game Concept 
10/10/2022 - Team Meeting

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
Don't Let Luke Fall is a 2D Platformer game.
Luke has been trapped, by the Darkness, into one of his own nightmares, the only way for him to wake up is to reach the end of the level where a light will awake him.
You will be able to forsee the next 3 moves of Luke, each will last for 5 seconds. Based on this information place one of the 3 platforms at your disposal in the right position.
After being positioned a new platform will appear in the slot with a slight dealy.

# Gameplay

## Core Mechanics

### Platform Positioning

#### Overlapping
- **Character**
Player will not be able to place platform over the character outline. In case the player will drop a platform on the siluette of the character this platform will endup at the feet of Luke.
- **Player Platform**
Player will be able to place a platform over one or more player platforms causing a **Platform Combination**.
- **Environmental Platform**
Player will be able to place a platform over one or more Environmental Platform

#### Platform Combination
When the player will drop a platform (**trigger**) over a platform (or multiple platforms) he previously placed (**base**), the platforms will combine toghter resulting in a platfrom characterized by:
 - **Outline** = **merge** of the outline of **trigger** and **base** 
 -  **Color** = **trigger** 
 - **Effect** = **trigger**


# Story
## Synopsis
