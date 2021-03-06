using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class DynamicPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line9()
		{
			// tag::e65e9805b8b17f72616f099e11a5c337[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::e65e9805b8b17f72616f099e11a5c337[]

			response0.MatchesExample(@"PUT my_index/_doc/1 \<1>
			{
			  ""username"": ""johnsmith"",
			  ""name"": {
			    ""first"": ""John"",
			    ""last"": ""Smith""
			  }
			}");

			response1.MatchesExample(@"GET my_index/_mapping \<2>");

			response2.MatchesExample(@"PUT my_index/_doc/2 \<3>
			{
			  ""username"": ""marywhite"",
			  ""email"": ""mary@white.com"",
			  ""name"": {
			    ""first"": ""Mary"",
			    ""middle"": ""Alice"",
			    ""last"": ""White""
			  }
			}");

			response3.MatchesExample(@"GET my_index/_mapping \<4>");
		}

		[U(Skip = "Example not implemented")]
		public void Line60()
		{
			// tag::4b478d9b1231513362d2fa8c766cd0a5[]
			var response0 = new SearchResponse<object>();
			// end::4b478d9b1231513362d2fa8c766cd0a5[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic"": false, \<1>
			    ""properties"": {
			      ""user"": { \<2>
			        ""properties"": {
			          ""name"": {
			            ""type"": ""text""
			          },
			          ""social_networks"": { \<3>
			            ""dynamic"": true,
			            ""properties"": {}
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}