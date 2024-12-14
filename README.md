# CS283-Project

![](https://github.com/olivia-boyer/CS283-Project/blob/main/SplashImage.png)


How To Play:
Use WASD or the arrow keys to move around the overworld. You can also press space to jump though there
is no reason to. When you encounter an enemy, combat begins. you can choose your actions by clicking the 
buttons with your cursor. Walk into the red mushroom to collect in and complete the game.

When you open the game in unity it's probably going to give you a bunch of compiler errors. Something in the github repo
got messed up (my laptop refused to acknowledge .gitignore), which I think is what made it so that necessary packages do not install.
So just download the following packages from the Unity registry to run:
- AI Navigation
- Visual Studio Editer 
- Unit UI
- TextMeshPro

Also open the scene called "Project"

Features:
The game is a short quest into a cave to find a mushroom. The game starts in the overworld section with the player
able to walk around and be followed by their companion and a combat section, where the camera switches to a static
view of the player characters facing the enemies (the CombatInitiator script controls these transitions). In the overworld 
sections I reused the PlayerMotionController and SpringFollowCamera scripts from pervious assignments.

There are two enemy types, the basic rats that wander around and chase the 
player and the fakeout mushroom which is more powerful and functions as a sort of boss enemy. The overworld behaviors
on enemies are controlled by the EnemyBehavior script and for the combat portions.

Both the player characters and enemies have stats and functions for taking/healing damage in a BattleUnit class.

The flow of combat is controlled by the BattleSystem script. THe BattleHUD script updates hp values.

Enemies just do basic attacks at random targets. The two player character each have a basic attack, defense that
reduces damage taken, and then one other ability. One character can boost attack for one turn, and the other can heal.
THe values for defense, attack buffs, and healing power are all just based on the attack stat. There is some randomness
to the exact values though.

The game ends when the player walks into the fly agaric mushroom at the end of the cave, which causes it 
to play a short animation and show an end screen using the AnimateMushroom script.

Gif Highlight reel:
![](https://github.com/olivia-boyer/CS283-Project/blob/main/Vids%26pics/1213.gif)

Full Playthrough video (on 1.5x speed):
https://youtu.be/yfTzPaKlVbA

Screenshots of scene:

Village
![](https://github.com/olivia-boyer/CS283-Project/blob/main/Vids%26pics/town.png)
Cave Entrance:
![](https://github.com/olivia-boyer/CS283-Project/blob/main/Vids%26pics/entrance.png)
Cave birds eye view
![](https://github.com/olivia-boyer/CS283-Project/blob/main/Vids%26pics/overview.png)
battle area
![](https://github.com/olivia-boyer/CS283-Project/blob/main/Vids%26pics/fightarea.png)


Asset Credits:

Low Poly Nature Essentials:
https://assetstore.unity.com/packages/3d/environments/low-poly-nature-essentials-266763

Baker's House:
https://assetstore.unity.com/packages/3d/environments/fantasy/lowpoly-baker-s-house-26443

Trees:
https://assetstore.unity.com/packages/3d/environments/landscapes/vegetation-stylized-kit-298238

Grass:
https://assetstore.unity.com/packages/3d/environments/lowpoly-environment-nature-free-medieval-fantasy-series-187052

Wizard:
https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/battle-wizard-poly-art-128097

Sword-fighter:
https://assetstore.unity.com/packages/3d/characters/humanoids/rpg-tiny-hero-duo-pbr-polyart-225148

Skybox:
https://assetstore.unity.com/packages/2d/textures-materials/sky/free-stylized-skybox-212257

Rat model and texture from:
https://www.turbosquid.com/3d-models/low-poly-hand-painted-rat-1194177

Ground textures from: 
https://arexxuru.itch.io/pixel-floor-texture-pack-ground-tile

Low poly nature pack:
https://www.turbosquid.com/3d-models/low-poly-art-nature-pack-lite-2235772
