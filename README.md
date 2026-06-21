# ZooQuest

A 2D top-down zoo exploration game built with Unity. Players navigate through themed biome zones, observing various animals and completing quests.

## Overview

ZooQuest is set in a zoo with a central Lobby hub connecting three distinct biome zones. Each zone is populated with unique animated animals, ambient sounds, and environmental assets.

**Game Objective:** Explore each biome, read the animal info boards, then answer the Game Master's quiz. Score 100% to unlock the next biome.

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
- **Quiz system** — the Game Master NPC asks a per-level set of multiple-choice questions (A/B/C)
- **Level progression & locked portals** — biomes unlock sequentially; a portal stays locked until the previous level's quiz is cleared
- **Pause menu** — press `Esc` to pause/resume (freezes time, mutes audio) and quit to the Main Menu
- Animal info boards — interact (`E`) to view an animal's name, description, and image
- Unique BGM and SFX per zone (animal sounds, ambient audio)
- Character footstep sounds
- Random animation offsets for NPC idle variety

## Gameplay & Progression

- Levels are ordered: **Savanna (1) → Iced (2) → Forest (3)**. Savanna is always unlocked.
- Walk up to the **Game Master** in the Lobby and press `E` to start the quiz for the current active level.
- Answering **every** question correctly (100%) completes the level and unlocks the next biome's portal.
- Progress is tracked **in memory** (no save file) and resets to Level 1 whenever you press **Mulai Game** from the Main Menu.
- Controls: `WASD`/arrows to move, `E` to interact, `Esc` to pause, `R` to reset progress (debug cheat).

## Quiz Data

Quizzes are authored as `AnimalQuizData` ScriptableObjects (`Assets/AnimalQuiz_*.asset`), one per level. Create new ones via **Assets → Create → ZooQuest → Animal Quiz**. Each asset holds a `levelID`, `levelName`, and a list of questions (question text + answers A/B/C + the correct answer).

## Project Structure

```
Assets/
  AnimalQuiz_*.asset  -- Per-level quiz data (Savanna, Iced, Forest)
  Animations/         -- Animation controllers and clips per zone
  Maps/               -- Full-map sprite images
  NPC/                -- Character and animal sprite sheets
  Objects/            -- Environmental objects (trees, fences, etc.)
  Scenes/             -- Main Menu, Lobby, Savanna, Iced, Forest
  Scripts/            -- C# game scripts (see below)
  UI/                 -- UI images (quiz panel, pause panel)
  Sounds/
    BGM/              -- Background music per zone
    SFX/              -- Sound effects (animal, character)
```

### Key Scripts

| Script | Responsibility |
|--------|----------------|
| `GameProgressManager` | Singleton tracking the active level and which biomes are unlocked; resets on new game |
| `AnimalQuizData` | ScriptableObject defining a level's quiz questions |
| `QuizManager` | Drives the quiz UI panel and reports answers back to the `Interactable` |
| `Interactable` | Shows animal info boards or runs the Game Master quiz flow and scoring |
| `ScenePortal` | Zone transition with spawn handling and lock/unlock state |
| `PauseManager` | Esc-driven pause/resume, time freeze, audio mute, quit to menu |
| `MainMenu` | Play (resets progress) / Quit |

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
