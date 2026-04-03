using BCSS.Services;
using Bunit;
using MudBlazor.Services;
using MudExtensions.Services;

namespace BCSS.Test.Core
{
    public abstract class BunitTest
    {
        protected Bunit.TestContext Context { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            Context = new();
            Context.JSInterop.Mode = JSRuntimeMode.Loose;
            Context.Services.AddMudServices(options =>
            {
                options.SnackbarConfiguration.ShowTransitionDuration = 0;
                options.SnackbarConfiguration.HideTransitionDuration = 0;
            });
            Context.Services.AddMudExtensions();
            Context.Services.AddBcss();
            //Context.AddTestServices();
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Context.Dispose();
            }
            catch (Exception)
            {
                /*ignore*/
            }
        }

        protected async Task ImproveChanceOfSuccess(Func<Task> testAction)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    await testAction();
                    return;
                }
                catch(Exception) { /*we don't care here*/ }
            }
            await testAction();
        }
    }
}
