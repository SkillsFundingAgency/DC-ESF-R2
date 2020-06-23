using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Service.Services;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Logging.Interfaces;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.Service.Tests
{
    public class ZipServiceTests
    {
        [Fact]
        public async Task CreateZipAsync_NewZip()
        {
            var cancellationToken = CancellationToken.None;
            var outputFileName = "FileName";
            var container = "Container";

            var fileNames = new List<string>
            {
                "Container/File1",
                "Container/File2",
                "Container/File3"
            };

            Stream writeStreamZip = new MemoryStream();
            Stream writeStreamFile1 = new MemoryStream();
            Stream writeStreamFile2 = new MemoryStream();
            Stream writeStreamFile3 = new MemoryStream();
            var readStreamMock = new Mock<MemoryStream>();
            Stream readStream = readStreamMock.Object;

            var fileServiceMock = new Mock<IFileService>();
            var zipArchiveServiceMock = new Mock<IZipArchiveService>();

            fileServiceMock.Setup(sm => sm.ExistsAsync(outputFileName, container, cancellationToken)).ReturnsAsync(false).Verifiable();
            fileServiceMock.Setup(sm => sm.OpenWriteStreamAsync(outputFileName, container, cancellationToken)).Returns(Task.FromResult(writeStreamZip)).Verifiable();
            fileServiceMock.Setup(sm => sm.OpenWriteStreamAsync("Container/File1", container, cancellationToken)).Returns(Task.FromResult(writeStreamFile1)).Verifiable();
            fileServiceMock.Setup(sm => sm.OpenWriteStreamAsync("Container/File2", container, cancellationToken)).Returns(Task.FromResult(writeStreamFile2)).Verifiable();
            fileServiceMock.Setup(sm => sm.OpenWriteStreamAsync("Container/File3", container, cancellationToken)).Returns(Task.FromResult(writeStreamFile3)).Verifiable();

            zipArchiveServiceMock.Setup(x => x.AddEntryToZip(It.IsAny<ZipArchive>(), It.IsAny<Stream>(), It.IsAny<string>(), cancellationToken)).Returns(Task.CompletedTask);

            await NewService(fileServiceMock.Object, zipArchiveServiceMock.Object).CreateZipAsync(outputFileName, fileNames, container, cancellationToken);

            fileServiceMock.VerifyAll();
            zipArchiveServiceMock.VerifyAll();
        }

        [Fact]
        public async Task CreateZipAsync_ExistingZip()
        {
            var cancellationToken = CancellationToken.None;
            var outputFileName = "FileName";
            var container = "Container";

            var fileNames = new List<string>
            {
                "Container/File1",
                "Container/File2",
                "Container/File3"
            };

            Stream readStreamZip = new MemoryStream();
            Stream writeStreamZip = new MemoryStream();
            Stream writeStreamFile1 = new MemoryStream();
            Stream writeStreamFile2 = new MemoryStream();
            Stream writeStreamFile3 = new MemoryStream();
            var readStreamMock = new Mock<MemoryStream>();
            Stream readStream = readStreamMock.Object;

            var fileServiceMock = new Mock<IFileService>();
            var zipArchiveServiceMock = new Mock<IZipArchiveService>();

            fileServiceMock.Setup(sm => sm.ExistsAsync(outputFileName, container, cancellationToken)).ReturnsAsync(true).Verifiable();
            fileServiceMock.Setup(sm => sm.OpenReadStreamAsync(outputFileName, container, cancellationToken)).Returns(Task.FromResult(readStreamZip)).Verifiable();
            fileServiceMock.Setup(sm => sm.OpenWriteStreamAsync("Container/File1", container, cancellationToken)).Returns(Task.FromResult(writeStreamFile1)).Verifiable();
            fileServiceMock.Setup(sm => sm.OpenWriteStreamAsync("Container/File2", container, cancellationToken)).Returns(Task.FromResult(writeStreamFile2)).Verifiable();
            fileServiceMock.Setup(sm => sm.OpenWriteStreamAsync("Container/File3", container, cancellationToken)).Returns(Task.FromResult(writeStreamFile3)).Verifiable();

            zipArchiveServiceMock.Setup(x => x.AddEntryToZip(It.IsAny<ZipArchive>(), It.IsAny<Stream>(), It.IsAny<string>(), cancellationToken)).Returns(Task.CompletedTask);

            await NewService(fileServiceMock.Object, zipArchiveServiceMock.Object).CreateZipAsync(outputFileName, fileNames, container, cancellationToken);

            fileServiceMock.VerifyAll();
            zipArchiveServiceMock.VerifyAll();
        }

        private ZipService NewService(IFileService fileService = null, IZipArchiveService zipArchiveService = null)
        {
            return new ZipService(fileService, zipArchiveService, Mock.Of<ILogger>());
        }
    }
}
