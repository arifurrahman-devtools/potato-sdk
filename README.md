# PotatoSDK
This repository is maintained with the motivation of making SDK integration easier or faster for the developers of Alpha Potato. Please follow the document and keep an eye out for changes when making a new integration. For any queries or feedbacks feel free to give me a knock!

### Quick Links:
* [Getting Started](#getting-started)
* [Building For Android](#building-for-android)
* [About Analytics](#about-analytics)
* [SDKs](#sdk-walkthroughs)
  * [Facebook](sdk_docs/facebook/README.md)
  * [GameAnalytics(GA)](sdk_docs/game_analytics/README.md)
  * [Adjust](sdk_docs/adjust/README.md)
  * [Firebase](sdk_docs/firebase/README.md)
  * [Max Ad Mediation](sdk_docs/max/README.md)
  * [InAppPurchase](sdk_docs/iap/README.md)
* [Additional Features](#additional-features)
  * [AB testing](sdk_docs/ab_test/README.md)
  * [GDPR](sdk_docs/gdpr/README.md)
* [Building For iOS](#building-for-ios)

## Getting Started
#### Environment Setup
* Make sure you are using minimum recommended Unity version: 2021.1.16f1
* Setup your project properly at the beginning (to avoid future complications)
  * packageName or bundleID.  
  * Version and bundle version code (both will change with submissions)
  * (Android) Scripting backend =  IL2CPP
  * (Android) target architectures = armv7 and arm64
  * (Android) Sign your application and create a signed build. 
  * (Android) If you have an android manifest you should change the debuggable attribute to false
* While copying IDs or keys (e.g. ad unit ID, app token etc) make sure to check for white space characters on both end of the key
* You might want to add [logs-viewer](https://assetstore.unity.com/packages/tools/integration/log-viewer-12047) package for viewing logs on mobile devices


#### Using PotatoSDK
* Download the PotatoSDK. Check in the release section for latest package.
* Import the PotatoSDK unitypackage to your project. 
* You can use the “PotatoSDK” menu to “Open Config Prefab” and access wrappers relevant to your implementation.
* You can activate modules after importing relevant SDKs. 
* The activate button is platform dependent (if you  activate for android, you will still need to activate when you move to iOS).
* Recommended to allow using the potatoSDK to add a loading scene when prompted.

#### Managing “External Dependency Manager(EDM)”/”Play Service Resolver(PSR)” 
* Always check when you import a new package, if it uses the External Dependency Manager or Play Service Resolver. 
* And if it does, check if you already have one of them(and which version) in your projects.
* You have to keep in mind that PSR is a lower version of EDM.
* Check for the following situations.
  * Your **project has Lower version** of EDM/PSR than the SDK:
    * First, delete the EDM/PSR folder,
    * Then import the new sdk
  * Your **project has Higher version** of EDM than the SDK:**
    * While importing the SDK, uncheck the EDM/PSR folder then import it.

## Build Guidelines For Platforms
The PotatoSDK is prepared with the assumption that the builds are usually stabilized in android before iOS. The following sections describes the recommended practice while building for android. The next section describes SDKs and feature specific workflows, followed by a section on iOS build practices.

## Building For Android
### Situations to Delete and Resolve Libraries
When working on andorid and if your project contains either of EDM/PSR, check for the following situations:
* Any new sdk package imported or upgraded (if you are confident that the package does not contain native android code, you can skip this)
* Change to bundle id
* Change to gradle/android manifest (usually due to new package import or update)
* Working on a new computer

If any of the situations arise please make sure you do the following:
* Assets/External Dependency Manager or Play Service Resolver/Android Resolver/ Delete Resolved Libraries
* Assets/External Dependency Manager or Play Service Resolver/Android Resolver/ Resolve

### Before Build Submission
* Your android manifest must have the following attribute inside its application element. `android:debuggable="false"`
* Max Upgrade: If applicable upgrade max plugin, and all the mediated network that needs upgrade.After that delete all resolved libraries and resolve again.
---

## SDK and Feature specific walkthrough

### About Analytics:
The analytics platforms that are used for games are constantly changing. On the other hand,  specific calls and its format can vary from game to game. Thus, for analytics calls you will have to make your calls according to the documentation of each SDK that you need (Potato SDK doesn't have a wrapper for common analytics calls, but will handle initialization of  the SDKs). You need to`verify with your PM` which analytics logs you need to send (and to which platform). As of `29/10/21` we are only required to make calls using  “Lion Analytics”. 

##### About Lion Analytics and PotatoSDK:
Potato SDK does not contain a wrapper for Lion Analytics. If you need to use Lion Analytics you have to follow the most current guidelines provided by Lion (ask your PM).  However, while using the Lion Analytics with PotatoSDK as of  `29/10/21`, the following rules apply,
* `Firebase sdk and LionAnalytics don't work with each other` properly as of 29/10/2021. Please refrain from adding both sdks at the same time. Or discuss with your PM
* PotatoSDK provides a loading `splash scene` which **is recommended** to use. Depending on whether you are using it or not you have
  * **Without Loading Scene:**  You have to make sure that the `Potato.isReady` flag is true before making any calls on LionAnalytics.
  * **With Loading Scene:** In this case you can safely make the calls to LionAnalytics on any scene loaded afterward. But if you choose to extend the provided loading scene, you still have to check if `Potato.isReady` is true.
---
### SDK Walkthroughs
Here are links to specific integration and testing walkthroughs. The SDKs in question are primarily documented for the android platform, additional steps for iOS are noted in the next section.
* [Facebook](sdk_docs/facebook/README.md)
* [GameAnalytics(GA)](sdk_docs/game_analytics/README.md)
* [Adjust](sdk_docs/adjust/README.md)
* [Firebase](sdk_docs/firebase/README.md)
* [Max Ad Mediation](sdk_docs/max/README.md)
* [InAppPurchase](sdk_docs/iap/README.md)

### Additional Features
* **Splash Screen** 
  
  Quick Guide:
  * You will be prompted to add the Splash Scene provided with PotatoSDK. Using this ensures all the SDKs are ready for use when you get to your main game scene.
  * You can choose which scene is loaded next at Potato root object. (build index 1 is loaded by default)
  * **If you need to do any preload task** for your game specific tasks on the splash screen you extend the provided splash scene by adding your scripts/gameobjects to it.
  * **If your extended tasks are asynchronous** (won't be executed immediately and you need to wait indefinitely) and you need to halt the loading process of the next scene, you can enable manual loading in the root Potato script. In this case you will need to load the next scene when your work is complete. But please check if the `Potato.isReady` flag is true before loading the next scene.
* [AB testing](sdk_docs/ab_test/README.md)
  * This module uses Max sdk’s variable services
* [GDPR](sdk_docs/gdpr/README.md)
  * The result of the consent flow is communicated to Max and Adjust SDK
* **NoADs**
  
  QuickGuide: 
  * This module needs InAppPurchases module and wrapper to be active
  * NoAd nonconsumable must be set up in the relevant stores and added in the InAppPurchase wrapper.
  * To use this module you need to
    * Activate the wrapper
    * In the wrapper select which InAppPurchase item is “NoAd”
  * Expected behaviour: buying/restoring  noads
    *  would instantly stop banner if it was on display
    *  prevents showing banners/interstitials from that point



---
# Building For iOS
Additional steps while building for iOS are noted in this section. It's recommended that you stabilize your android build prior to moving to iOS.

#### About the recommended iOS Build Process:
*This process is recommended since this can avoid some common issues in a more automated process and also makes some issues more solvable. (for example “broken pod installation” avoiding, “FB Lexical Preprocessor” solve)*

### First Time Build Specific:
* Minimal mandatory MAX integration: You must have max sdk integrated (even if you are not using ads right now) fill up the details as follows:
  
  ![MAX sdk settings](readme_images/max_ready.png)
  * SDK Key : `Lzi5VR_J50y55PM5ctwAwALT5d9g1CKMhT1TF0naOa4fSUn98Vd6rXsvAp4I3A-5LaPvNk4RSvKe5fesxKhRzh`
  * Privacy URL : “https://lionstudios.cc/privacy/”
  * Terms URL : “https://lionstudios.cc/terms/”
  * Enable Facebook mediated network in max
    ![FB ATT check](readme_images/max_ready_a.png)
  * Activate the potatoSDK MAX wrapper.  

* “Play Service Resolver(PSR)/External Dependency Manager(EDM)”: (if your having errors after opening project for IOS check troubleshooting point 1)
  * Do not run “install cocoapods” in EDM/PSR
  * This is what the iOS Resolver settings should look like
    
    ![iOS POD setup](readme_images/pod.png)
* If you are using firebase don't forget to drag and drop **“GoogleService-Info.plist”** file in your asset folder.
* If you are using Game Analytics please click “Widnow/GameAnalytics/SelectSettings” and check if the iphone platform is already added or not.  If not add it by either getting keys from your PM or logging in to GA and selecting the right game.
  
  ![GA setup](readme_images/ga.png)

### General Build Checklist
1. Check if all the wrappers are properly configured (you have to enable them the first time you are using that specific wrapper on iOS)
2. Choose `Build` instead  of “Build and Run”
3. Recommended to build in a newly created folder. (Append might work, but more risky)
4. Right click on the build folder and choose “New Terminal at Folder”.
5. (Check troubleshooting #2 if you are using FB before you do this)Run command `pod install`. (will generate workspace file)
6. Open workspace file and sign application and run it on device.

### Common iOS Troubleshooting:
#### 1. iOS Resolver Name Mismatch
![iOS resolver name error](readme_images/rename_0.png)
* The error shows a common issue in many Unity 2021.x.x versions. The solution is to rename all IOSResolver files
  ![Rename Files](readme_images/rename_1.png)
  * Google.IOSResolver`_v1.2.135.0`.dll => Google.IOSResolver.dll 
  * Google.IOSResolver`_v1.2.135.0`.dll.meta => Google.IOSResolver.dll.meta
  * Google.IOSResolver`_v1.2.135.0`.dll.mdb => Google.IOSResolver.dll.mdb
  * Google.IOSResolver`_v1.2.135.0`.dll.mdb.meta => Google.IOSResolver.dll.mdb.meta
* An alternate solution to the above is to completely replace current EDM or PSR with the latest version of EDM. You can get the latest version from this [link](https://github.com/googlesamples/unity-jar-resolver/blob/master/external-dependency-manager-latest.unitypackage)
#### 2. Lexical or Preprocessor Issue (FB)
![FB error](readme_images/fb_error.png)
* If you have **Facebook SDK 11.0.0** you will see this error when you build from xcode.  	
* To fix this follow these steps
  * Open the build folder after building from unity
  * Edit the **Podfile**, you will need to change the version of each facebook related pod version to `11.1.0` from 11.0.0.
    ![FB fix](readme_images/fb_fix.png)
  * Save the pod file
  * Run terminal at the build folder and `pod install`.
  * Continue with your build according to the build guidelines.

[Back To Top](#potatosdk)




