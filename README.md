# Unity SSX Tricky Level Editor

## Current Features
- Patch Editing
- Texture Libaray Editing

## File Read/Write CheckList
:heavy_check_mark: - Fully Working, :o: - Somewhat Working, :x: - Not Working
| File     	| Reading            	| Loading            	|
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
 
 ## Todo Today
- About Page
- Selectable Splines
- Hide Point
- Hide Gizmo
- Make way to dismiss notifcation
 
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

## Todo For Spline System
- Moveable Points
- Proper Rendering instead of basic line render
- Panel
- Movable

## Todo Change
- Redo Dropdown Menus so it auto adds hide menu
- Tweak Toggle Object Script
- Standardise SSXModder XYZ Points
- Scroll wheel to have variable speed
- Limit Mouse Look so camera doesnt freak out
- Settings for keybindings, workspace path
- Open File for Settings Page

## Todo BugFixes
- Fix Adding Image using 0 as base (I think all i really need to do is set the matrix type)
- Fix Distance Generation Spline (Best guess is that they use the bezier curve generation to do it)

## Todo in Later Update
- Custom File Opener To fix issues with current code and allow cross platform support
- Figure Out Point R3C3
- Threading or warning about exporting taking a while
- Redo Saving/Loading So it works on a project based system and pressing import generates a project
- Skybox Emulation
- Auto Updater
- Fix UI Scaling
- Controller Camera Movement
- Allow Control Of Patch Possition In Tree
- Attempt Auto Sort Of Patches
- Double Sided Model Option
- Show Patch List Postion
- Edge Selector
- Point Combine
- Patch Rotation
- fix Point Colliders, Fix Selection tool it seems to be very broken the further you get from 0,0,0 (While fixed by changing scale want to see if theres a better way)

## Special Thanks
https://github.com/Erickson400/SSXTrickyModelExporter <br>
https://github.com/WouterBaeyens/Ssx3SshConverter <br>
https://github.com/SSXModding/bigfile <br>

## Donate
[![Donate](https://www.paypalobjects.com/en_AU/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?business=VT6TG8KKZM98E&no_recurring=0&currency_code=AUD)