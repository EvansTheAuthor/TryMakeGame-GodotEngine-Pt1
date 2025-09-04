// Calling Godot's core logic since this script is written in C#
using Godot;

//Yeah, this script written for Sprite2D object
public partial class Sprite2d : Sprite2D
{
	// The [Export] attribute makes this variable show up in the Godot Inspector,
	// so you can change it without editing the code.
	//set up the speed of the object per frame
	[Export]
	public float Speed { get; set; } = 400.0f; // { get; set; } is a C# auto-property.
	
	// Set up the power of the bounce.
	// Also Exported to the Inspector.
	[Export]
	public float BounceForce { get; set; } = 600.0f;
	
	// Set up the state "bouncing"
	private bool _isBouncing = false;
	
	// Set up the direction of the bounce.
	// It is using Vector2 since it's 2D object.
	private Vector2 _bounceDirection;
	
	// Set up the timer to set the bounce time
	private Timer _bounceTimer;
	
	// Set up the startup
	public override void _Ready()
	{
		// Calling the child node Timer and save it as "reference"
		_bounceTimer = GetNode<Timer>("Timer");
		
		// GetViewportRect().Size gets the current size of the game window (width and height).
		// Divide it with 2 make the sprite on the center of screen at startup.
		Position = GetViewportRect().Size / 2;
	}
	
	// Controlling the process.
	// 'delta' is the time that has passed since the previous frame.
	public override void _Process(double delta)
	{
		// The quit logic when escape pressed.
		//It is set up as the default Godot input action so later just write down this code when you need make the quit logic with pressing Escape button.
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			// GetTree().Quit() safely closes the entire game.
			GetTree().Quit();
		}
		
		//the if-else logic for bouncing
		if (_isBouncing)
		{
			// Make the object bounced to the direction it comes.
			// We multiply by delta to make the movement smooth and frame-rate independent.
			Position += _bounceDirection * (float)delta;
		}
		else
		{
			// Let the player control the object when it is in normal state.
			// We pass delta down to the input handling function. The (float) part
			// converts the 'double' delta into a 'float' to match what Vector2 needs.
			HandlePlayerInput((float)delta);
			
			// Always checking is it it the wall or not
			CheckForWallCollision();
		}
	}
	
	// Void that handle how player control the object.
	private void HandlePlayerInput(float delta)
	{
		// Make the object direction as how the input sent.
		// It returns values like (1, 0) for right, (-1, 1) for left-down, etc.
		Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		
		// Set the object speed when moved. It's called on control script from the main Speed object.
		// We multiply the direction by our Speed variable to get the final velocity.
		Vector2 velocity = inputDirection * Speed;
		
		// Set the position to keep updated as the control tell it where it goes.
		// We update the sprite's position based on its velocity and delta time.
		Position += velocity * delta;
	}
	
	// Function to check is the object hit the wall or not.
	private void CheckForWallCollision()
	{
		// Get the screen's boundaries again, just in case the window was resized.
		Rect2 viewportRect = GetViewportRect();
		
		// GetRect().Size gets the width and height of our sprite's texture.
		// We divide by 2 because all position calculations are from the sprite's CENTER.
		Vector2 spriteHalfSize = GetRect().Size / 2;
		
		// Mathf.Clamp() forces a value to stay within a min/max range.
		// This line is a safety measure that prevents the sprite from ever leaving the screen.
		Position = new Vector2(
			Mathf.Clamp(Position.X, spriteHalfSize.X, viewportRect.Size.X - spriteHalfSize.X),
			Mathf.Clamp(Position.Y, spriteHalfSize.Y, viewportRect.Size.Y - spriteHalfSize.Y)
		);
		
		// If the object hit the left wall,
		if (Position.X <= spriteHalfSize.X)
		{
			StartBounce(Vector2.Right); // it bounced to right.
		}
		// If the object hit the right wall,
		else if (Position.X >= viewportRect.Size.X - spriteHalfSize.X)
		{
			StartBounce(Vector2.Left); // it bounced to left.
		}
		
		// If the object hit the top wall,
		else if (Position.Y <= spriteHalfSize.Y)
		{
			StartBounce(Vector2.Down); // it bounced down.
		}
		
		//If the object hit the bottom wall,
		else if (Position.Y >= viewportRect.Size.Y - spriteHalfSize.Y)
		{
			StartBounce(Vector2.Up); // it bounced up.
		}
	}
	
	// Function to set the bouncing logic with asking
	// the direction as void input to make sure
	// the object bounced to the direction it comes.
	private void StartBounce(Vector2 direction)
	{
		// Set the bouncing state as true
		_isBouncing = true;
		
		// Send back the object to the direction it comes
		// with the distance as BounceForce set up earlier.
		_bounceDirection = direction * BounceForce;
		
		// Call the bouncing loop prevention logic.
		_bounceTimer.Start();
	}
	
	// Set the prevent from bouncing loop
	// with making sure the bounce stopped
	// when the timer ended.
	private void OnTimerTimeout()
	{
		// Stop the bouncing.
		_isBouncing = false;
	}
}
