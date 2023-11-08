namespace GTRouteApp.Tests.Sample;

public static class SampleJson
{
    public const string SampleEmptyJson = """
        {
            "numberOfData": 0,
            "data": []
        }   
    """;

    public const string SampleEmptyObjectJson = """
        {
            "numberOfData": 0,
            "data": {}
        } 
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

    public const string SampleBasicTrackDetail = """
        {
            "numberOfData": 1,
            "data": {
                "name": "City Circuit Track Name",
                "logoUrl": "img/logo.jpg",
                "category": "City Circuit",
                "country": {
                    "name": "Japan",
                    "code": "jp"
                }
            }
        }
    """;

    public const string SampleTrackDetail = """
        {
            "numberOfData": 1,
            "data": {
                "name": "City Circuit Track Name",
                "coverUrl": "img/cover.jpg",
                "logoUrl": "img/logo.jpg",
                "category": "City Circuit",
                "roadType": "Tarmac",
                "numberOfLayouts": 3,
                "country": {
                    "name": "Japan",
                    "code": "jp"
                },
                "layouts": [
                    {
                        "slug": "city-circuit-track-name",
                        "layoutName": "Full Course",
                        "mapUrl": "img/track-samples/gtroad-sample-layout-map.jpg",
                        "length": 7.65,
                        "corners": 16
                    },
                    {
                        "slug": "city-circuit-track-name",
                        "layoutName": "Short Course",
                        "mapUrl": "img/track-samples/gtroad-sample-layout-map.jpg",
                        "length": 2.56,
                        "corners": 8
                    },
                    {
                        "slug": "city-circuit-track-name",
                        "layoutName": "GP Course",
                        "mapUrl": "img/track-samples/gtroad-sample-layout-map.jpg",
                        "length": 5.12,
                        "corners": 12
                    }
                ],
                "videos": [],
                "images": [
                    {
                        "slug": "city-circuit-track-name",
                        "imageName": "city-circuit-track-name_01",
                        "imageUrl": "img/track-samples/gtroad-sample-city-circuit.jpg",
                        "description": "Picture of City Circuit from GTRoad.",
                        "author": "gtroad-admin"
                    },
                    {
                        "slug": "city-circuit-track-name",
                        "imageName": "city-circuit-track-name_02",
                        "imageUrl": "img/track-samples/gtroad-sample-city-circuit.jpg",
                        "description": "Picture of City Circuit from GTRoad.",
                        "author": "gtroad-admin"
                    },
                    {
                        "slug": "city-circuit-track-name",
                        "imageName": "city-circuit-track-name_03",
                        "imageUrl": "img/track-samples/gtroad-sample-city-circuit.jpg",
                        "description": "Picture of City Circuit from GTRoad.",
                        "author": "gtroad-admin"
                    },
                    {
                        "slug": "city-circuit-track-name",
                        "imageName": "city-circuit-track-name_04",
                        "imageUrl": "img/track-samples/gtroad-sample-city-circuit.jpg",
                        "description": "Picture of City Circuit from GTRoad.",
                        "author": "gtroad-admin"
                    },
                    {
                        "slug": "city-circuit-track-name",
                        "imageName": "city-circuit-track-name_05",
                        "imageUrl": "img/track-samples/gtroad-sample-city-circuit.jpg",
                        "description": "Picture of City Circuit from GTRoad.",
                        "author": "gtroad-admin"
                    }
                ]
            }
        }
    """;

    public const string SampleVideoData = """
        {
            "numberOfData": 1,
            "data": {
                "id": "video-id",
                "slug": "orginal-circuit-track-name",
                "videoName": "Trial Mountain GT3 Race",
                "videoUrl": "https://www.youtube.com/embed/EQPgWtBC8ec",
                "thumbnailUrl": "img/thumbnail.jpg",
                "durationInSeconds": 120,
                "videoType": "Race",
                "description": "Video description.",
                "author": "admin"
            }
        }
    """;

    public const string SampleOtherVideos = """
        {
            "numberOfData": 2,
            "data": [
                {
                    "id": "video-id-1",
                    "slug": "orginal-circuit-track-name",
                    "videoName": "Trial Mountain GT1 Race",
                    "videoUrl": "https://www.youtube.com/embed/EQPgWtBC8ec",
                    "thumbnailUrl": "img/thumbnail.jpg",
                    "durationInSeconds": 60,
                    "videoType": "Race",
                    "description": "Video description.",
                    "author": "admin"
                },
                {
                    "id": "video-id-2",
                    "slug": "orginal-circuit-track-name",
                    "videoName": "Porsche 911 Race",
                    "videoUrl": "https://www.youtube.com/embed/EQPgWtBC8ec",
                    "thumbnailUrl": "img/thumbnail.jpg",
                    "durationInSeconds": 30,
                    "videoType": "Race",
                    "description": "Video description.",
                    "author": "admin"
                }
            ]
        }
    """;
}