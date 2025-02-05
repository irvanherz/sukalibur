using AutoMapper;
using BunnyCDN.Net.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Sukalibur.Shared.Options;
using System.Text.Json;

namespace Sukalibur.Graph.Medias
{
    public class MediaService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;
        private readonly BunnyStorageOptions _bunnyStorageOpts;
        private readonly BunnyCDNStorage _bunnyStorage;

        public MediaService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper, IOptions<BunnyStorageOptions> bunnyStorageOpts)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _bunnyStorageOpts = bunnyStorageOpts.Value;
            _bunnyStorage = new BunnyCDNStorage(_bunnyStorageOpts.Zone, _bunnyStorageOpts.AccessKey, _bunnyStorageOpts.MainRegion);
        }

        public async Task<Media> CreateMediaAsync(CreateMediaInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var media = _mapper.Map<Media>(input);
            await context.Medias.AddAsync(media);
            await context.SaveChangesAsync();
            return media;
        }

        public async Task<Media> CreateOrganizerTripImageMediaAsync(UploadOrganizerTripImageMediaInput input)
        {
            var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp", "image/bmp" };
            var sizes = new[] { (Id: "sm", Width: 300, Height: 300), (Id: "md", Width: 600, Height: 600), (Id: "lg", Width: 1200, Height: 1200) };

            try
            {
                using var context = _contextFactory.CreateDbContext();
                if (input.File == null || input.File.Length == 0)
                    throw new Exception("No file uploaded.");
                if (!validImageTypes.Contains(input.File.ContentType))
                    throw new Exception("Invalid file type.");

                using var originalImage = await Image.LoadAsync(input.File.OpenReadStream());
                var originalName = input.File.Name;
                var originalMime = input.File.ContentType;
                var uploadId = Guid.NewGuid().ToString();
                var uploadZone = _bunnyStorageOpts.Zone;
                var uploadBaseUrl = _bunnyStorageOpts.BaseUrl;

                var resizeTasks = sizes.Select(async size =>
                {
                    var resizedImageFileName = $"{size.Width}x{size.Height}.jpg";
                    var resizedImageFilePath = $"{uploadZone}/{uploadId}/{resizedImageFileName}";
                    var resizedImageUrl = $"{uploadBaseUrl}/{uploadId}/{resizedImageFileName}";

                    using var resizedImage = originalImage.Clone(ctx =>
                        ctx.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Crop, // Object-cover equivalent
                            Size = new Size(size.Width, size.Height)
                        }));

                    // Save the resized image as JPEG with 80% quality
                    using var resizedImageStream = new MemoryStream();
                    await resizedImage.SaveAsJpegAsync(resizedImageStream, new JpegEncoder { Quality = 80 });
                    resizedImageStream.Position = 0;
                    var resizedImageFileSize = resizedImageStream.Length;
                    await _bunnyStorage.UploadAsync(resizedImageStream!, resizedImageFilePath);

                    return new ImageMediaData.Item
                    {
                        Id = size.Id,
                        FileName = resizedImageFileName,
                        FileSize = resizedImageFileSize,
                        Width = size.Width,
                        Height = size.Height,
                        Url = resizedImageUrl
                    };
                });

                // Wait for all tasks to complete
                var resizedImages = await Task.WhenAll(resizeTasks);
                var media = new Media
                {
                    Name = uploadId,
                    Type = MediaType.Image,
                    Subtype = "photo",
                    Data = new ImageMediaData
                    {
                        OriginalName = originalName,
                        OriginalMime = originalMime,
                        Sizes = resizedImages!.ToList()
                    }
                };
                await context.Medias.AddAsync(media);
                await context.SaveChangesAsync();
                return media;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public async Task<Media> CreateOrganizerPhotoMediaAsync(UploadOrganizerPhotoMediaInput input)
        {
            var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp", "image/bmp" };
            var sizes = new[] { (Id: "sm", Width: 150, Height: 150), (Id: "md", Width: 300, Height: 300), (Id: "lg", Width: 400, Height: 400) };

            try
            {
                using var context = _contextFactory.CreateDbContext();
                if (input.File == null || input.File.Length == 0)
                    throw new Exception("No file uploaded.");
                if (!validImageTypes.Contains(input.File.ContentType))
                    throw new Exception("Invalid file type.");

                using var originalImage = await Image.LoadAsync(input.File.OpenReadStream());
                var originalName = input.File.Name;
                var originalMime = input.File.ContentType;
                var uploadId = Guid.NewGuid().ToString();
                var uploadZone = _bunnyStorageOpts.Zone;
                var uploadBaseUrl = _bunnyStorageOpts.BaseUrl;

                var resizeTasks = sizes.Select(async size =>
                {
                    var resizedImageFileName = $"{size.Width}x{size.Height}.jpg";
                    var resizedImageFilePath = $"{uploadZone}/{uploadId}/{resizedImageFileName}";
                    var resizedImageUrl = $"{uploadBaseUrl}/{uploadId}/{resizedImageFileName}";

                    using var resizedImage = originalImage.Clone(ctx =>
                        ctx.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Crop, // Object-cover equivalent
                            Size = new Size(size.Width, size.Height)
                        }));

                    // Save the resized image as JPEG with 80% quality
                    using var resizedImageStream = new MemoryStream();
                    await resizedImage.SaveAsJpegAsync(resizedImageStream, new JpegEncoder { Quality = 80 });
                    resizedImageStream.Position = 0;
                    var resizedImageFileSize = resizedImageStream.Length;
                    await _bunnyStorage.UploadAsync(resizedImageStream!, resizedImageFilePath);

                    return new ImageMediaData.Item
                    {
                        Id = size.Id,
                        FileName = resizedImageFileName,
                        FileSize = resizedImageFileSize,
                        Width = size.Width,
                        Height = size.Height,
                        Url = resizedImageUrl
                    };
                });

                // Wait for all tasks to complete
                var resizedImages = await Task.WhenAll(resizeTasks);
                var media = new Media
                {
                    Name = uploadId,
                    Type = MediaType.Image,
                    Subtype = "photo",
                    Data = new ImageMediaData
                    {
                        OriginalName = originalName,
                        OriginalMime = originalMime,
                        Sizes = resizedImages!.ToList()
                    }
                };
                await context.Medias.AddAsync(media);
                await context.SaveChangesAsync();
                return media;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        public async Task<Media> CreateUserPhotoMediaAsync(UploadUserPhotoMediaInput input)
        {
            var validImageTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp", "image/bmp" };
            var sizes = new[] { (Id: "sm", Width: 150, Height: 150), (Id: "md", Width: 300, Height: 300), (Id: "lg", Width: 400, Height: 400) };

            try
            {
                using var context = _contextFactory.CreateDbContext();
                if (input.File == null || input.File.Length == 0)
                    throw new Exception("No file uploaded.");
                if (!validImageTypes.Contains(input.File.ContentType))
                    throw new Exception("Invalid file type.");

                using var originalImage = await Image.LoadAsync(input.File.OpenReadStream());
                var originalName = input.File.Name;
                var originalMime = input.File.ContentType;
                var uploadId = Guid.NewGuid().ToString();
                var uploadZone = _bunnyStorageOpts.Zone;
                var uploadBaseUrl = _bunnyStorageOpts.BaseUrl;

                var resizeTasks = sizes.Select(async size =>
                {
                    var resizedImageFileName = $"{size.Width}x{size.Height}.jpg";
                    var resizedImageFilePath = $"{uploadZone}/{uploadId}/{resizedImageFileName}";
                    var resizedImageUrl = $"{uploadBaseUrl}/{uploadId}/{resizedImageFileName}";

                    using var resizedImage = originalImage.Clone(ctx =>
                        ctx.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Crop, // Object-cover equivalent
                            Size = new Size(size.Width, size.Height)
                        }));

                    // Save the resized image as JPEG with 80% quality
                    using var resizedImageStream = new MemoryStream();
                    await resizedImage.SaveAsJpegAsync(resizedImageStream, new JpegEncoder { Quality = 80 });
                    resizedImageStream.Position = 0;
                    var resizedImageFileSize = resizedImageStream.Length;
                    await _bunnyStorage.UploadAsync(resizedImageStream!, resizedImageFilePath);

                    return new ImageMediaData.Item
                    {
                        Id = size.Id,
                        FileName = resizedImageFileName,
                        FileSize = resizedImageFileSize,
                        Width = size.Width,
                        Height = size.Height,
                        Url = resizedImageUrl
                    };
                });

                // Wait for all tasks to complete
                var resizedImages = await Task.WhenAll(resizeTasks);
                var media = new Media
                {
                    Name = uploadId,
                    Type = MediaType.Image,
                    Subtype = "photo",
                    Data = new ImageMediaData
                    {
                        OriginalName = originalName,
                        OriginalMime = originalMime,
                        Sizes = resizedImages!.ToList()
                    }
                };
                await context.Medias.AddAsync(media);
                await context.SaveChangesAsync();
                return media;
            }
            catch (Exception ex)
            {
                return null!;
            }
        }

        //public async Task<Media> UpdateMediaAsync(UpdateMediaInput input)
        //{
        //    using var context = _contextFactory.CreateDbContext();
        //    var media = await context.Medias.FindAsync(input.Id);
        //    if (media == null)
        //    {
        //        throw new Exception("Media not found");
        //    }

        //    _mapper.Map(input, media);
        //    await context.SaveChangesAsync();
        //    return media;
        //}
    }
}
