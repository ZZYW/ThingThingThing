<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
**Table of Contents**  *generated with [DocToc](https://github.com/thlorenz/doctoc)*

- [About The Project](#about-the-project)
- [Collaborators and Their _Things_](#collaborators-and-their-_things_)
- [Past Events](#past-events)
- [Steps for participants:](#steps-for-participants)
- [Technical Document](#technical-document)
  - [Properties/Fields](#propertiesfields)
  - [Events](#events)
  - [Ready to Use Methods](#ready-to-use-methods)
- [Tips](#tips)
  - [How to keep your cloned repository up to date](#how-to-keep-your-cloned-repository-up-to-date)
  - [Where to put my files](#where-to-put-my-files)
      - [Where to put my prefabs:](#where-to-put-my-prefabs)
      - [Where to put my materials and scripts](#where-to-put-my-materials-and-scripts)
      - [Where to put my sound files](#where-to-put-my-sound-files)
  - [How to use your own sound files](#how-to-use-your-own-sound-files)
  - [Some Basic about C# Programming](#some-basic-about-c-programming)
  - [Some useful methods from Unity](#some-useful-methods-from-unity)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->


# About The Project

Making an art game is like daydreaming - one that we can go back to over and over again. Game Objects are external manifestations of creators’ spirits. When their creators are tied up with the reality of life, these tiny things awake in the wondrous space of “grandeur” (Gustave Bachelard, 1948) created through the imagination of their creators. They wonder around, make a friend or some weird sound. 
 

ThingThingThing is an experimental collaboration between artist duo ZZYW, formed by Yang Wang and Zhenzhen Qi, with multiple art & technology insitutions and organizations. A group of participants will attend an artist-led presentation and workshop, learning fundamentals of video game development, and make a collective art game along the way. At the end of the workshop, the result is a film that generates it own plots in real time, composed by all participants using Unity, a video game development platform and C# as the programming language. The latest version will always be downloadable from Github.

制作艺术游戏有点像做梦 —— 一个可以反复回顾的白日梦。游戏里的角色就像是创作者某种的精神上的化身。当创作者们为现实生活忙的团团转时，他们的“精神化身”则在一个广阔无限的，在创作者们的想象力构建的虚拟空间里苏醒。它们逛来逛去，和其它的“物(Thing)”们相聚，或时不时的发出怪异的响声。
 
《物物物》是艺术家组合zzyw（汪洋+漆贞贞）的一个实验性项目。通过与美术馆，艺术机构和组织合作，举办参与式工作坊。参与者们将参加由两位艺术家带领的媒体艺术工作坊，学习使用版本控制Git，编程语言C#，和游戏开发引擎Unity编写一个自己的虚拟的物 (Thing)，并使用代码赋予其独特的样子和行为、逻辑模式，当工作坊结束时，所有参与者的物都会被放入“物物物”的主世界。物物物将得到一次版本更新。

----------

# Collaborators and Their _Things_

- Zhenzhen  `PrinceZ`
- Yang `Elo` & `Dummy`
- Jingling `JZ` & `JZPig`
- Evian `Cloud Cloud` & `Sheep_Mushy`
- Sara `Margarita` & `Tomas`
- JHMun `Chicken`

----------


# Past Events

-  Saturday, April 14 and Sunday, April 15, 1:00-5:00pm    |   Asia Art Archive in America   |   43 Remsen Street, Brooklyn, NY 11201



------------


# Steps for participants:

[Google Doc Link](https://docs.google.com/document/d/18rqBA01xjrEOiLuYqoa7b_HeCmha066y6eLI37iUFIA/edit?usp=sharing)



----------


#  Technical Document


## Properties/Fields


```csharp
//Environment
float TOD_Data.main.TimeNow; //e.x. 3:30PM will be represented as 15.5
bool TOD_Data.main.IsDay;
bool TOD_Data.main.IsNight; 
bool inWater; //if is in water now
int NeighborCount; //how many neighbors do you have currently
```

## Events

```csharp
void OnSunset();
void OnSunrise();
void OnMeetingSomeone(GameObject other);
void OnLeavingSomeone(GameObject other);
void OnNeighborSpeaking();
void OnNeigborSparkingParticles();
void OnTouchWater();
void OnLeaveWater();
```

## Ready to Use Methods

```csharp

//movement
void SetTarget(Vector3 target);
void StopMoving();
void StopMoving(float forHowManySeconds);
void RestartWalking();
void RandomSetDestination();//get a new random target
void ResetPosition(); //change position to spawn point

//shape and form
void SetScale(Vector3 newScale);
void ChangeColor(Color newColor); //change color, might not work well if you have more than one renderer or more than one material
void ResetColor(); //reset to original color

//social
void Speak(string content, float stayLength);
void Speak(string content);
void Mute(); //Speak no longer works
void DeMute(); //regain ability to Speak again
void Spark(Color particleColor, int numberOfParticles);
void PlaySound(string soundName);
void CreateCube(); //throw a cube on the small ground


```



# Tips


----------


## How to keep your cloned repository up to date

Only do this once

`git remote add upstream https://github.com/ZZYW/thing-thing-thing.git`

Do this to update

`git pull upstream master`

## Where to put my files

#### Where to put my prefabs:
`Resources/Things/[here]`

#### Where to put my materials and scripts
`CREATORS/[your first name]/[here]`

#### Where to put my sound files
`Resources/Sounds/[here]`


## How to use your own sound files

Find a sound file that is one of the formats below

- .Aif.
- .wav.
- .mp3.
- .ogg.

Compress it if necessary, make sure its file size is smaller than 2 MB, and move it into `Resources/Sounds` folder.

Use `PlaySound(“filename”)` to play your sound.


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




