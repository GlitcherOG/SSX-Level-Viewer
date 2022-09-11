# Unity SSX Tricky Level Editor

## Current Features
- Patch Editing
- Texture Libaray Editing

## File Read/Write CheckList
:heavy_check_mark: - Fully Working, :o: - Somewhat Working, :x: - Not Working
| File     	| Reading            	| Saving            	|
|----------	|--------------------	|--------------------	|
| .adl     	| :x:                	| :x:                	|
| .api     	| :x:                	| :x:                	|
| .ltg     	| :x:                	| :x:                	|
| .map     	| :heavy_check_mark: 	| :heavy_check_mark: 	|
| .pbd     	| :o:                	| :o:                	|
| .sop     	| :x:                	| :x:                	|
| .ssf     	| :x:                	| :x:                	|
| .ssh     	| :heavy_check_mark: 	| :heavy_check_mark: 	|
| _L.ssh   	| :x:                	| :x:                	|
| _sky.ssh 	| :heavy_check_mark: 	| :x:                	|
| _sky.pbd 	| :x:                	| :x:                	|
 
## Next Big Update
- Loading and Saving the Entire PBD File
- Figure Out Point R3C3
 
## Todo add
- Xray Mesh
- Patch Panel Texture Selector
- key to focus on object
- Add Patch Size Warning
- Highlight View Based on Patch Type
- Auto Save View Modes
- Hotkey For toggling No Light Mode
- UV Editing Clicking Points to edit UV and fix colors
- Texture Flipbook Support
- ImgBurn Support
- Buttons to make spline ends connect
- Instance Objects Spawning
- About Page
- Hide Point
- Hide Gizmo
- Make way to dismiss notifcation/Nofication Sounds

## Todo Change
- Redo Dropdown Menus so it auto adds hide menu
- Tweak Toggle Object Script
- Standardise SSXModder XYZ Points
- Scroll wheel to have variable speed
- Limit Mouse Look so camera doesnt freak out
- Settings for keybindings, workspace path
- Open File for Settings Page
- Fix Darkened Bug

## Todo BugFixes
- Fix Adding Image using 0 as base (I think all i really need to do is set the matrix type)
- Fix Distance Generation Spline (Best guess is that they use the bezier curve generation to do it)
- Fix Mesh Generation Spline

## Todo in Later Update
- Custom File Opener To fix issues with current code and allow cross platform support
- Threading or warning about exporting taking a while
- Redo Saving/Loading So it works on a project based system and pressing import generates a project
- Skybox Emulation
- Auto Updater
- Fix UI Scaling
- Controller Camera Movement
- Allow Control Of Patch Possition In Tree
- Attempt Auto Sort Of Patches
- Show Patch List Postion
- Edge Selector
- Point Combine
- Rotation Tool
- fix Point Colliders, Fix Selection tool it seems to be very broken the further you get from 0,0,0 (While fixed by changing scale want to see if theres a better way)
- mathf half to float

## Special Thanks
https://github.com/Erickson400/SSXTrickyModelExporter <br>
https://github.com/WouterBaeyens/Ssx3SshConverter <br>
https://github.com/SSXModding/bigfile <br>

## Donate
[![Donate](https://www.paypalobjects.com/en_AU/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?business=VT6TG8KKZM98E&no_recurring=0&currency_code=AUD)



Using Model Offset and model Points you can go to a model data header
Model Data first 80 bytes Header
next 4 bytes declares length