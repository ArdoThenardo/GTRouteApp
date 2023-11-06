namespace GTRouteApp.Tests.Sample;

public static class SampleJson
{
    public const string SampleFeaturedTrackJson = """
        {
            "numberOfData": 2,
            "data": [
                {
                    "slug": "test-track",
                    "name": "Test Track",
                    "logoUrl": "img/logo.jpg",
                    "coverUrl": "img/cover.jpg",
                    "category": "Original Circuit",
                    "country": {
                        "name": "United Kingdom",
                        "code": "gb"
                    },
                    "text": "Featured original track"
                },
                {
                    "slug": "test-track-2",
                    "name": "Test Track 2",
                    "logoUrl": "img/logo.jpg",
                    "coverUrl": "img/cover.jpg",
                    "category": "Real Circuit",
                    "country": {
                        "name": "Japan",
                        "code": "jp"
                    },
                    "text": "Featured real track"
                }
            ]
        }
    """;

    public const string SampleEmptyFeaturedTrackJson = """
        "numberOfData": 0,
        "data": []
    """;
}