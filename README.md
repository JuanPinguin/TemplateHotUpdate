# Hot Update
Instructions to setup the project
First make sure HybridCLR is installed
Going on top bar HybridCLR > Installer... > Install (if you see "installed version:" with a number is already installed)
Then Follow PDF instructions in assets.

Instructions of what are we doing behind the scenes:
After you press the button

- DLL Hot Update
The hot update DLL (HotUpdate.dll.bytes) is downloaded at runtime in this case is locally loaded but this can be replaced with WebRequest.
The DLL is loaded into memory and also some AOT assembly metadata is loaded to support interpreted execution for missing native generics.
The entry point (Entry.Start) is invoked via reflection, enabling hot update logic.
It Debugs a log message of "Hello World Entry.Start", we create a object and add a component in runtime and we run a Generic Method which creates a List of structs just to show the features we can do with Hybrid CLR (You can see all of those initialization in a console that is added in the build)

- Asset Bundle Handling
Asset bundles (like "prefabs") are downloaded similarly to DLLs.
Asset bundles are loaded from memory using AssetBundle.LoadFromMemory.
Here we instantiate a Cube prefab and that cube prefab has scripts in it, which dynamically we change the behaviour by changing its original color and we make it move.

Note: keep in mind that the functionalities we dont use in the build code are gonna be stripped in the code, so for example you wanna change a image sprite, in the hot update but that code hasnt been used in build then is gonna return an exception unless we add the Meta Assembly Files first.

For more info check the PDF i added in the project Assets.

