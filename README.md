# ZooQuest

A 2D top-down zoo exploration game built with Unity. Players navigate through themed biome zones, observing various animals and completing quests.

## Overview

ZooQuest is set in a zoo with a central Lobby hub connecting three distinct biome zones. Each zone is populated with unique animated animals, ambient sounds, and environmental assets.

**Game Objective:** Explore the zoo, interact with animals, and follow quests from the Game Master NPC.

## Biomes

| Zone | Animals |
|------|---------|
| **Lobby** | NPCs (Game Master, Visitors) |
| **Savanna** | Elephant, Giraffe, Lion, Zebra |
| **Arctic** | Bear, Penguin, Husky |
| **Forest** | Blue Bird, Red Bird, Komodo Dragon, Monkey |

## Features

- Top-down 2D movement with directional animations (front, back, side)
- Smooth camera follow system
- Portal-based zone transitions with spawn-point management
- Unique BGM and SFX per zone (animal sounds, ambient audio)
- Character footstep sounds
- Random animation offsets for NPC idle variety

## Project Structure

```
Assets/
  Animations/     -- Animation controllers and clips per zone
  Maps/           -- Full-map sprite images
  NPC/            -- Character and animal sprite sheets
  Objects/        -- Environmental objects (trees, fences, etc.)
  Scenes/         -- Main Menu, Lobby, Savanna, Iced, Forest
  Scripts/        -- C# game scripts
  Sounds/
    BGM/          -- Background music per zone
    SFX/          -- Sound effects (animal, character)
```

## Technologies

- **Engine:** Unity 2019.4.41f2 (LTS)
- **Template:** Unity 2D
- **Language:** C#
- **Key Packages:** 2D Animation, 2D Pixel Perfect, 2D Tilemap, TextMeshPro

## Scenes

| Index | Scene | Description |
|-------|-------|-------------|
| 0 | Main Menu | Title screen (Play / Quit) |
| 1 | Lobby | Central hub with portals to biome zones |
| 2 | Savanna | Savanna biome |
| 3 | Iced | Arctic biome |
| 4 | Forest | Forest biome |

## How to Run

1. Open the project in Unity 2019.4 LTS or later
2. Open `Assets/Scenes/Main Menu.unity`
3. Press Play

## License

This project is for educational purposes.
