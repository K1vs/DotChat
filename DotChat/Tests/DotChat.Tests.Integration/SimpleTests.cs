namespace K1vs.DotChat.Tests.Integration
{
    using System.Threading.Tasks;
    using Chats;
    using Tools;
    using Xunit;
    using Xunit.Abstractions;

    public class SimpleTests
    {
        private readonly ITestOutputHelper _output;

        public SimpleTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task ShouldCreateChat()
        {
            var context = new TestContext();
            var ucA = context.UserContexts["A"];
            var ucB = context.UserContexts["B"];
            var ucC = context.UserContexts["C"];
            var ucD = context.UserContexts["D"];
            var ucE = context.UserContexts["E"];

            await ucA.Activate();
            await ucB.Activate();
            await ucD.Activate();

            await ucA.CreateChat("chat1", ChatPrivacyMode.Public, context.Users["B"].UserId, context.Users["C"].UserId);
            await context.Check(ucA.CheckChat("chat1"));

            await context.Check(ucB.CheckChat("chat1"));

            await ucC.Activate();
            await context.Check(ucC.CheckChat("chat1"));

            ucD.CheckNotChat("chat1");

            await ucE.Activate();
            ucD.CheckNotChat("chat1");
        }

        [Fact]
        public async Task ShouldSendMessage()
        {
            var context = new TestContext();
            var ucA = context.UserContexts["A"];
            var ucB = context.UserContexts["B"];
            var ucC = context.UserContexts["C"];
            var ucD = context.UserContexts["D"];
            var ucE = context.UserContexts["E"];

            await ucA.Activate();
            await ucB.Activate();
            await ucD.Activate();

            await ucA.CreateChat("chat1", ChatPrivacyMode.Public, context.Users["B"].UserId, context.Users["C"].UserId);

            await context.Check(ucA.CheckChat("chat1"));
            await ucA.AddTextMessage("chat1", "msg1");
            await context.Check(ucA.CheckMessages("chat1", "msg1"));
            ucA.CheckNotMessages("chat1", "msgX");

            await context.Check(ucB.CheckChat("chat1"));
            await ucB.AddTextMessage("chat1", "msg1");
            await context.Check(ucB.CheckMessages("chat1", "msg1"));
            ucB.CheckNotMessages("chat1", "msgX");

            await ucC.Activate();
            await context.Check(ucC.CheckChat("chat1"));
            await ucC.AddTextMessage("chat1", "msg1");
            await context.Check(ucC.CheckMessages("chat1", "msg1"));
            ucC.CheckNotMessages("chat1", "msgX");

            ucD.CheckNotChat("chat1");
            ucD.CheckNotMessages("chat1", "msg1");

            await ucE.Activate();
            ucE.CheckNotChat("chat1");
            ucE.CheckNotMessages("chat1", "msg1");
        }
    }
}
