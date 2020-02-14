# About The Project

_ThingThingThing_

_ThingThingThing_ is a computational system that invites audiences to take part in collective world-making. It is built with easily-accessible tools and interfaces, allowing everyone to contribute a computational object of their own. All computational objects created by audiences are integrated automatically by the software algorithms. The result is an ever-evolving, never concluding, three-dimensional system of instantiations, created by everyone yet owned by no one. 

_ThingThingThing_ is both object and process. The world of ThingThingThing is a real-time simulation, a video game that plays itself, a never-ending film. The participants jointly determine its evolution by setting an initial seed state of their computational object, which later requires no further human control. All viewers can interact with the world by navigating an in-game eyeball and observe the behavior of all living objects. 

### Piloting 

The artists and technologists have been hosting a series of workshops with museums and art institutions around the world. The workshop enables everyone from students, artists, designers, architects and technologists to create their own programmed creatures with intelligence. Past collaborators include: 

- Macy Art Gallery, Teachers College, Columbia University, New York, NY
- City University of New York (CUNY), New York, NY
- Guangzhou Academy of Art (GAFA), Guangzhou, China
- Creative Tech Week (CTW), New York, NY
- Asia Art Archive in America (AAA-A), Brooklyn, NY
- Power Station of Art (PSA), Shanghai, China. 

The latest ThingThingThing exhibition and workshop were hosted at NEW INC, the Art & Technology incubator of New Museum in September, 2019.

<!-- ---------- -->

<!-- # Collaborators and Their _Things_

- Zhenzhen  `PrinceZ`
- Yang `Elo` & `Dummy`
- Jingling `JZ` & `JZPig`
- Evian `Cloud Cloud` & `Sheep_Mushy`
- Sara `Margarita` & `Tomas`
- JHMun `Chicken` -->

---



<!-- # Steps for participants:

[Google Doc Link](https://docs.google.com/document/d/18rqBA01xjrEOiLuYqoa7b_HeCmha066y6eLI37iUFIA/edit?usp=sharing)  
[石墨文档](https://shimo.im/docs/DhxNEZhaCGgABntM/)

--- -->

# Technical Document

## Properties/Fields For You To Get

```csharp
//Environment
float TOD_Data.main.TimeNow; //e.x. 3:30PM will be represented as 15.5
bool TOD_Data.main.IsDay;
bool TOD_Data.main.IsNight;
bool inWater; //if is in water now
int NeighborCount; //how many neighbors do you have currently
```

## Events You can Use to Fill Code Inside

```csharp
protected override void OnSunset(){}//日落时
protected override void OnSunrise(){}//日出时
protected override void OnMeetingSomeone(GameObject other){}//碰到其他物时
protected override void OnLeavingSomeone(GameObject other){}//离开了其他物时
protected override void OnNeighborSpeaking(){}//有邻居说话时
protected override void OnNeigborSparkingParticles(){}//有邻居发出发光时
protected override void OnTouchWater(){}//进入水时
protected override void OnLeaveWater(){}//离开水时
```

## Settings of your Thing you can change

```csharp
//value is the default setting
settings.cameraOffset = 15; //追踪相机离我的距离
settings.acceleration = 4;//加速度
settings.drag = 1.8f;//速度阻力
settings.mass = 0.2f;//质量
settings.chatBubbleOffsetHeight = 2; //!important! chat bubble offset on Y axis, adjust to avoid chat bubble being blocked by your model 重要！！！！我的对话泡泡的高度！！注意测试调整
settings.getNewDestinationInterval = 5;
settings.newDestinationRange = 40;//随机寻找新目的的时候新目的的范围半径
settings.alwaysFacingTarget = true;
settings.myCubeColor;//我生产出来的小方块的颜色
```

## Ready to Use Methods

```csharp
//movement
SetTarget(Vector3 target); //设定一个目的地
StopMoving(); //不再移动
StopMoving(float forHowManySeconds); //不再移动（并制定一个时间，这个时间后自动开始移动）
RestartWalking(); //重新开始移动
RandomSetDestination();//get a new random target  随机指定一个目的地
ResetPosition(); //change position to spawn point 重置位置

//shape and form
SetScale(Vector3 newScale); //设定尺寸，注意参考你现在的尺寸
ChangeColor(Color newColor); //change color, might not work well if you have more than one renderer or more than one material 更改颜色
ResetColor(); //reset to original color 重制颜色到默认值

//social
Speak(string content, float stayLength); //
Speak(string content);
Mute(); //Speak no longer works //开始沉默，不再讲话
DeMute(); //regain ability to Speak again //不再沉默
Spark(Color particleColor, int numberOfParticles);
PlaySound(int soundEffectId); // Range: 1 ... 102 //播放一个音效，从1-102中选择
PlaySound(string soundFileName); //if you want to play your own sound. but pls make your sound short!!!//播放一个音效，如果你要用自己的声音文件
CreateCube(); //throw a cube on the small ground//制造一个小方块
```

# Tips

---

## How to keep your cloned repository up to date

Only do this once

`git remote add upstream https://github.com/ZZYW/thing-thing-thing.git`

Do this to update

`git pull upstream master`

## Where to put my files

#### Where to put my prefabs:

`Resources/Things/[here]`

#### Where to put my materials and scripts

`CREATORS/[your name]/[put here]`

#### Where to put my own sound files

`Resources/Sounds/[here]`

## How to use your own sound files

Find a sound file that is one of the formats below

- .Aif.
- .wav.
- .mp3.
- .ogg.

Compress it if necessary, make sure its file size is smaller than 2 MB, and move it into `Resources/Sounds` folder.

Use `PlaySound(“filename”)` to play your sound, _no extension name needed_.

## Some Basic about C# Programming

[If Else Statement](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/if-else)
AND
[For Loop](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/for)

OR

[Everything Else About C#](https://docs.microsoft.com/en-us/dotnet/csharp/index)

## Some useful methods from Unity

```csharp
//Print things to Console for debugging
print(object message);

//Invokes the method methodName in time seconds.
Invoke(string methodName, float time);

//Invokes the method methodName in time seconds, then repeatedly every repeatRate seconds.
InvokeRepeating(string methodName, float time, float repeatRate);

//Cancels all Invoke calls on this MonoBehaviour.
CancelInvoke();
```
