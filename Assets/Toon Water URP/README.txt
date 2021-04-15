Hello, thank you for purchasing my asset!
If you have questions or problems please let me know at piotrtplaystore@gmail.com

If you like my asset don't forget to write a review it helps me and my future customers a lot :)!

Setup for mobile & normal version of the shader:
I) Make sure you have URP version at least 7.2.1 (it will work on older version but shadow problem can occur when using cascades) to do so:
    1. Go to Package Manager
    2. Search for Universal RP on the list
    3. If your version if under 7.2.1:
        - Click on Universal RP
        - Click the arrow on the left side to show more options
        - Click on newer version number that just showed 
        - Click 'Update to <verion_you_selected>' on the right side under description of package

II) Now we have to make sure that depth texture and opaque texture option (only if you use refraction) is enabled:
    1. Go to your URP settings files (usually they should be in the folder called Settings folder in the Project inspector)
    2. In every settings file (UniversalRP-HighQuality, UniversalRP-LowQuality, UniversalRP-MediumQuality) 
    make sure option called Depth Texture and option called Opaque Texture is checked (you can leave the second setting disabled if don't want refractions)
    3. When you enabled Opaque Texture option you can see you can choose Opaque Downsampling option 4x Bilinear is the fastest but will result in pixelated refractions, 
    None is the slowest but will make refractions look the best. Setting this option is up to you and your performance preferences.

IIIA) If you want to use Planar Reflections:
	1. Select your scene camera
	2. Add script called "Planar Reflections Renderer" to the camera
	3. Remember this effect is resources intense (this option renders again everything above water surface) play around with settings in Planar Reflections Renderer to get better performance
	4. To make the reflection of the water visible Fresnel Power parameter must be above 0! 

IIIB) If you don't want to use Planar Reflections:
	1. MAKE SURE THE PARAMETER Reflection Visibility IN WATER MATERIAL YOU'RE USING IS SET TO 0 TO AVOID ARTIFACTS!

IV) Create material or apply premade by me Toon Water Material to surface that should be water in your scene! 

V) Done I hope you will like my asset. Remember to write me an email if you have any problems. 

When using ORTHOGRAPHIC CAMERA and mobile version of the shader be sure to check the option called Using Orthographic Camera in your material!!!!


When using the mobile version:
Mobile version of the shader is designed to give you the ability to control what features of the shader are enabled/disabled here are available options:
-Stylized Specular Enabled - decide if you want to use stylized specular or unity build in, leaving this option disabled will give you a performance boost
-Refraction Enabled - performance heavy option if enabled, if this option is enabled you have to have opaque texture option enabled!!
-Using Orthographic Camera - to squeeze out every performance bit I could you have to switch from perspective to orthographic mode manually
-Use Normal Texture - performance heavy option if enabled, should surface of water be flat or have waves?

What mobile version lacks compared to normal:
-Support for stylized specular for point lights 
-Procedurally generated foam & normals, mobile version uses textures
-Automatic switch from orthographic to perspective, in the mobile version you have to click the checkbox manually in material inspector
-Fresnel effect
-Planar reflections

Useful pieces of information:
- When Fresnel Power is set to 0 it means Fresnel effect and Planar Reflections are disabled 
- To control the transparency of the water use alpha values of Shallow and Deep watercolor
- Specular Edges Smoothness Factor - is used to smooth out the edges of toon specular highlight however setting this value too big will make your highlight look not sharp
- Use Refraction In Depth Based Water Color - decide whether depth gradient should also wave or be static