## HybridCLR Hot-Update Template

A Unity project template demonstrating how to use [HybridCLR](https://github.com/focus-creative-games/HybridCLR) for runtime DLL hot-updates and asset-bundle loading.

---

### 🚀 Prerequisites

1. **Unity 2021.3+**, i used **Unity 6000.0.54f1**
2. **HybridCLR package** installed via the top-bar menu:  
` HybridCLR > Installer... > Install` 
(If you see “Installed version: x.x.x,” you’re good to go.)  
3. **.NET Framework** scripting runtime and **IL2CPP** enabled in **Project Settings > Player**.

---

### 🛠️ Project Setup

1. **Assembly Definitions**  
- Under `Assets/HotUpdate/`, create an **Assembly Definition** named `HotUpdate`.  
- In **Project Settings > HybridCLR**, add `HotUpdate` to the **Hotfix Assemblies** list.

2. **Generate AOT Metadata**  
- Menu: `HybridCLR > Generate > All`  
- This compiles your hot-update DLL and extracts AOT metadata needed for any missing native generics at runtime.

3. **Build Assets & Copy**  
- Menu: `Build > BuildAssetsAndCopyToStreamingAssets`  
- This builds your AssetBundles (in this case the folder Prefabs) and places both the `HotUpdate.dll.bytes` and your bundles into `Assets/StreamingAssets`.

---

### 📦 Build & Hot-Update Workflow

1. First you need to do a build as a base project.
2. Once you have a build, then go to the Unity Project and do the changes you wanna see Hot Updated.
3. Re-run steps 2 and 3 in Project Setup
4. Copy the updated `HotUpdate.dll.bytes` and `.bundle` files into your existing build’s `StreamingAssets` folder (e.g. `/Build/TestHotUpdate.app/Contents/Resources/Data/StreamingAssets`).  
5. (iOS/Android only) host these files on your server and fetch them. The core process is identical to Standalone the main difference is how we handle the DLL file paths and deployment method. Hot updates require downloading and `File.WriteAllBytes` new DLLs to `Application.persistentDataPath`.

---

### 🔍 Behind the Scenes

#### 1. DLL Hot-Update

When the user presses the **Hot Update** button:

1. **Download**  
- In this template we read from **StreamingAssets**, but you can swap in `UnityWebRequest` to fetch from a server at runtime.

2. **Load & Initialize**  
- Loads HotUpdate.dll.bytes into memory.
- Loads AOT metadata files (to support generics that Unity didn’t AOT-compile).
- Invokes Entry.Start() via reflection.

3. **Demonstration Logic**  
   - **Logs**:  
     - Outputs logs in build console to confirm the hot-update entry point was invoked.  
   - **Runtime Creation**:  
     - Instantiates a new GameObject and adds a component entirely via the hot-update DLL.  
     - Calls a generic method that constructs a `List<Struct>` to demonstrate support for structs and generics through HybridCLR.  

---

#### 2. AssetBundle Handling

1. **Loading**  
   - Reads bundle bytes from `StreamingAssets` (or use `UnityWebRequest` for remote hosting).  

2. **Load & Instantiate**  
   - Loads the in-memory asset bundle.  
   - Instantiates the “Cube” prefab contained in that bundle.
   - The Cube has a script attached which makes it change color and rotate constantly thats what we change at runtime to illustrate dynamic behavior updates.

---

### ⚠️ Cautions: Stripping & Metadata

Unity’s build process strips out any code paths not referenced by the original player.  
- If the hot-update DLL invokes a method or type never used in the base build, you’ll encounter missing‐method or missing‐type errors at runtime.  
- **Mitigation**  
  - Add dummy references in your main build to all methods/classes you plan to hot-update.  
  - Include the generated AOT metadata assemblies (the “meta” DLLs) alongside your hot-update DLL in `StreamingAssets`.  

