namespace aws_s3_url.Services
{
    using Amazon.S3.Util;

    public interface IS3UriHelper
    {
        AmazonS3Uri CreateTempUri(string relativeS3Key);
        AmazonS3Uri CreateUri(string relativeS3Key);
        AmazonS3Uri ParseS3Uri(string s3Key);
    }
}