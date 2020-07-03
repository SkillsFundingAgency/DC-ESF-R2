﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.Service.Services
{
    public class ZipService : IZipService
    {
        private readonly IFileService _fileService;
        private readonly IZipArchiveService _zipArchiveService;
        private readonly ILogger _logger;

        public ZipService(IFileService fileService, IZipArchiveService zipArchiveService, ILogger logger)
        {
            _fileService = fileService;
            _zipArchiveService = zipArchiveService;
            _logger = logger;
        }

        public async Task CreateZipAsync(string zipFileName, IEnumerable<string> reportNames, string container, CancellationToken cancellationToken)
        {
            try
            {
                if (await _fileService.ExistsAsync(zipFileName, container, cancellationToken))
                {
                    using (var fileStream = await _fileService.OpenReadStreamAsync(zipFileName, container, cancellationToken))
                    {
                        using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Update, true))
                        {
                            await AddReportsToZip(archive, reportNames, container, cancellationToken);
                        }
                    }
                }
                else
                {
                    using (var fileStream = await _fileService.OpenWriteStreamAsync(zipFileName, container, cancellationToken))
                    {
                        using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Update, true))
                        {
                            await AddReportsToZip(archive, reportNames, container, cancellationToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private async Task AddReportsToZip(ZipArchive zipArchive, IEnumerable<string> fileNames, string container, CancellationToken cancellationToken)
        {
            foreach (var fileName in fileNames.Where(f => !string.IsNullOrWhiteSpace(f)))
            {
                using (var fileStream = await _fileService.OpenWriteStreamAsync(fileName, container, cancellationToken))
                {
                    await _zipArchiveService.AddEntryToZip(zipArchive, fileStream, FormatFileName(fileName), cancellationToken);
                }
            }
        }

        private string FormatFileName(string fileName)
        {
            return fileName.Split('/').Last();
        }
    }
}