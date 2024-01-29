

# Soundboard Of Siofria

A solution for DIY soundboards, inspired by a lack of such solution for my DM-ing needs. I only use the laptop while DM-ing to curate music, and really believe in the "no technology at the table" philosophy, BUT even curating music in the browser/Spotify is still way more interaction with a computer than I would like to have. 
Introducing the **Soundboard Of Siofria**, an application that you configure once, plug in your external (preferably wireless) keyboard, leave the laptop on the side, and rule over ~100 sounds/songs under your fingertips.
In more technical terms: this app allows the user to map the inputs from an external keyboard to certain sounds. These sounds are then played when corresponding keys are pressed, without interfering with the rest of the system.  

##
![soundboard](https://github.com/BarlowTheKeeper/SiofriaSoundboardProject/assets/147258093/c2c82a54-9618-444a-98cb-4c4d7285a483)
##

**To install the software follow** [this link](https://github.com/BarlowTheKeeper/SiofriaSoundboardProject/releases/tag/0.0.4) or click on Releases in the sidebar on the right.


### SUPPORTED AUDIO FILE FORMATS ARE  `.MP3` & `WAV`


## Getting Started

 1. **Download the installer** [**here.**](https://github.com/BarlowTheKeeper/SiofriaSoundboardProject/releases/download/v0.0.1/Soundboard.of.Siofria.exe) 
 
 2. **Run the installer.** That this will not really install the application, it will just unpack it to a location you choose. **Important:** *Set a location that doesn't need admin privileges to access (or leave default)*
 
 3. **Install dependencies.** If you don't have `.NET Desktop Runtime` you will be prompted to install it. In case you are not prompted, the .NET installation files are in the `redist` directory (in the unpacked directory from step 2)
 
 4. **Plug in the keyboard you want to use as a soundboard**
 5. **Run the program**

The basic idea is that there are actually **2 programs** that need to be running for the app to work. 
- The UI window you will be presented with. 
- The one managing the keyboard input.

**Both need to be up and running for everything to work.**

The status of the second program is shown as colorful text in the bottom left corner of the window. 

6. **Wait for the status to change to**  `"Press Enter to register the keyboard!"` or `"All Good!"` 

7. **Press the Enter key on the external keyboard.** This will tell the application *which keyboard to block*. 

**Note:** This keyboard's **input will not be registered by any other programs** on the computer until the application is closed. If for any reason you want to get the keyboard "unstuck",  click on the status text `All Good!` in the corner and it will restart the underlining keyboard input process releasing the keyboard. 

**Note:** If you ever get stuck with a non-functioning keyboard and don't have the Soundboard app open, you can kill this keyboard input process under the name `SiofriaKeyboardHookEngine`

8. **You are all set!** Import someone else's sound package or create your own. I unfortunately couldn't share mine because of some licensing problems. But I can recommend https://freesound.org/ as a huge library of free sounds compatible with this app! 

9. **Visit** [**The Barlow Keep**](https://www.youtube.com/@BarlowKeep) YouTube channel for a more in-depth feature/usage overview! [This video is a bit outdated after v0.0.3, but still useul to get a general idea]

10. **Coffee?** *This project is completely Open Source and maintained by one person, if you like it consider* [**buying me a coffee :)**](https://ko-fi.com/barlowkeep)


## How to use & Feature list 

When your **external keyboard** is connected and you have **pressed enter** and made sure it is **not typing** you are *ready to use it*. 

Pressing almost any button (some don't work and some are reserved) will add a **new entry** to the table on the **left side** of the app window. 

Whether you **press a key** or **left-click the entry** in the table, it will open **clip settings** for that key in the right side of the app window.  You will be adding **.wav** or **.mp3** files and configuring their volume and **playback here**.

***Note:** You might notice that there is **no sound when you press a key**. Make sure the **"Play!"** button is **toggled on!***
 
 ### Clip Settings 
 **Warning**: Any settings configured here will **NOT** be **automatically applied**. You need to click the **Apply button** or press the **Enter key** on any of your keyboards!
 
 #### Fade in/out
- **Fade In**: Check the box to enable it and write the amount of fade in seconds in the input field (not supported for looping sounds)
- **Fade Out**: Check the box to enable it and write the number of fadeout seconds in the input field (partially works for non-looping sounds...try it anyways)

 #### Playback
 - **Loop**: Should the sound be looped (continue playing after it is done)
 - **Stream**: Should the file be loaded or streamed from the disk (always leave this on for bigger files)
  
 #### Cut Range
 - **Enable:** Should this sound be cut (change the beginning and the end point of the playback)
 - **First input field** sets how much into the sound to start playing
 - **Second** sets how long after the first sound should be played.
 
 **Note:** These settings are buggy at best for some sounds & they don't always work with looping sounds.
 
#### File
Any **.wav** or **.mp3** can be selected from the computer by clicking on the **Browse** button or **dragging and dropping** the file into the **File** rectangle.

**Recommended file length** is up to **10 minutes** (but it works with >1h files if *stream is checked*...although a bit unstable)


#### Volume
All files are **played with normalized volume**, so all sounds should sound the same (in terms of volume) when this setting is set to 100%. 

Controlling this slider you control **how loud the sound playback will be**.
 
 
 ### Loading and Saving
  
The app recognizes **2 different** ways to create a sound mapping and save it. 
**1. Configs
2. Packages**

#### Configs
When you start adding files from your computer to the program, the program **will not copy the files** anywhere. When you use **File->Save** or **File->Save As** settings you are **telling the program to create a save file** that when **File->Load** is used tells the program where to find sounds on your computer.

**This save file will not work on other people's computers** and will also not be able to read sounds that **change location** on your computer (if *cat_sound.wav* is **deleted** or **moved** the program **won't be able to play it**).
 
#### Packages
Packages are the way to **copy all files** that you have added to the application into one **single archive** that **can be moved between the computers** and is **non-dependent on the original file locations** or the original save file.

If you go to **File->Packages** there are the following options:

 - **Export**: Opens a dialog for you to choose the location. Copies all the files currently added to the application into the directory of your choice, creates a save file inside it, and exports it as a **.zip** archive
 
 - **Import**: Opens a dialog for you to choose an already existing **.zip** archive (must be exported from this application) and imports it into the packages list. You will see all the sounds from this package load into your application **ready to be used!**
 
 - **Manage**: Opens another window in the right panel. From here you can see all your imported packages and manage them. Available options are:
	1. **Load a package:** Select a package from the list and press this button to load it
	2. **Duplicate**: This is used if you want to create another package based on the one selected. It will prompt you to enter a new name.
	3. **Import**: Works the same as above described (if it is buggy just use the one above)
	4. **Delete**: Removes the package from the app. Do not remove the original .zip archive

**Note**: All packages are saved in  **AppInstallDir/packages/**

**Note**: There is **no way to add sounds directly to the package yet**. That will come in the **next update**. As of now, all sounds that you add to the application after the package is imported, will just change the package save file (to tell the program where they are) and **won't be copied to the package folder**.  
**Workaround** is to **Export** and then **Import** the package **again** when you are done adding new sounds.

 ### Other Features
 - **Tools/Always On Top**: Sets this window as "always on top", no other app window can be on top of it. This is useful if you want to have the app window always visible
 - **All playing sounds** will be labeled with red text in the table on the left
 - It is possible to **delete the mappings** from the table on the left by selecting one or multiple entries (multi-select by holding CTRL) and pressing the Delete key on your primary keyboard
 - **Stopping all sounds** is done by clicking the Stop! button or pressing Backspace on your soundboard (external keyboard). 
 - T**ools/Clear Cache**: Removes the reference to which file was open last (as the app will always try to reopen the last file you worked on)
 - **Tools/Apply to other sounds**: The opened clips settings (applied) will be also applied to all other selected sounds from the table on the left.
 -  **Checking for updates**: The app will check on startup if there is a new release and inform you. I haven't done the "don't show this again" feature yet, but it is **coming in the next release.**
 
 
 
## Converting packages from 0.0.3 to 0.0.4

Let's say you have a package **my_package.zip**

1. **Unzip** it so you get a folder called **my_package**

**Inside** of it there is a folder called **SbPkg** and inside are 2 things:
- **Audio** folder
- **config.sbcfg** file

2. Move these two from **SbPkg** into **my_package**. Now inside **my_package** you should have:

- **Audio** folder
- **config.sbcfg**
- **SbPkg** (which is empty)

3. **Delete SbPkg.**

4. **Open** the **config.sbcfg** file in any **text editing** software (notepad, notepad++, sublime text, etc) and 
**replace all** occurrences of `"SbPkg\\Audio"` with `"packages\\my_package\\Audio"` 

5. **Save** **it** and exit the text editor.

6. Zip **my_package** directory (using built-in windows archiver, Winrar, 7z, etc), but make sure it is **.zip** and **not** **.rar** or **.7z**

**Done!!**

> **And I am very sorry I promise I will never break backward compatibility again**


## How to build
The project consists of 2 solutions. 
1. **C++ Solution for input grabbing**, split into 2 smaller projects:
	-  **Console app** using raw input to detect which keyboard produces the input
	-  **DLL** with low-level keyboard hooks that actually block the input from reaching other applications 

When built, both projects produce artifacts in the same dir. (for example `SiofriaKeyboardHookEngine\x64\Debug`, depending on the build config you use)

2. **.NET Solution that manages the C++ process**, gets keyboard input from it and processes that input. This is where all the UI and features are implemented.

Build this as a regular Visual Studio project, copy the artifacts from solution #1 next to the newly built binaries and you are all set to use/further develop the app.

## Roadmap & Known Bugs [things I am already working on - not in this particular order :) ]
1. Feature to add sounds directly to a package! 
2. "Don't show this again" for the update.
3. Fix fade-in/fade-out for non-looping sounds (it has proven much more complicated than I imagined)
4. Fix backwards compatibility
5. Implement proper fade-in for looping sounds (only first playback fades in - only last playback fades out)
6. Cuttoff range check & message for the user
7. Mixer volume view of all sounds (maybe some gain settings) 
8. A help page inside the app (this should be the first in the list I know!)
9. Interface with Lifx for light sync with music. (PoC in progress)

Please report any issues you come across, here on Git Hub. You can also attach the application logs so that I can see what the errors were. 
Logs are located in the place where you installed the application. Look for files with names of this example format: "Log-10-27-2023.txt"
