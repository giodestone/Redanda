# Redanda
A game about shooting doughnuts at flies, which in turn throw knives at you while trying to chase you down.

![GIF of playing game](https://github.com/giodestone/Redanda/tree/master/Images/GIF1.gif)

# Running
[Download here!](https://github.com/giodestone/Redanda/releases)

Available for Windows, Mac, and Linux. A WebGL build is included.

## Controls
* **W, S, A, D** to move the pineapple.
* **1** to pause the game.
* **R** to reload.
    * After reloading type the letters you see - 
    * **Escape** to Cancel and; 
    * **Enter/Return** to submit.
    * The more you type the better your score is.
* **Mouse** to aim.
* **Left Mouse Button** to fire a doughnut.

On the top left is your score, and on the bottom left is your ammunition.

## Goal
Survive as many waves of flies, while amusing yourself with the randomly generated words. 

![GIF of being overrun by flies and game over](https://github.com/giodestone/Redanda/tree/master/Images/GIF2.gif)
![Image of game over](https://github.com/giodestone/Redanda/tree/master/Images/Image3.jpg)
# Story
![Image of reloading](https://github.com/giodestone/Redanda/tree/master/Images/Image1.jpg)
This game was made as a late Ludum Dare entry. The theme was Combine 2 Incompatible Genres and we decided initially to combine snakes and ladders with a shooter (tabletop + shooter) but none of us had multiplayer programming skills at the time, as the whole thing would have had to be networked or split screen.

So we settled for the shooting typing idea which made you type if you wanted to reload or do some actions. In the end just got the typing done.

![Image of pineapple getting swarmed by flies](https://github.com/giodestone/Redanda/tree/master/Images/Image2.jpg)

Initially the main character was supposed to be a red panda that was going to shoot things (inspired by Pusheen) but then changed to a pineapple... for some reason (ask the artist). Same with the doughnuts. This is why the name is Redanda **Red** p**anda**.

All SFX was created using foley - paper tearing, key strokes, writing on paper, and spraying Detol in a cup. The microphone used was a Blue Yeti Snowball for all those who wish to recreate this.

# Behind the scenes
Made using Unity 2018, the project was updated to 2018.9.3f1 to compile binaries.

The random word typing is based on a large random dictionary from Google with most common English words. There is a minimum size you get at each stage so the words get longer the more you type. In addition your score gets bigger the more you type.

The enemies are relatively simple, there are three types - small & fast, small ranged, and big & slow.

There is also a wave system, more, faster enemies will spawn the more waves you survive.

# Credits
**Adam S** - Enemies, implementing animations, Assembling the whole game together, programming.
**Feliks J** - Player, doughnuts, sfx, programming.
**Deyan S** - Art, Main menu, game over, credits, instruction screens, and sourcing sounds, animations.
