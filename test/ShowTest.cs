using System.Runtime.Serialization;
using System;
using Xunit;
using RA;

namespace test
{
    public class ShowTest
    {
        [Fact]
        public void RepetitiousIdTest()
        {
            var body = new
            {
                Id = "1",
                StartTime = "2020-11-01T00:00:00",
                EndTime = "2020-11-01T01:00:00",
                Summary = "jafarshow",
                Price = "30",
                SalonId = "1",
                Title = "fhdajlk"
            };
            new RestAssured()
            .Given()
                .Name(" repetitious id test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("repetitious id test", b =>
                {
                    return b == 409;
                })
                .AssertAll();
        }

        [Fact]
        public void ConflictShowTimeTest()
        {
            var body = new
            {
                StartTime = "2020-11-02T00:00:00",
                EndTime = "2020-11-02T01:00:00",
                Summary = "jafarshow",
                Price = "30",
                SalonId = "1",
                Title = "fhdajlk"
            };
            new RestAssured()
            .Given()
                .Name("conflict time test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("conflict time test", b => b == 409)
                .AssertAll();

        }

        [Fact]
        public void StartTimeGreatarThanEndTimeTest()
        {
            var body = new
            {

                StartTime = "2020-11-03T01:00:00",
                EndTime = "2020-11-03T00:00:00",
                Summary = "jafarshow",
                Price = "30",
                SalonId = "1",
                Title = "fhdajlk"

            };
            new RestAssured()
            .Given()
                .Name("time test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("time test", b => b == 400)
                .AssertAll();
        }

        [Fact]
        public void NegativePricePost()
        {
            var body = new
            {

                StartTime = "2020-11-04T01:00:00",
                EndTime = "2020-11-04T02:00:00",
                Summary = "jafarshow",
                Price = "-45",
                SalonId = "1",
                Title = "fhdajlk"

            };
            new RestAssured()
            .Given()
                .Name("negative price  test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("negative price test", b => b == 400)
                .AssertAll();

        }

        [Fact]
        public void StartTimeTest()
        {
            var body = new
            {

                StartTime = "2017-11-03T02:00:00",
                EndTime = "2017-11-03T01  :00:00",
                Summary = "jafarshow",
                Price = "30",
                SalonId = "1",
                Title = "fhdajlk"

            };
            new RestAssured()
            .Given()
                .Name("start time test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("start time test", b => b == 400)
                .AssertAll();
        }

        [Fact]
        public void SalonIdIsNotAvailableTest()
        {
            var body = new
            {

                StartTime = "2020-11-01T02:00:00",
                EndTime = "2020-11-01T01:00:00",
                Summary = "jafarshow",
                Price = "30",
                SalonId = "18",
                Title = "fhdajlk"

            };
            new RestAssured()
            .Given()
                .Name("salon id not available test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("salon id not available test", b => b == 400)
                .AssertAll();
        }

        [Fact]
        public void TitleCharacterTest()
        {
            var body = new
            {

                StartTime = "2020-11-07T00:00:00",
                EndTime = "2020-11-07T01:00:00",
                Summary = "moretha",
                Title = "woejfnerijnfwirjnfeirjfnerijfnerijfnerifjn",
                Price = "30",
                SalonId = "1",

            };
            new RestAssured()
            .Given()
                .Name("title  test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("title test", b => b == 400)
                .AssertAll();
        }

        [Fact]
        public void PriceCeilingTest()
        {
            var body = new
            {

                StartTime = "2020-11-08T00:00:00",
                EndTime = "2020-11-08T01:00:00",
                Summary = "jafarshow",
                Price = "120",
                SalonId = "1",
                Title = "fhdajlk"

            };
            new RestAssured()
            .Given()
                .Name("max price test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("max price test", b => b == 400)
                .AssertAll();
        }

        [Fact]
        public void MinimumShowTimeTest()
        {
            var body = new
            {
                StartTime = "2020-11-09T00:00:00",
                EndTime = "2020-11-09T00:15:00",
                Summary = "jafarsh//comparing diffrence between start and end with show timeow",
                Price = "80",
                SalonId = "1",
                Title = "fhdajlk"
            };
            new RestAssured()
            .Given()
                .Name("min show time  test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("min show time test", b => b == 400)
                .AssertAll();
        }

        [Fact]
        public void MaximumShowTimeTest()
        {
            var body = new
            {
                StartTime = "2020-11-10T00:00:00",
                EndTime = "2020-11-10T18:00:00",
                Summary = "jafarshow",
                Price = "80",
                SalonId = "1",
                Title = "fhdajlk"

            };
            new RestAssured()
            .Given()
                .Name("max show time  test")
                .Header("Content-Type", "application/json")
                .Header("Accept-Encoding", "utf-8")
                .Body(body)
            .When()
                .Post("http://localhost:5000/api/v1/shows")
                .Then()
                .TestStatus("max show time test", b => b == 400)
                .AssertAll();
        }
    }
}