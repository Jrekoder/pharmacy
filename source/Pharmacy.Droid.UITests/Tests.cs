using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace Pharmacy.Droid.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the Android app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            app = ConfigureApp
                .Android
                // TODO: Update this path to point to your Android app and uncomment the
                // code if the app is not included in the solution.
                //.ApkFile ("../../../Android/bin/Debug/UITestsAndroid.apk")
                //.InstalledApp("com.rcervantes.pharmacy.test")
                .StartApp();
        }

        [Test]
        public void SelectPharmacy()
        {
            //app.Repl ();
            app.Screenshot ("Tap Login");
            app.Tap (x=>x.Id ("login"));
            app.Screenshot ("ScrollDown List");
            app.ScrollDown (x => x.Id ("sliding_layout"), ScrollStrategy.Gesture, 0.67, 200);
            app.Screenshot ("Tap Pharmacy");
            app.Tap (x => x.Class ("AppCompatTextView").Text ("GRUPO DIAZ BARRIGA"));
            app.Screenshot ("Go Back To The List");
            app.Back ();
            app.Screenshot ("Go Back And Close The App");
            app.Back ();
        }

    }
}
