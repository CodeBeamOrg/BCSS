using Bunit;
using BCSSViewer.Docs.Pages;
using FluentAssertions;
using MudBlazor;
using MudExtensions;

namespace BCSS.Test.Core
{
    [TestFixture]
    public class RenderTests : BunitTest
    {
        [Test]
        public void IndexPageRenderTest()
        {
            var comp = Context.RenderComponent<BCSSViewer.Docs.Pages.Index>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void BreakpointPageRenderTest()
        {
            var comp = Context.RenderComponent<BreakpointPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void PerformanceModePageRenderTest()
        {
            var comp = Context.RenderComponent<PerformanceModePage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void SpacingPageRenderTest()
        {
            var comp = Context.RenderComponent<SpacingPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ContentGeneralPageRenderTest()
        {
            var comp = Context.RenderComponent<ContentGeneralPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void BenchmarkPageRenderTest()
        {
            var comp = Context.RenderComponent<BenchmarkPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void QuickDebugPageRenderTest()
        {
            var comp = Context.RenderComponent<QuickDebugPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void HowItWorksPageRenderTest()
        {
            var comp = Context.RenderComponent<HowItWorksPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void InstallationPageRenderTest()
        {
            var comp = Context.RenderComponent<InstallationPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void WhatPageRenderTest()
        {
            var comp = Context.RenderComponent<WhatPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void FilterPageRenderTest()
        {
            var comp = Context.RenderComponent<FilterPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void FlexboxPageRenderTest()
        {
            var comp = Context.RenderComponent<FlexboxPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ShadowPageRenderTest()
        {
            var comp = Context.RenderComponent<ShadowPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void TransformPageRenderTest()
        {
            var comp = Context.RenderComponent<TransformPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void BasicSyntaxPageRenderTest()
        {
            var comp = Context.RenderComponent<BasicSyntaxPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void CaseInsensitivePageRenderTest()
        {
            var comp = Context.RenderComponent<CaseInsensitivePage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void CustomValuesPageRenderTest()
        {
            var comp = Context.RenderComponent<CustomValuesPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void DoubleDashPageRenderTest()
        {
            var comp = Context.RenderComponent<DoubleDashPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void PrefixesPageRenderTest()
        {
            var comp = Context.RenderComponent<PrefixesPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void UnitMeasurementPageRenderTest()
        {
            var comp = Context.RenderComponent<UnitMeasurementPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void VendorPrefixesPageRenderTest()
        {
            var comp = Context.RenderComponent<VendorPrefixesPage>();
            comp.Markup.Should().NotBeNullOrEmpty();
        }

    }
}
