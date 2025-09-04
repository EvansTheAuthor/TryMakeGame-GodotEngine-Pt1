# TryMakeGame-GodotEngine-Pt1
Written in: 5/9/2025 (5th September 2025)

## A Bit Chit Chat
Hello-hello. There's Evan The Developer.

Since I decided to start learning game development, I try to make my initial move with Godot (you can check it [here](https://godotengine.org/)) with AI guiding.

And, since it using AI, so think it's not a huge deal if I make this repository a public one. Hopefully, my documentation can giving you insight as a newbie programmer alike me. Big hug for you all, guys :3

### The Godot I use
It's Godot Stable Mono version (or .NET version) v4.4.1.

As a note, .NET/Mono version is supporting you for C# scripting throught .NET/Mono framework, in addition to GDScript. If you want this version too, make sure you have .NET installed, at least v8 or above. Download it [here](https://dotnet.microsoft.com/en-us/download).

### Steps to Starting With Godot
1. Download Godot. Check the link above.
2. Download the latest .NET version. It's better to use the latest version. Check the link above.
3. Locate your Godot zip, extract it on your desired location.
4. Run Godot. It does not need any installation (as how portable application usually goes).

## My Initial Try: DVD Animation Copycat
This is our goal, but we'll use the Godot icon to make it easy for beginner.

![DVDAnimation](/documentation-assets/dvd-logo.gif)

If you try to check the code, there's no trace since I forgot to commit this initial (heheh, sorry). So, here's the tutorial.

### Step 1: Create Your Own Project
I named my project itself as "trylearnbygeminiai.pt1" so name it as you desired.

### Step 2: Create A Scene
1. In the Scene dock (Top-Left), click Add (`+`).

![AddingNewNode](/documentation-assets/AddingNewParentNode.png)

Choose the `2D Scene` button.

![ChoosingNodeType](/documentation-assets/ChoosingNodeType.png)

This will create a `Node2D` as the root of your scene.
2. Right-click the `Node2D`

![RightClickOnNode](/documentation-assets/RightClickOnNode.png)

and select `Add Child Node`.
3. Search for `Sprite2D` and create it. This node is used to display a 2D image.

### Step 3: Add The Sprite's Texture
1. Select the `Sprite2D` node int the Scene dock.
2. In the **Inspector** on the right, you'll see a property called `Texture`.
3. In the **FileSystem** dock on the bottom-left, you'll see the default `icon.svg`.

![IconSvgOnRes](/documentation-assets/IconSvgOnrRes.png)

Drag this file onto the `Texture` property slot in the Inspector.

![TextureOnInspector](/documentation-assets/TextureOnInspector.png)

You should now see the Godot icon in the center of your viewport.

4. Save your scene by pressing `Ctrl+S` (or C`md+S`). Name it something like `main.tscn`.

### Step 4: Write The C# Script
1. Select the `Sprite2D` node again.
2. In the **Inspector**, check a property called `Script`. Click on dropdown icon.

![MakeNewScript](/documentation-assets/MakeNewScript.png)

Click `New Script...` to attach a new script.

3. In the pop-up, set the **Language** to `C#`. The path and name should be fine. Click `Create`.

4. Write down this script and save it (press `Ctrl + S` or `Cmd + S`):
```
using Godot;

public partial class Sprite2d : Sprite2D
{
	private Vector2 _velocity = new Vector2(250, 200);
	
	public override void _Ready()
	{
		GD.Print("Sprite is ready!");
	}
	
	public override void _Process(double delta)
	{
		Rect2 viewportRect = GetViewportRect();
		
		Position += _velocity * (float)delta;
		
		// Check if the object reach the left or right walls
		// Position.X < 0 means left and the next is right
		if (Position.X < 0 || Position.X > viewportRect.Size.X)
		{
			_velocity.X *= -1; // Reverse horizontal direction
		}
		
		// It goes the same but for top and bottom walls
		if (Position.Y < 0 || Position.Y > viewportRect.Size.Y)
		{
			_velocity.Y *= -1; //Reverse vertical direction
		}
	}
}
```
### Step 5: Run The Program
1. Click the Play button (or press `F5`) at the top-right.
2. Godot will ask you to select a main scene. Choose the `main.tscn` you just saved.
3. Your game window will appear, and you should see the Godot icon bouncing around!
===============
Now, you was making a DVD animation copycat with Godot icon. Let's step ahead to the my second try.

## My Second Try: A Simple Controlable Object in Fixed Maps
*This Section Still Under Construction 🚧🛠️👷*