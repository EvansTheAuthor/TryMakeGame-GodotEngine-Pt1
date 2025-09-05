using Godot;

public partial class Sprite2d : Sprite2D
{
	private Vector2 _velocity = new Vector2(250, 200);
	
	public override void _Ready()
	{
		GD.Print("Sprite is ready!");
		
		Position = GetViewportRect().Size / 2;
	}
	
	public override void _Process(double delta)
	{
		Rect2 viewportRect = GetViewportRect();
		
		Position += _velocity * (float)delta;
		
		if(Position.X < 0 || Position.X > viewportRect.Size.X)
		{
			_velocity.X *= -1;
		}
		
		if(Position.Y < 0 || Position.Y > viewportRect.Size.Y)
		{
			_velocity.Y *= -1;
		}
	}
}
