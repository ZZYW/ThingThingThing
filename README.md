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

https://docs.google.com/document/d/18rqBA01xjrEOiLuYqoa7b_HeCmha066y6eLI37iUFIA/edit?usp=sharing

## Detailed Steps for TTT Workshops

1. Fork the TTT repo > https://github.com/ZZYW/thing-thing-thing

2. Clone your own repo to local

3. Duplicate TTTWorld scene as your test scene

4. Set up you model and script on it

5. Make your Thing as a prefab, rename it as *[YourInitial]+[Name]* e.x. YWQui

6. Setting up your prefab

    1. Pick one model from [models] folder

    2. rename the model to your own name

    3. Duplicate Creature.cs, rename it to CreatureXXX.cs

    4. add your "CreatureXXX.cs" to it

    5. open "Prefabs" folder, drag "Particle Explode" and "Chat Balloon" under your model

    6. Adjust the scales

        1. adjust scale of "Particle Explode", make sure particles are visible, use "restart" button to test out

        2. adjust the scale and Y position of chat balloon, make sure it appear on top of your model

    7. [optional] tweak your particleSystem to meet the look you desired

7. Put your prefab into folder Resources/Things, and put your CreatureXX.cs script into */scripts*

8. Add & Commit your YWQui **and** your CreatureXXX.cs and push it to your forked repository on Github

9. Create a pull request to the original TTT repo, we will check your code and merge it into the main repo



### How to use your own sound files

1. Find a sound file that is one of the formats below

    1. aif.

    2. .wav.

    3. .mp3.

    4. .ogg.

2. Compress it if necessary, make sure its file size is smaller than 2 MB, and move it into Resources/Sounds folder

3. Use PlaySound("filename") to play your sound




