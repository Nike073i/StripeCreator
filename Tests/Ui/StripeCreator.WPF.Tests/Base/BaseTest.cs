﻿using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;
using System.IO;

namespace StripeCreator.WPF.Tests.Base
{
    internal class BaseTest
    {
        protected string ApplicationExec = "StripeCreator.WPF.exe";

        protected Application App { get; private set; }
        protected UIA3Automation Automation { get; private set; }
        protected Window CurrentWindow => App.GetMainWindow(Automation);

        [SetUp]
        protected void Start()
        {
            App = Application.Launch(ApplicationExec);
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
