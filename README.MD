# Title here
Description

## 1. Folder Structure
```
Assets/
├── _Project/                  # Main project folder (underscore keeps it at top)
│   ├── Scripts/
│   │   ├── Core/              # Game managers, singletons
│   │   ├── Player/            # VR player controller, locomotion
│   │   ├── Interaction/       # XR grab, physics interactables
│   │   ├── UI/                # UI/UX scripts
│   │   └── Utilities/         # Helpers, extensions
│   │
│   ├── Art/
│   │   ├── Models/
│   │   │   ├── Characters/
│   │   │   ├── Environment/
│   │   │   └── Props/
│   │   ├── Textures/
│   │   │   ├── Characters/
│   │   │   ├── Environment/
│   │   │   └── Props/
│   │   ├── Materials/
│   │   ├── Animations/
│   │   └── VFX/
│   │
│   ├── Audio/
│   │   ├── Music/
│   │   ├── SFX/
│   │   └── Ambience/
│   │
│   ├── Scenes/
│   │   ├── _Boot.unity        # First scene (loads managers)
│   │   ├── MainMenu.unity
│   │   └── Levels/
│   │
│   ├── Prefabs/
│   │   ├── Player/
│   │   ├── Enemies/
│   │   ├── Environment/
│   │   └── UI/
│   │
│   ├── Settings/              # Input actions, XR settings, render pipeline
│   └── Resources/             # Only what needs Runtime.Load()
│
├── ThirdParty/                # Asset store / imported packages (DON'T modify)
│   ├── XR Interaction Toolkit/
│   └── [PackageName]/
│
└── Plugins/                   # Native plugins, SDKs (Meta SDK, SteamVR, etc.)
```

---

## 2. Branching Strategy

Use **Git Flow** adapted for a small team:
```
main          ← stable, shippable builds only
develop       ← integration branch, always working
├── player-locomotion      (programmer)
├── grab-interaction       (programmer)
├── environment-kitchen        (artist)
├── character-npc-01           (artist)
└── scene-load-crash        (tech lead)
```

**Commits naming convention:**
| Role | Prefix | Example |
|------|--------|---------|
| Tech Lead | `feat:`, `fix:` | `fix: xr rig null ref` |
| Programmers | `feat:` | `feat: ui menu system` |
| 3D Artists | `art:` | `art: prop sword v2` |

**Rules:**
- No one pushes directly to `main` or `develop`
- All merges via **Pull Requests**
- PR requires at least **1 reviewer** (Tech Lead reviews all code PRs)

---

## 3. Scene Merge Conflicts — The Biggest Pain Point

Unity scenes are YAML and will conflict when multiple people edit the same scene.

**Strategy:**
- One person "owns" each scene at a time
- Use **prefabs aggressively** — artists build in prefab files, not directly in scenes
- Use **Prefab Mode** (double-click prefab) for isolated editing
- Assign scene ownership in your sprint board

---

## 4. First time setup

### 1. Clone the repo
```git clone https://github.com/your-org/your-project.git```

### 2. Install Git LFS (if not installed)
```git lfs install```

### 3. Pull LFS files
```git lfs pull```

### 4. Open project in Unity Hub
 - Add project → browse to cloned folder
 - Select Unity 6000.3.10f1
 - Open — Unity will reimport everything (normal, ~5 min)

---

## Adding VR to Your Unity 6 Project

Unity's modern VR solution is the **XR Interaction Toolkit (XRI)**. Forget SteamVR or Oculus SDK directly — XRI abstracts over all headsets (Quest, Valve Index, Pico, etc.).

### Step 1 — Install Required Packages

Go to **Window → Package Manager**:

1. Click the **+** → *Add package by name*
2. Add these one by one:

| Package Name | What it does |
|---|---|
| `com.unity.xr.interaction.toolkit` | Core VR interactions (grab, teleport, UI) |
| `com.unity.xr.management` | Manages which XR platform loads |
| `com.unity.xr.openxr` | The universal VR backend (works on Quest, PC VR, etc.) |
| `com.unity.inputsystem` | New Input System (required by XRI) |

After installing XRI, it'll prompt you to also import the **Starter Assets** — say **yes**. This gives you ready-made prefabs for hands, controllers, and locomotion.

---

### Step 2 — Configure XR Management

1. **Edit → Project Settings → XR Plug-in Management**
2. On the **PC** tab: enable **OpenXR**
3. On the **Android** tab (if targeting Quest): enable **OpenXR**
4. Under **OpenXR → Features**, enable:
   - Meta Quest Support (if Quest)
   - Hand Tracking (if needed)
   - Controller Profile for your target headset

---

### Step 3 — Set Up the VR Rig in Your Scene

Delete the default **Main Camera** from your scene first.

Then in your scene hierarchy, right-click → **XR → XR Origin (VR)**

This creates:
```
XR Origin
├── Camera Offset
│   ├── Main Camera        ← your VR headset view
│   ├── Left Controller    ← left hand / controller
│   └── Right Controller   ← right hand / controller

---