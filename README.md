# PotatoSDK
This repository is maintained with the motivation of making SDK integration easier or faster for the developers of Alpha Potato. Please follow the document and keep an eye out for changes when making a new integration. For any queries or feedbacks feel free to give me a knock!
Quick Links
* [Getting Started](#getting-started)
* [SDK Walkthroughs](#ios-build-guidelines)
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

### Additional Feature Walkthroughs
* Splash Screen 
  Quick Guide:
  * You will be prompted to add the Splash Scene provided with PotatoSDK. Using this ensures all the SDKs are ready for use when you get to your main game scene.
  * You can choose which scene is loaded next at Potato root object. (build index 1 is loaded by default)
  * If you need to do any preload task for your game specific tasks on the splash screen you extend the provided splash scene by adding your scripts/gameobjects to it.
  * If your extended tasks are asynchronous (won't be executed immediately and you need to wait indefinitely) and you need to halt the loading process of the next scene, you can enable manual loading in the root Potato script. In this case you will need to load the next scene when your work is complete. But please check if the Potato.isReady flag is true before loading the next scene.
* AB testing 
  * This module uses Max sdk’s variable services
* GDPR 
  * The result of the consent flow is communicated to Max and Adjust SDK
* NoADs
  QuickGuide: 
  * Needs InAppPurchases module and wrapper to be active
  * NoAd nonconsumable must be set up in the relevant stores and added in the InAppPurchase wrapper.
  * To use this module you need to
    * Activate the wrapper
    * In the wrapper select which InAppPurchase item is “NoAd”
  * Expected behaviour: buying/restoring  noads instantly stops banner/interstitials



---




# iOS Build Guidelines
Additional steps while building for iOS are noted in this section. It's recommended that you stabilize your android build prior to moving to iOS.

#### About the recommended iOS Build Process:
This process is recommended since this can avoid some common issues in a more automated process and also makes some issues more solvable. (for example “broken pod installation” avoiding, “FB Lexical Preprocessor” solve)

#### First Time Build Specific:
* Minimal mandatory MAX integration: You must have max sdk integrated (even if you are not using ads right now) fill up the details as follows:
  ![alt text](image.jpg)
  * SDK Key : “Lzi5VR_J50y55PM5ctwAwALT5d9g1CKMhT1TF0naOa4fSUn98Vd6rXsvAp4I3A-5LaPvNk4RSvKe5fesxKhRzh”
  * Privacy URL : “https://lionstudios.cc/privacy/”
  * Terms URL : “https://lionstudios.cc/terms/”
  * Enable Facebook mediated network in max
  * Activate the potatoSDK MAX wrapper.  

