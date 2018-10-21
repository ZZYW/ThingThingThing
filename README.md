<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [About The Project](#about-the-project)
- [Past Events](#past-events)
- [Steps for participants:](#steps-for-participants)
- [Technical Document](#technical-document)
  - [Properties/Fields For You To Get](#propertiesfields-for-you-to-get)
  - [Events You can Use to Fill Code Inside](#events-you-can-use-to-fill-code-inside)
  - [Settings of your Thing you can change](#settings-of-your-thing-you-can-change)
  - [Ready to Use Methods](#ready-to-use-methods)
- [Tips](#tips)
  - [How to keep your cloned repository up to date](#how-to-keep-your-cloned-repository-up-to-date)
  - [Where to put my files](#where-to-put-my-files)
      - [Where to put my prefabs:](#where-to-put-my-prefabs)
      - [Where to put my materials and scripts](#where-to-put-my-materials-and-scripts)
      - [Where to put my own sound files](#where-to-put-my-own-sound-files)
  - [How to use your own sound files](#how-to-use-your-own-sound-files)
  - [Some Basic about C# Programming](#some-basic-about-c-programming)
  - [Some useful methods from Unity](#some-useful-methods-from-unity)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# About The Project

_ThingThingThing_

_ThingThingThing_ explores open source software development’s potential as a framework for generative narrative made collectively.

_ThingThingThing_ is a zero-player game. The artists and contributors jointly determine its evolution by setting an initial seed state of each _Thing_, which later requires no further human control. Viewers interact with _ThingThingthing_ by navigating a giant eyeball and observing how the world evolves. _ThingThingthing_ is an unending, three-dimensional system of instantiations, each of which is populated by an individual contributor. _ThingThingthing_’s aggregation algorithm copies individual avatars into an upstream account, enabling them to interact with each other.

The artists have been hosting a series of workshops, enabling anyone from students, designers to technologists to quickly build creatures with intelligence and interaction. One can also contribute remotely through _ThingThingthing_’s online development environment. _ThingThingthing_ is not perfect. Anyone can make a _Thing_, with its unique glitches, awkwardness, and clumsiness. Once creators send off their code, they walk away from the keyboard and computer screen, let their Things take on a life of its own in the universe of _ThingThingthing_.

Prior _ThingThingthing_ collaborations also include

- Macy Art Gallery, Teachers College Columbia University;
- Creative Tech Week(CTW) New York;
- Asia Art Archive in America.

<!-- ---------- -->

<!-- # Collaborators and Their _Things_

- Zhenzhen  `PrinceZ`
- Yang `Elo` & `Dummy`
- Jingling `JZ` & `JZPig`
- Evian `Cloud Cloud` & `Sheep_Mushy`
- Sara `Margarita` & `Tomas`
- JHMun `Chicken` -->

---

# Past Events

- Saturday, April 14 and Sunday, April 15, 1:00-5:00pm | Asia Art Archive in America | 43 Remsen Street, Brooklyn, NY 11201

---

# Steps for participants:

[Google Doc Link](https://docs.google.com/document/d/18rqBA01xjrEOiLuYqoa7b_HeCmha066y6eLI37iUFIA/edit?usp=sharing)

---

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
protected override void OnSunset(){}
protected override void OnSunrise(){}
protected override void OnMeetingSomeone(GameObject other){}
protected override void OnLeavingSomeone(GameObject other){}
protected override void OnNeighborSpeaking(){}
protected override void OnNeigborSparkingParticles(){}
protected override void OnTouchWater(){}
protected override void OnLeaveWater(){}
```

## Settings of your Thing you can change

```csharp
//value is the default setting
settings.cameraOffset = 15;
settings.acceleration = 4;
settings.drag = 1.8f;
settings.mass = 0.2f;
settings.getNewDestinationInterval = 5;
settings.newDestinationRange = 40;
settings.alwaysFacingTarget = true;
settings.myCubeColor;
```

## Ready to Use Methods

```csharp
//movement
SetTarget(Vector3 target);
StopMoving();
StopMoving(float forHowManySeconds);
RestartWalking();
RandomSetDestination();//get a new random target
ResetPosition(); //change position to spawn point

//shape and form
SetScale(Vector3 newScale);
ChangeColor(Color newColor); //change color, might not work well if you have more than one renderer or more than one material
ResetColor(); //reset to original color

//social
Speak(string content, float stayLength);
Speak(string content);
Mute(); //Speak no longer works
DeMute(); //regain ability to Speak again
Spark(Color particleColor, int numberOfParticles);
PlaySound(string soundName);
CreateCube(); //throw a cube on the small ground
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

`CREATORS/[your first name]/[here]`

#### Where to put my own sound files

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
