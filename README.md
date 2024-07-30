# Island-Master

## An rpg game with elements of surviving developed in Unity that uses perlin noise for generating the map.

Features:
- inventory system (for storing items collected or crafted by the player)
- crafting system (upgrading the equipment by converting resurses or lower rarity item for better armors or weapons)
- mission system (used for getting rewards, gold and progressing in the game)
- interacting system (allowing the player to interact with npc, chests and picking up item from the ground)
- the map generator (combines individuals islands created by adding multiple octaves of perlin noise and applying a square gradient to obtain the shape of an island, than the height and color maps are used to create a tridimensional mesh)
- character controls (using state pattern for simplifying the complexity of the character's states)
- health system (for decoupling the components the observer design pattern was utilized)
- collision detection (colliders and ray casting)