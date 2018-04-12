ThingThingThing: Unity Workshop and Collective Creation
===================

Saturday, April 14 and Sunday, April 15, 1:00-5:00pm

Asia Art Archive in America

43 Remsen Street, Brooklyn, NY 11201



This event is free and open to the public, but space is limited and registration is required

Please register [here](https://www.eventbrite.com/e/thingthingthing-unity-workshop-and-collective-creation-tickets-44722475127)

An art video game is like daydreaming – a dream that one can go back to over and over again. Objects within the Game are external manifestations of their creators’ spirits. While their creators are tied up with the reality of life, these tiny Objects awaken in this wondrous space of “grandeur” (Gustave Bachelard, 1948), brought to life through the imagination of the Game creators. They twist, turn, wiggle, roll around. On a sunny day, they wander within the Game land, make a friend, sing a song by the river. Gently, they bring together heaven and earth, and open their creators up to the future of reality.

ThingThingThing is an experimental collaboration between Asia Art Archive in America and the artist duo ZZYW, formed by Yang Wang and Zhenzhen Qi. Participants will spend two action-packed days at AAAinA, learning the fundamentals of video game development and making a collective art game along the way. At the end of the two days, the result is a film of the Game that generates its own plot in real time, created by all of the participants using Unity, a video game development platform, and C# as the programming language.

Day One (Saturday, April 14): participants will be supplied with virtual 3D models that roughly invoke humans, animals, or objects. They will learn about creating materials, editing textures, as well as using computer code to manipulate the location, movement, rotation, and size of the models.

Day Two (Sunday, April 15): participants will be supplied with simple computer code, which can be attached to the 3D model and give instructions for how the model should act, such as “make a sound” when it approaches the river or “disperse tiny stars” when meeting a new friend. Participants are also welcome to modify or write their own code to create more involved Game play.

Throughout the entire workshop, the artists will collect the 3D models, or “Things”, made by the participants and place them into a virtual world. At the end of the two-day workshop, we will play the final version of the Game in AAAinA’s screening room and watch our models explore their world and meet each other – thing to thing, while we also get to know one another, human to human.



## Steps for participants:

[Google Doc Link](https://docs.google.com/document/d/18rqBA01xjrEOiLuYqoa7b_HeCmha066y6eLI37iUFIA/edit?usp=sharing)




## Public Properties

### Environment

`float TOD_Data.main.TimeNow`

e.x. 3:30PM will be represented as 15.5

`bool TOD_Data.main.IsDay`

`bool TOD_Data.main.IsNight`

### Events

`void OnSunset();`

`void OnSunrise();`

`void OnMeetingSomeone(GameObject other);`

`void OnLeavingSomeone(GameObject other);`

`void OnNeighborSpeaking();`

`void OnNeigborSparkingParticles();`


### Ready to Use Behaviours

`void SetTarget(Vector3 target)`

`void RotateSelf(Vector3 angle)`

`void SetScale(Vector3 newScale)`

`void Speak(string content, float stayLength)`
`void Speak(string content)`

`void Spark(Color particleColor, int numberOfParticles)`
	
`void PlaySound(string soundName)`








