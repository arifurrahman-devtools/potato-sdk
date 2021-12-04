# potato-sdk
This repository is maintained with the motivation of making SDK integration easier or faster for the developers of Alpha Potato. Please follow the document and keep an eye out for changes when making a new integration. For any queries or feedbacks feel free to give me a knock!

### Initial Setup
* Make sure you are using minimum recommended Unity version: 2021.1.16f1
* Setup your project properly at the beginning (to avoid future complications)
  * packageName or bundleID.  
  * Version and bundle version code (both will change with submissions)
  * Scripting backend =  IL2CPP
  * target architectures = armv7 and arm64
  * Sign your application and create a signed build. 
  * If you have an android manifest you should change the debuggable attribute to false
* While copying IDs or keys (e.g. ad unit ID, app token etc) make sure to check for white space characters on both end of the key
* You might want to add logs-viewer package for viewing logs on mobile devices

