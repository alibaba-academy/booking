using System.Runtime.Serialization;
using System;
using Xunit;
using RA;

namespace test
{
    public class SalonTest
    {
        [Fact]
        public void NameLengthTest()
        {
            var body = new
            {
                Name = "AkbarAsgharMamad",
                SeatWidth = 20,
                SeatHeight = 20
            };
            new RestAssured()
            .Given()
                .Name("max length test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("max length test", b =>
                {
                    return b == 400;
                })
                .AssertAll();
        }

        [Fact]
        public void SeatDimensionTest()
        {
            var body = new
            {
                Name = "Ali",
                SeatWidth = -20,
                SeatHeight = 20
            };
            new RestAssured()
            .Given()
                .Name("seat dimension test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("seat dimension test", b =>
                {
                    return b == 400;
                })
                .AssertAll();
        }
    }
}