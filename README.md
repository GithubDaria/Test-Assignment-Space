Project: [Space Scavanger]

Unique Feature:
----------------
A **custom AudioManager** handles sound playback for item collection. Collectible calls the AudioManager when collected, triggering a short sound effect.

Implementation:
----------------
- **GameManager**: Handles game loop, timer, spawning logic, score management, and events.
- **PlayerMovement**: Rigidbody-based controller with left/right movement and jump, using the Unity Input System.
- **ItemBehavior**: Controls rotation, floating animation, lifetime, and triggers AudioManager on collection.
- **GameUIManager**: Updates the score and timer UI, manages the main gameplay screen and end game screen, and handles replay, main menu, and exit buttons.
- **AudioManager**: Singleton pattern managing collectible sounds.
- **AudioManager**: Singleton pattern managing collectible sounds.
- **MainMenuUIManager**: Handles main menu fade-in and scene transitions.

Unity Components Used:
----------------------
- Rigidbody & Collider (player physics & item triggers)
- Canvas & CanvasGroup (UI fade-in)
- TextMeshProUGUI (score, timer, end game text)
- Coroutine (game loop, fading, spawning timing)
- AudioSource & AudioClip (item collection sounds)
- SerializeField (editable variables in Inspector)

Controls:
- Move: A/D or Left/Right Arrows
- Jump: Space

Gameplay:
- Collect the items before they disappear!
