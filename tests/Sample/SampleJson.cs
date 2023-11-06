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

    public const string SampleFeaturedImageJson = """
        {
            "numberOfData": 2,
            "data": [
                {
                    "slug": "city-circuit-track",
                    "trackName": "City Circuit Track",
                    "logoUrl": "img/logo.jpg",
                    "category": "City Circuit",
                    "country": {
                        "name": "Japan",
                        "code": "jp"
                    },
                    "imageName": "image-01",
                    "imageUrl": "img/track-01.jpg",
                    "description": "picture desc",
                    "author": "admin"
                },
                {
                    "slug": "dirt-track",
                    "trackName": "Dirt Track",
                    "logoUrl": "img/logo.jpg",
                    "category": "Dirt Circuit",
                    "country": {
                        "name": "USA",
                        "code": "us"
                    },
                    "imageName": "image-02",
                    "imageUrl": "img/track-02.jpg",
                    "description": "picture desc",
                    "author": "admin"
                }
            ]
        }
    """;

    public const string SampleAllRaceTracks = """
        {
            "numberOfData": 5,
            "data": [
                {
                    "slug": "city-circuit-track-name",
                    "name": "City Circuit Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-city-circuit.jpg",
                    "category": "City Circuit",
                    "roadType": "Tarmac",
                    "numberOfLayouts": 3,
                    "country": {
                        "name": "Japan",
                        "code": "jp"
                    }
                },
                {
                    "slug": "dirt-track-name",
                    "name": "Dirt Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-dirt-circuit.jpg",
                    "category": "Dirt Circuit",
                    "roadType": "Dirt/Tarmac",
                    "numberOfLayouts": 2,
                    "country": {
                        "name": "USA",
                        "code": "us"
                    }
                },
                {
                    "slug": "orginal-circuit-track-name",
                    "name": "Original Circuit Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-original-circuit.jpg",
                    "category": "Original Circuit",
                    "roadType": "Tarmac",
                    "numberOfLayouts": 3,
                    "country": {
                        "name": "United Kingdom",
                        "code": "gb"
                    }
                },
                {
                    "slug": "real-circuit-track-name",
                    "name": "Real Circuit Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-real-circuit.jpg",
                    "category": "Real Circuit",
                    "roadType": "Tarmac",
                    "numberOfLayouts": 3,
                    "country": {
                        "name": "Belgium",
                        "code": "be"
                    }
                },
                {
                    "slug": "snow-track-name",
                    "name": "Snow Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-snow-circuit.jpg",
                    "category": "Snow Circuit",
                    "roadType": "Snow/Tarmac",
                    "numberOfLayouts": 2,
                    "country": {
                        "name": "Italy",
                        "code": "it"
                    }
                }
            ]
        }
    """;

    public const string SampleOriginalRaceTracks = """
        {
            "numberOfData": 2,
            "data": [
                {
                    "slug": "orginal-circuit-track-name",
                    "name": "Original Circuit Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-original-circuit.jpg",
                    "category": "Original Circuit",
                    "roadType": "Tarmac",
                    "numberOfLayouts": 3,
                    "country": {
                        "name": "United Kingdom",
                        "code": "gb"
                    }
                },
                {
                    "slug": "orginal-circuit-2-track-name",
                    "name": "Original Circuit 2 Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-original-circuit.jpg",
                    "category": "Original Circuit",
                    "roadType": "Tarmac",
                    "numberOfLayouts": 3,
                    "country": {
                        "name": "United Kingdom",
                        "code": "gb"
                    }
                }
            ]
        }
    """;

    public const string SampleOffroadRaceTracks = """
        {
            "numberOfData": 2,
            "data": [
                {
                    "slug": "dirt-track-name",
                    "name": "Dirt Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-dirt-circuit.jpg",
                    "category": "Dirt Circuit",
                    "roadType": "Dirt/Tarmac",
                    "numberOfLayouts": 2,
                    "country": {
                        "name": "USA",
                        "code": "us"
                    }
                },
                {
                    "slug": "snow-track-name",
                    "name": "Snow Track Name",
                    "logoUrl": "img/track-samples/gtroad-sample-logo.png",
                    "coverUrl": "img/track-samples/gtroad-sample-snow-circuit.jpg",
                    "category": "Snow Circuit",
                    "roadType": "Snow/Tarmac",
                    "numberOfLayouts": 2,
                    "country": {
                        "name": "Italy",
                        "code": "it"
                    }
                }
            ]
        }
    """;
}