using Godot;
using System;
using System.Collections.Generic;

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
    public float totalLenght = 600;
    [Export]
    Color color = new Color(51, 51, 240);
    [Export]
    public float width = 5;
    // Called when the node enters the scene tree for the first time.
    public override void _Draw()
    {
        var rnd = new RandomNumberGenerator();
        rnd.Randomize();
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        List<Vector2> ground = new List<Vector2>();
        Vector2 startPos = new Vector2(-20, screenSize.y / 4);
        ground.Add(startPos);
        float lenght = 0;
        totalLenght = screenSize.x * 3;
        bool x = true;
        Vector2 newPoint = new Vector2(startPos);
        while(lenght < totalLenght){
            if(x){
                lenght += rnd.RandfRange(minLengthX, maxLengthX);
                newPoint.x = lenght;
            }
            else{
                int up = rnd.RandiRange(0, 1);
                up = up == 0 ? 1 : -1;
                float currentY = ground[ground.Count - 1].y;
                
                if(currentY + (maxLengthY * up) < (screenSize.y - maxLengthY)){
                    newPoint.y = rnd.RandfRange(currentY + minLengthY, currentY + maxLengthY);
                }
                else{
                    newPoint.y = rnd.RandfRange(currentY - minLengthY, currentY - maxLengthY);
                }
            }
            x = !x;
            var sas = ground[100000];
            ground.Add(newPoint);
        }

        for (int i = 0; i < ground.Count - 1; ++i){
            DrawLine(ground[i], ground[i + 1], color, width);
        }
    }

    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
