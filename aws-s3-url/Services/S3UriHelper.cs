namespace aws_s3_url.Services
{
    using Amazon.S3.Util;
    using aws_s3_url.Models;
    using System;

    public class S3UriHelper : IS3UriHelper
    {
        private readonly AWSSettings awsSettings;
        private readonly Uri bucketUri;
        private readonly Uri tempBucketUri;

        public S3UriHelper(
            AWSSettings awsSettings)
        {
            this.awsSettings = awsSettings;

            this.bucketUri = new Uri(
                $"s3://{this.awsSettings.Bucket}",
                UriKind.Absolute);

            this.tempBucketUri = new Uri(
                $"s3://{this.awsSettings.TempBucket}",
                UriKind.Absolute);
        }

        public AmazonS3Uri ParseS3Uri(string s3Key)
        {
            if (string.IsNullOrEmpty(s3Key))
            {
                throw new ArgumentException($"'{nameof(s3Key)}' cannot be null or empty.", nameof(s3Key));
            }

            if (AmazonS3Uri.TryParseAmazonS3Uri(s3Key, out AmazonS3Uri amazonS3Uri))
            {
                return amazonS3Uri;
            }

            return new AmazonS3Uri(
                new Uri(
                    this.bucketUri,
                    s3Key));
        }

        public AmazonS3Uri CreateUri(string relativeS3Key)
        {
            if (string.IsNullOrEmpty(relativeS3Key))
            {
                throw new ArgumentException($"'{nameof(relativeS3Key)}' cannot be null or empty.", nameof(relativeS3Key));
            }

            return new AmazonS3Uri(
                new Uri(
                    this.bucketUri,
                    relativeS3Key));
        }

        public AmazonS3Uri CreateTempUri(string relativeS3Key)
        {
            if (string.IsNullOrEmpty(relativeS3Key))
            {
                throw new ArgumentException($"'{nameof(relativeS3Key)}' cannot be null or empty.", nameof(relativeS3Key));
            }

            return new AmazonS3Uri(
                new Uri(
                    this.tempBucketUri,
                    relativeS3Key));
        }
    }
}