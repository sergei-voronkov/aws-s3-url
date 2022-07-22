namespace aws_s3_url.Extensions
{
    using Amazon.S3.Model;
    using Amazon.S3.Transfer;
    using Amazon.S3.Util;

    public static class AmazonS3UriExtensions
    {
        public static string S3Url(
            this AmazonS3Uri uri) =>
            $"s3://{uri.Bucket}/{uri.Key}";

        public static TransferUtilityUploadRequest CreateRequest(
            this AmazonS3Uri uri,
            TransferUtilityUploadRequest instance)
        {
            instance.BucketName = uri.Bucket;
            instance.Key = uri.Key;
            return instance;
        }

        public static PutObjectRequest CreateRequest(
            this AmazonS3Uri uri,
            PutObjectRequest instance)
        {
            instance.BucketName = uri.Bucket;
            instance.Key = uri.Key;
            return instance;
        }

    }
}