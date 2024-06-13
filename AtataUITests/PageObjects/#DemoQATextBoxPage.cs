﻿using Atata;
using _ = AtataUITests.PageObjects.DemoQATextBoxPage;

namespace AtataUITests.PageObjects
{
    public sealed class DemoQATextBoxPage : DemoQAPage<_>
    {
        [FindByPlaceholder("Full Name")]
        public TextInput<_> FullName { get; set; }

        [FindById("userName-label")]
        public Label<_> FullNameLabel { get; set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        public Button<_> Submit { get; set; }

        [FindById("name")]
        public Text<_> FullNameText { get; set; }
    }
}
