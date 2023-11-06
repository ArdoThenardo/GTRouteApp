namespace GTRouteApp.Tests.Sample;

public static class SampleJson
{
    public const string SampleEmptyJson = """
        "numberOfData": 0,
        "data": []
    """;

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

    public const string SampleFeaturedVideoJson = """
        {
            "numberOfData": 2,
            "data": [
                {
                    "slug": "test-track",
                    "trackName": "Test Track",
                    "logoUrl": "img/logo.jpg",
                    "category": "Original Circuit",
                    "country": {
                        "name": "United Kingdom",
                        "code": "gb"
                    },
                    "videoName": "Track Video 001",
                    "videoType": "Race",
                    "videoId": "video-id-001",
                    "thumbnailUrl": "img/thumbnail.jpg",
                    "durationInSeconds": 120,
                    "description": "video desc",
                    "author": "admin"
                },
                {
                    "slug": "test-track",
                    "trackName": "Test Track",
                    "logoUrl": "img/logo.jpg",
                    "category": "Original Circuit",
                    "country": {
                        "name": "United Kingdom",
                        "code": "gb"
                    },
                    "videoName": "Track Video 002",
                    "videoType": "Time Trial",
                    "videoId": "video-id-002",
                    "thumbnailUrl": "img/thumbnail.jpg",
                    "durationInSeconds": 60,
                    "description": "video desc",
                    "author": "admin"
                }
            ]
        }
    """;
}