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
	*2.x.x: Alpha: Maps
	*3.x.x: Beta: Graphics
	*4+.x.x: Release:
