namespace aws_s3_url
{
    using Amazon.S3;
    using Amazon.S3.Model;
    using Amazon.S3.Transfer;
    using Amazon.S3.Util;
    using Autofac;
    using aws_s3_url.Extensions;
    using aws_s3_url.Models;
    using aws_s3_url.Services;
    using Newtonsoft.Json;
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new();

            builder
                .RegisterInstance(
                    new AWSSettings
                    {
                        Bucket = "main-bucket",
                        TempBucket = "temp-bucket"
                    });

            builder
                .RegisterType<S3UriHelper>()
                .AsImplementedInterfaces();


            using IContainer container = builder.Build();

            IS3UriHelper s3UriHelper = container.Resolve<IS3UriHelper>();

            // Samples with various urls.
            TestS3Key(s3UriHelper, "classification_documents/18/338316/88f89992-4077-4111-bd8a-c8bf3eec90a3.pdf");
            TestS3Key(s3UriHelper, "s3://cybexys-temp-us-east-1/classification_documents/18/338316/88f89992-4077-4111-bd8a-c8bf3eec90a3.pdf");
            TestS3Key(s3UriHelper, "https://cybexys-temp-us-east-1.s3.amazonaws.com/classification_documents/18/338316/88f89992-4077-4111-bd8a-c8bf3eec90a3.pdf");

            // Creating AWS S3 Requests.
            var uri = s3UriHelper.ParseS3Uri(
                "s3://cybexys-temp-us-east-1/classification_documents/18/338316/88f89992-4077-4111-bd8a-c8bf3eec90a3.pdf");


            TransferUtilityUploadRequest transferUtilityUploadRequest = uri.CreateRequest(
                new TransferUtilityUploadRequest
                {
                    InputStream = null, // Insert real stream here.
                    ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
                });

            Console.WriteLine(
                JsonConvert.SerializeObject(
                    transferUtilityUploadRequest,
                    Formatting.Indented));

            Console.WriteLine();



            PutObjectRequest putObjectRequest = uri.CreateRequest(
                new PutObjectRequest
                {
                    ContentBody = "Yep! I'm a content body!",
                    ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
                });

            Console.WriteLine(
                JsonConvert.SerializeObject(
                    putObjectRequest,
                    Formatting.Indented));

            Console.WriteLine();
        }

        private static void TestS3Key(
            IS3UriHelper s3UriHelper, 
            string s3Key)
        {
            AmazonS3Uri uri = s3UriHelper.ParseS3Uri(
                s3Key);

            Console.WriteLine(
                $"Case: {s3Key}.");

            Console.WriteLine(
                $"S3Url: {uri.S3Url()}.");

            Console.WriteLine(
                "Uri:");

            Console.WriteLine(
                JsonConvert.SerializeObject(
                    uri, 
                    Formatting.Indented));

            Console.WriteLine();
        }
    }
}
