/*using System.Collections.Generic;
using UnityEngine;
using System;

//Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
[Serializable]
public class Position
{
    public string x { get; set; }
    public string y { get; set; }
    public string z { get; set; }
}
[Serializable]
public class Rotation
{
    public string a { get; set; }
    public string b { get; set; }
    public string c { get; set; }
    public string w { get; set; }
}
[Serializable]
public class Parent
{
    public Position position { get; set; }
    public Rotation rotation { get; set; }
    public string scale { get; set; }
    public string colour { get; set; }
}
[Serializable]
public class Child
{
    public string id { get; set; }
    public Position position { get; set; }
    public Rotation rotation { get; set; }
    public string scale { get; set; }
    public string colour { get; set; }
}
[Serializable]
public class GameObj
{
    public Parent parent { get; set; }
    public List<Child> child { get; set; }
}
[Serializable]
public class Proximity
{
    public string type { get; set; }
    public Position position { get; set; }
    public string tiedWith { get; set; }
    public string boneId { get; set; }
}
[Serializable]
public class WristRotation
{
    public string a { get; set; }
    public string b { get; set; }
    public string c { get; set; }
    public string w { get; set; }
}
[Serializable]
public class Handpose
{
    public string name { get; set; }
    public string threshold { get; set; }
    public WristRotation wristRotation { get; set; }
}
[Serializable]
public class Sensor
{
    public List<Proximity> proximity { get; set; }
    public List<Handpose> handpose { get; set; }
}
[Serializable]
public class HPAnimation
{
    public string tiedWith { get; set; }
    public string animationId { get; set; }
}
[Serializable]
public class SceneObj
{
    public SceneObj(int id)
    {
        this.sceneID = id;
        this.gameObj = new List<GameObj>();
        this.sensors = new List<Sensor>();
        this.animations = new List<HPAnimation>();
    }

    public int sceneID { get; set; }
    public List<GameObj> gameObj { get; set; }
    public List<Sensor> sensors { get; set; }
    public List<HPAnimation> animations { get; set; }
}
[Serializable]
public class Root
{
    public List<SceneObj> sceneObj { get; set; }

    public Root()
    {
        this.sceneObj = new List<SceneObj>();
    }
}
*/
