using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Ground : Node2D
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";
  [Export]
  public float minLengthX = 100f;
  [Export]
  public float maxLengthX = 400f;
  [Export]
  public float minLengthY = 30f;
  [Export]
  public float maxLengthY = 70f;
  [Export]
  public float totalLenght = 2000f;
  [Export]
  Color color = new Color(51, 51, 240);
  [Export]
  public float width = 5;
  [Export]
  public float sizeMod = 1;
  // Called when the node enters the scene tree for the first time.
  private RandomNumberGenerator rnd = new RandomNumberGenerator();

  public override void _Draw()
  {
    maxLengthX = maxLengthX / sizeMod;
    maxLengthY = maxLengthY / sizeMod;
    minLengthX = minLengthX / sizeMod;
    minLengthY = minLengthY / sizeMod;

    rnd.Randomize();
    Vector2 screenSize = GetViewport().GetVisibleRect().Size;
    List<Vector2> ground = new List<Vector2>();
    Vector2 startPos = new Vector2(0, screenSize.y - 100);
    ground.Add(startPos);
    float length = startPos.x;
    // totalLenght = screenSize.x * 3;
    bool x = true;
    Vector2 newPoint = new Vector2(startPos);
    while (length < totalLenght)
    {
      float currentY = ground[ground.Count - 1].y;
      float currentX = ground[ground.Count - 1].x;
      if (x)
      {
        length += rnd.RandfRange(minLengthX, maxLengthX);
        newPoint.x = length;
        newPoint.y = currentY;
      }
      else
      {
        int dir = rnd.RandiRange(0, 1);
        bool up = dir == 0 ? false : true;
        GD.Print("up: " + up);

        newPoint.y = CreateNewPoint(currentY, up, minLengthY, maxLengthY);
        GD.Print("curPoint: " + currentY + "  frstPoint: " + newPoint.y);
        if (newPoint.y < 0)
        {
          GD.Print("Smaller than 0");
          newPoint.y = CreateNewPoint(currentY, false, minLengthY, maxLengthY);
        }
        else if (newPoint.y > screenSize.y)
        {
          GD.Print("Bigger than max");
          newPoint.y = CreateNewPoint(currentY, true, minLengthY, maxLengthY);
        }
        newPoint.x = currentX;
        GD.Print("newPoint: " + newPoint.y + "  Max: " + screenSize.y + "\n");
      }

      x = !x;
      ground.Add(newPoint);
    }

    ground.Add(new Vector2(ground[ground.Count - 1].x, 10000f));
    ground.Add(new Vector2(0, 10000f));
    ground.Add(startPos);
    CollisionPolygon2D colPol = new CollisionPolygon2D();
    colPol.BuildMode = CollisionPolygon2D.BuildModeEnum.Solids;
    colPol.Polygon = ground.ToArray();
    colPol.Notification(CollisionPolygon2D.NotificationDragBegin);
    DrawPolygon(colPol.Polygon, new Color[] { color });

		CollisionPolygon2D collisionPoly = new CollisionPolygon2D();
		collisionPoly.Polygon = colPol.Polygon;
		collisionPoly.Name = "Collider";
		this.AddChild(collisionPoly);
  }

  public void DrawBorder(Vector2 screenSize)
  {
    screenSize = new Vector2(screenSize.x - 10, screenSize.y - 10);
    List<Vector2> border = new List<Vector2>();
    border.Add(new Vector2(10, 10)); //top left
    border.Add(new Vector2(screenSize.x, 10)); //top right
    border.Add(new Vector2(screenSize.x, screenSize.y)); //bottom right
    border.Add(new Vector2(10, screenSize.y)); //bottom left
    border.Add(new Vector2(10, 10));
  }

  public float CreateNewPoint(float current, bool up, float min, float max)
  {
    GD.Print("CURRENT: " + current);
    if (up)
    {
      return (rnd.RandfRange(current - max, current - min));
    }
    else
    {
      return (rnd.RandfRange(current + min, current + max));
    }
  }
}
