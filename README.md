# RateLimiter
Simple server-side API rate limiter implementation via ASP.NET Core.

## Requirements
- Accurately limit excessive requests
- Low latency
- Exception handling: shows clear exceptions to users when their requests are throttled

## Sample implementation
Sample URL redirects to my portfolio web-site. Allows maximum of 3 requests within 1 minute. If the rate is exceeded, returns HTTP status code 429 and informs the user in how many seconds to retry. The current version of the rate limiter is global (does not throttle based on IP, user ID, etc.).

Rate limiting is implemented using Token Bucket algorithm. A token bucket is a container with pre-defined capacity (configurable, `BucketSize`). Tokens are put in the bucket at preset rates periodically (configurable, `RefillRateInSeconds`). Once the bucket is full, no more tokens are added. Each request consumes one tocken. When a request arrives, we check if there are enough tokens in the bucket. If there are not enough tokens, the request is dropped. The bucket is global (shared by all requests).

## Deployment
The application is published in Azure as Azure App Service. 

The [Microsoft.Extensions.Logging.AzureAppServices](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging-providers#azure-app-service) provider package writes logs to text files in an Azure App Service app's file system and to blob storage in an Azure Storage account.

## Next steps
Implements throttling based on IP and/or User ID.
