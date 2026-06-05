using MatchPet.Infrastructure.Services.Contracts;
using System.Text.RegularExpressions;
using MatchPet.Shared.Extensions;
using MatchPet.Shared.Errors;
using Amazon.S3.Model;
using System.Net;
using Amazon.S3;
using Amazon;

namespace MatchPet.Infrastructure.Services;

public class S3ManagerService : IFileManagerService
{
  public async Task<string> SaveFile(string base64, string fileKey)
  {
    if (!base64.IsBase64())
      return base64;

    string? accessKey = Environment.GetEnvironmentVariable("S3_ACCESS_KEY");
    string? secretKey = Environment.GetEnvironmentVariable("S3_SECRET_KEY");
    string? bucketName = Environment.GetEnvironmentVariable("S3_BUCKET_NAME");
    string? bucketRegion = Environment.GetEnvironmentVariable("S3_BUCKET_REGION");
    if (
      (string.IsNullOrEmpty(accessKey)) ||
      (string.IsNullOrEmpty(secretKey)) ||
      (string.IsNullOrEmpty(bucketName)) ||
      (string.IsNullOrEmpty(bucketRegion))
    )
    {
      throw new InternalServerError("Failed to connect to S3");
    }
    var config = new AmazonS3Config { RegionEndpoint = RegionEndpoint.GetBySystemName(bucketRegion) };
    var client = new AmazonS3Client(accessKey, secretKey, config);
    var contentType = GetContentTypeFromBase64(base64);
    var base64WithoutDataSpecification = Regex.Replace(base64, @"^[\w/\:.-]+;base64,", "");
    var bytes = Convert.FromBase64String(base64WithoutDataSpecification);
    var key = $"animals/{fileKey}";
    var response = await client.PutObjectAsync(
      new PutObjectRequest
      {
        BucketName = bucketName,
        Key = key,
        InputStream = new MemoryStream(bytes),
        ContentType = contentType
      }
    );

    if (response.HttpStatusCode != HttpStatusCode.OK)
      throw new InternalServerError("Failed to upload file");
    return $"https://s3.{bucketRegion}.amazonaws.com/{bucketName}/{key}";
  }
  public async Task RemoveFile(string fileKey)
  {
    string? accessKey = Environment.GetEnvironmentVariable("S3_ACCESS_KEY");
    string? secretKey = Environment.GetEnvironmentVariable("S3_SECRET_KEY");
    string? bucketName = Environment.GetEnvironmentVariable("S3_BUCKET_NAME");
    string? bucketRegion = Environment.GetEnvironmentVariable("S3_BUCKET_REGION");
    if (
      (string.IsNullOrEmpty(accessKey)) ||
      (string.IsNullOrEmpty(secretKey)) ||
      (string.IsNullOrEmpty(bucketName)) ||
      (string.IsNullOrEmpty(bucketRegion))
    )
    {
      throw new InternalServerError("Failed to connect to S3");
    }

    var config = new AmazonS3Config { RegionEndpoint = RegionEndpoint.GetBySystemName(bucketRegion) };
    var client = new AmazonS3Client(accessKey, secretKey, config);

    var key = $"animals/{fileKey}";
    var response = await client.DeleteObjectAsync(
      new DeleteObjectRequest
      {
        BucketName = bucketName,
        Key = key
      }
    );

    if (response.HttpStatusCode != HttpStatusCode.OK)
      throw new InternalServerError("Failed to remove file");
  }

  private string GetContentTypeFromBase64(string base64)
  {
    if (string.IsNullOrEmpty(base64))
      return string.Empty;
    var match = Regex.Match(base64, @"^data:([a-zA-Z0-9]+\/[a-zA-Z0-9-.+]+)(?:;\w+)*;base64,");

    return match.Success ? match.Groups[1].Value : string.Empty;
  }
}