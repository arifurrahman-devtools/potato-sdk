[Go Back To Main Page](../../README.md)
## Firebase Integration:
* Get the latest FireBase unity sdk from: [here](https://developers.google.com/unity/archive#google_analytics_for_firebase) . [tried and tested version: **8.1.0, 8.3.0**] 
* Import the SDK (Please check **Managing “External Dependency Manager(EDM)”/”Play Service Resolver(PSR)”** section for importing instruction)
* Recommended to choose “no” when prompted **“Enable Auto Resolution?"** (you can also do this anytime by going to Assets/ExternalDependencyManager/AndroidResolver/Settings)
* Activate PotatoSDK Firebase Wrapper
* Obtain “google-services.json” (for ios it’s “GoogleService-Info.plist”) file from your project manager for your specific app (or ask how to get it)
* Drop it in your Assets folder of your project
* Restart Unity (Yes.)
* Delete and resolve libraries

### Testing Firebase Integration:
* EnableTest Analytics in your FireBase wrapper of PotatoSDK
* Build and run. 
* Goto the firebase console which was used to setup the project and select your project
* Test it on firebase [console](https://console.firebase.google.com)
   
   ![FirebaseConsole](img_0.png)

* When you are running test analytics for a fresh game, you should see “testevent” as your top event. [sometimes you might need to restart your mobile’s wifi to see events]
* Disable Test Analytics when you are done testing




[Go Back To Main Page](../../README.md)
