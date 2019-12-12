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
    public float maxLengthX = 300f;
    [Export]
    public float minLengthY = 30f;
    [Export]
    public float maxLengthY = 100f;
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
        maxLengthX = maxLengthX/sizeMod;
        maxLengthY = maxLengthY/sizeMod;
        minLengthX = minLengthX/sizeMod;
        minLengthY = minLengthY/sizeMod;
        
        rnd.Randomize();
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        List<Vector2> ground = new List<Vector2>();
        Vector2 startPos = new Vector2(0, screenSize.y - 100);
        ground.Add(startPos);
        float lenght = startPos.x;
        // totalLenght = screenSize.x * 3;
        bool x = true;
        Vector2 newPoint = new Vector2(startPos);
        while(lenght < totalLenght){
            GD.Print("Lenght: " + lenght);
            if(x){
                lenght += rnd.RandfRange(minLengthX, maxLengthX);
                newPoint.x = lenght;
            }
            else{
                int dir = rnd.RandiRange(0, 1);
                bool up = dir == 0 ? true : false;
                float currentY = ground[ground.Count - 1].y;
                
                if(up){
                    newPoint.y = CreateNewPoint(currentY, up, minLengthY, maxLengthY);
                    if(newPoint.y > (screenSize.y - maxLengthY)){
                        newPoint.y = CreateNewPoint(currentY, !up, minLengthY, maxLengthY);
                    }
                }
                else{
                    newPoint.y = CreateNewPoint(currentY, !up, minLengthY, maxLengthY);
                    if(newPoint.y < (0 + maxLengthY)){
                        newPoint.y = CreateNewPoint(currentY, !up, minLengthY, maxLengthY);
                    }
                }
            }
            x = !x;
            ground.Add(newPoint);
        }

        for (int i = 0; i < ground.Count - 1; ++i){
            DrawLine(ground[i], ground[i + 1], color, width);
        }
    }

    // public override void _Ready()
    // {
        
    // }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public float CreateNewPoint(float current, bool up, float min, float max){
        if(up){
            return(rnd.RandfRange(current + min, current + max));
        }
        else{
            return(rnd.RandfRange(current - max, current - min));
        }
    }
}
