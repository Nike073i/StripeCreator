using FlaUI.Core;
using FlaUI.UIA3;
using NUnit.Framework;
using System;

namespace StripeCreator.WPF.Tests.Base
{
    internal class BaseTest
    {
        protected string ApplicationRelativePath = @"T:\Repositories\Net\StripeCreatorClone\UI\StripeCreator.WPF\bin\Debug\net7.0-windows\";
        protected string ApplicationExec = "StripeCreator.WPF.exe";

        [SetUp]
        protected void Start()
        {
            var app = Application.Launch(ApplicationRelativePath + ApplicationExec);
            using var automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);
            Console.WriteLine(window.Title);
        }

        [TearDown]
        protected void Stop()
        {
        }
    }
}
