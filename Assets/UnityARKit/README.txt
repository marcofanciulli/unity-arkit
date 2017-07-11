Environment:
	iOS 11
	Xcode 9
	MacOS 10.12.5
	Unity 5.6.2f1

1. Download the UnityARKit.unitypackage.
2. Open your Unity project or create a new one.
3. Switch platform to iOS.
3.1 IMPORTANT! PlayerSettings->Settings for iOS->Debugging and crash reporting-> Camera Usage Description field must be filled.
4. Import the UnityARKit by either double clicking the extensions *.unitypackage file on your file system, or by selecting Assets/Import Package/Custom Package in the Editor's menu and then selecting the *.unitypackage on your file system.
5. The extension archive will self install into your project.
6. Open the /UnityARKit/Prefabs folder.
7. Delete the “Main Camera” in your current scene hierarchy, and drag an instance of the CameraPrefab into your scene.
8. Drag an instance of the HitPrefab into your scene. 
8.1 If you need shadows, drag an instance of the shadowPlanePrefab into your scene.
9. Create a simple Empty object (e.g. ARCameraManager). Drag an instance of the UnityARCameraManager.cs into your ARCameraManager and select Main camera into Inspector.
9.1 If you need shadows, create a simple Empty object (e.g. ARPlane). Drag an instance of the UnityARGeneratePlane.cs into your ARPlane and select shadowPlanePrefab into Inspector.
10. Open Window->Lighting->Settings->Scene tab. Change Environment->Skybox Material to None and change Environment Lighting->Source to Color.
11. Build Unity project.