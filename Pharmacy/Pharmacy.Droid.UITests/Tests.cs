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
        public void Login()
        {
            app.Query(x => x.Class("EditText"));
			app.EnterText(x => x.Id("login_username"), "rcervantes@outlook.com");
			app.Back();
			app.EnterText(x => x.Id("login_password"), "password");
			app.Back();
			app.Query(x => x.Class("Button"));
			app.Tap(x => x.Id("login"));
        }

    }
}
