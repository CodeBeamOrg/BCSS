using FluentAssertions;
using BCSS.Services;

namespace BCSS.Test.Core
{
    [TestFixture]
    public class ConverterTests : BunitTest
    {
        [Test]
        public void BasicConvertTest()
        {
            BcssService service = new BcssService();
            service.Decode("w-200").Should().Be("w-200");
            BlazorCssConverter.Convert("w-200").Should().Be("width:200px");
            BlazorCssConverter.Convert("bg-yellow").Should().Be("background:yellow");
        }

        [Test]
        public void DoubleDashTest()
        {
            BcssService service = new BcssService();
            service.Decode("w--200").Should().Be("w--200");
            BlazorCssConverter.Convert("w--200").Should().Be("width:200px");
            BlazorCssConverter.Convert("bg--yellow").Should().Be("background:yellow");

            BlazorCssConverter.Convert("d-inline-flex").Should().Be("display:inline-flex");
            BlazorCssConverter.Convert("d--inline-flex").Should().Be("display:inline-flex");

            BlazorCssConverter.Convert("box-sizing--border-box").Should().Be("box-sizing:border-box");
        }

        [Test]
        public void AdvancedConvertTest()
        {
            BcssService service = new BcssService();
            BlazorCssConverter.PostProcess(BlazorCssConverter.Convert("transition-color-1s-ease+in+out")).Should().Be("transition:color 1s ease-in-out");
            BlazorCssConverter.PostProcess(BlazorCssConverter.Convert("transition-color*1s*ease+in+out")).Should().Be("transition:color 1s ease-in-out");
            BlazorCssConverter.PostProcess(BlazorCssConverter.Convert("transition--color-1s-ease+in+out")).Should().Be("transition:color 1s ease-in-out");
            BlazorCssConverter.PostProcess(BlazorCssConverter.Convert("transition--color*1s*ease+in+out")).Should().Be("transition:color 1s ease-in-out");

        }

        [Test]
        public void SpacedResultTest()
        {
            BcssService service = new BcssService();
            BlazorCssConverter.PostProcess(BlazorCssConverter.Convert("transition-color-1-ease-in-out")).Should().Be("transition:ease-in-out color 1s");
            BlazorCssConverter.PostProcess(BlazorCssConverter.Convert("transition-color-1s-ease-in")).Should().Be("transition:ease-in color 1s");
            //Wrong example
            BlazorCssConverter.PostProcess(BlazorCssConverter.Convert("transition-color-1-easein-out")).Should().Be("transition:color 1s easein out");
        }

        [Test]
        public void UnitMeasurementTest()
        {
            BcssService service = new BcssService();
            service.Decode("w-200").Should().Be("w-200");
            BlazorCssConverter.Convert("w-200").Should().Be("width:200px");

            service.Decode("w-200%").Should().Be("w-200a");
            BlazorCssConverter.Convert("w-200%").Should().Be("width:200%");

            service.Decode("w-2.0").Should().Be("w-2s0");
            BlazorCssConverter.Convert("w-2.0").Should().Be("width:2.00rem");
            service.Decode("w-20.").Should().Be("w-20s");
            BlazorCssConverter.Convert("w-20.").Should().Be("width:20.0rem");
            service.Decode("w-2rem").Should().Be("w-2rem");
            BlazorCssConverter.Convert("w-2rem").Should().Be("width:2rem");

            service.Decode("w-2,0").Should().Be("w-2t0");
            BlazorCssConverter.Convert("w-2,0").Should().Be("width:2.00em");
            service.Decode("w-20,").Should().Be("w-20t");
            BlazorCssConverter.Convert("w-20,").Should().Be("width:20.0em");
            service.Decode("w-2em").Should().Be("w-2em");
            BlazorCssConverter.Convert("w-2em").Should().Be("width:2em");

            service.Decode("h-2vh").Should().Be("h-2vh");
            BlazorCssConverter.Convert("h-2vh").Should().Be("height:2vh");
            service.Decode("w-20vw").Should().Be("w-20vw");
            BlazorCssConverter.Convert("w-20vw").Should().Be("width:20vw");

            service.Decode("rotate-20").Should().Be("rotate-20");
            BlazorCssConverter.Convert("rotate-20").Should().Be("transform:rotate(20deg)");
        }

        [Test]
        public void CustomValuesTest()
        {
            BcssService service = new BcssService();
            service.Decode("bg-[#123456]").Should().Be("bg-r123456");
            BlazorCssConverter.Convert("bg-[#123456]").Should().Be("background:rgba(18,52,86,255)");

            service.Decode("bg-[--mud-palette-primary]").Should().Be("bg---mud-palette-primary");
            BlazorCssConverter.Convert("bg-[--mud-palette-primary]").Should().Be("background:var(--mud-palette-primary)");

            service.Decode("bgimage-[/paper.png]").Should().Be("bgimage-wpaperspng");
            BlazorCssConverter.Convert("bgimage-[/paper.png]").Should().Be("background-image:url(paper.png)");

            service.Decode("bg-[49,59,114]").Should().Be("bg-49t59t114");
            BlazorCssConverter.Convert("bg-[49,59,114]").Should().Be("background:rgba(49,59,114)");

            service.Decode("bg-[49,59,114,0.8]").Should().Be("bg-49t59t114t0s8");
            BlazorCssConverter.Convert("bg-[49,59,114,0.8]").Should().Be("background:rgba(49,59,114,0.8)");
        }

        [Test]
        public void CaseInsensitiveTest()
        {
            BcssService service = new BcssService();
            service.Decode("bG-Yellow").Should().Be("bg-yellow");
            BlazorCssConverter.Convert("bG-Yellow").Should().Be("background:yellow");

            service.Decode("BG-YELLOW").Should().Be("bg-yellow");
            BlazorCssConverter.Convert("BG-YELLOW").Should().Be("background:yellow");

            service.Decode("Bg-[--Mud-paLette-pRimary]").Should().Be("bg---mud-palette-primary");
            BlazorCssConverter.Convert("Bg-[--Mud-paLette-pRimary]").Should().Be("background:var(--mud-palette-primary)");

            //On Url values, property name should be lowercase, but value not.
            service.Decode("bgiMage-[/Paper.Png]").Should().Be("bgimage-wpaperspng");
            BlazorCssConverter.Convert("bgiMage-[/Paper.Png]").Should().Be("background-image:url(Paper.Png)");
        }

        [Test]
        public void PrefixesTest()
        {
            BcssService service = new BcssService();
            service.Decode("hover:bg-yellow").Should().Be("hoverqbg-yellow");
            BlazorCssConverter.Convert("hover:bg-yellow").Should().Be("background:yellow");

            service.Decode("h:bg-yellow").Should().Be("hqbg-yellow");
            BlazorCssConverter.Convert("h:bg-yellow").Should().Be("background:yellow");

            service.Decode("w:bg-yellow").Should().Be("wqbg-yellow");
            BlazorCssConverter.Convert("w:bg-yellow").Should().Be("background:yellow");

            service.Decode("sm:w:bg-yellow").Should().Be("smqwqbg-yellow");
            BlazorCssConverter.Convert("sm:w:bg-yellow").Should().Be("background:yellow");

            service.Decode("sm:bg-yellow").Should().Be("smqbg-yellow");
            BlazorCssConverter.Convert("sm:bg-yellow").Should().Be("background:yellow");
        }

    }
}
