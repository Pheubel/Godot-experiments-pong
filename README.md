# Godot-experiments-pong

This project was for learning the basic ins and outs of Godot. I've learned how to:
* use the node tree
* dispay a sprite
* create a trigger volume
* create collissions
* play sounds
* create and use action maps
* use signals for handling collission events

## Using the node tree:
Each scene is composed of nodes, each node represents a single aspect or feature. 
Using this strategy it allows you to create a composition of nodes that together forms a more complex behavior, for example an enemy can have a sprite node to display itself and another node to handle physics interactions.

Nodes can be turned into a resource that can be easily copied around by dragging it into the project tab. This is similar to Unity's prefab system. When a node is can be saved in different ways; `.scn`, `.tscn` and `.res`. 
At this point I am not yet sure what the prefered scenario is for each, but for general use `.tscn` seems to be a good choice ddue to it saving the node scene as a text file, making it easier for git to merge.

After you have saved the sprite you can open it and treat it like it is it's own scene. This can be useful create segments of a level or to simply work with a game entity in it's local world space.

## Display a sprite:
Sprites are the bread and butter of 2D games, without it it will be hard to display the gameplay. Creating a sprite in a scene is as simple as adding a new node to the node tree and selecting the `Sprite2D` node.
Now to actually see the sprite you need to add a texure, like a png, to the sprite node in order to have it render image.

You can however spruce up the sprite by adding some shader magic. First go to the material properties and replace empty by a shader material. This is a resource that tells the shader the properties it needs to keep in mind when executing.
If you plan on using the same shader material properties for different sprite nodes, it can be useful to turn it into it's own resource to make sharing it easier.

Inside of the shader material you still need to assign a shader, this is a little program that performs calculations over each pixel on the GPU. 
tl;dr - you can make some pretty cool effects, but for now i made a simple shader that allows you to tint a sprite with a given color.
