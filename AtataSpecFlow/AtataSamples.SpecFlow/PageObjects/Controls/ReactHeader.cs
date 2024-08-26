﻿using Atata;

namespace AtataSamples.SpecFlow.PageObjects.Controls
{
    [ControlDefinition("div", ContainingClass = "rt-thead -header", ComponentTypeName = "Header")]
    public class ReactHeader<TOwner> : TableHeader<TOwner>
        where TOwner : PageObject<TOwner>
    {
    }
}