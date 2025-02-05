using HotChocolate.Data.Sorting;
using NetTopologySuite.Geometries;

namespace Sukalibur.Graph
{
    public class PointSortInputType : SortInputType<Point>
    {
        protected override void Configure(ISortInputTypeDescriptor<Point> descriptor)
        {
            descriptor.Ignore(f => f.Boundary);
            descriptor.Ignore(f => f.Envelope);
            descriptor.Ignore(f => f.Factory);
        }
    }
}
