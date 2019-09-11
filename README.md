# GameDevClubUnityLesson
A game created for RPI's NRB 2018 for the RPI Game Development Club



# Current Lesson Plan:
Start by showing what Unity is:
- Download the project from here with only the graphics. I should get a .zip file for people to download
	- or use a flash drive
- Show them about Unity
	- files in the bottom area
		- what are scenes?
	- Game objects
	- object inspector
	- play window/mode
- Create their first game object
	- what is a transform?
	- add a sprite renderer
		- look it's a thing!
	- add a Rigidbody2D
		- hit play! Magic! Yay!
	- add a CircleCollider2D
		- otherwise your next step will be kinda useless
- Create a sprite with a box collider in the way of the fall of the other gameobject
	- oh man interaction!
- now turn those things into prefabs!
	- then take a bunch of them out and show what's happening!
	- this is what I created for you guys, a bunch of simple terrain items

- now we get to custom scripting
- Create a player gameobject with a sprite renderer
- Start with jumping
	- create a Player script
	- show them what the default scripts look like
		- Start
			- runs when it's stuck into the world
		- update
			- runs every frame, used for repeating actions
			- The game loop
				- player game loop versus the program game loop
	- create a variable storing the rigidbody
		- initialize it in start
	- if space is pressed, add an upwards force to the character
	- hit play and let the fun begin.
- then add a way to reload the scene by pressing R
	- SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
- add the jumping sprite to the mix
- now show left and right motion
	- use input.getaxis()
	- oh wait, if the character is turned, the force goes the wrong way
		- use transform.up and transform.right
- switch scale depending on movement
- now limit jumping to only detecting things that are on the ground
	- raycast from the transform to like .6f downwards from the transform's POV.
	- if it hits, then we know we're on the ground and we can jump

- For future note, I got to about here in about 2.5 hours, without doing anything involving changing sprites or scaling, and taking about 10-15 minutes to get the stragglers set up with the art and prefabs (which I distributed in a zip via flash drive)

- now that we have movement pretty much figured out, and they're able to place tiles, we probably want the flag to swap levels next, because then we can create as many levels as we want.
- but we're not done yet!
- next up is buttons
	- make the collider a trigger
	- OnTriggerEnter2D(collider)
	- OnTriggerExit2D(collider)
	- LayerMask.LayerFromName(string name) ? I think this is it. >For comparing with collider.gameobject.layer
- then doors
	- get the collider, then set enabled to true or false to enable or disable the collisions.
	- create public functions etc.
	- then go back and create the function that opens and closes doors when the button is pressed
