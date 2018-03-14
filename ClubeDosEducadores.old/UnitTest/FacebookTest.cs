using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using System.Dynamic;
using System.Web.Script.Serialization;
using Helper;

namespace UnitTest
{
    [TestClass]
    public class FacebookTest
    {
        //Get accessToken
        //https://developers.facebook.com/tools/explorer

        private static readonly long FacebookUserId = 848079501909831;
        private static readonly string AccessToken = "EAACEdEose0cBAIVMnRXQMcPVZAy1np9zZBkMaHCEyhdaXmj6fhcWgJup0cHoSBeJaLOAubcQDvnEiTUwnTBcJtUkWpb70qUomZAsMK7M6nyjKXmX453QMwT8LzLZAAq1k07gECXsQMMsUw2DUZAW5ZC8ZCkW6ZCZB3oL3AzGQYpzPW9nafjP4HIGgb55JaZCb43R8ZD";

        [TestMethod]
        public void LoginTest()
        {
            var fb = new FacebookClient(AccessToken);

            var result = fb.Get("/me");
            var jsonResult = result.FromJson();
            var userId = jsonResult.Value<long>("id");

            Assert.IsTrue(userId == FacebookUserId);
        }

        [TestMethod]
        public void PostTimelineTest()
        {
            var fb = new FacebookClient(AccessToken);

            dynamic parameters = new ExpandoObject();
            parameters.title = "test api";
            parameters.message = "test message";

            var result = fb.Post(FacebookUserId + "/feed", parameters);
            var jsonResult = result.FromJson();
            var userId = jsonResult.Value<long>("id");

            Assert.IsTrue(userId == FacebookUserId);
        }

    }
}
