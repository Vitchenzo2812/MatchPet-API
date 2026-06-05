namespace MatchPet.Infrastructure.Services.Contracts;

public interface IFileManagerService
{
  Task<string> SaveFile(string base64, string fileKey);
  Task RemoveFile(string fileKey);
}