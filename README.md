# Title: **Polarize**

## Description

**Polarize** is a retro-inspired action-platformer built in Unity for 2026, strictly adhering to the technical constraints of the **NES**.  You play as A.R.I., a robot capable of manipulating magnetic polarity to traverse a hazardous, interconnected tower.

The core gameplay centers on a Polarity Swap mechanic: players use modern controller bumbers (LB/RB) or keyboard keys (Q/E) to instantly shift between **Red (Positive)** and **Blue (Negative)** states.  This changes affects every interaction in the world.

* **Combat:** Basic shots are unaffected by the polarity change and deal damage to enemies of both positive and negative types.  The **Polarized Charge Shot** adds physics-based depth, pulling toward opposite-colored enemies for high damage or physically pushing same-colored enemies away.

* **Traversal:** Utilizing a 4 way **Tractor Beam**, players can grapple toward opposite-polarity surfaces or "rocket jump" by repelling off surfaces of the same polarity.  This tractor beam also works on enemies, pulling opposite polarized enemies toward the player and pushing same polarized enemies away.

* **Authenticity:** Despite being built in a modern engine, the game enforces 8-bit hardware limitations, including a strict **NES color palette, a 320x180 resolution, and only two 128x128 texture maps** for all sprites and tiles.  Sounds and music will also be made using augmented chiptune instruments and noise generators.

The result is a tight, mechanics-driven experience that blends the precision of MegaMan with the environmental puzzle-solving of Metroid.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Requirements

**Unity Hub**
**Unity Editor Version:** '6.4 (6000.4.0b2)'
**Target Platform:** 'Windows' (Requires the Windows Build Support module installed via Unity Hub)

### Installation

1. **Clone the repository:** 
'''bash
git clone https://github.com/3Dever96/ProjectPolarize.git
'''

2. **Open in Unity**
* Open ** Unity Hub**.
* Click **"Add Project from Disk"**.
* Navigate to the cloned directory and select the root folder.
* Ensure the correct Unity Editor Version ('6.4 (6000.4.0b2)') is selected in the Hub and open the project.

3. **Run the Project:**
* Once the Editor loads, navigate to the primary scene file (usually in 'Assets/Scenes/').
* Press the **Play** button in the Unity Editor to begin.


## Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create.  Any contributions you make are **greatly appreciated**.

*   Please adhere to the **https://leotgo.github.io/unity-coding-standards/** before submitting code.

1. Fork the Project
2. Create your Feature Branch ('git checkout -b feature/AmazingFeature')
3. Commit your Changes ('git commit -m 'Add some AmazaingFeature'')
4. Push to the Branch ('git push origin feature/AmazingFeature')
5. Open a Pull Request

## License

This project is licensed under the MIT License- see the LICENSE file for details.

## Acknowledgments
