# PotatoSDK
This repository is maintained with the motivation of making SDK integration easier or faster for the developers of Alpha Potato. Please follow the document and keep an eye out for changes when making a new integration. For any queries or feedbacks feel free to give me a knock!

## General Guilelines
### Environment Setup
* Make sure you are using minimum recommended Unity version: 2021.1.16f1
* Setup your project properly at the beginning (to avoid future complications)
  * packageName or bundleID.  
  * Version and bundle version code (both will change with submissions)
  * (Android) Scripting backend =  IL2CPP
  * (Android) target architectures = armv7 and arm64
  * (Android) Sign your application and create a signed build. 
  * (Android) If you have an android manifest you should change the debuggable attribute to false
* While copying IDs or keys (e.g. ad unit ID, app token etc) make sure to check for white space characters on both end of the key
* You might want to add logs-viewer package for viewing logs on mobile devices


### Using PotatoSDK
* Download the PotatoSDK. Check in the release section for latest package.
* Import the PotatoSDK unitypackage to your project. 
* You can use the “PotatoSDK” menu to “Open Config Prefab” and access wrappers relevant to your implementation.
* You can activate modules after importing relevant SDKs. 
* The activate button is platform dependent (if you  activate for android, you will still need to activate when you move to iOS).
* Recommended to allow using the potatoSDK to add a loading scene when prompted.


### Managing “External Dependency Manager(EDM)”/”Play Service Resolver(PSR)” 
* Always check when you import a new package, if it uses the External Dependency Manager or Play Service Resolver. 
* And if it does, check if you already have one of them(and which version) in your projects.
* You have to keep in mind that PSR is a lower version of EDM.
* Check for the following situations.
  * Your project has Lower version of EDM/PSR than the SDK:
    * First, delete the EDM/PSR folder,
    * Then import the new sdk
  * Your project has Higher version of EDM than the SDK:
    * While importing the SDK, uncheck the EDM/PSR folder then import it.

