
# Soundboard Of Siofria

A solution for DIY soundboards, inspired by a lack of such solution for my DM-ing needs. I only use the laptop while DM-ing to curate music, and really believe in the "no technology at the table" philosophy, BUT even curating music in the browser/spotify is still way more interatction with a computer than I would like to have. 
Introducing the **Soundboard Of Siofria**, an application that you configure once, plug in your external (preferrably wireless) keyboard, leave the laptop on the side and rule over ~100 sounds/songs under your fingertips.

In more technical terms: this app allows the user to map the inputs from an external keyboard to certain sounds. These sounds are then played when corresponding keys are pressed, without interfering with the rest of the system.  

**To install the software follow** [this link](https://github.com/BarlowTheKeeper/SiofriaSoundboardProject/releases/download/v0.0.1/Soundboard.of.Siofria.exe) or click on Releases in the sidebar on the right.


### SUPPORTED AUDIO FILE FORMATS ARE  `.MP3` & `WAV`


## How to use

 1. **Download the installer** [**here.**](https://github.com/BarlowTheKeeper/SiofriaSoundboardProject/releases/download/v0.0.1/Soundboard.of.Siofria.exe) 
 
 2. **Run the installer.** That this will not really install the application, it will just unpack it to a location you choose. **Important:** *Set a location that doesn't need admin privillages to access (or leave default)*
 
 3. **Install dependencies.** If you don't have `.NET Desktop Runtime` you will be prompted to install it. In case you are not prompted, the .NET installation files are in the `redist` directory (in the unpack directory from step 2)
 
 4. **Plug in the keyboard you want to use as a soundboard**
 5. **Run the program**

Bastic idea is that there are actually **2 programs** that need to be running for the app to work. 
- The UI window you will be presented with. 
- The one managing the keyboard input.

**Both need to be up and running for everything to work.**

The status of the second program is shown as colorful text in the bottom left corner of the window. 

6. **Wait for status to change to**  `"Press Enter to register the keyboard!"` or `"All Good!"` 

7. **Press the Enter key on the external keyboard.** This will tell the application *which keyboard to block*. 

**Note:** This keyboard's **input will not be registered by any other programs** on the computer until the application is closed. If for any reason you want to get the keyboard "unstuck",  click on the status text `All Good!` in the corner and it will restart the underlining keyboard input process releasing the keyboard. 

**Note:** If you ever get stuck with a non functioning keyboard and don't have the Soundboard app open, you can kill this keyboard input process under the name `SiofriaKeyboardHookEngine`

8. **You are all set!** Import someone elses sound package or create your own. I unfortunately couldn't share mine because of some licencing problems. But I can recommend https://freesound.org/ as a huge library of free sounds compatible with this app! 

9. **Visit** [**The Barlow Keep**](https://www.youtube.com/@BarlowKeep) youtube channel for a more in depth feature/usage overview!

10. **Coffee?** *This project is completly Open Source and maintained by one person, if you like it consider* [**buying me a coffee :)**](https://ko-fi.com/barlowkeep)


## How to build
The project consists of 2 solutions. 
1. **C++ Solution for input grabbing**, split into 2 smaller projects:
	-  **Console app** using raw input to detect wich keyboard produces the input
	-  **DLL** with low level keyboard hooks that actually block the input from reaching other applications 

When built, both projects produce artifacts in the same dir. (for example `SiofriaKeyboardHookEngine\x64\Debug`, depending on the build config you use)

2. **.NET Solution that manages the C++ process**, gets keyboard input from it and processes that input. This is where all the UI and features are implemented.

Build this as a regular Visual Studio project, copy the artifacts from the solution #1 next to the newly built binaries and you are all set to use/further develop the app.
