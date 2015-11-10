﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;
using FluentAssertions;

namespace Tests.Aggregations.Metric.GeoBounds
{
	public class GeoBoundsAggregationUsageTests : AggregationUsageTestBase
	{
		public GeoBoundsAggregationUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson => new
		{
			aggs = new
			{
				viewport = new
				{
					geo_bounds = new
					{
						field = Field<Project>(p => p.Location),
						wrap_longitude = true
					}
				}
			}
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Aggregations(a => a
				.GeoBounds("viewport", gb => gb
					.Field(p => p.Location)
					.WrapLongitude(true)
				)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Aggregations = new GeoBoundsAggregation("viewport", Field<Project>(p => p.Location))
				{
					WrapLongitude = true
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			var viewport = response.Aggs.GeoBounds("viewport");
			viewport.Should().NotBeNull();
			viewport.Bounds.Should().NotBeNull();

			var bottomRight = viewport.Bounds.BottomRight;
			bottomRight.Should().NotBeNull();
			bottomRight.Lat.Should().HaveValue();
			GeoLocation.IsValidLatitude(bottomRight.Lat.Value).Should().BeTrue();
			bottomRight.Lon.Should().HaveValue();
			GeoLocation.IsValidLongitude(bottomRight.Lon.Value).Should().BeTrue();

			var topLeft = viewport.Bounds.TopLeft;
			topLeft.Should().NotBeNull();
			topLeft.Lat.Should().HaveValue();
			GeoLocation.IsValidLatitude(topLeft.Lat.Value).Should().BeTrue();
			topLeft.Lon.Should().HaveValue();
			GeoLocation.IsValidLongitude(topLeft.Lon.Value).Should().BeTrue();
		}
	}
}
