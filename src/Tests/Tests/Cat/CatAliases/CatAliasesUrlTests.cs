﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatAliases
{
	public class CatAliasesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/aliases")
					.Fluent(c => c.Cat.Aliases())
					.Request(c => c.Cat.Aliases(new CatAliasesRequest()))
					.FluentAsync(c => c.Cat.AliasesAsync())
					.RequestAsync(c => c.Cat.AliasesAsync(new CatAliasesRequest()))
				;

			await GET("/_cat/aliases/foo")
					.Fluent(c => c.Cat.Aliases(a => a.Name("foo")))
					.Request(c => c.Cat.Aliases(new CatAliasesRequest("foo")))
					.FluentAsync(c => c.Cat.AliasesAsync(a => a.Name("foo")))
					.RequestAsync(c => c.Cat.AliasesAsync(new CatAliasesRequest("foo")))
				;
		}
	}
}
