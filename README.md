Untitled-Stealth-Game-Developement-Build
========================================
This repository will be used for source controlling my upcoming stealth game.
========================================
Versions:
	*1.x.x: Developement Build: Core Game Mechanics
		*1.1.x:
			*Player
				*Basic movement system
				*Click to damage
			*Basic map tiles
			*Basic AI
				*FOV
				*Follows player in straight line
				*Ignores player if behind wall
		*1.2.x:
			*Basic ingame GUI
				*Rotation of camera
				*Top down and ortho
		*1.3.x
			*Placeholder main menu
				*Level select
					*Premade
					*Load
				*Loadout
					*Primary
						*M16
						*Uzi
					*Secondary
						*M9
						*M1911
					*Nonlethal
						*Tranq gun
						*Taser
				*Options (unused)
				*Quit
			*Cover system
				*Snap to lock
					*A/D to lean
				*Tab to disconnect
			*Weapon improvements
				*Fully auto
					*Fires when AI locked on and in sight
				*Variable damage and fire rate per gun
			*Ingame GUI
				*Weapon select using mousewheel
			*AI
				*Added unity navmesh usage
		*1.4.x: Not working
			*Basic map builder app
				*Menu
					*X,Y coords for new map size
					*Map loading feature
				*App
					*Base map made of "Grid" tiles
					*GUI for scrolling through block types
					*Click to place
					*Tiles
						*Start (No use in game)
						*AI (No use in game)
						*Ammo (No use in game)
						*Grid/Base
						*Block/Wall
				+Less time consuming
				+More maps
				+Player friendly
				-Requires new navigation system: Dijkstra's Algorithm
		*1.5.x:
			*Player
				*Ammo counter
				*Random weapon damages based on weapon damage
			*New tiles: Functioning in editor and game; needs art assets
				*Character
					*AI
					*Player start
					*Player end
				*Utility
					*Ammo
					*Health pack
				*Other
					*Vents
					*Doors/keys: Corresponding IDs
			*Cover system
				*"Cover block"
					*4 sides: toggleable by raycast, manual toggle, or checking adjacent tiles in the maparray
					*Click to attach, tab to detach
					*A/D to strafe
					*Can swap to nearest cover to left/right
					*Can swap to cover across hall
					*Can round corners
			*Project refactoring 
				*Making code efficient
				*Organize project folder
				*Remake/organize gameobjects
		1.6.x:
			*Weapons
				*Armor
					*Ammo cap
					*HP bonus
					*Armor bonus
					*Speed
				*Accesories
					*Extended clips
					*Silencers
					*Sights/Optics
					*Laser sights
					*Grips
					*Fire rate selectors
	*2.x.x: Alpha: Maps
	*3.x.x: Beta: Graphics
	*4+.x.x: Release:
