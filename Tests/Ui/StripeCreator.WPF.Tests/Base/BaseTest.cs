using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;
using System.IO;

namespace StripeCreator.WPF.Tests.Base
{
    internal class BaseTest
    {
        protected string ApplicationRelativePath = @"T:\Repositories\Net\StripeCreatorClone\UI\StripeCreator.WPF\bin\Debug\net7.0-windows\";
        protected string ApplicationExec = "StripeCreator.WPF.exe";

        protected Application App { get; private set; }
        protected UIA3Automation Automation { get; private set; }
        protected Window CurrentWindow => App.GetMainWindow(Automation);

        [SetUp]
        protected void Start()
        {
            Directory.SetCurrentDirectory(ApplicationRelativePath);
            App = Application.Launch(ApplicationRelativePath + ApplicationExec);
            Automation = new UIA3Automation();
            Preparation();
            DetectComponents();
        }

        [TearDown]
        protected void Stop()
        {
            if (App != null)
            {
                Automation.Dispose();
                Automation = null;
                App.Kill();
                App.Dispose();
                App = null;
            }
        }

        protected virtual void DetectComponents() { }

        protected virtual void Preparation() { }
    }
}
