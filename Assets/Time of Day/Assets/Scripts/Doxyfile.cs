/*! \mainpage

\tableofcontents



\n \section about About

Time of Day is a package to render realistic dynamic sky domes with day and night cycle, clouds, cloud shadows, weather types and physically based atmospheric scattering.<br>

<br><strong>Sky:</strong><br>
- Physically based sky shading<br>
- Rayleigh & Mie scattering<br>
- Highly customizable<br>
- Dynamic weather manager<br>
- Presets for various locations & planets<br>
- Specially optimized sun shafts<br>

<br><strong>Clouds:</strong><br>
- Dynamic wind speed & direction<br>
- Configurable shape, color & scale<br>
- Correctly projected cloud shadows<br>

<br><strong>Time & Location:</strong><br>
- Dynamic day & night cycle<br>
- Full longitude, latitude & time zone support<br>
- Full Gregorian calendar support<br>
- Continuously animated moon phases<br>

<br><strong>Performance & Requirements:</strong><br>
- Extremely optimized shaders & scripts<br>
- Supports shader model 2.0<br>
- Supports all platforms<br>
- Supports linear & gamma color space<br>
- Supports forward & deferred rendering<br>
- Supports HDR & LDR rendering<br>
- Supports virtual reality hardware<br>

<br>[ <a href="http://goo.gl/jAvPoE">Forum Thread</a> | <a href="http://goo.gl/uwZsbo">Web Player</a> | <a href="http://goo.gl/n0RP0i">Documentation</a> ]<br>

<br><strong>You can expect a thoroughly documented, well-written and highly optimized code base.<br>
All equations used in the shaders and scripts include references to the scientific papers they are based on.</strong>



\n \section start Getting Started

1. Add the sky dome to your scene:
 - Drag the prefab found in "Sky Assets/Prefabs" into your scene
 - If your scene uses fog, it is recommended to enable "World -> Set Fog Color" in the prefab inspector
 - If your scene uses ambient light, it is recommended to enable "World -> Set Ambient Light" in the prefab inspector
 - Tweak the other parameters until you are satisfied with the result

2. Move the sky dome to the camera position in every frame:
 - Select your camera and add the Time of Day camera script (Component -> Time of Day -> Camera Main Script)
 - Drag your instance of the sky dome onto the the "Sky" property of the script
 - Set "Dome Pos To Camera" to enabled if it is not already
 - Tag this camera game object as "MainCamera"

3. Automatically fit the sky dome size to the far clip plane:
 - Go to the Time of Day camera script of your camera
 - Set "Dome Scale To Far Clip" to enabled
 - Set "Dome Scale Factor" to a value suitable for your specific scene and platform

4. Render sun shafts on the main camera:
 - Select your camera and add the Time of Day sun shaft script (Component -> Time of Day -> Camera Sun Shafts)
 - Drag your instance of the sky dome onto the the "Sky" property of the script
 - Tweak the parameters until you are satisfied with the result

<strong>REMARK:</strong>
The camera script moves the sky dome directly before clipping the scene, guaranteeing that all other position updates have been processed. You should not move the sky dome in "LateUpdate" because this can cause minor differences in the sky dome position between frames when moving the camera.



\n \section cycle Day & Night Cycle

The sky dome prefab has a script TOD_Time attached to it that manages the dynamic day & night cycle. Enabling and disabling this script enables and disables the automatic cycle.

The following parameters are being set by the script:
- TOD_Sky.Cycle.Hour
- TOD_Sky.Cycle.Day
- TOD_Sky.Cycle.Month
- TOD_Sky.Cycle.Year
- TOD_Sky.Cycle.MoonPhase



\n \section weather Weather Manager

The script TOD_Weather can be used to automatically set various parameters of TOD_Sky according to weather presets.

The following parameters are being set by the script:
- TOD_Sky.Atmosphere.Fogginess
- TOD_Sky.Clouds.Density
- TOD_Sky.Clouds.Sharpness
- TOD_Sky.Clouds.Brightness



\n \section cycle Time & Location Setup

The TOD_Sky.Cycle parameter section allows for configuration of the sky dome to represent the exact sun movement and day length of any location on the planet depending on Gregorian date, UTC/GMT time zone and geographic coordinates. It is important to set the correct time zone with the longitude and latitude of the location to guarantee consistent results with the real world. All of those parameters are completely optional though - if the sky dome should be used in a generic fantasy world they can simply be ignored and left at their default values.



\n \section quality Quality Levels

There are various different quality levels for both the sky dome and the cloud shader. Those quality settings can be configured both dynamically at runtime and directly in the Unity editor window using two inspector enums.

TOD_Sky.CloudQuality:
- Bumped offers complex cloud shading with dynamic density and cloud normal mapping
- Density offers simplified cloud shading with dynamic density but without normal mapping
- Fastest offers extremely simplified cloud shading with simplified cloud shape calculations

TOD_Sky.MeshQuality:
- High sky dome and moon mesh vertex count
- Medium sky dome and moon mesh vertex count
- Low sky dome and moon mesh vertex count



\n \section performance Performance Remarks

- The size of a web player with just the sky dome is only around 200KB as most equations are evaluated dynamically
- All scripts and shaders are highly optimized and will not cause significant FPS drops on desktop computers
- Older mobile devices should switch to the cloud and dome quality settings that offer suitable performance
- The cloud shadows utilize a Unity projector and require another draw call for all objects they are projected on
- The sun shaft image effect is the only component that requires Unity Pro, everything else works just as well on Indie
- Reducing the texture resolution in the cloud texture import settings can increase performance on mobile



\n \section rendering Rendering Order

All components of the sky dome are being rendered after the opaque but before the transparent meshes of your scene.That means that only areas of the sky dome that are not being occluded by any other geometry have to be rendered.

The rendering order of the sky dome components is the following:
- Transparent-500 Unity Skybox (if your scene uses one)
- Transparent-490 Space Dome   (only at night)
- Transparent-480 Moon Mesh    (only at night and if visible)
- Transparent-470 Atmosphere   (if not manually disabled)
- Transparent-460 Sun Plane    (only at day and if visible)
- Transparent-455 Clear Alpha  (if sun shafts are enabled)
- Transparent-450 Cloud Dome   (if not manually disabled)

This leads to 2-5 draw calls to render the complete sky dome depending on the scene setup.



\n \section shaders Global Shader Parameters

There are some global shader parameters set by the TOD_Sky script that can be used in your custom shaders.

Gamma values:
- TOD_Gamma        = TOD_Sky.Gamma
- TOD_OneOverGamma = TOD_Sky.OneOverGamma

Various colors:
- TOD_SunColor      = TOD_Sky.SunColor
- TOD_MoonColor     = TOD_Sky.MoonColor
- TOD_LightColor    = TOD_Sky.LightColor
- TOD_CloudColor    = TOD_Sky.CloudColor
- TOD_AdditiveColor = TOD_Sky.AdditiveColor
- TOD_MoonHaloColor = TOD_Sky.MoonHaloColor

World space direction vectors:
- TOD_SunDirection   = TOD_Sky.SunDirection
- TOD_MoonDirection  = TOD_Sky.MoonDirection
- TOD_LightDirection = TOD_Sky.LightDirection

Sky dome object space direction vectors:
- TOD_LocalSunDirection   = InverseTransformDirection(TOD_Sky.SunDirection)
- TOD_LocalMoonDirection  = InverseTransformDirection(TOD_Sky.MoonDirection)
- TOD_LocalLightDirection = InverseTransformDirection(TOD_Sky.LightDirection)

Those variables can be used in your custom shaders by simply defining uniform variables with the same name. Those will then automatically be filled with the correct values. The Time of Day shaders call pow(color, TOD_OneOverGamma) on the result colors of either vertex or fragment shaders to assure consistent visuals in both linear and gamma space.



\n \section presets Presets

There are several presets that can be applied by clicking the "Choose Preset" button in the TOD_Sky inspector.



\n \section examples Examples

The package comes with various example scripts to demonstrate sky dome interaction.

- AudioAtDay / AudioAtNight / AudioAtWeather:
  Fade audio sources in and out according to daytime or a specific weather type
- ParticleAtDay / ParticleAtNight / ParticleAtWeather:
  Fade particle systems in and out according to daytime or a specific weather type
- RenderAtDay / RenderAtNight / RenderAtWeather:
  Enable or disable renderer components according to daytime or a specific weather type
- DeviceTime:
  Automatically set the daytime to the device time on scene start
- SkyboxGenerator:
  Renders the world around a camera to 6 static images to use in cubemaps or as a Unity skybox



\n \section faq Frequently Asked Questions

Q: How can I use the sky dome with the virtual reality devices like the Oculus Rift?
- Add the TOD_Camera script to one of the cameras (preferably the one that's being rendered first)
- The sky will render correctly without duplicate images or artifacts

Q: How can I use custom skybox at night?
- Disable the child object called "Space"
- Setup your camera to render the skybox of your choice, it will automatically be visible at night

Q: How can I align the cardinal directions with those of the scene?
- Rotate the sky dome around the y-axis such that the sun rises in the east of your scene
- Do not set the scale to negative values as this will lead to rendering artifacts

Q: How can I use lightmapping?
- Disable the shadows of the sun and moon directional lights
- Add a dummy directional light to bake your lightmaps from

<strong>REMARK:</strong>
Lightmaps are not meant to be used with moving light sources as they always represent static lighting conditions. You should therefore always use a static dummy directional light to bake your lightmaps with. Alternatively, you can also
set TOD_Sky.Light.MinimumHeight to 1 to force the sky dome light sources to always be at zenith.

Q: How can I synchronize the sky dome across multiple clients?
- To synchronize the cloud movement, synchronize the property TOD_Sky.Components.Animation.CloudUV of type Vector4
- To synchronize the cycle settings, synchronize the property TOD_Sky.Cycle.Ticks of tyle long

Q: How can I fix Z-fighting and sorting issues with the cloud shadows?
- Adjust the values for "Offset" directly in the shader code of the cloud shadow shaders

<strong>REMARK:</strong>
Offset values have to be constants and can therefore only be adjusted directly in the shader code. Suitable values depend on the depth buffer resolution of the targeted platform and hardware. While the default values work in most scenarios, some scenes might require some further tweaking. If you are having issues with setting those values up correctly, please feel free to contact me.

Q: How can I fix Unity tree creator trees causing issues if cloud shadows are enabled?
- This is a bug of the tree creator shaders if placed using the terrain tools
- To fix it you have to use different shaders for your trees, like Nature/Tree Soft Occlusion Bark & Leaves



\n \section contact Contact Information

If you have any questions that cannot be answered using the FAQ or documentation feel free to contact me:
- In the official <a href="http://forum.unity3d.com/threads/172763-Time-of-Day-Realistic-day-night-cycle-and-atmospheric-scattering">forum thread</a> of the package
- Via <a href="http://forum.unity3d.com/members/30479-plzdiekthxbye">personal message</a> on the Unity community forums
- Via <a href="https://twitter.com/andererandre">Twitter</a>
- Via <a href="http://modmonkeys.net/contact">my website</a>

<strong>REMARK:</strong>
I should always be able to reply within 48 hours. If I did not reply after several days, please try using a different method to contact me as there might be an issue with the one you chose. If I am not available for multiple days I will always try to announce this beforehand in the offical forum thread.



\n \section literature Literature

The following literature has been used to implement physically correct atmospheric scattering:
1. <a href="http://www.cs.utah.edu/~shirley/papers/sunsky/sunsky.pdf">Preetham, Shirley, Smits</a>
2. <a href="http://www.vis.uni-stuttgart.de/~schafhts/HomePage/pubs/wscg07-schafhitzel.pdf">Schafhitzel, Falk, Ertl</a>
3. <a href="http://www-ljk.imag.fr/Publications/Basilic/com.lmc.publi.PUBLI_Article@11e7cdda2f7_f64b69/article.pdf">Bruneton, Neyret</a>
4. <a href="https://engineering.purdue.edu/purpl/level2/papers/egsr2004_riley.pdf">Riley, Ebert, Kraus, Tessendorf, Hansen</a>
5. <a href="http://developer.amd.com/wordpress/media/2012/10/ATI-LightScattering.pdf">Hoffman, Preetham</a>
6. <a href="http://nishitalab.org/user/nis/cdrom/sig93_nis.pdf">Nishita, Sirai, Tadamura, Nakamae</a>
7. <a href="http://etd.dtu.dk/thesis/58645/imm2554.pdf">Nielsen, Christensen</a>

These papers are being referenced in the code in the following way:

    See [N] page P equation (E)

Where the letters are being replaced according to this:
- N: Paper #
- P: Page #
- E: Equation # (if available)



\n \section changelog Changelog

    VERSION 2.0.7
    -------------

    - Fixed an issue where the ambient light color would never fully lerp to the night value

    VERSION 2.0.6
    -------------

    - Replaced Day/Night.AmbientIntensity with Day/Night.AmbientColor to offer more customization options
    - Added Light.AmbientColoring to adjust ambient light coloring at dusk and dawn
    - Added example scripts to enable / disable lights in the scene at day / night / weather
    - Added inspector variable to adjust the time update interval in TOD_Time
    - Added option to use the real-life moon position rather than the fake "opposite to sun" moon position
    - Made all components of TOD_Sky initialize before Start() so that they are accessible from other scripts
    - Disabled the automatic light source shadow type adjustment so that the user can manually set it

    VERSION 2.0.5
    -------------

    - Changed cloud scale parameters from float to 2D vectors to define different scales in x and y direction
    - Fixed TOD_Camera always causing the scene to be edited if enabled
    - Fixed cloud inconsistencies between linear and gamma color space
    - Fixed moon halo disappearing in gamma color space and made the color alpha affect its visibility
    - Fixed an issue where the demo mouse look script could overwrite previously imported Standard Assets
    - Fixed possible sun and moon gimbal lock that could cause them to spin towards zenith
    - Fixed sun shafts being too faint in some setups
    - Improved overall lighting calculations
    - Improved moon visuals
    - Made the sky dome play nice with "depth only" clear flags
    - Made the cloud coloring still darken the clouds even for very low values
    - Made Components.Animation.CloudUV modulo with the cloud scale to avoid unnecessarily large values
    - Added inspector variables to adjust sun shaft base color and sun shaft coloring
    - Added the property Cycle.Ticks to get the time information as a long for easy network synchronization
    - Added the property Cycle.DateTime to get the time information as a System.DateTime
    - Added an inspector variable to set a minimum value for the light source height

    VERSION 2.0.4
    -------------

    - Added a property for the atmosphere renderer component to TOD_Components
    - Added properties for all child mesh filter components to TOD_Components
    - Changed the quality settings to be adjustable at runtime via public enum inspector variables
    - Merged the three prefabs into a single prefab as separate quality prefabs are no longer required
    - Fixed the materials always showing up in version control
    - Fixed the sky dome always causing the scene to be modified and the editor always asking to save on close
    - Fixed the customized sky dome inspector not always looking like the default inspector
    - Improved the performance of all cloud shaders by reducing interpolations from frag to vert
    - Improved the visuals of all cloud shaders and streamlined their style
    - Increased the default cloud texture import resolution to 1024x1024
    - Added a white noise texture for future use

    VERSION 2.0.3
    -------------

    - Fixed all issues with DX11 rendering in order to fully support DX11 from this point on

    VERSION 2.0.2
    -------------

    - Fixed an issue where the image effect shaders could overwrite previously imported Standard Assets

    VERSION 2.0.1
    -------------

    - Changed date and time organization to represent the valid Gregorian calendar
    - Addressed issues with the Unity sun shaft image effect by providing a modified image effect
    - Fixed clouds not correctly handling the planetary atmosphere curvature
    - Fixed clouds not offsetting according to the world position of the sky dome
    - Fixed cloud glow passing through even the thickest of clouds
    - Fixed cloud shadow projection
    - Fixed Light.Falloff not affecting the toggle point of the light position between sun and moon
    - Automatically disable the corresponding shadows if Day/Night/Clouds.ShadowStrength is set to 0
    - Removed Clouds.ShadowProjector toggle as it is no longer required
    - Tweaked the old moon halo to not require an additional draw call and added it back in
    - Made the sky dome position in world space add an offset to the cloud UV coordinates
    - Added Light.Coloring to adjust the light coloring separate from the sky coloring
    - Rescaled some parameters for easier use and tweaked their default values

    VERSION 2.0.0
    -------------

    - Moved all documentation to Doxygen
    - Renamed the folder "Sky Assets" to "Assets"
    - Made the color space be detected automatically by default
    - Reworked the sun texture and shader
    - Allow light source intensities greater than one
    - Reworked the way ambient light is being calculated
    - Reworked the way light affects the atmosphere and clouds
    - Improved all scattering calculations, especially the integral approximation
    - Automatically disable space the game object at night
    - Added a public method to sample the sky dome color in any viewing direction
    - Added a fog bias parameter to lerp between zenith and horizon color
    - Adjusted the atmosphere alpha calculation
    - Added a parameter to easily adjust the scattering color
    - Added shader parameters for the moon texture color and contrast
    - Adjusted the render queue positions
    - Removed the moon halo material as it is no longer required
    - Added the physical scattering model to the night sky
    - Greatly improved the weather system
    - Added fog and contrast parameters to the atmosphere
    - Restructured the parameter classes to be more intuitive to use
    - Moved all component references into a separate class
    - Made the sky presets be applied via editor script rather than separate prefabs
    - Improved cloud shading and performance across the board
    - Removed the cloud shading parameter
    - Added cloud glow from the sun and moon
    - Added sky and cloud tone multipliers to sun and moon
    - Added viewer height and horizon offset parameters
    - Slightly improved overall performance
    - Replaced ambient intensity with two parameters for sun and moon
    - Replaced the two directional lights with a single one that automatically follows either sun or moon

    VERSION 1.7.3
    -------------

    - Added two parameters "StarTiling" and "StarDensity" to the "Night" section
    - Added "Offset -1, -1" to the cloud shadow shaders to avoid Z-fighting on some platforms
    - Tweaked the cloud shader for more consistent results in linear and gamma color space
    - Tweaked the moon texture to be a lot brighter by default, especially on mobile
    - Tweaked the automatically calculated fog color to be similar to the horizon color
    - Removed the property "Brightness" from the moon shader as it is no longer needed

    VERSION 1.7.2
    -------------

    - Fixed the ambient light calculation being too dark, even with high ambient light parameter values
    - Added the properties "SunZenith" and "MoonZenith" to access sun and moon zenith angles in degrees
    - Added a paramter "Halo" to adjust the moon halo intensity and made its color be derived from the light
    - Changed several parameters to be clamped between 0 and 1
    - Changed the name of the property "OrbitRadius" to "Radius"
    - Tweaked the moon phase calculation of both moon mesh and moon halo
    - Tweaked several default parameter values of the prefabs

    VERSION 1.7.1
    -------------
    - Changed the default cardinal direction axes of the sky dome (x axis is now west/east, z axis south/north)
    - Removed the property "ZenithFactor" as it is no longer being used
    - Moved all child object references into a separate toggleable section called "Children"
    - Tweaked the default parameters of the prefabs (brightness, haziness, cloud color, moon light intensity)
    - Tweaked the calculations of the moon light color, ambient light at night and cloud tone at night
    - Tweaked the default sun and moon base color based on good real life approximations
    - Tweaked the moon halo
    - Renamed the parameter "ShadowAlpha" in "Clouds" to "ShadowStrength"
    - Added the parameter "ShadowStrength" for the sun and moon lights

    VERSION 1.7.0
    -------------

    - Fixed an issue where the sun could incorrectly travel around the north,
      even though the location is in the northern hemisphere (Thanks Gregg!)
    - Fixed an issue that led to the brightest parts of the sky dome being slightly too dark
    - Fixed the automatically calculated fog color not being exactly the same as the horizon
    - Added a name prefix to all components to prevent name collisions with other packages
    - Added cloud shadows (can be disabled)
    - Added UTC time zone support
    - Added a parameter to configure the color of the light reflected by the moon
    - Added parameters for wind direction in degrees and wind speed in knots
    - Added an option to automatically adjust the ambient light color (disabled by default)
    - Added a parameter to adjust the sun's light color
    - Added a plane with an additive shader at the sun's position to always render a circular sun
    - Added dynamic cloud shape adjustments to the "Low" prefab (cloud weather types will now also work)
    - Added shading calculations to the "Low" and "Medium" prefabs
    - Improved the performance of "Low" prefab by reducing the vertex count
    - Improved the performance of "Low" prefab by removing the moon halo for that prefab by default
    - Improved the cloud shading of the "High" prefab
    - Improved the visual quality of the weather presets
    - Improved the calculation of the sun's position
    - Changed the automatic fog color adjustment to be disabled by default
    - Changed the moon halo to adjust according to the moon phase
    - Changed the name of the parameter from "Color" to "AdditiveColor" for both day and night
    - Changed the cloud animation to support network synchronization
    - Changed the default tiling of the stars texture to 1 (was 3)
    - Changed the moon vertex count in all presets to scale with the device performance
    - Removed the parameter "CloudColor" from "NightParameters" as it is now derived from the moon light color

    VERSION 1.6.1
    -------------
    - Fixed an issue related to HDR rendering

    VERSION 1.6.0
    -------------

    - Improved the visuals and functionality of the weather system
      (most METAR codes should now be possible to achieve visually)
    - Improved performance of the moon halo shader
    - Added official support for HDR rendering
    - Replaced the sun mesh with implicit sun scattering in the atmosphere layer
      to reduce dome vertex count, draw calls and pixel overdraw
    - Added an additional quality level (now Low/Medium/High instead of Desktop/Mobile)
    - Added sky dome presets from various locations around the globe for easier use
    - Tweaked the wavelength constants a little to allow for a wider range of sun coloring adjustments

    VERSION 1.5.1
    -------------

    - Fixed an issue causing a missing sun material in the mobile prefab

    VERSION 1.5.0
    -------------

    - Enabled mip mapping of the stars texture by default to avoid flickering
    - Added support for using custom skyboxes at night (see readme for details)
    - Greatly improved the parametrization of the sun color influence at sunrise and sunset
    - Added internal pointers to commonly used components for faster access
    - Split the sun and moon parameters into their own property classes
    - Adjusted the cloud shading calculation to keep it from darkening some clouds too much
    - Adjusted the color wavelengths to produce a more realistic blue color of the sky by default
    - Made the moon phase influence the intensity of the sunlight reflected by the moon
    - Replaced the lens flares with custom halo shaders that are correctly being occluded by clouds
    - Enabled the new halo effects on mobile
    - Moved all shaders into a "Time of Day" category
    - Added a basic weather manager with three weather types

    VERSION 1.4.0
    -------------

    - Added "Fog { Mode Off }" to the shaders to properly ignore fog
    - Added the parameter "Night Cloud Color" to render clouds at night
    - Added the parameter "Night Haze Color" to render some haze at night
    - Added the parameter "Night Color" to add some color to the night sky
    - Renamed the parameter "Haze" to "Haziness"
    - Renamed the parameter "Sky Tone" to "Brightness"
    - Renamed the properties "Day" and "Night" to "IsDay" and "IsNight"
    - Restructured all sky parameters into groups
    - Improved the sun lens flare texture
    - Improved the stars texture
    - Fixed a rendering artifact at the horizon for low haziness values
    - Made the scattering calculation in gamma space look identical to linear space

    VERSION 1.3.0
    -------------

    - Greatly improved performance on mobile devices
    - Greatly improved sunset and sunrise visual quality
    - Added a parameter to control how strongly the sun color affects the sky color
    - Added realistic sun and moon lens flare effects
    - Added two additional cloud noise textures
    - Improved handling of latitude and longitude
    - Made the sky dome render correctly independent of its rotation

    VERSION 1.2.0
    -------------

    - Fixed some bugs regarding linear vs. gamma space rendering
    - Fixed some issues with the horizon fadeout
    - Adjusted sun and moon size
    - Optimized sun and fog color calculation
    - Greatly improved visual quality of the cloud system
    - Added parameter to control cloud tone, allowing for dark clouds
    - Added improved stars texture at night
    - Added parameter to control the sun color falloff speed

    VERSION 1.1.0
    -------------

    - First public release on the Asset Store

    VERSION 1.0.0
    -------------

    - First private release for internal use

*/